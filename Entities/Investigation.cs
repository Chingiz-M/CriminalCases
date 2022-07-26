using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CriminalCases
{
    public partial class Investigation
    {
        /// <summary>
        /// Таблица расследования
        /// </summary>
        public int IdInvestig { get; set; }
        public int IdCase { get; set; }
        public int IdFigurant { get; set; }
        public int IdOfficer { get; set; }
        public DateTime? DateEvent { get; set; }
        public int? IdDivision { get; set; }
        #region Поля, которые помечены аттрибутом [NotMapped] - не являются столбцами таблицы Расследования, но есть в классе
        [NotMapped]
        public string StringFigurantName { get; set; } // ФИО фигуранта из таблицы Пероны
        [NotMapped]
        public string StringOfficerName { get; set; }// ФИО следователя из таблицы Следователь
        [NotMapped]
        public string StringdivisionName { get; set; }// название подразделения из таблицы Подразделения
        [NotMapped]
        public string StringFigurantStatus { get; set; }// статус фигуранта из таблицы Фигуранты
        #endregion
        public virtual CriminalCase IdCaseNavigation { get; set; }
        public virtual Subdivision IdDivisionNavigation { get; set; }
        public virtual Figurant IdFigurantNavigation { get; set; }
        public virtual Investigator IdOfficerNavigation { get; set; }
    }
}
