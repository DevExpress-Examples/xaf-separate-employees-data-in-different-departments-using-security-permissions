using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FilterRecords.Module.BusinessObjects {
    [DefaultClassOptions]
    public class DepartmentGoal: INotifyPropertyChanged {

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
        string _goalName;
        public string GoalName {
            get => _goalName;
            set {
                if(_goalName == value) {
                    return;
                }

                _goalName = value;
                OnPropertyChanged();
            }
        }

        Department _department;
        public virtual Department Department {
            get => _department;
            set => SetReferencePropertyValue(ref _department, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetReferencePropertyValue<T>(ref T propertyValue, T newValue, [CallerMemberName] string propertyName = null) where T : class {
            if(propertyValue == newValue) {
                return false;
            }

            propertyValue = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
