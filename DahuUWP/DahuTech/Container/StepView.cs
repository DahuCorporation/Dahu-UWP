using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.DahuTech.Container
{
    public class StepView : INotifyPropertyChanged
    {
        private Type _view;
        public Type View
        {
            get
            {
                return _view;
            }

            set
            {
                _view = value;

                this.NotifyPropertyChanged("View");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        //public Action Listener { get; set; }
    }
}
