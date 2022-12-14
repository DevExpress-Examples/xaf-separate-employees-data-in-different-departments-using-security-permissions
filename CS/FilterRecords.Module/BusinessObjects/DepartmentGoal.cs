using System;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;

namespace FilterRecords.Module.BusinessObjects {
    [DefaultClassOptions]
    public class DepartmentGoal : BaseObject {
        public virtual string GoalName { get; set; }
        public virtual Department Department { get; set; }
    }
}
