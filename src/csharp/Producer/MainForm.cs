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
                    return new SimpleHttpClientStudio(p[0], p[1]);
                })
                .Each(x => x.AddList(this.destinationCheckBoxList));
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
            this.destinationCheckBoxList
                .CheckedItems
                .OfType<Studio>()
                .Each(s => s.Send(
                    this.textBox.Text, 
                    onSuccess: null, 
                    onFail: () =>
                    {
                        InvokeImpl(() =>
                        {
                            MessageBox.Show($"Could not send message to {s}.");
                        });      
                    }));
        }

        private void InvokeImpl(Action method)
        {
            if (method == null)
                return;

            if (this.InvokeRequired)
                this.Invoke(method);
            else
                method();
        }
    }
}