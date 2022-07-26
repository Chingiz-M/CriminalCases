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
    /// Логика взаимодействия для AddPerson.xaml
    /// </summary>
    public partial class AddPerson : Window
    {
        public static DbContextOptions Option { get; set; }

        public AddPerson(DbContextOptions option)
        {
            InitializeComponent();
            Option = option;

        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbFirstName.Text = "";
            tbSecondName.Text = "";
            tbMiddleName.Text = "";
            tbCountry.Text = "";
            tbPhoto.Text = "";
            tbRemark.Text = "";
            dateEvent.Value = new DateTime(2000, 01, 01);
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
                    await db.Persons.AddAsync(new Person
                    {
                        Fio = $"{tbSecondName.Text} {tbFirstName.Text} {tbMiddleName.Text}",
                        FirstName = tbFirstName.Text,
                        SecondName = tbSecondName.Text,
                        MiddleName = tbMiddleName.Text,
                        BirthDate = dateEvent.Value,
                        Citizen = tbCountry.Text,
                        Foto = tbPhoto.Text,
                        Remark = tbRemark.Text
                    });
                    await db.SaveChangesAsync();
                }
                MessageBox.Show("Персона добавлена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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
