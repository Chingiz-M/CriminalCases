using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CriminalCases
{
    public partial class Figurant
    {
        /// <summary>
        /// Таблица фигурантов
        /// </summary>
        public Figurant()
        {
            Investigations = new HashSet<Investigation>();
        }

        public int IdFigurant { get; set; }
        public int IdPerson { get; set; } // Id персоны из таблицы Персоны
        public string Status { get; set; }
        public int? IdPhone1 { get; set; }
        public int? IdPhone2 { get; set; }
        public int? IdPhone3 { get; set; }
        public int? IdAddressLiving { get; set; } // id адреса из таблицы адресов
        public int? IdAddressRegistr { get; set; }// id адреса из таблицы адресов
        public string Passport { get; set; }
        public string DrivePassport { get; set; }
        public string OtherDocument { get; set; }
        public string ScanDocument { get; set; }

        #region Поля, которые помечены аттрибутом [NotMapped] - не являются столбцами таблицы Фигурантов, но есть в классе
        [NotMapped]
        public DateTime? StringBirthday { get; set; } //дата рождения из таблицы Персона
        [NotMapped]
        public string StringCityzen { get; set; } //страна из таблицы Персона
        [NotMapped]
        public string StringRemark { get; set; }//примечание из таблицы Персона
        [NotMapped]
        public string StringFIO { get; set; }//ФИО из таблицы Персона
        [NotMapped]
        public string StringFoto { get; set; }//ссылка на фото из таблицы Персона
        [NotMapped]
        public string StringAddressLiving { get; set; }//адрес проживания из таблицы Персона
        [NotMapped]
        public string StringAddressRegister { get; set; }//адрес регистрации из таблицы Персона
        [NotMapped]
        public string Phones { get; set; }

        #endregion

        public virtual Adress IdAddressLivingNavigation { get; set; } 
        public virtual Adress IdAddressRegistrNavigation { get; set; }
        public virtual Person IdPersonNavigation { get; set; }
        public virtual Telephone IdPhone1Navigation { get; set; }
        public virtual Telephone IdPhone2Navigation { get; set; }
        public virtual Telephone IdPhone3Navigation { get; set; }
        public virtual ICollection<Investigation> Investigations { get; set; }//Коллекция расследования с данным id фигуранта(см. таблицу расследования)
    }
}
