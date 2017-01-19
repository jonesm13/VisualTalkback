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
        private readonly Action<string> setTextAction;

        public SimpleHttpClientStudio(string name, string address, Action<string> setTextAction)
        {
            Guard.AgainstNullOrEmptyString(name, nameof(name));
            Guard.AgainstNullOrEmptyString(address, nameof(address));

            this.name = name;
            this.address = address;
            this.setTextAction = setTextAction;
        }

        public override void Send(string text)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.ContentType] = "text/plain";
                var resp = webClient.UploadString(this.address, text);
            }
        }

        public override string ToString()
        {
            return name;
        }
    }
}