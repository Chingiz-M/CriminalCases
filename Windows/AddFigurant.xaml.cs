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
    /// Логика взаимодействия для AddFigurant.xaml
    /// </summary>
    public partial class AddFigurant : Window
    {
        public static DbContextOptions Option { get; set; }

        public AddFigurant(DbContextOptions option)
        {
            InitializeComponent();
            Option = option;
            StaticModeView.LoadPersonsAsync(Option);
            StaticModeView.LoadAddressesAsync(Option);
            StaticModeView.LoadPhonesAsync(Option);
            cbPersons.SelectedIndex = 0;
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            cbPersons.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;
            cbPhones1.SelectedIndex = -1;
            cbPhones2.SelectedIndex = -1;
            cbPhones3.SelectedIndex = -1;
            cbAddressLiving.SelectedIndex = -1;
            cbAddressReg.SelectedIndex = -1;
            tbPassport.Text = "";
            tbDrivePassport.Text = "";
            tbOtherDoc.Text = "";
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
                    await db.Figurants.AddAsync(new Figurant
                    {
                        IdPerson = ((Person)cbPersons.SelectedItem).IdPerson,
                        Status = cbStatus.SelectedValue.ToString().Split(" ")[1],
                        IdPhone1 = (Telephone)cbPhones1.SelectedItem == null ? null: ((Telephone)cbPhones1.SelectedItem).IdPhone, // если телефон1 не выбран то добавляет id=0
                        IdPhone2 = (Telephone)cbPhones2.SelectedItem == null ? null: ((Telephone)cbPhones2.SelectedItem).IdPhone, // если телефон2 не выбран то добавляет id=0
                        IdPhone3 = (Telephone)cbPhones3.SelectedItem == null ? null: ((Telephone)cbPhones3.SelectedItem).IdPhone, // если телефон3 не выбран то добавляет id=0
                        IdAddressLiving = (Adress)cbAddressLiving.SelectedItem == null ? null : ((Adress)cbAddressLiving.SelectedItem).IdAddress,// если адрес не выбран то добавляет id=0
                        IdAddressRegistr = (Adress)cbAddressReg.SelectedItem == null ? null: ((Adress)cbAddressReg.SelectedItem).IdAddress, // если адрес не выбран то добавляет id=0
                        Passport = tbPassport.Text,
                        DrivePassport = tbDrivePassport.Text,
                        OtherDocument = tbOtherDoc.Text
                    });
                    await db.SaveChangesAsync();
                }
                MessageBox.Show("Фигурант добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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
