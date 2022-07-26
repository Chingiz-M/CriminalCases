using System;
using System.Collections.Generic;

#nullable disable

namespace CriminalCases
{
    /// <summary>
    /// Таблица адресов
    /// </summary>
    public partial class Adress
    {
        public Adress()
        {
            Investigators = new HashSet<Investigator>();
            FigurantIdAddressLivingNavigations = new HashSet<Figurant>();
            FigurantIdAddressRegistrNavigations = new HashSet<Figurant>();
        }

        public int IdAddress { get; set; }
        public string Address { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Investigator> Investigators { get; set; } // Коллекция следователей с данным id адресом(см. таблицу следователей)
        public virtual ICollection<Figurant> FigurantIdAddressLivingNavigations { get; set; } // Коллекция фигурантов с данным id адресом проживания(см. таблицу фигурантов)
        public virtual ICollection<Figurant> FigurantIdAddressRegistrNavigations { get; set; } //Коллекция фигурантов с данным id адресом регистрации(см. таблицу фигурантов)
    }
}
