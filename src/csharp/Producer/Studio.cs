namespace Producer
{
    using System;
    using System.IO;
    using System.Net;
    using System.Windows.Forms;

    public class Studio
    {
        private readonly string name;
        private readonly string address;
        private readonly Action<string> setTextAction;

        public Studio(string name, string address, Action<string> setTextAction)
        {
            Guard.AgainstNullOrEmptyString(name, nameof(name));
            Guard.AgainstNullOrEmptyString(address, nameof(address));

            this.name = name;
            this.address = address;
            this.setTextAction = setTextAction;
        }

        public void AddMenuItem(ToolStripMenuItem parent)
        {
            parent.DropDownItems.Add(MakeMenuItem());
        }

        ToolStripMenuItem MakeMenuItem()
        {
            var result = new ToolStripMenuItem
            {
                CheckOnClick = true,
                Text = this.name,
                Tag = this
            };
            result.Click += MenuItem_Click;
            return result;
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem == null)
                return;

            try
            {
                using (var webClient = new WebClient())
                {
                    webClient.Headers[HttpRequestHeader.ContentType] = "text/plain";
                    using (var resp = webClient.OpenRead(this.address))
                    {
                        var reader = new StreamReader(resp);
                        this.setTextAction(reader.ReadToEnd());
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show($@"Cannot connect to {name}.");
            }
        }

        public void Send(string text)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.ContentType] = "text/plain";
                var resp = webClient.UploadString(this.address, text);
            }
        }
    }
}