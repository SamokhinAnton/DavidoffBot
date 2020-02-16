﻿// <auto-generated />
using DavidoffBot.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DavidoffBot.Migrations
{
    [DbContext(typeof(BotContext))]
    partial class BotContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1");

            modelBuilder.Entity("DavidoffBot.Models.BotMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Message")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("DavidoffBot.Models.BotMessageKeyword", b =>
                {
                    b.Property<int>("KeywordId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BotMessageId")
                        .HasColumnType("INTEGER");

                    b.HasKey("KeywordId", "BotMessageId");

                    b.HasIndex("BotMessageId");

                    b.ToTable("BotMessageKeywords");
                });

            modelBuilder.Entity("DavidoffBot.Models.BotMessageUser", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MessageId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "MessageId");

                    b.HasIndex("MessageId");

                    b.ToTable("BotMessageUsers");
                });

            modelBuilder.Entity("DavidoffBot.Models.Keyword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Keyword");
                });

            modelBuilder.Entity("DavidoffBot.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nickname")
                        .HasColumnType("TEXT");

                    b.Property<string>("TelegramId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TelegramId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DavidoffBot.Models.BotMessageKeyword", b =>
                {
                    b.HasOne("DavidoffBot.Models.BotMessage", "BotMessage")
                        .WithMany("BotMessageKeywords")
                        .HasForeignKey("BotMessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DavidoffBot.Models.Keyword", "Keyword")
                        .WithMany("BotMessageKeywords")
                        .HasForeignKey("KeywordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DavidoffBot.Models.BotMessageUser", b =>
                {
                    b.HasOne("DavidoffBot.Models.BotMessage", "Message")
                        .WithMany("BotMessageUsers")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DavidoffBot.Models.User", "User")
                        .WithMany("BotMessageUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
