namespace Producer
{
    using System;
    using System.Windows.Forms;

    public sealed class LoopbackStudio : Studio
    {
        public override void Send(
            string text, 
            Action onSuccess = null, 
            Action onFail = null)
        {
            MessageBox.Show($"Text set to '{text}'.");
            onSuccess?.Invoke();
        }

        public override string ToString()
        {
            return "Loopback";
        }
    }
}