using System;
using System.Collections.Generic;

#nullable disable

namespace CriminalCases
{
    public partial class Person
    {
        /// <summary>
        /// Таблица Персон
        /// </summary>
        public Person()
        {
            Figurants = new HashSet<Figurant>();
        }

        public int IdPerson { get; set; }
        public string Fio { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Citizen { get; set; }
        public string Foto { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<Figurant> Figurants { get; set; }
    }
}
