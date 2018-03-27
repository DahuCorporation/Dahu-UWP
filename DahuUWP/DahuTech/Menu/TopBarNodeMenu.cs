using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.DahuTech.Menu
{
    public class TopBarNodeMenu : NodeMenu
    {
        private int _hoverRectangleOpacity;
        public int HoverRectangleOpacity
        {
            get
            {
                return _hoverRectangleOpacity;
            }

            set
            {
                _hoverRectangleOpacity = value;

                this.NotifyPropertyChanged("HoverRectangleOpacity");
            }
        }

        private bool _isActive;
        public bool IsActive
        {
            get
            {
                return _isActive;
            }

            set
            {
                _isActive = value;

                this.NotifyPropertyChanged("IsActive");
            }
        }
    }
}
