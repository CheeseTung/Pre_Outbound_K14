using System;
using FPT_Education_IC.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FPT_Education_IC.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CommonMaster> CommonMasters { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Faq> Faqs { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<FptCampus> FptCampuses { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Partner> Partners { get; set; } = null!;
        public virtual DbSet<PartnerContact> PartnerContacts { get; set; } = null!;
        public virtual DbSet<PartnerDocument> PartnerDocuments { get; set; } = null!;
        public virtual DbSet<PartnerHistory> PartnerHistories { get; set; } = null!;
        public virtual DbSet<PaymentLog> PaymentLogs { get; set; } = null!;
        public virtual DbSet<PostComment> PostComments { get; set; } = null!;
        public virtual DbSet<Models.Program> Programs { get; set; } = null!;
        public virtual DbSet<ProgramCooperation> ProgramCooperations { get; set; } = null!;
        public virtual DbSet<ProgramLog> ProgramLogs { get; set; } = null!;
        public virtual DbSet<ProgramManagement> ProgramManagements { get; set; } = null!;
        public virtual DbSet<ProgramPost> ProgramPosts { get; set; } = null!;
        public virtual DbSet<Register> Registers { get; set; } = null!;
        public virtual DbSet<RegisterAnswer> RegisterAnswers { get; set; } = null!;
        public virtual DbSet<RegisterQuestion> RegisterQuestions { get; set; } = null!;
        public virtual DbSet<StudyExchange> StudyExchanges { get; set; } = null!;
        public virtual DbSet<StudyResult> StudyResults { get; set; } = null!;
        public virtual DbSet<UsrAccount> UsrAccounts { get; set; } = null!;
        public virtual DbSet<UsrAccountRole> UsrAccountRoles { get; set; } = null!;
        public virtual DbSet<UsrRolePermission> UsrRolePermissions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Vietnamese_100_CI_AI_KS_WS_SC_UTF8");

            modelBuilder.Entity<CommonMaster>(entity =>
            {
                entity.ToTable("CommonMaster");

                entity.Property(e => e.MasterCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Master_Code")
                    .IsFixedLength();

                entity.Property(e => e.MasterName)
                    .HasMaxLength(255)
                    .HasColumnName("Master_Name");

                entity.Property(e => e.MasterType)
                    .HasMaxLength(10)
                    .HasColumnName("Master_Type")
                    .IsFixedLength();

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.IsoCode);

                entity.ToTable("Country");

                entity.Property(e => e.IsoCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ISO_Code")
                    .IsFixedLength();

                entity.Property(e => e.IsoCode3)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ISO_Code3")
                    .IsFixedLength();

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Faq>(entity =>
            {
                entity.ToTable("FAQ");

                entity.Property(e => e.Type).HasMaxLength(500);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.Feedback1)
                    .HasMaxLength(500)
                    .HasColumnName("Feedback");

                entity.Property(e => e.ProgramId).HasColumnName("Program_Id");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feedback_Program");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feedback_Usr_Account");
            });

            modelBuilder.Entity<FptCampus>(entity =>
            {
                entity.HasKey(e => e.CampusCode);

                entity.ToTable("FPT_Campus");

                entity.Property(e => e.CampusCode).HasColumnName("Campus_Code");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.ReceiverUsr).HasColumnName("Receiver_Usr");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Partner>(entity =>
            {
                entity.ToTable("Partner");

                entity.Property(e => e.PartnerId).HasColumnName("Partner_Id");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Country)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.Property(e => e.Website).HasMaxLength(500);
                entity.Property(e => e.StartDate)
                  .HasColumnType("datetime")
                  .HasColumnName("StartDate");
                entity.Property(e => e.Description).HasColumnType("nvarchar(MAX)");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Partners)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Partner_Country");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Partners)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Partner_Usr_Account");
            });

            modelBuilder.Entity<PartnerContact>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.ToTable("Partner_Contact");

                entity.Property(e => e.ContactId).HasColumnName("Contact_Id");

                entity.Property(e => e.ContactLevel).HasColumnName("Contact_Level");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Facebook).HasMaxLength(500);

                entity.Property(e => e.LinkedIn).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.PartnerId).HasColumnName("Partner_Id");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Twitter).HasMaxLength(500);

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.PartnerContacts)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Partner_Contact_Partner");
            });

            modelBuilder.Entity<PartnerDocument>(entity =>
            {
                entity.HasKey(e => e.DocumentId);

                entity.ToTable("Partner_Document");

                entity.Property(e => e.DocumentId).HasColumnName("Document_Id");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("End_Date");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.PartnerId).HasColumnName("Partner_Id");

                entity.Property(e => e.Path).HasMaxLength(500);

                entity.Property(e => e.ProgramId).HasColumnName("Program_Id");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("Start_Date");

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.PartnerDocuments)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Partner_Document_Partner");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.PartnerDocuments)
                    .HasForeignKey(d => d.ProgramId)
                    .HasConstraintName("FK_Partner_Document_Program");
            });

            modelBuilder.Entity<PartnerHistory>(entity =>
            {
                entity.ToTable("Partner_History");

                entity.Property(e => e.Documents).HasMaxLength(500);

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("End_Date");

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.PartnerId).HasColumnName("Partner_Id");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("Start_Date");

                entity.Property(e => e.Title).HasMaxLength(500);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.PartnerHistories)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Partner_History_Partner");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PartnerHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Partner_History_Usr_Account");
            });

            modelBuilder.Entity<PaymentLog>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("Payment_Log");

                entity.Property(e => e.LogId).HasColumnName("Log_Id");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("date")
                    .HasColumnName("Payment_Date");

                entity.Property(e => e.PaymentValue)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Payment_Value");

                entity.Property(e => e.ProgramId).HasColumnName("Program_Id");

                entity.Property(e => e.RegisterId).HasColumnName("Register_Id");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.PaymentLogs)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_Log_Program");

                entity.HasOne(d => d.Register)
                    .WithMany(p => p.PaymentLogs)
                    .HasForeignKey(d => d.RegisterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_Log_Register");
            });

            modelBuilder.Entity<PostComment>(entity =>
            {
                entity.ToTable("Post_Comments");

                entity.Property(e => e.CommentText).HasMaxLength(500);

                entity.Property(e => e.PostId).HasColumnName("Post_Id");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostComments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_Comments_Program_Posts");
            });

            modelBuilder.Entity<Models.Program>(entity =>
            {
                entity.ToTable("Program");

                entity.Property(e => e.ProgramId).HasColumnName("Program_Id");

                entity.Property(e => e.Categorize)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Country)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("End_date");

                entity.Property(e => e.FaceBookLink)
                    .HasMaxLength(500)
                    .HasColumnName("FaceBook_Link");

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.Participants).HasMaxLength(500);

                entity.Property(e => e.PaymentDescription)
                    .HasMaxLength(500)
                    .HasColumnName("Payment_Description");

                entity.Property(e => e.PaymentEndDate)
                    .HasColumnType("date")
                    .HasColumnName("Payment_EndDate");

                entity.Property(e => e.PaymentStartDate)
                    .HasColumnType("date")
                    .HasColumnName("Payment_StartDate");

                entity.Property(e => e.PaymentValue)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Payment_Value");

                entity.Property(e => e.RegisterEndDate)
                    .HasColumnType("date")
                    .HasColumnName("Register_EndDate");

                entity.Property(e => e.RegisterStartDate)
                    .HasColumnType("date")
                    .HasColumnName("Register_StartDate");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("Start_Date");

                entity.Property(e => e.Title).HasMaxLength(500);

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Programs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Program_Usr_Account");
            });

            modelBuilder.Entity<ProgramCooperation>(entity =>
            {
                entity.HasKey(e => e.CooperationId);

                entity.ToTable("Program_Cooperation");

                entity.Property(e => e.CooperationId).HasColumnName("Cooperation_Id");

                entity.Property(e => e.PartnerContact).HasColumnName("Partner_Contact");

                entity.Property(e => e.PartnerId).HasColumnName("Partner_Id");

                entity.Property(e => e.ProgramId).HasColumnName("Program_Id");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.ProgramCooperations)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Program_Cooperation_Partner");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.ProgramCooperations)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Program_Cooperation_Program");
            });

            modelBuilder.Entity<ProgramLog>(entity =>
            {
                entity.ToTable("Program_Log");

                entity.Property(e => e.LogPath).HasMaxLength(500);

                entity.Property(e => e.ProgramId).HasColumnName("Program_Id");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.ProgramLogs)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Program_Log_Program");
            });

            modelBuilder.Entity<ProgramManagement>(entity =>
            {
                entity.HasKey(e => e.ManageId);

                entity.ToTable("Program_Management");

                entity.Property(e => e.ManageId).HasColumnName("ManageId");

                entity.Property(e => e.ManageRole).HasColumnName("Manage_Role");

                entity.Property(e => e.ProgramId).HasColumnName("Program_Id");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Program)
                    .WithMany()
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Program_Management_Program");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Program_Management_Usr_Account");
            });

            modelBuilder.Entity<ProgramPost>(entity =>
            {
                entity.HasKey(e => e.PotsId);

                entity.ToTable("Program_Posts");

                entity.Property(e => e.PotsId).HasColumnName("Pots_Id");

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.ProgramId).HasColumnName("Program_Id");

                entity.Property(e => e.Title).HasMaxLength(500);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.ProgramPosts)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Program_Posts_Program");
            });

            modelBuilder.Entity<Register>(entity =>
            {
                entity.ToTable("Register");

                entity.Property(e => e.RegisterId).HasColumnName("Register_Id");

                entity.Property(e => e.PartnerId).HasColumnName("Partner_Id");

                entity.Property(e => e.ProgramId).HasColumnName("Program_Id");

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("date")
                    .HasColumnName("Register_Date");

                entity.Property(e => e.RegisterStatus).HasColumnName("Register_Status");

                entity.Property(e => e.RequestCancel)
                    .HasMaxLength(500)
                    .HasColumnName("Request_Cancel");

                entity.Property(e => e.RequestChange)
                    .HasMaxLength(500)
                    .HasColumnName("Request_Change");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.Registers)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Register_Program");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Registers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Register_Usr_Account");
            });

            modelBuilder.Entity<RegisterAnswer>(entity =>
            {
                entity.ToTable("Register_Answer");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.RegisterId).HasColumnName("RegisterID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.RegisterAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Register_Answer_Register_Question");

                entity.HasOne(d => d.Register)
                    .WithMany(p => p.RegisterAnswers)
                    .HasForeignKey(d => d.RegisterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Register_Answer_Register");
            });

            modelBuilder.Entity<RegisterQuestion>(entity =>
            {
                entity.ToTable("Register_Question");

                entity.Property(e => e.IsRequire).HasColumnName("isRequire");

                entity.Property(e => e.ProgramId).HasColumnName("Program_Id");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.RegisterQuestions)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Register_Question_Program");
            });

            modelBuilder.Entity<StudyExchange>(entity =>
            {
                entity.HasKey(e => e.ExchangeId);

                entity.ToTable("StudyExchange");

                entity.Property(e => e.ExchangeId).HasColumnName("Exchange_Id");

                entity.Property(e => e.CourseName)
                    .HasMaxLength(255)
                    .HasColumnName("Course_Name");

                entity.Property(e => e.FptCourse)
                    .HasMaxLength(255)
                    .HasColumnName("FPT_Course");

                entity.Property(e => e.MaxMark)
                    .HasColumnType("decimal(10, 1)")
                    .HasColumnName("Max_Mark");

                entity.Property(e => e.PassMark)
                    .HasColumnType("decimal(10, 1)")
                    .HasColumnName("Pass_Mark");

                entity.Property(e => e.ProgramId).HasColumnName("Program_Id");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.StudyExchanges)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudyExchange_Program");
            });

            modelBuilder.Entity<StudyResult>(entity =>
            {
                entity.ToTable("StudyResult");

                entity.Property(e => e.ConditionReason)
                    .HasMaxLength(255)
                    .HasColumnName("Condition_Reason");

                entity.Property(e => e.ExchangeId).HasColumnName("Exchange_Id");

                entity.Property(e => e.ResultMark)
                    .HasColumnType("decimal(10, 1)")
                    .HasColumnName("Result_Mark");

                entity.Property(e => e.SubjectStatus).HasColumnName("Subject_Status");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Exchange)
                    .WithMany(p => p.StudyResults)
                    .HasForeignKey(d => d.ExchangeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudyResult_StudyExchange");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.StudyResults)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudyResult_Usr_Account");
            });

            modelBuilder.Entity<UsrAccount>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Usr_Account");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Facebook).HasMaxLength(255);

                entity.Property(e => e.IdNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID_Number");

                entity.Property(e => e.IdNumberStDate)
                    .HasColumnType("date")
                    .HasColumnName("ID_Number_StDate");

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.Major)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Passport)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PassportEndDate)
                    .HasColumnType("date")
                    .HasColumnName("Passport_EndDate");

                entity.Property(e => e.PassportStartDate)
                    .HasColumnType("date")
                    .HasColumnName("Passport_StartDate");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RollNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(255);

                entity.HasOne(d => d.CampusNavigation)
                    .WithMany(p => p.UsrAccounts)
                    .HasForeignKey(d => d.Campus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usr_Account_FPT_Campus");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.UsrAccounts)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usr_Account_Usr_Account_Role");
            });

            modelBuilder.Entity<UsrAccountRole>(entity =>
            {
                entity.HasKey(e => e.RoleCode);

                entity.ToTable("Usr_Account_Role");

                entity.Property(e => e.RoleCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Role_Code");

                entity.Property(e => e.RoleName).HasMaxLength(500).HasColumnName("Role_Name"); ;
                entity.Property(e => e.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<UsrRolePermission>(entity =>
            {
                entity.ToTable("Usr_Role_Permission");

                entity.Property(e => e.PermissionCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Permission_Code");

                entity.Property(e => e.Role)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.UsrRolePermissions)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usr_Role_Permission_Usr_Account_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
