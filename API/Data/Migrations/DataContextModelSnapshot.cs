﻿#nullable disable

namespace API.Data.Migrations;

[DbContext(typeof(DataContext))]
partial class DataContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.12")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

        modelBuilder.Entity("API.Entities.AppRole", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Name")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("NormalizedName")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.HasKey("Id");

                b.HasIndex("NormalizedName")
                    .IsUnique()
                    .HasDatabaseName("RoleNameIndex")
                    .HasFilter("[NormalizedName] IS NOT NULL");

                b.ToTable("AspNetRoles", (string)null);
            });

        modelBuilder.Entity("API.Entities.AppUser", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<int>("AccessFailedCount")
                    .HasColumnType("int");

                b.Property<string>("City")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Country")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("Created")
                    .HasColumnType("datetime2");

                b.Property<DateOnly>("DateOfBirth")
                    .HasColumnType("date");

                b.Property<string>("Email")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<bool>("EmailConfirmed")
                    .HasColumnType("bit");

                b.Property<string>("Gender")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Interests")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Introduction")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("KnownAs")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("LastActive")
                    .HasColumnType("datetime2");

                b.Property<bool>("LockoutEnabled")
                    .HasColumnType("bit");

                b.Property<DateTimeOffset?>("LockoutEnd")
                    .HasColumnType("datetimeoffset");

                b.Property<string>("LookingFor")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("NormalizedEmail")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("NormalizedUserName")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("PasswordHash")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("PhoneNumber")
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("PhoneNumberConfirmed")
                    .HasColumnType("bit");

                b.Property<string>("SecurityStamp")
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("TwoFactorEnabled")
                    .HasColumnType("bit");

                b.Property<string>("UserName")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.HasKey("Id");

                b.HasIndex("NormalizedEmail")
                    .HasDatabaseName("EmailIndex");

                b.HasIndex("NormalizedUserName")
                    .IsUnique()
                    .HasDatabaseName("UserNameIndex")
                    .HasFilter("[NormalizedUserName] IS NOT NULL");

                b.ToTable("AspNetUsers", (string)null);
            });

        modelBuilder.Entity("API.Entities.AppUserRole", b =>
            {
                b.Property<int>("UserId")
                    .HasColumnType("int");

                b.Property<int>("RoleId")
                    .HasColumnType("int");

                b.HasKey("UserId", "RoleId");

                b.HasIndex("RoleId");

                b.ToTable("AspNetUserRoles", (string)null);
            });

        modelBuilder.Entity("API.Entities.Connection", b =>
            {
                b.Property<string>("ConnectionId")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("GroupName")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("Username")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("ConnectionId");

                b.HasIndex("GroupName");

                b.ToTable("Connections");
            });

        modelBuilder.Entity("API.Entities.Group", b =>
            {
                b.Property<string>("Name")
                    .HasColumnType("nvarchar(450)");

                b.HasKey("Name");

                b.ToTable("Groups");
            });

        modelBuilder.Entity("API.Entities.Message", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("Content")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime?>("DateRead")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("MessageSent")
                    .HasColumnType("datetime2");

                b.Property<bool>("RecipientDeleted")
                    .HasColumnType("bit");

                b.Property<int>("RecipientId")
                    .HasColumnType("int");

                b.Property<string>("RecipientUsername")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("SenderDeleted")
                    .HasColumnType("bit");

                b.Property<int>("SenderId")
                    .HasColumnType("int");

                b.Property<string>("SenderUsername")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.HasIndex("RecipientId");

                b.HasIndex("SenderId");

                b.ToTable("Messages");
            });

        modelBuilder.Entity("API.Entities.Photo", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<int>("AppUserId")
                    .HasColumnType("int");

                b.Property<bool>("IsApproved")
                    .HasColumnType("bit");

                b.Property<bool>("IsMain")
                    .HasColumnType("bit");

                b.Property<string>("PublicId")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Url")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.HasIndex("AppUserId");

                b.ToTable("Photos");
            });

        modelBuilder.Entity("API.Entities.UserLike", b =>
            {
                b.Property<int>("SourceUserId")
                    .HasColumnType("int");

                b.Property<int>("TargetUserId")
                    .HasColumnType("int");

                b.HasKey("SourceUserId", "TargetUserId");

                b.HasIndex("TargetUserId");

                b.ToTable("Likes");
            });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("ClaimType")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ClaimValue")
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("RoleId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("RoleId");

                b.ToTable("AspNetRoleClaims", (string)null);
            });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("ClaimType")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ClaimValue")
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("UserId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserClaims", (string)null);
            });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
            {
                b.Property<string>("LoginProvider")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("ProviderKey")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("ProviderDisplayName")
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("UserId")
                    .HasColumnType("int");

                b.HasKey("LoginProvider", "ProviderKey");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserLogins", (string)null);
            });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
            {
                b.Property<int>("UserId")
                    .HasColumnType("int");

                b.Property<string>("LoginProvider")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("Value")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("UserId", "LoginProvider", "Name");

                b.ToTable("AspNetUserTokens", (string)null);
            });

        modelBuilder.Entity("API.Entities.AppUserRole", b =>
            {
                b.HasOne("API.Entities.AppRole", "Role")
                    .WithMany("UserRoles")
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("API.Entities.AppUser", "User")
                    .WithMany("UserRoles")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Role");

                b.Navigation("User");
            });

        modelBuilder.Entity("API.Entities.Connection", b =>
            {
                b.HasOne("API.Entities.Group", null)
                    .WithMany("Connections")
                    .HasForeignKey("GroupName");
            });

        modelBuilder.Entity("API.Entities.Message", b =>
            {
                b.HasOne("API.Entities.AppUser", "Recipient")
                    .WithMany("MessagesReceived")
                    .HasForeignKey("RecipientId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.HasOne("API.Entities.AppUser", "Sender")
                    .WithMany("MessagesSent")
                    .HasForeignKey("SenderId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.Navigation("Recipient");

                b.Navigation("Sender");
            });

        modelBuilder.Entity("API.Entities.Photo", b =>
            {
                b.HasOne("API.Entities.AppUser", "AppUser")
                    .WithMany("Photos")
                    .HasForeignKey("AppUserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("AppUser");
            });

        modelBuilder.Entity("API.Entities.UserLike", b =>
            {
                b.HasOne("API.Entities.AppUser", "SourceUser")
                    .WithMany("LikedUsers")
                    .HasForeignKey("SourceUserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("API.Entities.AppUser", "TargetUser")
                    .WithMany("LikedByUsers")
                    .HasForeignKey("TargetUserId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                b.Navigation("SourceUser");

                b.Navigation("TargetUser");
            });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
            {
                b.HasOne("API.Entities.AppRole", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
            {
                b.HasOne("API.Entities.AppUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
            {
                b.HasOne("API.Entities.AppUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
            {
                b.HasOne("API.Entities.AppUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("API.Entities.AppRole", b =>
            {
                b.Navigation("UserRoles");
            });

        modelBuilder.Entity("API.Entities.AppUser", b =>
            {
                b.Navigation("LikedByUsers");

                b.Navigation("LikedUsers");

                b.Navigation("MessagesReceived");

                b.Navigation("MessagesSent");

                b.Navigation("Photos");

                b.Navigation("UserRoles");
            });

        modelBuilder.Entity("API.Entities.Group", b =>
            {
                b.Navigation("Connections");
            });
#pragma warning restore 612, 618
    }
}
