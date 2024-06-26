﻿using NetEti.ApplicationEnvironment;

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
            this.listBox1.Items.Add(String.Format("{0}: {1}", "Description", this._SettingsAccess.Description));
            this.listBox1.Items.Add(String.Format("{0}: {1}", "Harry", this._SettingsAccess.GetStringValue("Harry", "---")));
            this.listBox1.Items.Add(String.Format("{0}: {1}", "Peter", this._SettingsAccess.GetStringValue("Peter", "---")));
            this.listBox1.Items.Add(String.Format("{0}: {1}", "Claire", this._SettingsAccess.GetStringValue("Claire", "---")));
            this.listBox1.Items.Add(String.Format("{0}: {1}", "Eberhard", this._SettingsAccess.GetStringValue("Eberhard", "---")));
            this.listBox1.Items.Add(String.Format("{0}: {1}", "Butzemann", this._SettingsAccess.GetStringValue("Butzemann", "---")));
            this.listBox1.Items.Add(String.Format("{0}: {1}", "Pierre", this._SettingsAccess.GetStringValue("Pierre", "---")));
            this.listBox1.Items.Add(String.Format("{0}: {1}", "Noppes", this._SettingsAccess.GetStringValue("Noppes", "???")));
        }
    }
}
