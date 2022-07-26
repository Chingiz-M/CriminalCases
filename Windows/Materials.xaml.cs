﻿using Microsoft.EntityFrameworkCore;
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

namespace CriminalCases.Windows
{
    /// <summary>
    /// Логика взаимодействия для Materials.xaml
    /// </summary>
    public partial class Materials : Window
    {
        public static DbContextOptions Option { get; set; }
        public static int IDCriminalCase { get; set; }
        public Materials(DbContextOptions option, int id)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            Option = option;
            IDCriminalCase = id;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddMaterial window = new AddMaterial(Option, IDCriminalCase);
            window.Owner = this;
            window.ShowDialog();
        }
    }
}
