using BaseClassEnitiy.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWorkflow.Scaffolding
{
    //ต้องเพิ่ม record ขึ้นเรื่อยๆด้วย
    public class TFlowRecord<TUser,TFlow>:Entity<long>,IWFActiveFlag
    {
        public string FormId { get; set; }

        [ForeignKey("FormId")]
        public TFlow Flow { get; set; }

        public long User { get; set; }

        [ForeignKey("UserId")]
        public TUser ActionByUser { get; set; }

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