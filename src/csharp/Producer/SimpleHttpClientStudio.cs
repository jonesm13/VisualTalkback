namespace Producer
{
    using System;
    using System.Net;
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

        public override void Send(
            string text,
            Action onSuccess = null,
            Action onFailure = null)
        {
            Guard.AgainstNullOrEmptyString(text, nameof(text));

            using (var webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.ContentType] = "text/plain";

                try
                {
                    var resp = webClient.UploadString(this.address, text);

                    try
                    {
                        onSuccess?.Invoke();
                    }
                    catch (Exception)
                    {
                        // nop - eat any errors with the callback
                    }
                }
                catch (Exception ex)
                {
                    onFailure?.Invoke();
                }
            }
        }

        public override string ToString()
        {
            return name;
        }
    }
}