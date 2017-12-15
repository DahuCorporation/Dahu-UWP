using DahuUWP.Utils.Converter;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class StarRating : UserControl
    {
        private int NbStars = 5;
        private int Score = 0;

        /// <summary>
        /// This var is used to know if the star as been clicked before exiting
        /// When pointer exit without clicking the stars are reaff at the first score
        /// So this var avoid the fact to reaff if one star as been cliked
        /// </summary>
        private bool Clicked = false;
        

        public StarRating()
        {
            Stars = new ObservableCollection<StarsRat>();
            DataContext = this;
            this.InitializeComponent();
            CreateStars(3);
        }

        public ObservableCollection<StarsRat> Stars
        {
            get;
            set;
        }

        public void CreateStars(int score)
        {
            Score = score - 1;
            int it = 0;

            //feeling full stars
            while (it <= score && it < NbStars)
            {
                StarsRat star = new StarsRat()
                {
                    StarIcon = IconConverter.IconToImageBrush("star-full"),
                    FullStar = true
                };
                Stars.Add(star);
                it++;
            }
            //feeling empty stars
            while (it < NbStars)
            {
                StarsRat star = new StarsRat()
                {
                    StarIcon = IconConverter.IconToImageBrush("star-empty"),
                    FullStar = false
                };
                Stars.Add(star);
                it++;
            }
            StarList.DataContext = this.Stars;
        }

        private void ColorStarBeforEndIndex(int index)
        {
            int it = 0;

            while (it <= index && it < NbStars)
            {
                if ((((StarsRat)Stars[it]).FullStar == false)) {
                    ((StarsRat)Stars[it]).StarIcon = IconConverter.IconToImageBrush("star-full");
                    ((StarsRat)Stars[it]).FullStar = true;
                }
                it++;
            }   
            while (it < NbStars)
            {
                if ((((StarsRat)Stars[it]).FullStar == true))
                {
                    ((StarsRat)Stars[it]).StarIcon = IconConverter.IconToImageBrush("star-empty");
                    ((StarsRat)Stars[it]).FullStar = false;
                }
                it++;
            }
        }

        private void Grid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Clicked = true;
            var item = (sender as FrameworkElement).DataContext;
            int index = StarList.Items.IndexOf(item);

            Score = index;
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Clicked = false;
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            var item = (sender as FrameworkElement).DataContext;
            int index = StarList.Items.IndexOf(item);

            ColorStarBeforEndIndex(index);
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);

            if (!Clicked)
            {
                ColorStarBeforEndIndex(Score);
            }
            Clicked = false;
        }
    }

    public class StarsRat : ObservableObject
    {
        //public ImageBrush StarIcon { get; set; }
        public bool FullStar { get; set; }

        private ImageBrush starIcon;
        
        public ImageBrush StarIcon
        {
            get { return starIcon; }
            set
            {
                if (value != starIcon)
                {
                    starIcon = value;
                    RaisePropertyChanged("StarIcon");
                }
            }
        }
    }
}
