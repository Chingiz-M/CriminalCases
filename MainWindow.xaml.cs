using CriminalCases.Windows;
using CriminalCases.StaticModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CriminalCases
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Имя сервера для подключения
        /// </summary>
        public static string Server { get; set; }
        /// <summary>
        /// Имя Базы данных
        /// </summary>
        public static string DataBase { get; set; }
        /// <summary>
        /// Выбранный номер уголовного дела
        /// </summary>
        public static int IdSelectedCriminalCase { get; set; }
        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        public static DbContextOptions option = new DbContextOptionsBuilder<KPContext>()
                .UseSqlServer($"Server={Server};Database=KP;Trusted_Connection=True;ConnectRetryCount=0;").Options;
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Открытие окна подключения к БД
        /// </summary>
        private async void ConnectBDAsync(object sender, RoutedEventArgs e)
        {
            ConnectDB window = new ConnectDB(); 
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
                Server = window.Server;
                DataBase = window.DataBase;
                if (await DBIsConnectAsync(Server)) // проверка подлинности подключения
                {
                    MessageBox.Show("БД успешно подключена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    cbCases.IsEnabled = true;
                    AddCrCase.IsEnabled = true;
                    AddAddressItem.IsEnabled = true;
                    AddDivisionItem.IsEnabled = true;
                    AddTelephoneItem.IsEnabled = true;
                    AddBancnoteItem.IsEnabled = true;
                    AddPersonItem.IsEnabled = true;
                    AddFigurantItem.IsEnabled = true;
                    AddInvestigatorItem.IsEnabled = true;
                    StaticModeView.LoadNumbersCriminalCasesAsync(option); // загрузка номеров уголовных дел в combobox
                }
                else
                    MessageBox.Show("Неверное подключение!\nПопробуйте снова!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Подключения к БД master(есть по умолчанию на всех серверах) к выбранному серверу
        /// </summary>
        /// <param name="server"> имя сервера </param>
        private async Task<bool> DBIsConnectAsync(string server)
        {
            SqlConnection connection = new SqlConnection($"Server={server};Database=master;Trusted_Connection=True;");
            try
            {
                await connection.OpenAsync();
                await connection.CloseAsync();
                return true;
            }
            catch (SystemException ex)
            {
                if (connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
                return false;
            }
        }
        /// <summary>
        /// Закрытие приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Кнопка загрузки окна с материалами выбранного уголовного дела
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnMaterials_ClickAsync(object sender, RoutedEventArgs e)
        {
            StaticModeView.ClearMaterials();// очистка DataGrid в окне с материалами
            using (var db = new KPContext(option))
            {
                var materials = await db.Materials.Where(m => m.IdCase == IdSelectedCriminalCase).ToArrayAsync(); // получение всех материалов из выбранного уг. дела
                StaticModeView.LoadMaterialsAsync(materials, option); // загрузка материалов в DataGrid в окне материалов
            }
            Materials window = new Materials(option, IdSelectedCriminalCase);
            window.Owner = this;
            window.Show();
        }
        /// <summary>
        /// Кнопка загрузки окна с купюрами выбранного уголовного дела
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnMoney_ClickAsync(object sender, RoutedEventArgs e)
        {
            StaticModeView.ClearBancnotes();// очистка DataGrid в окне с купюрами
            using (var db = new KPContext(option))
            {
                var id_bancnotes = await db.Materials.Where(m => m.IdCase == IdSelectedCriminalCase).Select(m => m.IdBanknotes).ToArrayAsync();// получение всех id банкнот из материалов выбранного уг. дела
                var bancnotes = await db.Bancnotes.Select(b => b).ToArrayAsync(); // получение всех банкнот из выбранного уг. дела
                StaticModeView.LoadBancnotes(bancnotes, id_bancnotes); // загрузка банкнот в DataGrid в окне банкнот
            }
            Banknotes window = new Banknotes();
            window.Owner = this;
            window.Show();
        }
        /// <summary>
        /// Кнопка загрузки окна с фигурантами выбранного уголовного дела
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnFigurants_ClickAsync(object sender, RoutedEventArgs e)
        {
            StaticModeView.ClearFigurants(); // очистка DataGrid в окне с фигурантами
            int[] figurants = null;
            using (var db = new KPContext(option))
            {
                figurants = await db.Investigations.Where(i => i.IdCase == IdSelectedCriminalCase)
                    .Select(i => i.IdFigurant).Distinct().ToArrayAsync(); // получение всех id фигурантов из выбранного уг. дела
                StaticModeView.LoadFigurantsAsync(figurants, IdSelectedCriminalCase, option);  // загрузка фигурантов в DataGrid в окне фигурантов
            }
            Figurants window = new Figurants();
            window.Owner = this;
            window.Show();
        }
        /// <summary>
        /// Кнопка загрузки окна со следователями выбранного уголовного дела
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnInvestigators_ClickAsync(object sender, RoutedEventArgs e)
        {
            StaticModeView.ClearInvestigators(); // очистка DataGrid в окне со следователями
            int[] invistigators = null;
            using (var db = new KPContext(option))
            {
                invistigators = await db.Investigations.Where(i => i.IdCase == IdSelectedCriminalCase)
                    .Select(i => i.IdOfficer).Distinct().ToArrayAsync();// получение всех id следователей из выбранного уг. дела
                StaticModeView.LoadInvestigatorsAsync(invistigators, IdSelectedCriminalCase, option); // загрузка следователей в DataGrid в окне следователей
            }
            Investigators window = new Investigators();
            window.Owner = this;
            window.Show();
        }
        /// <summary>
        /// Кнопка загрузки окна с расследованием выбранного уголовного дела
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnInvestigation_ClickAsync(object sender, RoutedEventArgs e)
        {
            StaticModeView.ClearInvestigation(); // очистка DataGrid в окне с расследованием
            using (var db = new KPContext(option))
            {
                var investigation = await db.Investigations. Where(i => i.IdCase == IdSelectedCriminalCase).ToArrayAsync(); // получение всех изменений расследования из выбранного уг. дела
                StaticModeView.LoadInvestigationAsync(investigation, option);// загрузка расследоваия в DataGrid в окне расследования
            }
            Windows.Investigation window = new Windows.Investigation(option, IdSelectedCriminalCase);
            window.Owner = this;
            window.Show();
        }
        /// <summary>
        /// Событие изменения выбора номера уг. дела в combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void cbCases_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            var selectedItem = (CriminalCase)comboBox.SelectedItem; // номер выбранного уг. дела
            if (comboBox.SelectedItem != null)
            {
                StaticModeView.ClearCriminalCase(); // очистка DataGrid в главном окне приложения
                IdSelectedCriminalCase = selectedItem.IdCase;
                using (var db = new KPContext(option))
                {
                    var info = await db.CriminalCases.Where(c => c.NumCase == selectedItem.NumCase).ToArrayAsync();// получение информации по выбранноому уг. делу из БД
                    StaticModeView.LoadCriminalCase(info);// загрузка инфы по уг. делу в DataGrid в главном окне приложения
                }
            }
        }
        /// <summary>
        /// Открытие окна поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search window = new Search(option);
            window.Owner = this;
            window.Show();
        }
        /// <summary>
        /// Открытие сообщения с информацией о программе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutProgramm_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Resources.AboutProgramm, "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        /// <summary>
        /// Открытие окна добавления уголовного дела
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCrCase_Click(object sender, RoutedEventArgs e)
        {
            AddCriminalCase window = new AddCriminalCase(option);
            window.Owner = this;
            window.ShowDialog();
        }
        /// <summary>
        /// Открытие окна добавления адреса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddAddressItem_Click(object sender, RoutedEventArgs e)
        {
            AddAddress window = new AddAddress(option);
            window.Owner = this;
            window.ShowDialog();
        }
        /// <summary>
        /// Открытие окна добавления подразделения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDivisionItem_Click(object sender, RoutedEventArgs e)
        {
            AddDivision window = new AddDivision(option);
            window.Owner = this;
            window.ShowDialog();
        }
        /// <summary>
        /// Открытие окна добавления телефона
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTelephoneItem_Click(object sender, RoutedEventArgs e)
        {
            AddTelephone window = new AddTelephone(option);
            window.Owner = this;
            window.ShowDialog();
        }
        /// <summary>
        /// Открытие окна добавления банкнот
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBancnoteItem_Click(object sender, RoutedEventArgs e)
        {
            AddBancnote window = new AddBancnote(option);
            window.Owner = this;
            window.ShowDialog();
        }
        /// <summary>
        /// Открытие окна добавления персоны
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPersonItem_Click(object sender, RoutedEventArgs e)
        {
            AddPerson window = new AddPerson(option);
            window.Owner = this;
            window.ShowDialog();
        }
        /// <summary>
        /// Открытие окна добавления фигуранта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddFigurantItem_Click(object sender, RoutedEventArgs e)
        {
            AddFigurant window = new AddFigurant(option);
            window.Owner = this;
            window.ShowDialog();
        }
        /// <summary>
        /// Открытие окна добавления следователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddInvestigatorItem_Click(object sender, RoutedEventArgs e)
        {
            AddInvestigator window = new AddInvestigator(option);
            window.Owner = this;
            window.ShowDialog();
        }
    }
}
