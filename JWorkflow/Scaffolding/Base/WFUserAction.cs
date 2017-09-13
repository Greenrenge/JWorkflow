using BaseClassEnitiy.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWorkflow.Scaffolding
{
    public class WFUserAction<TUserKey,TGroupKey>:Entity<long>,IWFActiveFlag
    {
        public virtual string RoleName { get; set; }

        [Required]
        public virtual short RoleLevel { get; set; }

        public virtual TUserKey UserId { get; set; }
        public virtual TGroupKey GroupId { get; set; }
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