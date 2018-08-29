using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.DahuTech.Container
{
    public class Step : INotifyPropertyChanged
    {
        private StepProgressBarElem _stepProgressBarElem;
        public StepProgressBarElem StepProgressBarElem
        {
            get
            {
                return _stepProgressBarElem;
            }

            set
            {
                _stepProgressBarElem = value;

                this.NotifyPropertyChanged("StepProgressBarElem");
            }
        }

        private StepView _stepView;
        public StepView StepView
        {
            get
            {
                return _stepView;
            }

            set
            {
                _stepView = value;

                this.NotifyPropertyChanged("StepView");
            }
        }

        private Status _status;
        public Status Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;

                this.NotifyPropertyChanged("Status");
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
    }

    public enum Status { Virgin, Active, Passed };
}
