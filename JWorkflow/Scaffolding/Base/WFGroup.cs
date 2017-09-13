using BaseClassEnitiy.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWorkflow.Scaffolding
{
    public class WFGroup : Entity<long>
    {
        [Column(TypeName = "NVARCHAR")]
        [Required, Index(IsUnique = true)]
        public virtual string GroupName { get; set; }
    }
}