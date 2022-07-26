using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriminalCases.StaticModel
{
    /// <summary>
    /// Вспомогательный класс для привязки данных к элементам интерфейса
    /// </summary>
    public class HelpClass
    {
        public string Item { get; set; }
    }
    /// <summary>
    /// Класс для поиска элементов
    /// </summary>
    public class SearchFields
    {
        public string Role { get; set; }
        public string FIO { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
    public static class StaticModeView
    {
        public static ObservableCollection<CriminalCase> NumbersCriminalCases { get; } = new ObservableCollection<CriminalCase>(); // номера уг. дел
        public static ObservableCollection<Bancnote> NominalsBancnotes { get; } = new ObservableCollection<Bancnote>(); // номиналы банкнот
        public static ObservableCollection<Person> Persons { get; } = new ObservableCollection<Person>(); // персоны
        public static ObservableCollection<Subdivision> Divisions { get; } = new ObservableCollection<Subdivision>(); // подразделения в окне добавления расследования
        public static ObservableCollection<Figurant> FigurantsInInvestigation { get; } = new ObservableCollection<Figurant>(); // фигуранты в окне добавления расследования
        public static ObservableCollection<Investigator> InvestigatorsInInvestigation { get; } = new ObservableCollection<Investigator>(); // следователи в окне добавления расследования
        public static ObservableCollection<Adress> Addresses { get; } = new ObservableCollection<Adress>(); // адреса
        public static ObservableCollection<Telephone> Phones { get; } = new ObservableCollection<Telephone>(); // телефоны
        public static ObservableCollection<CriminalCase> InfoCriminalCase { get; } = new ObservableCollection<CriminalCase>(); // уголовные дела
        public static ObservableCollection<Material> Materials { get; } = new ObservableCollection<Material>(); // коллекция материалов
        public static ObservableCollection<Bancnote> Bancnotes { get; } = new ObservableCollection<Bancnote>();// коллекция банкнот
        public static ObservableCollection<Investigation> Investigations { get; } = new ObservableCollection<Investigation>();// коллекция расследований
        public static ObservableCollection<HelpClass> ItemsSearch { get; } = new ObservableCollection<HelpClass>();// коллекция элементов для поиска
        public static ObservableCollection<Investigator> Investigators { get; } = new ObservableCollection<Investigator>();// коллекция следователей
        public static ObservableCollection<Figurant> Figurants { get; } = new ObservableCollection<Figurant>();// коллекция фтгурантов
        public static ObservableCollection<SearchFields> ResultSearch { get; } = new ObservableCollection<SearchFields>();// коллекция результатов поиска

        /// <summary>
        /// Загрузка в Combobox на главной форме номеров уг.дел
        /// </summary>
        /// <param name="cases"> все уголовные дела из БД</param>
        public static async void LoadNumbersCriminalCasesAsync(DbContextOptions option)
        {
            ICollection<CriminalCase> cases; 
            ClearNumsCriminalCases();
            using (var db = new KPContext(option))
                cases = await db.CriminalCases.Select(c => c).ToArrayAsync(); // выбор всех уголовных дел из БД
            foreach (var @case in cases)
                NumbersCriminalCases.Add(new CriminalCase { NumCase = @case.NumCase, IdCase = @case.IdCase});
        }
        /// <summary>
        /// Загрузка номиналов банкнот в Combobox в окне добавления материалов
        /// </summary>
        public static async void LoadNominalsBancnotesAsync(DbContextOptions option)
        {
            ICollection<Bancnote> bancnotes;
            ClearNominalsBancnotes();
            using (var db = new KPContext(option))
                bancnotes = await db.Bancnotes.Select(c => c).ToArrayAsync(); // выбор всех банкнот
            foreach (var bancnote in bancnotes)
                NominalsBancnotes.Add(new Bancnote {IdBanknotes = bancnote.IdBanknotes, NumBanknotes = bancnote.NumBanknotes });
        }
        /// <summary>
        /// Загрузка персон в Combobox в окне добавления фигурантов
        /// </summary>
        public static async void LoadPersonsAsync(DbContextOptions option)
        {
            ICollection<Person> people;
            ClearPersons();
            using (var db = new KPContext(option))
                people = await db.Persons.Select(c => c).ToArrayAsync(); // выбор всех персон
            foreach (var person in people)
                Persons.Add(new Person { IdPerson = person.IdPerson, Fio = person.Fio });
        }
        /// <summary>
        /// Загрузка фигурантов в Combobox в окне добавления изменений в расследовании
        /// </summary>
        public static async void LoadFigurantsInInvestigationAsync(DbContextOptions option)
        {
            ICollection<Figurant> figurants;
            ClearFigurantsInInvestigation();
            using (var db = new KPContext(option))
                figurants = await db.Figurants.Include(c => c.IdPersonNavigation).ToArrayAsync(); // выбор всех персон
            foreach (var person in figurants)
                FigurantsInInvestigation.Add(new Figurant { IdFigurant = person.IdFigurant, StringFIO = person.IdPersonNavigation.Fio });
        }
        /// <summary>
        /// Загрузка подразделений в Combobox в окне добавления изменений в расследовании
        /// </summary>
        public static async void LoadDivisionsInInvestigationAsync(DbContextOptions option)
        {
            ICollection<Subdivision> subdivisions;
            ClearDivisionsInInvestigation();
            using (var db = new KPContext(option))
                subdivisions = await db.Subdivisions.Select(c => c).ToArrayAsync(); // выбор всех персон
            foreach (var div in subdivisions)
                Divisions.Add(new Subdivision { IdDivision = div.IdDivision, Name = div.Name });
        }
        /// <summary>
        /// Загрузка следователей в Combobox в окне добавления изменений в расследовании
        /// </summary>
        public static async void LoadInvestigatorsInInvestigationAsync(DbContextOptions option)
        {
            ICollection<Investigator> investigators;
            ClearInvestigatorsInInvestigation();
            using (var db = new KPContext(option))
                investigators = await db.Investigators.Select(c => c).ToArrayAsync(); // выбор всех персон
            foreach (var person in investigators)
                InvestigatorsInInvestigation.Add(new Investigator { IdOfficer = person.IdOfficer, Fio = person.Fio });
        }
        /// <summary>
        /// Загрузка адресов в Combobox в окне добавления фигурантов
        /// </summary>
        public static async void LoadAddressesAsync(DbContextOptions option)
        {
            ICollection<Adress> adresses;
            ClearAddresses();
            using (var db = new KPContext(option))
                adresses = await db.Addresses.Select(c => c).ToArrayAsync(); // выбор всех персон
            foreach (var address in adresses)
                Addresses.Add(new Adress { IdAddress = address.IdAddress, Address = address.Address });
        }
        /// <summary>
        /// Загрузка телефонов в Combobox в окне добавления фигурантов
        /// </summary>
        public static async void LoadPhonesAsync(DbContextOptions option)
        {
            ICollection<Telephone> telephones;
            ClearPhones();
            using (var db = new KPContext(option))
                telephones = await db.Telephones.Select(c => c).ToArrayAsync(); // выбор всех телефонов
            foreach (var phone in telephones)
                Phones.Add(new Telephone { IdPhone = phone.IdPhone, Phone = phone.Phone });
        }
        /// <summary>
        /// Загрузка в Combobox в окне поиска элементов для поиска
        /// </summary>
        public static void LoadItemsSearch()
        {
            string[] items = new string[] { "Телефон", "Фамилия", "Имя", "Отчество" };
            foreach (var i in items)
                ItemsSearch.Add(new HelpClass { Item = i });
        }
        /// <summary>
        /// Загрузка в DataGrid в главном окне информации по выбранному уг. делу
        /// </summary>
        /// <param name="criminalCase"></param>
        public static void LoadCriminalCase(ICollection<CriminalCase> criminalCase) => InfoCriminalCase.Add(criminalCase.ToArray()[0]);
        /// <summary>
        /// Загрузка материалов в DataGrid в окне материалов
        /// </summary>
        /// <param name="materials"> материалы для выбранного уг. дела</param>
        /// <param name="option">строка подключения</param>
        public static async void LoadMaterialsAsync(ICollection<Material> materials, DbContextOptions option)
        {
            foreach (var material in materials)
            {
                string[] bancnote = null;
                string resultbancnote = String.Empty;
                using (var db = new KPContext(option))
                    bancnote = await db.Bancnotes.Where(b => b.IdBanknotes == material.IdBanknotes).Select(b => b.NumBanknotes).ToArrayAsync(); // получение банкнот для необходимых материалов дела
                if (bancnote.Count() == 0)
                    resultbancnote = "";
                else
                    resultbancnote = bancnote[0];

                Materials.Add(new Material
                {
                    StringBancnoteName = resultbancnote,
                    NameDoc = material.NameDoc,
                    TypeDoc = material.TypeDoc,
                    FilesDoc = material.FilesDoc
                });
            }
        }
        /// <summary>
        /// Загрузка банкнот в DataGrid в окне банкнот
        /// </summary>
        /// <param name="bancnotes">все банкноты</param>
        /// <param name="id_bancnotes"> id необходимых банкнот</param>
        public static void LoadBancnotes(ICollection<Bancnote> bancnotes, int[] id_bancnotes)
        {
            foreach (var bancnote in bancnotes)
            {
                foreach(int id in id_bancnotes)
                {
                    if (bancnote.IdBanknotes == id)
                        Bancnotes.Add(bancnote);
                }
            }
        }
        /// <summary>
        /// Загрузка расследоваия в DataGrid в окне расследования
        /// </summary>
        /// <param name="investigation"> необходимое расследование</param>
        /// <param name="option"> строка подключения</param>
        public static async void LoadInvestigationAsync(ICollection<Investigation> investigation, DbContextOptions option)
        {
            foreach (var inv in investigation)
            {
                int[] idPerson = null;
                string[] fioFigurant = null;
                string[] statusFigurant = null;
                string[] investigator = null;
                string[] division = null;

                using (var db = new KPContext(option))
                    idPerson = await db.Figurants.Where(b => b.IdFigurant == inv.IdFigurant).Select(b => b.IdPerson).ToArrayAsync();// получение id персоны для фигуранта из расследования

                using (var db = new KPContext(option))
                    statusFigurant = await db.Figurants.Where(b => b.IdFigurant == inv.IdFigurant).Select(b => b.Status).ToArrayAsync(); // статус фигуранта

                using (var db = new KPContext(option))
                    fioFigurant = await db.Persons.Where(b => b.IdPerson == idPerson[0]).Select(b => b.Fio).ToArrayAsync();// получение ФИО фигуранта из табл Персоны по id

                using (var db = new KPContext(option))
                    investigator = await db.Investigators.Where(b => b.IdOfficer == inv.IdOfficer).Select(b => b.Fio).ToArrayAsync();// получение ФИО следователя из табл следователей

                using (var db = new KPContext(option))
                    division = await db.Subdivisions.Where(b => b.IdDivision == inv.IdDivision).Select(b => b.Name).ToArrayAsync();// название подразделения

                Investigations.Add(new Investigation
                {
                    StringFigurantName = fioFigurant[0],
                    StringFigurantStatus = statusFigurant[0],
                    StringOfficerName = investigator[0],
                    DateEvent = inv.DateEvent,
                    StringdivisionName = division[0]
                });
            }
        }
        /// <summary>
        /// Загрузка следователей в DataGrid в окне следователей
        /// </summary>
        /// <param name="numsinvestigators"> id следователей по делу</param>
        /// <param name="numCriminalCase"> id выбранного уг. дела</param>
        /// <param name="option"> строка подключения</param>
        public static async void LoadInvestigatorsAsync(ICollection<int> numsinvestigators, int numCriminalCase, DbContextOptions option)
        {
            foreach (var inv in numsinvestigators)
            {
                string[] numCase = null;
                ICollection<Investigator> investigators;;
                string[] address = null;
                string[] phone = null;

                using (var db = new KPContext(option))
                    investigators = await db.Investigators.Where(b => b.IdOfficer == inv).ToArrayAsync();// следователь по делу

                using (var db = new KPContext(option))
                    address = await db.Addresses.Where(b => b.IdAddress == investigators.ToArray()[0].IdAddress).Select(b => b.Address).ToArrayAsync();// адрес следователя

                using (var db = new KPContext(option))
                    phone = await db.Telephones.Where(b => b.IdPhone == investigators.ToArray()[0].IdPhone).Select(b => b.Phone).ToArrayAsync();// телефон следователя

                using (var db = new KPContext(option))
                    numCase = await db.CriminalCases.Where(b => b.IdCase == numCriminalCase).Select(b => b.NumCase).ToArrayAsync(); // номер уг. дела

                Investigators.Add(new Investigator
                {
                    StringCriminalCaseName = numCase[0],
                    Fio = investigators.ToArray()[0].Fio,
                    StringAddressName = address[0],
                    StringPhoneName = phone[0]
                });
            }
        }
        /// <summary>
        /// Загрузка фигурантов в DataGrid в окне фигурантов
        /// </summary>
        /// <param name="numfigurants"> id фигурантов по делу</param>
        /// <param name="numCriminalCase"> id выбранного уг. дела</param>
        /// <param name="option"> строка подключения</param>
        public static async void LoadFigurantsAsync(ICollection<int> numfigurants, int numCriminalCase, DbContextOptions option)
        {
            foreach (var inv in numfigurants)
            {
                string[] numCase = null;
                ICollection<Figurant> figurants;
                ICollection<Person> persons;

                string[] addressliving = null;
                string addresslivingstr = null;
                string[] addressregister = null;
                string addressregisterstr = null;

                using (var db = new KPContext(option))
                    figurants = await db.Figurants.Where(b => b.IdFigurant == inv).ToArrayAsync(); // фигурант по делу

                using (var db = new KPContext(option))
                    persons = await db.Persons.Where(b => b.IdPerson == figurants.ToArray()[0].IdPerson).ToArrayAsync(); // персоны для фигуранта выше

                using (var db = new KPContext(option))
                    addressliving = await db.Addresses.Where(b => b.IdAddress == figurants.ToArray()[0].IdAddressLiving).Select(b => b.Address).ToArrayAsync(); // адрес проживания фигуранта

                if (addressliving.Length == 0)
                    addresslivingstr = "";
                else
                    addresslivingstr = addressliving[0];

                using (var db = new KPContext(option))
                    addressregister = await db.Addresses.Where(b => b.IdAddress == figurants.ToArray()[0].IdAddressRegistr).Select(b => b.Address).ToArrayAsync();// адрес регистрации фигуранта

                if (addressregister.Length == 0)
                    addressregisterstr = "";
                else
                    addressregisterstr = addressregister[0];

                using (var db = new KPContext(option))
                    numCase = await db.CriminalCases.Where(b => b.IdCase == numCriminalCase).Select(b => b.NumCase).ToArrayAsync(); // номер уг. дела
                Figurants.Add(new Figurant
                {
                    Status = figurants.ToArray()[0].Status,
                    StringFIO = persons.ToArray()[0].Fio,
                    StringBirthday = persons.ToArray()[0].BirthDate,
                    StringCityzen = persons.ToArray()[0].Citizen,
                    StringFoto = persons.ToArray()[0].Foto,
                    Passport = figurants.ToArray()[0].Passport,
                    DrivePassport = figurants.ToArray()[0].DrivePassport,
                    OtherDocument = figurants.ToArray()[0].OtherDocument,
                    ScanDocument = figurants.ToArray()[0].ScanDocument,
                    Phones = await GetPhonesAsync(figurants.ToArray()[0], option),
                    StringAddressLiving = addresslivingstr,
                    StringAddressRegister = addressregisterstr,
                    StringRemark = persons.ToArray()[0].Remark
                });
            }
        }
        /// <summary>
        /// Загрузка в DataGrid результатов поиска в окне поиска 
        /// </summary>
        /// <param name="investigator"> найденный следователь</param>
        /// <param name="figurant"> найденный фигурант</param>
        public static void LoadResultSearch(Investigator investigator, Figurant figurant)
        {
            if(investigator.Fio != null)
                ResultSearch.Add(new SearchFields
                {
                    Role = "Следователь",
                    FIO = investigator.Fio,
                    Address = investigator.StringAddressName,
                    Phone = investigator.StringPhoneName
                });
            if(figurant.StringFIO != null)
                ResultSearch.Add(new SearchFields
                {
                    Role = figurant.Status,
                    FIO = figurant.StringFIO,
                    Address = figurant.StringAddressLiving,
                    Phone = figurant.Phones
                });
        }
        /// <summary>
        /// Получение номеров телефонов для фигуранта
        /// </summary>
        /// <param name="figurant"> фигурант</param>
        /// <param name="option"> строка подключения</param>
        /// <returns></returns>
        public static async Task<string> GetPhonesAsync(Figurant figurant, DbContextOptions option)
        {
            string[] phone1 = null;
            string[] phone2 = null;
            string[] phone3 = null;
            using (var db = new KPContext(option))
                phone1 = await db.Telephones.Where(b => b.IdPhone == figurant.IdPhone1).Select(b => b.Phone).ToArrayAsync();

            using (var db = new KPContext(option))
                phone2 = await db.Telephones.Where(b => b.IdPhone == figurant.IdPhone2).Select(b => b.Phone).ToArrayAsync();

            using (var db = new KPContext(option))
                phone3 = await db.Telephones.Where(b => b.IdPhone == figurant.IdPhone3).Select(b => b.Phone).ToArrayAsync();
            List<string> phones = new List<string>(); // коллекция номеров фигуранта
            string result = String.Empty;
            foreach (var phone in phone1)
                phones.Add(phone);
            foreach (var phone in phone2)
                phones.Add(phone);
            foreach (var phone in phone3)
                phones.Add(phone);
            foreach (var phone in phones)
                result += phone + "\n"; // добавление всех номеров фигуранта в строку 
            return result;
        }

        public static void ClearNumsCriminalCases() => NumbersCriminalCases.Clear(); // очистка combobox номеров уг дел
        public static void ClearNominalsBancnotes() => NominalsBancnotes.Clear(); // очистка combobox номиналов банкнот
        public static void ClearPersons() => Persons.Clear(); // очистка combobox персон
        public static void ClearFigurantsInInvestigation() => FigurantsInInvestigation.Clear(); // очистка combobox фигурантов
        public static void ClearDivisionsInInvestigation() => Divisions.Clear(); // очистка combobox подразделений
        public static void ClearInvestigatorsInInvestigation() => InvestigatorsInInvestigation.Clear(); // очистка combobox следователей
        public static void ClearAddresses() => Addresses.Clear(); // очистка combobox адресов
        public static void ClearPhones() => Phones.Clear(); // очистка combobox телефонов
        public static void ClearCriminalCase() => InfoCriminalCase.Clear(); // очистка коллекции номеров уг дел
        public static void ClearMaterials() => Materials.Clear();// очистка коллекции материалов
        public static void ClearBancnotes() => Bancnotes.Clear();// очистка коллекции банкнот
        public static void ClearInvestigation() => Investigations.Clear();// очистка коллекции расследования
        public static void ClearInvestigators() => Investigators.Clear();// очистка коллекции следователей
        public static void ClearFigurants() => Figurants.Clear();// очистка коллекции фигурантов
        public static void ClearItemsSearch() => ItemsSearch.Clear();// очистка коллекции элементов поиска
        public static void ClearResultSearch() => ResultSearch.Clear();// очистка коллекции результатов поиска
    }
}
