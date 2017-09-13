using BaseClassEnitiy.Models;
using System.ComponentModel.DataAnnotations;

namespace JWorkflow.Scaffolding
{
    public class WFUserAction<TUserKey,TGroupKey>:Entity<long>
    {
        public virtual string RoleName { get; set; }

        [Required]
        public virtual short RoleLevel { get; set; }

        public virtual TUserKey UserId { get; set; }
        public virtual TGroupKey GroupId { get; set; }
    }
}