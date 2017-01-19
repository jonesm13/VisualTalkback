namespace Producer
{
    using System.Windows.Forms;

    public abstract class Studio
    {
        public virtual void AddList(CheckedListBox list)
        {
            list.Items.Add(this);
        }

        public abstract void Send(string text);
    }
}