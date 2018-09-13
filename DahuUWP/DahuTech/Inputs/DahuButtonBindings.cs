using DahuUWP.Views;
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
        public string Name { get; set; }

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

        public void LinkIt()
        {
            if (RedirectedLink != null)
            {
                HomePage.DahuFrame.Navigate(RedirectedLink, Parameter);
            }
            else if (FuncListener != null)
            {
                FuncListener.Invoke(Parameter);
            }
        }

        public Action<object> FuncListener { get; set; }

        /// <summary>
        /// Take one of the two var : TappedFuncListener or RedirectedLink
        /// </summary>
        //public Action TappedFuncListener { get; set; }

        /// <summary>
        /// Take one of the two var : TappedFuncListener or RedirectedLink
        /// You can also combine with paramater
        /// </summary>
        public Type RedirectedLink { get; set; }

        /// <summary>
        /// This var is going with RedirectedLink
        /// </summary>
        public object Parameter { get; set; }
    }
}
