/* 
       ^ Author    : Cisamu
       ^ Name      : CyberEye-RAT
       ^ Github    : https://github.com/cisamu123
       > This program is distributed for educational purposes only.
*/
using System;
using System.Windows.Forms;

namespace CyberEye.Client.Builder
{
    public partial class FrmAssemblyChanger : Form
    {
        public FrmAssemblyChanger()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmBuild.AssemblyTitle = textBox1.Text;
            frmBuild.AssemblyDescription = textBox2.Text;
            frmBuild.AssemblyConfiguration = textBox3.Text;
            frmBuild.AssemblyCompany = textBox4.Text;
            frmBuild.AssemblyProduct = textBox5.Text;
            frmBuild.AssemblyCopyright = textBox6.Text;
            frmBuild.AssemblyTrademark = textBox7.Text;
            frmBuild.AssemblyCulture = textBox8.Text;
            frmBuild.AssemblyVersion = textBox9.Text;
            frmBuild.AssemblyFileVersion = textBox10.Text;
            this.Close();
        }

        private void FrmAssemblyChanger_Load(object sender, EventArgs e)
        {
            textBox1.Text = frmBuild.AssemblyTitle;
            textBox2.Text = frmBuild.AssemblyDescription;
            textBox3.Text = frmBuild.AssemblyConfiguration;
            textBox4.Text = frmBuild.AssemblyCompany;
            textBox5.Text = frmBuild.AssemblyProduct;
            textBox6.Text = frmBuild.AssemblyCopyright;
            textBox7.Text = frmBuild.AssemblyTrademark;
            textBox8.Text = frmBuild.AssemblyCulture;
            textBox9.Text = frmBuild.AssemblyVersion;
            textBox10.Text = frmBuild.AssemblyFileVersion;
        }
    }
}
