using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DahuUWP.DahuTech.Container
{
    public class StepProgressBarElem : INotifyPropertyChanged
    {
        public StepProgressBarElem()
        {
            TextBlockStyle = Application.Current.Resources["DahuTextLinkDark"] as Style;
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;

                this.NotifyPropertyChanged("Name");
            }
        }

        private string _link;
        public string Link
        {
            get
            {
                return _link;
            }

            set
            {
                _link = value;

                this.NotifyPropertyChanged("Link");
            }
        }
        
        private Style _textBlockStyle;
        public Style TextBlockStyle
        {
            get
            {
                return _textBlockStyle;
            }

            set
            {
                _textBlockStyle = value;

                this.NotifyPropertyChanged("TextBlockStyle");
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
