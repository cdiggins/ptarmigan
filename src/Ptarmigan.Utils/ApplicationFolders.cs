using System.IO;

namespace Ptarmigan.Utils
{
    public class ApplicationFolders
    {
        public string CompanyName;
        public string AppName;
        public string AppVersion;

        public string AppFolder
            => AppVersion != null
                ? Path.Combine(CompanyName, AppName, AppVersion)
                : Path.Combine(CompanyName, AppName);

        public ApplicationFolders(string companyName, string appName, string appVersion = null)
            => (CompanyName, AppName, AppVersion) = (companyName, appName, appVersion);

        public DirectoryPath ApplicationData => SpecialFolders.ApplicationData.RelativeFolder(AppFolder);
        public DirectoryPath CommonApplicationData => SpecialFolders.CommonApplicationData.RelativeFolder(AppFolder);
        public DirectoryPath ProgramFiles => SpecialFolders.ProgramFiles.RelativeFolder(AppFolder);
        public DirectoryPath Documents => SpecialFolders.ProgramFiles.RelativeFolder(AppFolder);
        public DirectoryPath TempFolder => SpecialFolders.Temp.RelativeFolder(AppFolder);
    }
}