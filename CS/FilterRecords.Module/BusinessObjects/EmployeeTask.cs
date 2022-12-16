using System;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;

namespace FilterRecords.Module.BusinessObjects {
    [DefaultClassOptions]
    public class EmployeeTask : BaseObject {
        public virtual string Subject { get; set; }
        public virtual bool IsSharedTask { get; set; }
        public virtual ApplicationUser AssignedTo { get; set; }
    }
}
