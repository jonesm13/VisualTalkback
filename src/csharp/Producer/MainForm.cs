using System;
using System.Windows.Forms;

namespace Producer
{
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var studios = ConfigurationManager.AppSettings[
                    "producer.studios.addresses"]
                .Split(',')
                .Select(x =>
                {
                    var p = x.Split('|');
                    return new Studio(p[0], p[1]);
                });

            foreach (var s in studios)
            {
                var item = new ToolStripMenuItem(s.Name)
                {
                    CheckOnClick = true,
                    Tag = s
                };
                item.Click += Item_Click;
                this.studioMenu.DropDownItems.Add(item);
            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
            var selectedMenuItem = (ToolStripMenuItem)sender;

            selectedMenuItem.Checked = true;

            foreach (var ltoolStripMenuItem in (from object
                                                    item in selectedMenuItem.Owner.Items
                                                let ltoolStripMenuItem = item as ToolStripMenuItem
                                                where ltoolStripMenuItem != null
                                                where !item.Equals(selectedMenuItem)
                                                select ltoolStripMenuItem))
                (ltoolStripMenuItem).Checked = false;

            var studio = selectedMenuItem.Tag as Studio;
            studio.Display(this.textBox);
        }
    }

    public class Studio
    {
        public string Name { get; }

        private readonly string address;

        public Studio(string name, string address)
        {
            this.Name = name;
            this.address = address;
        }

        public void Display(TextBox textBox)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.ContentType] = "text/plain";
                using (var resp = webClient.OpenRead(address))
                {
                    var reader = new StreamReader(resp);
                    textBox.Text = reader.ReadToEnd();
                }
            }
        }
    }
}
