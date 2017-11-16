using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace DahuUWP.Views.Components.Inputs
{
    public sealed partial class MyUserControl : UserControl
    {
        public MyUserControl()
        {
            this.InitializeComponent();
            //this.DataContext = this;
            (this.Content as FrameworkElement).DataContext = this;
        }

        public string CellValue
        {
            get { return (string)GetValue(CellValueProperty); }
            set { SetValue(CellValueProperty, value); }
        }

        // NOTE: essayer de faire avec Inotify pour que les changements soit fait directement et pas 

        public static readonly DependencyProperty CellValueProperty =
            DependencyProperty.Register("CellValue", typeof(string), typeof(MyUserControl), new PropertyMetadata(false));


        //public string Text
        //{
        //    get
        //    {
        //        return (string)GetValue(TextProperty);
        //    }

        //    set
        //    {
        //        SetValueDp(TextProperty, value);
        //    }
        //}

        //public static readonly DependencyProperty TextProperty =
        //DependencyProperty.Register("Text", typeof(string),
        //typeof(MyUserControl), null);

        //public event PropertyChangedEventHandler PropertyChanged;

        //void SetValueDp(DependencyProperty property, object value,
        //[System.Runtime.CompilerServices.CallerMemberName] string p = null)
        //{
        //    SetValue(property, value);
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(p));
        //    }
        //}








        //public string MyControlsText { get; set; }

        //public string MyControlsText
        //{
        //    get { return (string)GetValue(MyControlsTextProperty); }
        //    set { SetValue(MyControlsTextProperty, value); }
        //}

        //public static readonly DependencyProperty MyControlsTextProperty =
        //         DependencyProperty.Register("MyControlsText", typeof(string),
        //            typeof(MyUserControl), new PropertyMetadata(String.Empty));
    }
}
