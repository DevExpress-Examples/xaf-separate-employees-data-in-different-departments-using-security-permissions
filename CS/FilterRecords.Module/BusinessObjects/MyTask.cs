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
    public class MyTask : INotifyPropertyChanged {
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
        string _subject;
        public string Subject {
            get => _subject;
            set {
                if(_subject == value) {
                    return;
                }

                _subject = value;
                OnPropertyChanged();
            }
        }

        bool _isSharedTask;
        public bool IsSharedTask {
            get {
                return _isSharedTask;
            }
            set {
                if(_isSharedTask == value) {
                    return;
                }
                _isSharedTask = value;
                OnPropertyChanged();
            }
        }

        ApplicationUser _assignedUser;
        public virtual ApplicationUser AssignedUser {
            get => _assignedUser;
            set => SetReferencePropertyValue(ref _assignedUser, value);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) {
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
