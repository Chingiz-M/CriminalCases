using System;
using System.Collections.Generic;

#nullable disable

namespace CriminalCases
{
    public partial class Subdivision
    {
        /// <summary>
        /// Таблица Подразделения
        /// </summary>
        public Subdivision()
        {
            Investigations = new HashSet<Investigation>();
        }

        public int IdDivision { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }

        public virtual ICollection<Investigation> Investigations { get; set; }
    }
}
