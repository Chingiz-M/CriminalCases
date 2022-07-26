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
    /// Логика взаимодействия для AddMaterial.xaml
    /// </summary>
    public partial class AddMaterial : Window
    {
        public static DbContextOptions Option { get; set; }
        public static int IDCriminalCase { get; set; }

        public AddMaterial(DbContextOptions option, int id)
        {
            InitializeComponent();
            Option = option;
            IDCriminalCase = id;
            StaticModeView.LoadNominalsBancnotesAsync(Option);
            cbBancnotes.SelectedIndex = 0;
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbNameDoc.Text = "";
            tbType.Text = "";
            tbLink.Text = "";
            cbBancnotes.SelectedIndex = 0;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnAdd_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                var bancnote = (Bancnote)cbBancnotes.SelectedItem;
                using (var db = new KPContext(Option))
                {
                    await db.Materials.AddAsync(new Material
                    {
                        IdCase = IDCriminalCase,
                        IdBanknotes = bancnote.IdBanknotes,
                        NameDoc = tbNameDoc.Text,
                        TypeDoc = tbType.Text,
                        FilesDoc = tbLink.Text
                    });
                    await db.SaveChangesAsync();
                }
                MessageBox.Show("Материалы добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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
