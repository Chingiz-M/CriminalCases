using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CriminalCases
{
    public partial class Material
    {
        /// <summary>
        /// Таблица материалов
        /// </summary>
        public int IdDocs { get; set; }
        public int IdCase { get; set; }
        public int IdBanknotes { get; set; }
        public string NameDoc { get; set; }
        public string TypeDoc { get; set; }
        public string FilesDoc { get; set; }
        [NotMapped]
        public string StringBancnoteName { get; set; } // номинал банкноты
        public virtual Bancnote IdBanknotesNavigation { get; set; }
        public virtual CriminalCase IdCaseNavigation { get; set; }
    }
}
