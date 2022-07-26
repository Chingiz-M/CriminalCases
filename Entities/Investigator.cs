using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CriminalCases
{
    public partial class Investigator
    {
        /// <summary>
        /// Таблица следователей
        /// </summary>
        public Investigator()
        {
            Investigations = new HashSet<Investigation>();
        }

        public int IdOfficer { get; set; }
        public string Fio { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public int? IdAddress { get; set; }
        public int? IdPhone { get; set; }

        #region Поля, которые помечены аттрибутом [NotMapped] - не являются столбцами таблицы Фигурантов, но есть в классе
        [NotMapped]
        public string StringCriminalCaseName { get; set; } // описание уголовного дела
        [NotMapped]
        public string StringAddressName { get; set; }// адрес следователя
        [NotMapped]
        public string StringPhoneName { get; set; }// номер телефона
        #endregion
        public virtual Adress IdAddressNavigation { get; set; }
        public virtual Telephone IdPhoneNavigation { get; set; }
        public virtual ICollection<Investigation> Investigations { get; set; }
    }
}
