using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CriminalCases.StaticModel;
using Microsoft.EntityFrameworkCore;

namespace CriminalCases.Windows
{
    /// <summary>
    /// Логика взаимодействия для Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        public static DbContextOptions Option { get; set; }
        public Search(DbContextOptions option)
        {
            InitializeComponent();
            Option = option;
            StaticModeView.ClearItemsSearch(); // очистка коллекции элементов поиска
            StaticModeView.LoadItemsSearch(); // Загрузка в Combobox в окне поиска элементов для поиска
            this.WindowState = WindowState.Maximized;
        }
        /// <summary>
        /// Кнопка поиска по выбранному элементу поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSearch_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                var searchitem = (HelpClass)cbItemsSearch.SelectedItem; // выбранный элемент для поиска в ComboBоx
                var value = tbValueSearch.Text; // значение для поиска
                if (value != "")
                {
                    switch (searchitem.Item)
                    {
                        case "Телефон":
                            {
                                int[] phonesID = null;
                                ICollection<Investigator> Investigators;
                                List<Investigator> ResInvestigators = new List<Investigator>();
                                ICollection<Figurant> Figurants;
                                List<Figurant> AllSearchFigurants = new List<Figurant>();
                                ICollection<Person> Persons;
                                List<Person> ResPersons = new List<Person>();

                                using (var db = new KPContext(Option))
                                    phonesID = await db.Telephones.Where(b => EF.Functions.Like(b.Phone, $"%{value}%")).Select(b => b.IdPhone).ToArrayAsync(); // id скомого телефона

                                foreach (var id in phonesID)
                                {
                                    using (var db = new KPContext(Option))
                                        Investigators = await db.Investigators.Where(b => b.IdPhone == id).ToArrayAsync(); // следователь с искомым телефоном
                                    if (Investigators.Count() > 0)
                                        ResInvestigators.AddRange(Investigators);

                                    using (var db = new KPContext(Option))
                                        Figurants = await db.Figurants.Where(b => b.IdPhone1 == id || b.IdPhone2 == id
                                                                    || b.IdPhone3 == id).ToArrayAsync(); // фигурант с искомым телефоном
                                    if (Figurants.Count() > 0)
                                        AllSearchFigurants.AddRange(Figurants);
                                }
                                foreach (var f in AllSearchFigurants)
                                {
                                    using (var db = new KPContext(Option))
                                        Persons = await db.Persons.Where(b => b.IdPerson == f.IdPerson).ToArrayAsync();// персона найденных фигурантов
                                    if (Persons.Count() > 0)
                                        ResPersons.AddRange(Persons);
                                }
                                SearchValue(ResInvestigators, ResPersons);
                                break;
                            }

                        case "Фамилия":
                            {
                                StaticModeView.ClearResultSearch();// очистка коллекции результатов поиска
                                ICollection<Investigator> Investigators;
                                ICollection<Person> PersonFigurants;
                                using (var db = new KPContext(Option))
                                    Investigators = await db.Investigators.Where(b => EF.Functions.Like(b.SecondName, $"%{value}%")).ToArrayAsync(); // следователи с искомой фамилией
                                using (var db = new KPContext(Option))
                                    PersonFigurants = await db.Persons.Where(b => EF.Functions.Like(b.SecondName, $"%{value}%")).ToArrayAsync(); // персоны с искомой фамилией
                                SearchValue(Investigators, PersonFigurants);
                                break;
                            }
                        case "Имя":
                            {
                                StaticModeView.ClearResultSearch();// очистка коллекции результатов поиска
                                ICollection<Investigator> Investigators;
                                ICollection<Person> PersonFigurants;
                                using (var db = new KPContext(Option))
                                    Investigators = await db.Investigators.Where(b => EF.Functions.Like(b.FirstName, $"%{value}%")).ToArrayAsync();// следователи с искомым именем
                                using (var db = new KPContext(Option))
                                    PersonFigurants = await db.Persons.Where(b => EF.Functions.Like(b.FirstName, $"%{value}%")).ToArrayAsync(); // персоны с искомым именем
                                SearchValue(Investigators, PersonFigurants);
                                break;
                            }
                        case "Отчество":
                            {
                                StaticModeView.ClearResultSearch();// очистка коллекции результатов поиска
                                ICollection<Investigator> Investigators;
                                ICollection<Person> PersonFigurants;
                                using (var db = new KPContext(Option))
                                    Investigators = await db.Investigators.Where(b => EF.Functions.Like(b.MiddleName, $"%{value}%")).ToArrayAsync();// следователи с искомым отчестовом
                                using (var db = new KPContext(Option))
                                    PersonFigurants = await db.Persons.Where(b => EF.Functions.Like(b.MiddleName, $"%{value}%")).ToArrayAsync(); // персоны с искомым отчестовом
                                SearchValue(Investigators, PersonFigurants);
                                break;
                            }
                    }
                }
                else
                    MessageBox.Show("Введите значение для поиска!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch(SystemException ex) { MessageBox.Show($"{ex}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); }
            
        }
        /// <summary>
        /// Метод поска следователей и фигурантов
        /// </summary>
        /// <param name="Investigators"></param>
        /// <param name="PersonFigurants"></param>
        public static async void SearchValue(ICollection<Investigator> Investigators, ICollection<Person> PersonFigurants)
        {
            StaticModeView.ClearResultSearch();
            ICollection<Figurant> Figurants;
            Figurant resultFigurant = new Figurant();
            Investigator resultInvestigator = new Investigator();
            string[] InvestigatorsAddress = null;
            string[] InvestigatorPhone = null;
            string[] FigurantsAddress = null;

            if (Investigators.Count() > 0) // если есть искомые следователи
            {
                foreach (var inv in Investigators)
                {
                    using (var db = new KPContext(Option))
                        InvestigatorsAddress = await db.Addresses.Where(b => b.IdAddress == inv.IdAddress).Select(b => b.Address).ToArrayAsync();// адрес следователя
                    using (var db = new KPContext(Option))
                        InvestigatorPhone = await db.Telephones.Where(b => b.IdPhone == inv.IdPhone).Select(b => b.Phone).ToArrayAsync(); // телефон следователя
                    resultInvestigator.StringAddressName = InvestigatorsAddress.ToArray()[0];
                    resultInvestigator.StringPhoneName = InvestigatorPhone.ToArray()[0];
                    resultInvestigator.Fio = inv.Fio;
                    StaticModeView.LoadResultSearch(resultInvestigator, new Figurant()); // Загрузка в DataGrid результатов поиска в окне поиска 
                }
            }

            if (PersonFigurants.Count() > 0)// если есть искомые фигуранты
            {
                foreach (var p in PersonFigurants)
                {
                    using (var db = new KPContext(Option))
                        Figurants = await db.Figurants.Where(b => b.IdPerson == p.IdPerson).ToArrayAsync(); // фигурант
                    using (var db = new KPContext(Option))
                        FigurantsAddress = await db.Addresses.Where(b => b.IdAddress == Figurants.ToArray()[0].IdAddressLiving).Select(b => b.Address).ToArrayAsync();// адрес проживания фигуранта

                    resultFigurant.StringFIO = p.Fio; // фио фигуранта
                    resultFigurant.Status = Figurants.ToArray()[0].Status;
                    resultFigurant.StringAddressLiving = FigurantsAddress.ToArray()[0];
                    resultFigurant.Phones = await StaticModeView.GetPhonesAsync(Figurants.ToArray()[0], Option); // телефоны фигуранта
                    StaticModeView.LoadResultSearch(new Investigator(), resultFigurant); // Загрузка в DataGrid результатов поиска в окне поиска 
                }
            }
        }
    }
}
