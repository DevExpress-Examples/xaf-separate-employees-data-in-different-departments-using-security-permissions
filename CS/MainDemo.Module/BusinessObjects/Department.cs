using System;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

namespace MainDemo.Module.BusinessObjects {
    [DefaultClassOptions]
    [ImageName("BO_Department")]
    [DefaultProperty("Title")]
    public class Department : BaseObject {
        private string title;
        private string office;
        public Department(Session session)
            : base(session) {
        }
        public string Title {
            get {
                return title;
            }
            set {
                SetPropertyValue("Title", ref title, value);
            }
        }
        public string Office {
            get {
                return office;
            }
            set {
                SetPropertyValue("Office", ref office, value);
            }
        }
        [Association]
        public XPCollection<Employee> Employees {
            get {
                return GetCollection<Employee>("Employees");
            }
        }
    }
}
