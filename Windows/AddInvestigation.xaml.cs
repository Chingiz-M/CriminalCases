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
    /// Логика взаимодействия для AddInvestigation.xaml
    /// </summary>
    public partial class AddInvestigation : Window
    {
        public static DbContextOptions Option { get; set; }
        public static int IDCriminalCase { get; set; }

        public AddInvestigation(DbContextOptions option, int id)
        {
            InitializeComponent();
            Option = option;
            IDCriminalCase = id;
            StaticModeView.LoadFigurantsInInvestigationAsync(Option);
            StaticModeView.LoadInvestigatorsInInvestigationAsync(Option);
            StaticModeView.LoadDivisionsInInvestigationAsync(Option);
            cbFigurants.SelectedIndex = 0;
            cbInvestigators.SelectedIndex = 0;
            dateEvent.Value = new DateTime(2021, 01, 01, 00, 00, 00);
            cbInvestigators.SelectedIndex = 0;
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            cbFigurants.SelectedIndex = 0;
            cbInvestigators.SelectedIndex = 0;
            dateEvent.Value = new DateTime(2021, 01, 01, 00, 00, 00);
            cbInvestigators.SelectedIndex = 0;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnAdd_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                //var bancnote = (Bancnote)cbBancnotes.SelectedItem;
                using (var db = new KPContext(Option))
                {
                    await db.Investigations.AddAsync(new CriminalCases.Investigation
                    {
                        IdCase = IDCriminalCase,
                        IdFigurant = ((Figurant)cbFigurants.SelectedItem).IdFigurant,
                        IdOfficer = ((Investigator)cbInvestigators.SelectedItem).IdOfficer,
                        DateEvent = dateEvent.Value,
                        IdDivision = ((Subdivision)cbDivisions.SelectedItem).IdDivision
                    });
                    await db.SaveChangesAsync();
                }
                MessageBox.Show("Изменения добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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
