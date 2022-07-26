using System;
using System.Collections.Generic;

#nullable disable

namespace CriminalCases
{
    public partial class Telephone
    {
        /// <summary>
        /// Таблица телефонов
        /// </summary>
        public Telephone()
        {
            Investigators = new HashSet<Investigator>();
            FigurantIdPhone1Navigations = new HashSet<Figurant>();
            FigurantIdPhone2Navigations = new HashSet<Figurant>();
            FigurantIdPhone3Navigations = new HashSet<Figurant>();
        }

        public int IdPhone { get; set; }
        public string Phone { get; set; }
        public int? PhoneDec { get; set; }
        public string PhoneType { get; set; }

        public virtual ICollection<Investigator> Investigators { get; set; }
        public virtual ICollection<Figurant> FigurantIdPhone1Navigations { get; set; }
        public virtual ICollection<Figurant> FigurantIdPhone2Navigations { get; set; }
        public virtual ICollection<Figurant> FigurantIdPhone3Navigations { get; set; }
    }
}
