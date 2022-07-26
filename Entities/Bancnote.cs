using System;
using System.Collections.Generic;

#nullable disable

namespace CriminalCases
{
    public partial class Bancnote
    {
        /// <summary>
        /// Таблица банкнот
        /// </summary>
        public Bancnote()
        {
            Materials = new HashSet<Material>();
        }

        public int IdBanknotes { get; set; }
        public string NumBanknotes { get; set; }
        public int? Nominal { get; set; }
        public bool? Watermark { get; set; }
        public bool? Microperf { get; set; }
        public bool? MicroPattren { get; set; }
        public string SmallItems { get; set; }
        public bool? MircoTxt { get; set; }
        public bool? MicroTxtWithTransition { get; set; }
        public bool? MircoTxtTape { get; set; }
        public bool? MicroTxtGrid { get; set; }
        public bool? Emboss { get; set; }
        public bool? EffectTransDigits { get; set; }
        public bool? Pp { get; set; }
        public bool? OpticalPaint { get; set; }
        public bool? MoireStripes { get; set; }
        public bool? BiletBankRus { get; set; }
        public bool? ImpairedVision { get; set; }
        public bool? DigitalNominal { get; set; }
        public bool? TxtNominal { get; set; }
        public bool? SubtleTouches { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<Material> Materials { get; set; }
    }
}
