using System;
using System.Collections.Generic;
using System.ComponentModel; 
using System.Linq;
using System.Windows.Controls;
using WpfChurgel.Models; 

namespace WpfChurgel.View.Pages
{
    public partial class MenuPage : Page, INotifyPropertyChanged 
    {
        public MenuPage()
        {
            InitializeComponent();
            DataContext = this;
            LoadMenuData(DateTime.Today);
        }

        private void LoadMenuData(DateTime menuDate)
        {
           
            var menuForDate = App.context.Menu.FirstOrDefault(m => m.Date == menuDate);

            if (menuForDate != null)
            {
                SelectedMenuDate = menuForDate.Date ?? DateTime.MinValue;

              
                var menuDishes = menuForDate.Dishes.ToList();

                MenuDishes = menuDishes;

               
                OnPropertyChanged(nameof(MenuDishes));
            }
            else
            {
             
                MenuDishes = new List<Dishes>();

               
                OnPropertyChanged(nameof(MenuDishes));
            }
        }

        
        public event PropertyChangedEventHandler PropertyChanged;

     
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

      
        public DateTime SelectedMenuDate { get; set; }

      
        private List<Dishes> _menuDishes;

   
        public List<Dishes> MenuDishes
        {
            get { return _menuDishes; }
            set
            {
                _menuDishes = value;
                OnPropertyChanged(nameof(MenuDishes));
            }
        }

        private void MenuDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
         
            if (MenuDatePicker.SelectedDate.HasValue)
            {
                LoadMenuData(MenuDatePicker.SelectedDate.Value);
            }
        }
    }
}
