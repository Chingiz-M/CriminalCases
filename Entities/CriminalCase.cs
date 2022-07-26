using System;
using System.Collections.Generic;

#nullable disable

namespace CriminalCases
{
    public partial class CriminalCase
    {
        /// <summary>
        /// Таблица Уголовных дел
        /// </summary>
        public CriminalCase()
        {
            Materials = new HashSet<Material>();
            Investigations = new HashSet<Investigation>();
        }

        public int IdCase { get; set; }
        public string NumCase { get; set; } // номер уг. дела
        public DateTime? DateStart { get; set; } // дата начала
        public string Article { get; set; } // описание
        public string Title { get; set; } // заголовок
        public string Remark { get; set; } // примечание

        public virtual ICollection<Material> Materials { get; set; }  // Коллекция материалов дела с данным id уг.дела(см. таблицу материалов)
        public virtual ICollection<Investigation> Investigations { get; set; }//Коллекция расследования с данным id уг.дела(см. таблицу расследования)
    }
}
