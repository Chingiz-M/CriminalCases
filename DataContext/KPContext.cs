using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CriminalCases
{
    /// <summary>
    /// Контекст данных для БД "KP"
    /// </summary>
    public partial class KPContext : DbContext
    {
        public KPContext()
        {
        }

        public KPContext(DbContextOptions options)
            : base(options)
        {
        }
        #region Определение таблиц для работы с БД "KP"
        public virtual DbSet<Adress> Addresses { get; set; }
        public virtual DbSet<Bancnote> Bancnotes { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Subdivision> Subdivisions { get; set; }
        public virtual DbSet<Investigation> Investigations { get; set; }
        public virtual DbSet<Investigator> Investigators { get; set; }
        public virtual DbSet<Telephone> Telephones { get; set; }
        public virtual DbSet<CriminalCase> CriminalCases { get; set; }
        public virtual DbSet<Figurant> Figurants { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Adress>(entity =>
            {
                entity.HasKey(e => e.IdAddress)
                    .HasName("Адреса_РђРґСЂРµСЃР°_pkey")
                    .IsClustered(false);

                entity.ToTable("Адреса");

                entity.Property(e => e.IdAddress).HasColumnName("id_address");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.Country)
                    .HasMaxLength(20)
                    .HasColumnName("country");

                entity.Property(e => e.Region)
                    .HasMaxLength(20)
                    .HasColumnName("region");
            });

            modelBuilder.Entity<Bancnote>(entity =>
            {
                entity.HasKey(e => e.IdBanknotes)
                    .HasName("Банкноты_Р‘Р°РЅРєРЅРѕС‚С‹_pkey")
                    .IsClustered(false);

                entity.ToTable("Банкноты");

                entity.Property(e => e.IdBanknotes).HasColumnName("id_banknotes");

                entity.Property(e => e.BiletBankRus).HasColumnName("bilet_bank_rus");

                entity.Property(e => e.DigitalNominal).HasColumnName("digital_nominal");

                entity.Property(e => e.EffectTransDigits).HasColumnName("effect_trans_digits");

                entity.Property(e => e.Emboss).HasColumnName("emboss");

                entity.Property(e => e.ImpairedVision).HasColumnName("impaired_vision");

                entity.Property(e => e.MicroPattren).HasColumnName("micro_pattren");

                entity.Property(e => e.MicroTxtGrid).HasColumnName("micro_txt_grid");

                entity.Property(e => e.MicroTxtWithTransition).HasColumnName("micro_txt_with_transition");

                entity.Property(e => e.Microperf).HasColumnName("microperf");

                entity.Property(e => e.MircoTxt).HasColumnName("mirco_txt");

                entity.Property(e => e.MircoTxtTape).HasColumnName("mirco_txt_tape");

                entity.Property(e => e.MoireStripes).HasColumnName("moire_stripes");

                entity.Property(e => e.Nominal).HasColumnName("nominal");

                entity.Property(e => e.NumBanknotes)
                    .HasMaxLength(10)
                    .HasColumnName("num_banknotes");

                entity.Property(e => e.OpticalPaint).HasColumnName("optical_paint");

                entity.Property(e => e.Pp).HasColumnName("pp");

                entity.Property(e => e.Remark)
                    .HasMaxLength(2000)
                    .HasColumnName("remark");

                entity.Property(e => e.SmallItems)
                    .HasMaxLength(20)
                    .HasColumnName("small_items");

                entity.Property(e => e.SubtleTouches).HasColumnName("subtle_touches");

                entity.Property(e => e.TxtNominal).HasColumnName("txt_nominal");

                entity.Property(e => e.Watermark).HasColumnName("watermark");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.IdDocs)
                    .HasName("Материалы дела_РњР°С‚РµСЂРёР°Р»С‹_РґРµР»Р°_pkey")
                    .IsClustered(false);

                entity.ToTable("Материалы дела");

                entity.Property(e => e.IdDocs).HasColumnName("id_docs");

                entity.Property(e => e.FilesDoc)
                    .HasMaxLength(255)
                    .HasColumnName("files_doc");

                entity.Property(e => e.IdBanknotes).HasColumnName("id_banknotes");

                entity.Property(e => e.IdCase).HasColumnName("id_case");

                entity.Property(e => e.NameDoc)
                    .HasMaxLength(50)
                    .HasColumnName("name_doc");

                entity.Property(e => e.TypeDoc)
                    .HasMaxLength(10)
                    .HasColumnName("type_doc");

                entity.HasOne(d => d.IdBanknotesNavigation)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.IdBanknotes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Материалы дела_Банкноты");

                entity.HasOne(d => d.IdCaseNavigation)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.IdCase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Материалы дела_Уголовное дело");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.IdPerson)
                    .HasName("Персона_РџРµСЂСЃРѕРЅР°_pkey")
                    .IsClustered(false);

                entity.ToTable("Персона");

                entity.Property(e => e.IdPerson).HasColumnName("id_person");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birth_date");

                entity.Property(e => e.Citizen)
                    .HasMaxLength(10)
                    .HasColumnName("citizen");

                entity.Property(e => e.Fio)
                    .HasMaxLength(60)
                    .HasColumnName("FIO");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .HasColumnName("First_name");

                entity.Property(e => e.Foto)
                    .HasMaxLength(255)
                    .HasColumnName("foto");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(20)
                    .HasColumnName("Middle_name");

                entity.Property(e => e.Remark)
                    .HasMaxLength(2000)
                    .HasColumnName("remark");

                entity.Property(e => e.SecondName)
                    .HasMaxLength(20)
                    .HasColumnName("Second_name");
            });

            modelBuilder.Entity<Subdivision>(entity =>
            {
                entity.HasKey(e => e.IdDivision)
                    .HasName("Подразделение_РџРѕРґСЂР°Р·РґРµР»РµРЅРёРµ_pkey")
                    .IsClustered(false);

                entity.ToTable("Подразделение");

                entity.Property(e => e.IdDivision).HasColumnName("id_division");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Region)
                    .HasMaxLength(20)
                    .HasColumnName("region");
            });

            modelBuilder.Entity<Investigation>(entity =>
            {
                entity.HasKey(e => e.IdInvestig)
                    .HasName("Расследование_Р Р°СЃСЃР»РµРґРѕРІР°РЅРёРµ_pkey")
                    .IsClustered(false);

                entity.ToTable("Расследование");

                entity.Property(e => e.IdInvestig).HasColumnName("id_investig");

                entity.Property(e => e.DateEvent)
                    .HasColumnType("datetime")
                    .HasColumnName("date_event");

                entity.Property(e => e.IdCase).HasColumnName("id_case");

                entity.Property(e => e.IdDivision).HasColumnName("id_division");

                entity.Property(e => e.IdFigurant).HasColumnName("id_figurant");

                entity.Property(e => e.IdOfficer).HasColumnName("id_officer");

                entity.HasOne(d => d.IdCaseNavigation)
                    .WithMany(p => p.Investigations)
                    .HasForeignKey(d => d.IdCase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Расследование_Уголовное дело");

                entity.HasOne(d => d.IdDivisionNavigation)
                    .WithMany(p => p.Investigations)
                    .HasForeignKey(d => d.IdDivision)
                    .HasConstraintName("FK_Расследование_Подразделение");

                entity.HasOne(d => d.IdFigurantNavigation)
                    .WithMany(p => p.Investigations)
                    .HasForeignKey(d => d.IdFigurant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Расследование_Фигурант");

                entity.HasOne(d => d.IdOfficerNavigation)
                    .WithMany(p => p.Investigations)
                    .HasForeignKey(d => d.IdOfficer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Расследование_Следователь");
            });

            modelBuilder.Entity<Investigator>(entity =>
            {
                entity.HasKey(e => e.IdOfficer)
                    .HasName("Следователь_РЎР»РµРґРѕРІР°С‚РµР»СЊ_pkey")
                    .IsClustered(false);

                entity.ToTable("Следователь");

                entity.Property(e => e.IdOfficer).HasColumnName("id_officer");

                entity.Property(e => e.Fio)
                    .HasMaxLength(60)
                    .HasColumnName("FIO");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .HasColumnName("First_name");

                entity.Property(e => e.IdAddress).HasColumnName("id_address");

                entity.Property(e => e.IdPhone).HasColumnName("id_phone");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(20)
                    .HasColumnName("Middle_name");

                entity.Property(e => e.SecondName)
                    .HasMaxLength(20)
                    .HasColumnName("Second_name");

                entity.HasOne(d => d.IdAddressNavigation)
                    .WithMany(p => p.Investigators)
                    .HasForeignKey(d => d.IdAddress)
                    .HasConstraintName("FK_Следователь_Адреса");

                entity.HasOne(d => d.IdPhoneNavigation)
                    .WithMany(p => p.Investigators)
                    .HasForeignKey(d => d.IdPhone)
                    .HasConstraintName("FK_Следователь_Телефоны");
            });

            modelBuilder.Entity<Telephone>(entity =>
            {
                entity.HasKey(e => e.IdPhone)
                    .HasName("Телефоны_РўРµР»РµС„РѕРЅС‹_pkey")
                    .IsClustered(false);

                entity.ToTable("Телефоны");

                entity.Property(e => e.IdPhone).HasColumnName("id_phone");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .HasColumnName("phone");

                entity.Property(e => e.PhoneDec).HasColumnName("phone_dec");

                entity.Property(e => e.PhoneType)
                    .HasMaxLength(10)
                    .HasColumnName("phone_type");
            });

            modelBuilder.Entity<CriminalCase>(entity =>
            {
                entity.HasKey(e => e.IdCase)
                    .HasName("Уголовное дело_РЈРіРѕР»РѕРІРЅРѕРµ_РґРµР»Рѕ_pkey")
                    .IsClustered(false);

                entity.ToTable("Уголовное дело");

                entity.Property(e => e.IdCase).HasColumnName("id_case");

                entity.Property(e => e.Article)
                    .HasMaxLength(50)
                    .HasColumnName("article");

                entity.Property(e => e.DateStart)
                    .HasColumnType("datetime")
                    .HasColumnName("date_start");

                entity.Property(e => e.NumCase)
                    .HasMaxLength(15)
                    .HasColumnName("num_case");

                entity.Property(e => e.Remark)
                    .HasColumnType("ntext")
                    .HasColumnName("remark");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Figurant>(entity =>
            {
                entity.HasKey(e => e.IdFigurant)
                    .HasName("Фигурант_Р¤РёРіСѓСЂР°РЅС‚_pkey")
                    .IsClustered(false);

                entity.ToTable("Фигурант");

                entity.Property(e => e.IdFigurant).HasColumnName("id_figurant");

                entity.Property(e => e.DrivePassport)
                    .HasMaxLength(50)
                    .HasColumnName("drive_passport");

                entity.Property(e => e.IdAddressLiving).HasColumnName("id_address_living");

                entity.Property(e => e.IdAddressRegistr).HasColumnName("id_address_registr");

                entity.Property(e => e.IdPerson).HasColumnName("id_person");

                entity.Property(e => e.IdPhone1).HasColumnName("id_phone1");

                entity.Property(e => e.IdPhone2).HasColumnName("id_phone2");

                entity.Property(e => e.IdPhone3).HasColumnName("id_phone3");

                entity.Property(e => e.OtherDocument)
                    .HasMaxLength(50)
                    .HasColumnName("other_document");

                entity.Property(e => e.Passport)
                    .HasMaxLength(50)
                    .HasColumnName("passport");

                entity.Property(e => e.ScanDocument)
                    .HasMaxLength(255)
                    .HasColumnName("scan_document");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status");

                entity.HasOne(d => d.IdAddressLivingNavigation)
                    .WithMany(p => p.FigurantIdAddressLivingNavigations)
                    .HasForeignKey(d => d.IdAddressLiving)
                    .HasConstraintName("FK_Фигурант_Адреса");

                entity.HasOne(d => d.IdAddressRegistrNavigation)
                    .WithMany(p => p.FigurantIdAddressRegistrNavigations)
                    .HasForeignKey(d => d.IdAddressRegistr)
                    .HasConstraintName("FK_Фигурант_Адреса1");

                entity.HasOne(d => d.IdPersonNavigation)
                    .WithMany(p => p.Figurants)
                    .HasForeignKey(d => d.IdPerson)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Фигурант_Персона");

                entity.HasOne(d => d.IdPhone1Navigation)
                    .WithMany(p => p.FigurantIdPhone1Navigations)
                    .HasForeignKey(d => d.IdPhone1)
                    .HasConstraintName("FK_Фигурант_Телефоны");

                entity.HasOne(d => d.IdPhone2Navigation)
                    .WithMany(p => p.FigurantIdPhone2Navigations)
                    .HasForeignKey(d => d.IdPhone2)
                    .HasConstraintName("FK_Фигурант_Телефоны1");

                entity.HasOne(d => d.IdPhone3Navigation)
                    .WithMany(p => p.FigurantIdPhone3Navigations)
                    .HasForeignKey(d => d.IdPhone3)
                    .HasConstraintName("FK_Фигурант_Телефоны2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
