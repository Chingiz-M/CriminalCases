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
    /// Логика взаимодействия для AddInvestigator.xaml
    /// </summary>
    public partial class AddInvestigator : Window
    {
        public static DbContextOptions Option { get; set; }
        public AddInvestigator(DbContextOptions option)
        {
            InitializeComponent();
            Option = option;
            StaticModeView.LoadAddressesAsync(Option);
            StaticModeView.LoadPhonesAsync(Option);
            cbPhones.SelectedIndex = 0;
            cbAddressLiving.SelectedIndex = 0;
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            cbPhones.SelectedIndex = 0;
            cbAddressLiving.SelectedIndex = 0;
            tbFirstName.Text = "";
            tbSecondName.Text = "";
            tbMiddleName.Text = "";
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnAdd_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new KPContext(Option))
                {
                    await db.Investigators.AddAsync(new Investigator
                    {
                        Fio = $"{tbSecondName.Text} {tbFirstName.Text} {tbMiddleName.Text}",
                        FirstName = tbFirstName.Text,
                        SecondName = tbSecondName.Text,
                        MiddleName = tbMiddleName.Text,
                        IdAddress = ((Adress)cbAddressLiving.SelectedItem).IdAddress,
                        IdPhone = ((Telephone)cbPhones.SelectedItem).IdPhone
                    });
                    await db.SaveChangesAsync();
                }
                MessageBox.Show("Следователь добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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
    }
}
