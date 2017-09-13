using BaseClassEnitiy.Models;

namespace JWorkflow.Scaffolding
{
    public interface IWFActiveFlag:IActiveFlagEntity
    {
        bool IsActive { get; }
        void SetActiveFlagToHistory();
        void SetActive();
    }
}