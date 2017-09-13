using System.ComponentModel.DataAnnotations.Schema;

namespace JWorkflow.Scaffolding
{
    public class JUserAction : WFUserAction<long,long>, IWFUserAction, IWFActiveFlag 
    {
        public JUserAction()
        {

        }
        //We could add more props here
        
    }
}
