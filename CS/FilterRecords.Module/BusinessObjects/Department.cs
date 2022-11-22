using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterRecords.Module.BusinessObjects {
    [DefaultClassOptions]
    public class Department: BaseObject {

        public Department() {
            _applicationUsers = new ObservableCollection<ApplicationUser>();
            _goals = new ObservableCollection<DepartmentGoal>();
        }

   
        public string DepartmentName { get; set; }
        ObservableCollection<ApplicationUser> _applicationUsers;
        public virtual IList<ApplicationUser> ApplicationUsers {
            get => _applicationUsers;
        }

        ObservableCollection<DepartmentGoal> _goals;
        public virtual IList<DepartmentGoal> Goals {
            get => _goals;
        }

    
    }
}
