using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CriminalCases.StaticModel;
using Microsoft.EntityFrameworkCore;

namespace CriminalCases.Windows
{
    /// <summary>
    /// Логика взаимодействия для Investigators.xaml
    /// </summary>
    public partial class Investigators : Window
    {
        public Investigators()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }
    }
}
