using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.DahuTech.Inputs
{
    public class DahuButtonBindings : ObservableObject
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (value != _isBusy)
                {
                    _isBusy = value;
                    RaisePropertyChanged("IsBusy");
                }
            }
        }

        //private bool NotifyPropertyChanged<T>(ref T variable, T valeur, [CallerMemberName] string nomPropriete = null)
        //{
        //    if (object.Equals(variable, valeur)) return false;

        //    variable = valeur;
        //    RaisePropertyChanged(nomPropriete);
        //    return true;
        //}
        

        public Action TappedFuncListener { get; set; }
    }
}
