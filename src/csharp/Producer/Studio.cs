namespace Producer
{
    using System.Windows.Forms;

    public abstract class Studio
    {
        public abstract void AddMenuItem(ToolStripMenuItem parent);

        public abstract void Send(string text);
    }
}