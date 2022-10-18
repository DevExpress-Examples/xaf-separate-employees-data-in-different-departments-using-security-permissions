using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterRecords.Module.BusinessObjects {
    [DefaultClassOptions]
    public class Department: INotifyPropertyChanged {

        public Department() {
            _applicationUsers = new ObservableCollection<ApplicationUser>();
            _goals = new ObservableCollection<DepartmentGoal>();
        }

        int id;
        [Browsable(false)]
        public int ID {
            get => id; protected set {
                if(id == value) {
                    return;
                }

                id = value;
                OnPropertyChanged();
            }
        }
        string _departmentName;
        public string DepartmentName {
            get => _departmentName;
            set {
                if(_departmentName == value) {
                    return;
                }

                _departmentName = value;
                OnPropertyChanged();
            }
        }
        ObservableCollection<ApplicationUser> _applicationUsers;
        public virtual IList<ApplicationUser> ApplicationUsers {
            get => _applicationUsers;
        }

        ObservableCollection<DepartmentGoal> _goals;
        public virtual IList<DepartmentGoal> Goals {
            get => _goals;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
