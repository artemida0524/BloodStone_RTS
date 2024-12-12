namespace Unit
{
    public interface ISelectable
    {
        bool IsSelection { get; }
        bool CanSelected { get; }

        bool Select();
        void Unselect();
    }
}