namespace Producer
{
    using System.Windows.Forms;

    public sealed class LoopbackStudio : Studio
    {
        public override void AddMenuItem(ToolStripMenuItem parent)
        {
            var menuItem = new ToolStripMenuItem
            {
                CheckOnClick = true,
                Text = "Loopback",
                Tag = this
            };
            parent.DropDownItems.Add(menuItem);
        }

        public override void Send(string text)
        {
            MessageBox.Show($"Text set to '{text}'.");
        }
    }
}