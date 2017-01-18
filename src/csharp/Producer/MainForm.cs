namespace Producer
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Windows.Forms;
    using Helpers;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ConfigurationManager
                .AppSettings["producer.studios.addresses"]
                .Split(',')
                .Select<string, Studio>(x =>
                {
                    if (Equals(x, "LOOPBACK"))
                    {
                        return new LoopbackStudio();
                    }

                    var p = x.Split('|');
                    return new SimpleHttpClientStudio(p[0], p[1], s =>
                    {
                        this.textBox.Text = s;
                    });
                })
                .Each(x => x.AddMenuItem(this.studioMenu));
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.fontDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox.Font = this.fontDialog1.Font;
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            this.studioMenu
                .DropDownItems
                .OfType<ToolStripMenuItem>()
                .Where(x => x.Checked)
                .Select(x => x.Tag)
                .OfType<Studio>()
                .Each(s => s.Send(this.textBox.Text));
        }
    }
}