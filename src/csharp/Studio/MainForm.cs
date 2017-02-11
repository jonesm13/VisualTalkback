namespace Studio
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public partial class MainForm : Form, ITalkbackService
    {
        public MainForm()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        private static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
                talkbackText.Font = fontDialog1.Font;
        }

        private void InvokeImpl(Action method)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker) delegate
                {
                    method();
                    FlashWindow(Handle, false);
                });
            }
            else
            {
                method();
                FlashWindow(Handle, false);
            }
        }

        #region ITalkbackService members

        public void SetText(string text)
        {
            InvokeImpl(() => SetTextImpl(text));
        }

        private void SetTextImpl(string text)
        {
            if ((text.IndexOf("\n", StringComparison.InvariantCulture) != -1) &
                (text.IndexOf("\r\n", StringComparison.InvariantCulture) == -1))
                text = text.Replace("\n", "\r\n");
            talkbackText.Text = text;
            talkbackText.SelectionLength = 0;
        }

        #endregion
    }
}