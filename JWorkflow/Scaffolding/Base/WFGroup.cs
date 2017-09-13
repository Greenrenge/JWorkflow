using BaseClassEnitiy.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWorkflow.Scaffolding
{
    public class WFGroup : Entity<long>,IWFActiveFlag
    {
        [Column(TypeName = "NVARCHAR")]
        [Required, Index(IsUnique = true)]
        public virtual string GroupName { get; set; }

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