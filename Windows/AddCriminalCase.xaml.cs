using CriminalCases.StaticModel;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для AddCriminalCase.xaml
    /// </summary>
    public partial class AddCriminalCase : Window
    {
        public static DbContextOptions Option { get; set; }
        public AddCriminalCase(DbContextOptions option)
        {
            InitializeComponent();
            Option = option;
        }
        private async void btnAdd_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new KPContext(Option))
                {
                    await db.CriminalCases.AddAsync(new CriminalCase
                    {
                        NumCase = tbNumCrCase.Text,
                        DateStart = dateEvent.Value,
                        Article = tbArticle.Text,
                        Title = tbTitle.Text,
                        Remark = tbRemark.Text
                    });
                    await db.SaveChangesAsync();
                }
                StaticModeView.LoadNumbersCriminalCasesAsync(Option); // загрузка номеров уголовных дел в combobox
                MessageBox.Show($"Уголовное дело № {tbNumCrCase.Text} добавлено!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbNumCrCase.Text = "";
            tbArticle.Text = "";
            tbTitle.Text = "";
            tbRemark.Text = "";
            dateEvent.Value = new DateTime(2021, 01, 01, 00, 00, 00);
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
