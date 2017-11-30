using DahuUWP.Utils.Converter;
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
        

        public StarRating()
        {
            Stars = new ObservableCollection<object>();
            DataContext = this;
            this.InitializeComponent();
            CreateStars(3);
            //List<TodoItem> items = new List<TodoItem>();
            //items.Add(new TodoItem() { StarIcon = "star-full" });
            //items.Add(new TodoItem() { StarIcon = "star-full" });
            //items.Add(new TodoItem() { StarIcon = "star-full" });

            //starList.ItemsSource = items;
            //ColumnDefinition column = new ColumnDefinition();
            //colum
            //StarRatingGrid.ColumnDefinitions.Add();
        }

        //public List<object> Stars { get; set; }

        public ObservableCollection<object> Stars
        {
            get;
            set;
        }

        public void CreateStars(int score)
        {
            int it = 0;

            //feeling full stars
            while (it < score && it < NbStars)
            {
                object star = new
                {
                    StarIcon = IconConverter.IconToImageBrush("star-full")
                };
                Stars.Add(star);
                it++;
            }
            //feeling empty stars
            while (it < NbStars)
            {
                object star = new
                {
                    StarIcon = IconConverter.IconToImageBrush("star-empty")
                };
                Stars.Add(star);
                it++;
            }
            StarList.DataContext = this.Stars;
        }

        private void ColorStarBeforEndIndex(int index)
        {
            int it = 0;

            while (it < index && it < NbStars)
            {
                object star = new
                {
                    StarIcon = IconConverter.IconToImageBrush("star-full")
                };
                Stars[it] = star;
                //StarList.Items.Insert(it + 1, star);
                it++;
            }
            while (it < NbStars)
            {
                object star = new
                {
                    StarIcon = IconConverter.IconToImageBrush("star-empty")
                };
                Stars[it] = star;
                //StarList.Items.Insert(it, star);
                it++;
            }
        }



        public class TodoItem
        {
            public string StarIcon { get; set; }
        }

        private void Grid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            //object star = new
            //{
            //    StarIcon = IconConverter.IconToImageBrush("star-empty")
            //};
            //Stars.Add(star);
            //List<object> stars = new List<object>();


            var item = (sender as FrameworkElement).DataContext;
            int index = StarList.Items.IndexOf(item);

            //var star = new Grid()
            //{
            //    Background = IconConverter.IconToImageBrush("star-empty")
            //};
            //Stars.Remove(0);
            //StarList.ItemsSource = Stars;
            //stars.Add(star);
            //StarList.ItemsSource = stars;
            ColorStarBeforEndIndex(index);
        }
    }
}
