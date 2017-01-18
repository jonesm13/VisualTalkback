namespace Producer
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Windows.Forms;

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
            var selectedMenuItem = (ToolStripMenuItem) sender;

            selectedMenuItem.Checked = true;

            foreach (var ltoolStripMenuItem in from object
                item in selectedMenuItem.Owner.Items
                let ltoolStripMenuItem = item as ToolStripMenuItem
                where ltoolStripMenuItem != null
                where !item.Equals(selectedMenuItem)
                select ltoolStripMenuItem)
            {
                ltoolStripMenuItem.Checked = false;
            }

            var studio = selectedMenuItem.Tag as Studio;
            studio.Display(this.textBox);
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
            var checkedStudio = this.studioMenu.DropDownItems
                .OfType<ToolStripMenuItem>()
                .SingleOrDefault(x=>x.Checked);

            if (checkedStudio == null)
                return;

            var studio = checkedStudio.Tag as Studio;
            studio.Send(this.textBox.Text);
        }
    }
}