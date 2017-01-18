namespace Producer
{
    using System.IO;
    using System.Net;
    using System.Windows.Forms;

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
                using (var resp = webClient.OpenRead(this.address))
                {
                    var reader = new StreamReader(resp);
                    textBox.Text = reader.ReadToEnd();
                }
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