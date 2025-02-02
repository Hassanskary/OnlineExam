﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Online_Exam.Models;

#nullable disable

namespace Online_Exam.Migrations
{
    [DbContext(typeof(OnlineExammDbContext))]
    [Migration("20231215221447_Test11")]
    partial class Test11
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Online_Exam.Models.Answers", b =>
                {
                    b.Property<string>("U_Email")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnOrder(2);

                    b.Property<int>("Exam_id")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("Question_id")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<string>("Answer_text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ChoiceIndex")
                        .HasColumnType("int");

                    b.Property<int>("Points_Earned")
                        .HasColumnType("int");

                    b.HasKey("U_Email", "Exam_id", "Question_id");

                    b.HasIndex("Exam_id", "Question_id");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Online_Exam.Models.Choices", b =>
                {
                    b.Property<int>("Exam_id")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("Question_id")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<string>("Choice_text")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnOrder(2);

                    b.Property<bool>("Is_correct")
                        .HasColumnType("bit");

                    b.HasKey("Exam_id", "Question_id", "Choice_text");

                    b.ToTable("Choices");
                });

            modelBuilder.Entity("Online_Exam.Models.Exam", b =>
                {
                    b.Property<int>("Exam_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Exam_id"));

                    b.Property<string>("Adminstration_Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<string>("Exam_title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IS_shuffle_A")
                        .HasColumnType("bit");

                    b.Property<bool>("IS_shuffle_Q")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Start_time")
                        .HasColumnType("datetime2");

                    b.HasKey("Exam_id");

                    b.HasIndex("Adminstration_Email");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("Online_Exam.Models.Questions", b =>
                {
                    b.Property<int>("Exam_id")
                        .HasColumnType("int");

                    b.Property<int>("Question_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Question_id"));

                    b.Property<bool>("Is_required")
                        .HasColumnType("bit");

                    b.Property<decimal>("Points")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Question_title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Question_type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Exam_id", "Question_id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Online_Exam.Models.Users", b =>
                {
                    b.Property<string>("U_Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("U_FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("U_Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("U_PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("U_Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("U_Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Online_Exam.Models.Answers", b =>
                {
                    b.HasOne("Online_Exam.Models.Users", "UEmailNavigation")
                        .WithMany("Answers")
                        .HasForeignKey("U_Email")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Online_Exam.Models.Questions", "Questions")
                        .WithMany("Answers")
                        .HasForeignKey("Exam_id", "Question_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Questions");

                    b.Navigation("UEmailNavigation");
                });

            modelBuilder.Entity("Online_Exam.Models.Choices", b =>
                {
                    b.HasOne("Online_Exam.Models.Questions", "Questions")
                        .WithMany("Choices")
                        .HasForeignKey("Exam_id", "Question_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Online_Exam.Models.Exam", b =>
                {
                    b.HasOne("Online_Exam.Models.Users", "AdminstrationEmailNavigation")
                        .WithMany("Exams")
                        .HasForeignKey("Adminstration_Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdminstrationEmailNavigation");
                });

            modelBuilder.Entity("Online_Exam.Models.Questions", b =>
                {
                    b.HasOne("Online_Exam.Models.Exam", "Exam")
                        .WithMany("Questions")
                        .HasForeignKey("Exam_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");
                });

            modelBuilder.Entity("Online_Exam.Models.Exam", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Online_Exam.Models.Questions", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Choices");
                });

            modelBuilder.Entity("Online_Exam.Models.Users", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Exams");
                });
#pragma warning restore 612, 618
        }
    }
}
