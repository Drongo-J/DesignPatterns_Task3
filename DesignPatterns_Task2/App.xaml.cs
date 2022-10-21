using DesignPatterns_Task2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DesignPatterns_Task2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Grid MyGrid { get; internal set; }
        internal static Converter Converter { get; set; }
    }
}
