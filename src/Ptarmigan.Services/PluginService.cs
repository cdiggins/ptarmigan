using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ptarmigan.Utils;
using Ptarmigan.Utils.Roslyn;
using Assembly = System.Reflection.Assembly;
using Domo;

namespace Ptarmigan.Services
{
    public class PluginService : BaseService
    {
        public class Options
        {
            public string[] PluginDirectories { get; set; }
            public string[] PluginFiles { get; set; }
            public string PluginPattern { get; set; }
            public string ScriptsDirectory { get; set; }
            public string[] InputDlls { get; set; }
        }

        public IApi Api { get; }
        public List<IPlugin> LoadedPlugins { get; set; } = new List<IPlugin>();
        public List<IPlugin> ScriptedPlugins { get; set; } = new List<IPlugin>();
        public CompilerService CompilerService { get; }
        public CompilerRepo CompilerRepo { get; }
        public ILogger Logger { get; }
        public string Name => nameof(PluginService);

        public PluginService(IApi api, Options options, CompilerRepo repo, ILogger logger)
            : base(api)
        {
            Api = api;
            CompilerRepo = repo;
            Logger = logger;

            if (!string.IsNullOrWhiteSpace(options.ScriptsDirectory))
            {
                CompilerService = new CompilerService(null, options.ScriptsDirectory, options.InputDlls);
                CompilerService.RecompileEvent += Controller_RecompileEvent;
                CompilerService.ViewModel.AutoCompile = true;
                CompilerService.Recompile();
            }

            foreach (var dir in options.PluginDirectories ?? Array.Empty<string>())
            {
                LoadedPlugins.AddRange(LoadPluginsFromDirectory(dir, options.PluginPattern ?? "*.dll", Logger));
            }

            foreach (var file in options.PluginFiles ?? Array.Empty<string>())
            {
                LoadedPlugins.AddRange(LoadPluginsFromFile(file, Logger));
            }

            foreach (var plugin in LoadedPlugins)
            {
                plugin.Initialize(Api);
            }
        }

        public static void InitializePlugin(IApi api, IPlugin plugin)
        {
            plugin.Initialize(api);
            api.EventBus.AddSubscriberUsingReflection(plugin);
        }

        private void Controller_RecompileEvent(object sender, EventArgs e)
        {
            foreach (var p in ScriptedPlugins)
            {
                p.Dispose();
            }

            ScriptedPlugins.Clear();

            CompilerRepo.Model.Update(x =>
                new CompilerState()
                {
                    Diagnostics = CompilerService.ViewModel.Diagnostics.ToArray(),
                    InputDirectory = CompilerService.ScriptDirectory,
                    InputFiles = CompilerService.ViewModel.InputFiles.ToArray(),
                    OutputDll = CompilerService.ViewModel.OutputDll,
                    Success = CompilerService.ViewModel.Compiled
                });

            var model = CompilerService.Model;
            if (model.Compiled)
            {
                var plugins = LoadPluginsFromFile(model.OutputDll, Logger);
                if (plugins.Count > 0)
                {
                    foreach (var p in plugins.ToArray())
                    {
                        try
                        {
                            InitializePlugin(Api, p);

                            // TODO: for each repository it watches, set it up as a published 
                        }
                        catch (Exception ex)
                        {
                            Logger.LogError($"Failed to initialize plugin {p.Name}", ex);
                            plugins.Remove(p);
                        }
                    }

                    ScriptedPlugins.AddRange(plugins);
                }
            }
        }

        public static bool IsPluginType(Type t)
            => t.ImplementsInterface<IPlugin>() && t.HasDefaultConstructor();

        public static List<IPlugin> LoadPluginsFromFile(string fileName, ILogger logger, bool useLoadFrom = true)
        {
            var r = new List<IPlugin>();
            Assembly asm;
            try
            {
                asm = Assembly.LoadFile(fileName);
                //asm = useLoadFrom ? Assembly.LoadFrom(fileName) : Assembly.LoadFile(fileName);
            }
            catch (Exception e)
            {
                logger.LogError(e);
                return r;
            }

            var pluginTypes = asm.GetExportedTypes().Where(IsPluginType);
            foreach (var t in pluginTypes)
            {
                try
                {
                    var plugin = (IPlugin)Activator.CreateInstance(t);
                    if (plugin != null) 
                    {
                        r.Add(plugin);
                    }
                }
                catch (Exception e)
                {
                    logger.LogError($"Error when creating {t.Name}", e);
                }
            }
            return r;
        }

        public static IPlugin[] LoadPluginsFromDirectory(string pluginDirectory, string filePattern, ILogger logger)
        {
            if (!Directory.Exists(pluginDirectory))
            {
                logger.LogWarning($"Could not find directory {pluginDirectory}");
                return Array.Empty<IPlugin>();
            }
            var dlls = Directory.GetFiles(pluginDirectory, filePattern);
            return dlls.SelectMany(f => LoadPluginsFromFile(f, logger)).ToArray();
        }
    }
}
