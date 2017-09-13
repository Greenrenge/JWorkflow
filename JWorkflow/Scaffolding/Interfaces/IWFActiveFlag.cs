namespace JWorkflow.Scaffolding
{
    public interface IWFActiveFlag
    {
        bool IsActive { get; }
        void SetActiveFlagToHistory();
        void SetActive();
    }
}