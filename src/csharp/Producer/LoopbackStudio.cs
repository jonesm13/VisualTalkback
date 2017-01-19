namespace Producer
{
    using System.Windows.Forms;

    public sealed class LoopbackStudio : Studio
    {
        public override void AddList(CheckedListBox list)
        {
            list.Items.Add(this);
        }

        public override void Send(string text)
        {
            MessageBox.Show($"Text set to '{text}'.");
        }

        public override string ToString()
        {
            return "Loopback";
        }
    }
}