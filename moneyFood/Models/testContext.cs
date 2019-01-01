using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace moneyFood.Models
{
    public partial class testContext : DbContext
    {
        public testContext()
        {
        }

        public testContext(DbContextOptions<testContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Component> Component { get; set; }
        public virtual DbSet<EnrollPaid> EnrollPaid { get; set; }

        public virtual DbSet<EnrollStudentClass> EnrollStudentClass { get; set; }
        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<Monthly> Monthly { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<EnrollPay> EnrollPay { get; set; }

        // Unable to generate entity type for table 'food.ENROLL_STUDENT_CLASS'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-C686SHK\SQLEXPRESS01;Database=test;Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("CLASS", "food");

                entity.Property(e => e.ClassId)
                    .HasColumnName("CLASS_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClassNameId)
                    .HasColumnName("CLASS_NAME_ID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ClassRoom).HasColumnName("CLASS_ROOM");

                entity.Property(e => e.ClassYearIndex).HasColumnName("CLASS_YEAR_INDEX");

                entity.Property(e => e.Status)
                    .HasColumnName("STATUS")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherId).HasColumnName("TEACHER_ID");
            });

            modelBuilder.Entity<Component>(entity =>
            {
                entity.ToTable("COMPONENT", "food");

                entity.Property(e => e.ComponentId)
                    .HasColumnName("COMPONENT_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ComponentDes)
                    .HasColumnName("COMPONENT_DES")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ComponentName)
                    .HasColumnName("COMPONENT_NAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ComponentType)
                    .HasColumnName("COMPONENT_TYPE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ComponentValue)
                    .HasColumnName("COMPONENT_VALUE")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EnrollPaid>(entity =>
            {
                entity.HasKey(e => e.PaidId);

                entity.ToTable("ENROLL_PAID", "food");

                entity.Property(e => e.PaidId)
                    .HasColumnName("PAID_ID");

                entity.Property(e => e.MonthlyId).HasColumnName("MONTHLY_ID");
                entity.Property(e => e.PayId).HasColumnName("PAY_ID");
                entity.Property(e => e.Paid).HasColumnName("PAID");

                entity.Property(e => e.StaffPaidId).HasColumnName("STAFF_PAID_ID");

                entity.Property(e => e.StudentId).HasColumnName("STUDENT_ID");
               // entity.Property(e=>e.enroll)
                entity.Property(e => e.StudentTypeId).HasColumnName("STUDENT_TYPE_ID");

                entity.Property(e => e.Timestamp).HasColumnName("TIMESTAMP");
                entity.Property(e => e.ClassId).HasColumnName("CLASS_ID");
            });
            modelBuilder.Entity<EnrollStudentClass>(entity =>
            {
                entity.HasKey(e => e.EnrolStuClassId);

                entity.ToTable("ENROLL_STUDENT_CLASS", "food");

                entity.Property(e => e.EnrolStuClassId)
                    .HasColumnName("ENROL_STU_CLASS_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClassId).HasColumnName("CLASS_ID");

                entity.Property(e => e.StudentId).HasColumnName("STUDENT_ID");
            });
            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("FOOD", "food");

                entity.Property(e => e.FoodId).HasColumnName("FOOD_ID");

                entity.Property(e => e.FoodClass)
                    .HasColumnName("FOOD_CLASS")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.FoodFullPaid).HasColumnName("FOOD_FULL_PAID");

                entity.Property(e => e.FoodIdStudent)
                    .HasColumnName("FOOD_ID_STUDENT")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FoodLastname)
                    .HasColumnName("FOOD_LASTNAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FoodMonthIndex)
                    .HasColumnName("FOOD_MONTH_INDEX")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.FoodMonthTh)
                    .HasColumnName("FOOD_MONTH_TH")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FoodName)
                    .HasColumnName("FOOD_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FoodPaid).HasColumnName("FOOD_PAID");

                entity.Property(e => e.FoodPaidId).HasColumnName("FOOD_PAID_ID");

                entity.Property(e => e.FoodTimesatamp)
                    .HasColumnName("FOOD_TIMESATAMP")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FoodTitle)
                    .HasColumnName("FOOD_TITLE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FoodUpdate)
                    .HasColumnName("FOOD_UPDATE")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FoodYear)
                    .HasColumnName("FOOD_YEAR")
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Monthly>(entity =>
            {
                entity.ToTable("MONTHLY", "food");

                entity.Property(e => e.MonthlyId)
                    .HasColumnName("MONTHLY_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.MonthCourse).HasColumnName("MONTH_COURSE");

                entity.Property(e => e.MonthName)
                    .HasColumnName("MONTH_NAME")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.MonthStatus).HasColumnName("MONTH_STATUS");

                entity.Property(e => e.MonthYear).HasColumnName("MONTH_YEAR");

                entity.Property(e => e.MonthYearindex).HasColumnName("MONTH_YEARINDEX");
            });
            modelBuilder.Entity<EnrollPay>(entity =>
            {
                entity.HasKey(e => e.PayId);

                entity.ToTable("ENROLL_PAY", "food");

                entity.Property(e => e.PayId).HasColumnName("PAY_ID");

                entity.Property(e => e.PaidId).HasColumnName("PAID_ID");

                entity.Property(e => e.Pay).HasColumnName("PAY");

                entity.Property(e => e.StaffId).HasColumnName("STAFF_ID");

                entity.Property(e => e.StudentId).HasColumnName("STUDENT_ID");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("TIMESTAMP")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.HasKey(e => e.StudentId);

                entity.ToTable("STUDENTS", "food");

                entity.Property(e => e.StudentId)
                    .HasColumnName("STUDENT_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.StudentBirthday)
                    .HasColumnName("STUDENT_BIRTHDAY")
                    .HasColumnType("datetime");

                entity.Property(e => e.StudentIdcard)
                    .HasColumnName("STUDENT_IDCARD")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.StudentLname)
                    .HasColumnName("STUDENT_LNAME")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StudentName)
                    .HasColumnName("STUDENT_NAME")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StudentPsisId).HasColumnName("STUDENT_PSIS_ID");

                entity.Property(e => e.StudentStatus)
                    .HasColumnName("STUDENT_STATUS")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.StudentTimestamp)
                    .HasColumnName("STUDENT_TIMESTAMP")
                    .HasColumnType("datetime");

                entity.Property(e => e.StudentTitle)
                    .HasColumnName("STUDENT_TITLE")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });
        }
    }
}
