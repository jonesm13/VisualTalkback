namespace Studio
{
    using System;
    using System.Windows.Forms;

    public partial class MainForm : Form, ITalkbackService
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region ITalkbackService members

        public void SetText(string text)
        {
            if (InvokeRequired)
            {
                Invoke((Action) delegate { SetTextImpl(text); });
            }
            else
            {
                SetTextImpl(text);
            }
        }

        void SetTextImpl(string text)
        {
            if (text.IndexOf("\n", StringComparison.InvariantCulture) != -1 &
                text.IndexOf("\r\n", StringComparison.InvariantCulture) == -1)
            {
                text = text.Replace("\n", "\r\n");
                
            }
            this.talkbackText.Text = text;
            this.talkbackText.SelectionLength = 0;
        }

        public string GetText()
        {
            return this.talkbackText.Text;
        }

        #endregion

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.fontDialog1.ShowDialog() == DialogResult.OK)
            {
                this.talkbackText.Font = this.fontDialog1.Font;
            }
        }
    }
}