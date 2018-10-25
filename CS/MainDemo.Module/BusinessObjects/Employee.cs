using System;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Persistent.Validation;

namespace MainDemo.Module.BusinessObjects {
    [DefaultClassOptions]
    [DefaultProperty("FullName")]
    [ImageName("BO_User")]
    public class Employee : SecuritySystemUser {
        private string _LastName;
        private string _FirstName;
        private Department department;
        public Employee(Session session) :
            base(session) {
        }
        public string FirstName {
            get {
                return _FirstName;
            }
            set {
                SetPropertyValue("FirstName", ref _FirstName, value);
            }
        }
        public string LastName {
            get {
                return _LastName;
            }
            set {
                SetPropertyValue("LastName", ref _LastName, value);
            }
        }
        [PersistentAlias("concat(FirstName, ' ', LastName)")]
        public string FullName {
            get {
                return Convert.ToString(EvaluateAlias("FullName"));
            }
        }
        [Association]
		[RuleRequiredField]
        public Department Department {
            get {
                return department;
            }
            set {
                SetPropertyValue("Department", ref department, value);
            }
        }
        [Association]
        public XPCollection<EmployeeTask> Tasks {
            get {
                return GetCollection<EmployeeTask>("Tasks");
            }
        }
    }
}
