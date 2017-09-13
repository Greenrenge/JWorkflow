using System.ComponentModel.DataAnnotations.Schema;

namespace JWorkflow.Scaffolding
{
    public class JUserAction : WFUserAction<long,long>, IWFUserAction, IWFActiveFlag 
    {
        public JUserAction()
        {

        }
        //We could add more props here
        #region IJActiveFlag
        [NotMapped]
        bool IWFActiveFlag.IsActive
        {
            get
            {
                return Active == "A";
            }
        }

        void IWFActiveFlag.SetActive()
        {
            Active = "A";
        }

        void IWFActiveFlag.SetActiveFlagToHistory()
        {
            Active = "N";
        }
        #endregion IJActiveFlag
    }
}
