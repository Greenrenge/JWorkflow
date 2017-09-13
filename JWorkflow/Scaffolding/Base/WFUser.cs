
using BaseClassEnitiy.Models;

namespace JWorkflow.Scaffolding
{
    public class WFUser : Entity<long>
    {
        public virtual string UserName { get; set; }
    }
}