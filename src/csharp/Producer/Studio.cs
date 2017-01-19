namespace Producer
{
    using System.Windows.Forms;

    public abstract class Studio
    {
        public abstract void AddList(CheckedListBox list);

        public abstract void Send(string text);
    }
}