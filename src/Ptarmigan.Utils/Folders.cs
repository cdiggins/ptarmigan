    using System;

namespace Ptarmigan.Utils
{
    public static class Folders
    {
        public static string AdminTools => Environment.GetFolderPath(Environment.SpecialFolder.AdminTools);
        public static string ApplicationData => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string CDBurning => Environment.GetFolderPath(Environment.SpecialFolder.CDBurning);

        public static string Windows => Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        public static string UserProfile => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static string Templates => Environment.GetFolderPath(Environment.SpecialFolder.Templates);
        public static string SystemX86 => Environment.GetFolderPath(Environment.SpecialFolder.SystemX86);

        public static string System => Environment.GetFolderPath(Environment.SpecialFolder.System);
        public static string StartMenu => Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
        public static string SendTo => Environment.GetFolderPath(Environment.SpecialFolder.SendTo);
        public static string Resources => Environment.GetFolderPath(Environment.SpecialFolder.Resources);
        public static string Recent => Environment.GetFolderPath(Environment.SpecialFolder.Recent);
        public static string Programs => Environment.GetFolderPath(Environment.SpecialFolder.Programs);
        public static string ProgramFilesX86 => Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        public static string ProgramFiles => Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        public static string PrinterShortcuts => Environment.GetFolderPath(Environment.SpecialFolder.PrinterShortcuts);
        public static string Personal => Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public static string NetworkShortcuts => Environment.GetFolderPath(Environment.SpecialFolder.NetworkShortcuts);
        public static string MyVideos => Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);

        public static string MyPictures => Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        public static string MyDocuments => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        
        public static string MyComputer => "";

        public static string LocalizedResources =>
            Environment.GetFolderPath(Environment.SpecialFolder.LocalizedResources);

        public static string LocalApplicationData =>
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public static string InternetCache => Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
        public static string History => Environment.GetFolderPath(Environment.SpecialFolder.History);
        public static string Fonts => Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
        public static string Favorites => Environment.GetFolderPath(Environment.SpecialFolder.Favorites);
        public static string DesktopDirectory => Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        public static string Desktop => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static string Cookies => Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
        public static string CommonVideos => Environment.GetFolderPath(Environment.SpecialFolder.CommonVideos);
        public static string CommonTemplates => Environment.GetFolderPath(Environment.SpecialFolder.CommonTemplates);
        public static string CommonStartup => Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);
        public static string CommonStartMenu => Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
        public static string CommonPrograms => Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms);

        public static string CommonProgramFilesX86 =>
            Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86);

        public static string CommonProgramFiles =>
            Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles);

        public static string CommonPictures => Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);
        public static string CommonOemLinks => Environment.GetFolderPath(Environment.SpecialFolder.CommonOemLinks);
        public static string CommonMusic => Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic);
        public static string CommonDocuments => Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);

        public static string CommonDesktopDirectory =>
            Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);

        public static string CommonApplicationData =>
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

        public static string CommonAdminTools => Environment.GetFolderPath(Environment.SpecialFolder.CommonAdminTools);
    }
}