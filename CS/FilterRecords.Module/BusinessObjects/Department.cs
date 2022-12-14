using System;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System.Collections.ObjectModel;

namespace FilterRecords.Module.BusinessObjects {
    [DefaultClassOptions]
    public class Department: BaseObject {
        public virtual string DepartmentName { get; set; }
        public virtual IList<ApplicationUser> ApplicationUsers { get; set; } = new ObservableCollection<ApplicationUser>();
        public virtual IList<DepartmentGoal> Goals { get; set; } = new ObservableCollection<DepartmentGoal>();
    }
}
