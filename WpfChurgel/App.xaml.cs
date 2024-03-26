using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfChurgel.Models;

namespace WpfChurgel
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static FoodDispatcherDBEntities context = new FoodDispatcherDBEntities();
        public static Products selectedPosition;
    }
}
