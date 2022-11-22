using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FilterRecords.Module.BusinessObjects {
    [DefaultClassOptions]
    public class DepartmentGoal : BaseObject {


        public string GoalName { get; set; }

        public virtual Department Department { get; set; }


    }
}
