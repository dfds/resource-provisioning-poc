﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ResourceProvisioning.Broker.Infrastructure.EntityFramework;

namespace ResourceProvisioning.Broker.Infrastructure.Migrations
{
    [DbContext(typeof(DomainContext))]
    partial class DomainContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("ResourceProvisioning.Abstractions.Grid.GridActorStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(1);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Status","DomainContext");

                    b.HasDiscriminator<string>("Discriminator").HasValue("GridActorStatus");
                });

            modelBuilder.Entity("ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate.EnvironmentResourceReference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EnvironmentId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("EnvironmentRootId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Provisioned")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ResourceId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EnvironmentRootId");

                    b.ToTable("EnvironmentResourceReference","DomainContext");
                });

            modelBuilder.Entity("ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate.EnvironmentRoot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("StatusId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Environment","DomainContext");
                });

            modelBuilder.Entity("ResourceProvisioning.Broker.Domain.Aggregates.ResourceAggregate.ResourceRoot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RegisteredDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("StatusId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Resource","DomainContext");
                });

            modelBuilder.Entity("ResourceProvisioning.Broker.Infrastructure.Idempotency.ClientRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Request","DomainContext");
                });

            modelBuilder.Entity("ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate.EnvironmentStatus", b =>
                {
                    b.HasBaseType("ResourceProvisioning.Abstractions.Grid.GridActorStatus");

                    b.HasDiscriminator().HasValue("EnvironmentStatus");
                });

            modelBuilder.Entity("ResourceProvisioning.Broker.Domain.Aggregates.ResourceAggregate.ResourceStatus", b =>
                {
                    b.HasBaseType("ResourceProvisioning.Abstractions.Grid.GridActorStatus");

                    b.HasDiscriminator().HasValue("ResourceStatus");
                });

            modelBuilder.Entity("ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate.EnvironmentResourceReference", b =>
                {
                    b.HasOne("ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate.EnvironmentRoot", null)
                        .WithMany("Resources")
                        .HasForeignKey("EnvironmentRootId");
                });

            modelBuilder.Entity("ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate.EnvironmentRoot", b =>
                {
                    b.HasOne("ResourceProvisioning.Broker.Domain.Aggregates.EnvironmentAggregate.EnvironmentStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("ResourceProvisioning.Broker.Domain.ValueObjects.DesiredState", "DesiredState", b1 =>
                        {
                            b1.Property<Guid>("EnvironmentRootId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Option1")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Option2")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Option3")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Option4")
                                .HasColumnType("TEXT");

                            b1.HasKey("EnvironmentRootId");

                            b1.ToTable("Environment");

                            b1.WithOwner()
                                .HasForeignKey("EnvironmentRootId");
                        });
                });

            modelBuilder.Entity("ResourceProvisioning.Broker.Domain.Aggregates.ResourceAggregate.ResourceRoot", b =>
                {
                    b.HasOne("ResourceProvisioning.Broker.Domain.Aggregates.ResourceAggregate.ResourceStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("ResourceProvisioning.Broker.Domain.ValueObjects.DesiredState", "DesiredState", b1 =>
                        {
                            b1.Property<Guid>("ResourceRootId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Option1")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Option2")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Option3")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Option4")
                                .HasColumnType("TEXT");

                            b1.HasKey("ResourceRootId");

                            b1.ToTable("Resource");

                            b1.WithOwner()
                                .HasForeignKey("ResourceRootId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}