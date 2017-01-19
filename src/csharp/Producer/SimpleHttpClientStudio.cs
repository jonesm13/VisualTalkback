namespace Producer
{
    using System;
    using System.Net;
    using System.Windows.Forms;
    using Helpers;

    public class SimpleHttpClientStudio : Studio
    {
        private readonly string name;
        private readonly string address;

        public SimpleHttpClientStudio(string name, string address)
        {
            Guard.AgainstNullOrEmptyString(name, nameof(name));
            Guard.AgainstNullOrEmptyString(address, nameof(address));

            this.name = name;
            this.address = address;
        }

        public override void Send(string text)
        {
            Guard.AgainstNullOrEmptyString(text, nameof(text));

            using (var webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.ContentType] = "text/plain";

                try
                {
                    var resp = webClient.UploadString(this.address, text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Could not send to {name}, error={ex.Message}");
                }
            }
        }

        public override string ToString()
        {
            return name;
        }
    }
}