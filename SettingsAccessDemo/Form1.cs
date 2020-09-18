using System;
using System.Windows.Forms;

using NetEti.ApplicationEnvironment;

namespace NetEti.DemoApplications
{
    /// <summary>
    /// Demo
    /// </summary>
    public partial class Form1 : Form
    {
        private SettingsAccess _SettingsAccess;

        public Form1()
        {
            InitializeComponent();
            this._SettingsAccess = new SettingsAccess();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.listBox1.Items.Add(String.Format("{0}: {1}", "Harry", this._SettingsAccess.GetStringValue("Harry", "---")));
            this.listBox1.Items.Add(String.Format("{0}: {1}", "Noppes", this._SettingsAccess.GetStringValue("Noppes", "???")));
        }
    }
}
