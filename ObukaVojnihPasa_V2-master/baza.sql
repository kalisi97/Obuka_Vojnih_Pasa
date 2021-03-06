USE [master]
GO
/****** Object:  Database [DatabaseVojniPsiProba]    Script Date: 4/2/2020 12:48:07 AM ******/
CREATE DATABASE [DatabaseVojniPsiProba]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DatabaseVojniPsiProba', FILENAME = N'C:\Users\Kaca\DatabaseVojniPsiProba.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DatabaseVojniPsiProba_log', FILENAME = N'C:\Users\Kaca\DatabaseVojniPsiProba_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DatabaseVojniPsiProba].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET ARITHABORT OFF 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET  MULTI_USER 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET QUERY_STORE = OFF
GO
USE [DatabaseVojniPsiProba]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [DatabaseVojniPsiProba]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 4/2/2020 12:48:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Angazovanje]    Script Date: 4/2/2020 12:48:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Angazovanje](
	[PasId] [int] NOT NULL,
	[ZadatakId] [int] NOT NULL,
	[DatumUnosaOcene] [datetime2](7) NULL,
	[Ocena] [int] NULL,
 CONSTRAINT [PK_Angazovanje] PRIMARY KEY CLUSTERED 
(
	[PasId] ASC,
	[ZadatakId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 4/2/2020 12:48:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 4/2/2020 12:48:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 4/2/2020 12:48:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 4/2/2020 12:48:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 4/2/2020 12:48:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 4/2/2020 12:48:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[Discriminator] [nvarchar](max) NOT NULL,
	[Cin] [nvarchar](max) NULL,
	[ImePrezime] [nvarchar](max) NULL,
	[ObukaId] [int] NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 4/2/2020 12:48:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Obuka]    Script Date: 4/2/2020 12:48:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Obuka](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [nvarchar](max) NULL,
	[Opis] [nvarchar](max) NULL,
	[Trajanje] [int] NOT NULL,
 CONSTRAINT [PK_Obuka] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pas]    Script Date: 4/2/2020 12:48:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BrojZdravstveneKnjizice] [nvarchar](max) NOT NULL,
	[Ime] [nvarchar](max) NULL,
	[DatumRodjenja] [datetime2](7) NOT NULL,
	[Rasa] [nvarchar](max) NULL,
	[Pol] [nvarchar](max) NULL,
	[ObukaId] [int] NOT NULL,
 CONSTRAINT [PK_Pas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Zadatak]    Script Date: 4/2/2020 12:48:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Zadatak](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NazivZadatka] [nvarchar](max) NULL,
	[Datum] [datetime2](7) NOT NULL,
	[Teren] [nvarchar](max) NULL,
 CONSTRAINT [PK_Zadatak] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200329200948_Initial', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200329201311_Init2', N'3.1.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200330201336_Migration2', N'3.1.3')
INSERT [dbo].[Angazovanje] ([PasId], [ZadatakId], [DatumUnosaOcene], [Ocena]) VALUES (10, 1, CAST(N'2019-10-12T00:00:00.0000000' AS DateTime2), 5)
INSERT [dbo].[Angazovanje] ([PasId], [ZadatakId], [DatumUnosaOcene], [Ocena]) VALUES (10, 6, CAST(N'2020-03-28T00:00:00.0000000' AS DateTime2), 5)
INSERT [dbo].[Angazovanje] ([PasId], [ZadatakId], [DatumUnosaOcene], [Ocena]) VALUES (11, 6, CAST(N'2020-03-28T00:00:00.0000000' AS DateTime2), 8)
INSERT [dbo].[Angazovanje] ([PasId], [ZadatakId], [DatumUnosaOcene], [Ocena]) VALUES (14, 4, CAST(N'2020-01-03T00:00:00.0000000' AS DateTime2), 10)
INSERT [dbo].[Angazovanje] ([PasId], [ZadatakId], [DatumUnosaOcene], [Ocena]) VALUES (14, 6, CAST(N'2020-03-28T00:00:00.0000000' AS DateTime2), 10)
INSERT [dbo].[Angazovanje] ([PasId], [ZadatakId], [DatumUnosaOcene], [Ocena]) VALUES (15, 2, CAST(N'2019-11-12T00:00:00.0000000' AS DateTime2), 10)
INSERT [dbo].[Angazovanje] ([PasId], [ZadatakId], [DatumUnosaOcene], [Ocena]) VALUES (15, 4, CAST(N'2020-01-03T00:00:00.0000000' AS DateTime2), 10)
INSERT [dbo].[Angazovanje] ([PasId], [ZadatakId], [DatumUnosaOcene], [Ocena]) VALUES (17, 1, CAST(N'2019-10-12T00:00:00.0000000' AS DateTime2), 7)
INSERT [dbo].[Angazovanje] ([PasId], [ZadatakId], [DatumUnosaOcene], [Ocena]) VALUES (18, 4, CAST(N'2020-01-03T00:00:00.0000000' AS DateTime2), 9)
INSERT [dbo].[Angazovanje] ([PasId], [ZadatakId], [DatumUnosaOcene], [Ocena]) VALUES (18, 6, CAST(N'2020-03-26T00:00:00.0000000' AS DateTime2), 10)
INSERT [dbo].[Angazovanje] ([PasId], [ZadatakId], [DatumUnosaOcene], [Ocena]) VALUES (19, 2, CAST(N'2019-11-12T00:00:00.0000000' AS DateTime2), 5)
INSERT [dbo].[Angazovanje] ([PasId], [ZadatakId], [DatumUnosaOcene], [Ocena]) VALUES (21, 1, CAST(N'2019-10-12T00:00:00.0000000' AS DateTime2), 10)
INSERT [dbo].[Angazovanje] ([PasId], [ZadatakId], [DatumUnosaOcene], [Ocena]) VALUES (21, 2, CAST(N'2019-11-12T00:00:00.0000000' AS DateTime2), 8)
INSERT [dbo].[Angazovanje] ([PasId], [ZadatakId], [DatumUnosaOcene], [Ocena]) VALUES (26, 4, CAST(N'2020-10-10T00:00:00.0000000' AS DateTime2), 6)
INSERT [dbo].[Angazovanje] ([PasId], [ZadatakId], [DatumUnosaOcene], [Ocena]) VALUES (30, 2, CAST(N'2020-10-10T00:00:00.0000000' AS DateTime2), 5)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'69cdb75e-cd87-4bc9-8dc2-63818aeea315', N'Admin', N'ADMIN', N'bc278a72-cc8d-4943-b67d-35e81985187c')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'd70206a2-02d0-4f89-a79f-5f25e883c9d3', N'Korisnik', N'KORISNIK', N'2813413f-1e45-49d2-97e9-e9ed7a82b30d')
SET IDENTITY_INSERT [dbo].[AspNetUserClaims] ON 

INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (1, N'62f211a9-3747-4784-9d74-e9b92b7cfdfa', N'Upravljanje angazovanjima', N'true')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (2, N'62f211a9-3747-4784-9d74-e9b92b7cfdfa', N'Izmena i brisanje podataka o psu', N'true')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (3, N'62f211a9-3747-4784-9d74-e9b92b7cfdfa', N'Unos zadatka', N'true')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (4, N'62f211a9-3747-4784-9d74-e9b92b7cfdfa', N'Unos angazovanja', N'true')
SET IDENTITY_INSERT [dbo].[AspNetUserClaims] OFF
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'62f211a9-3747-4784-9d74-e9b92b7cfdfa', N'69cdb75e-cd87-4bc9-8dc2-63818aeea315')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Discriminator], [Cin], [ImePrezime], [ObukaId]) VALUES (N'1da79b2b-ef05-441c-8aa6-0cc6818389f2', N'i3', N'I3', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEJABxOFNn8lNea6sK7stmyirzDLKVwKRU371Udcpt/MnG02GeTmQBHrISnzchcuRug==', N'YMSC4ED5M7WZ25ZO55IRYWXV7YWFEKMV', N'cf2efb8f-c0e6-4078-a723-28b3afa5c2fc', NULL, 0, 0, NULL, 1, 0, N'Instruktor', N'Vodnik', N'Pera Peric', 3)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Discriminator], [Cin], [ImePrezime], [ObukaId]) VALUES (N'62f211a9-3747-4784-9d74-e9b92b7cfdfa', N'admin', N'ADMIN', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEJnqYQgexo0b7PgltuZZMFUQdN984oE7fnJsyR/RGqnS3mJWkTOdE1j8k9kB24QvMw==', N'ABHWF7WMQL4SSRBQNAZPUYS4DJ3PK3IL', N'3ad68f53-5f07-4f99-9b52-71dd26f94629', NULL, 0, 0, NULL, 1, 0, N'Instruktor', N'Kapetan', N'Nikola Simic', 2)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Discriminator], [Cin], [ImePrezime], [ObukaId]) VALUES (N'86a46f9a-4575-43f7-a8d4-1ebd0a0ccac5', N'i4', N'I4', NULL, NULL, 0, N'AQAAAAEAACcQAAAAENSnU7MDVnt2DaEinnICH5yhgxUG60piHJtUjH7C632B0OFFpw3iR8xwjZ/2uZhOgQ==', N'THWLMBKZ2544X2SQHUG2RLHVRLRCTNNI', N'83856f51-2287-4ea9-acb3-e292057b86eb', NULL, 0, 0, NULL, 1, 0, N'Instruktor', N'Porucnik', N'Bratimir Simic', 4)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Discriminator], [Cin], [ImePrezime], [ObukaId]) VALUES (N'c40838d5-ec35-4e6b-8e06-9e2de7baedb9', N'i2', N'I2', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEGQpLWHQHOIFo4hoY7HHwGpGz0EV0sx9GR/TsW6yqK5W+ta4BUAEmCfUgxDbPj3+jA==', N'Z7BN2TEVUJUFJO6H3VFMASELEUYOHHFH', N'f7f65085-9be7-487c-a67f-f137d06fa762', NULL, 0, 0, NULL, 1, 0, N'Instruktor', N'Zastavik', N'Mika Mikic', 4)
SET IDENTITY_INSERT [dbo].[Obuka] ON 

INSERT [dbo].[Obuka] ([Id], [Naziv], [Opis], [Trajanje]) VALUES (1, N'Tragačka služba', N'Upotreba pasa za otkrivanje i praćenje tragova mirisa predstavlja najstariju vrstu upotrebe ovih životinja. Prema saznanjima moderne odorologije i kinologije, čulo mirisa psa je višestruko istančanije nego kod ljudi, a samo osećanje mirisa zavisi od njegove granične koncentracije koja se određuje brojem molekula mirisa u jedinici zapremine.', 5)
INSERT [dbo].[Obuka] ([Id], [Naziv], [Opis], [Trajanje]) VALUES (2, N'Zaštitna služba', N'Ovu službu čine psi koji se dresiraju za potrebe izvođenja hapšenja agresivnih lica kao i zaštite ovlašćenih službenih lica prilikom obavljanja patrolne delatnosti ili drugih intervencija. Drugim rečima, zaštitni pas može se koristiti i za potrebe napada i za potrebe odbrane.', 6)
INSERT [dbo].[Obuka] ([Id], [Naziv], [Opis], [Trajanje]) VALUES (3, N'Čuvarska služba', N'Jedna od prvih zadataka pasa u vojsci bila je da danju i posebno noću čuvaju vojne kapmove. Psi bi lajali i zavijali ukoliko bi se stranci kretali u blizini kampa i tako upozorili na potencijalnu opasnost. Ovu obuku pohađa veliki broj pasa i može smatrati jednom od najbitnijih obuka.', 4)
INSERT [dbo].[Obuka] ([Id], [Naziv], [Opis], [Trajanje]) VALUES (4, N'Pronalaženje opojnih sredstava', N'U ovoj ulozi isprepleću se zadaci vojnih i policijskih pasa. Kako imaju vrlo dobar i istreniran njuh, ovakvi psi vrlo lako pronađu skrivene zabranjene supstance na graničnim prelazima, kontrlolama ili u zračnim lukama. Takođe, vrlo lako otkrivaju i opojna sredstva.', 9)
INSERT [dbo].[Obuka] ([Id], [Naziv], [Opis], [Trajanje]) VALUES (5, N'Pronalaženje eksploziva', N'Veliki broj pasa koristi se u razotkrivanju mina i ekspoziva. Psi minotragači obučavaju se tako da se koriste ogoljene električne žice ispod tla gde električna energija delomično udari psa te on na taj način uči da se opasnost krije ispod površine. Međutim, pronalaženje mina za pse je izrazito stresna aktivnost, tako da je mogu obavljati tek u intervalu oko pola sata nakon čega se moraju odmoriti.', 7)
SET IDENTITY_INSERT [dbo].[Obuka] OFF
SET IDENTITY_INSERT [dbo].[Pas] ON 

INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (6, N'17039', N'Leo', CAST(N'2019-10-10T00:00:00.0000000' AS DateTime2), N'Nemački ovčar', N'Muški', 1)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (7, N'92939', N'Lara', CAST(N'2018-10-19T00:00:00.0000000' AS DateTime2), N'Labrador retriver', N'Ženski', 3)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (10, N'99011', N'Boni', CAST(N'2019-01-12T00:00:00.0000000' AS DateTime2), N'Šarplaninac', N'Muški', 1)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (11, N'45123', N'Aki', CAST(N'2019-08-19T00:00:00.0000000' AS DateTime2), N'Belgijski ovčar', N'Muški', 4)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (13, N'11007', N'Liza', CAST(N'2018-02-12T00:00:00.0000000' AS DateTime2), N'Nemacki ovčar', N'Ženski', 2)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (14, N'22219', N'Betoven', CAST(N'2019-03-11T00:00:00.0000000' AS DateTime2), N'Belgijski ovčar', N'Muški', 3)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (15, N'44522', N'Lea', CAST(N'2018-09-10T00:00:00.0000000' AS DateTime2), N'Šarplaninac', N'Ženski', 4)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (16, N'10105', N'Bea', CAST(N'2020-01-04T00:00:00.0000000' AS DateTime2), N'Šarplaninac', N'Ženski', 3)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (17, N'14145', N'Maks', CAST(N'2019-02-28T00:00:00.0000000' AS DateTime2), N'Labrador retriver', N'Muški', 4)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (18, N'20004', N'Reks', CAST(N'2020-01-05T00:00:00.0000000' AS DateTime2), N'Šarplaninac', N'Muški', 3)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (19, N'11000', N'Linda', CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'Šarplaninac', N'Ženski', 3)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (20, N'93000', N'Luna', CAST(N'2018-04-04T00:00:00.0000000' AS DateTime2), N'Belgijski ovčar', N'Ženski', 3)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (21, N'22121', N'Hana', CAST(N'2019-04-12T00:00:00.0000000' AS DateTime2), N'Belgijski ovčar', N'Ženski', 4)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (22, N'34221', N'Hektor', CAST(N'2018-03-11T00:00:00.0000000' AS DateTime2), N'Nemački ovčar', N'Muški', 4)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (23, N'17111', N'Galileo', CAST(N'2019-06-06T00:00:00.0000000' AS DateTime2), N'Nemački ovčar', N'Muški', 4)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (24, N'43211', N'Džesi', CAST(N'2018-12-12T00:00:00.0000000' AS DateTime2), N'Labdrador retriver', N'Ženski', 4)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (26, N'11213', N'Dona', CAST(N'2019-10-04T00:00:00.0000000' AS DateTime2), N'Šarplaninac', N'Ženski', 1)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (28, N'21344', N'Kasper', CAST(N'2018-01-12T00:00:00.0000000' AS DateTime2), N'Šarplaninac', N'Muški', 1)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (30, N'99994', N'Blek', CAST(N'2019-01-10T00:00:00.0000000' AS DateTime2), N'Belgijski ovčar', N'Muški', 1)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (32, N'90987', N'Arči', CAST(N'2018-02-12T00:00:00.0000000' AS DateTime2), N'Belgijski ovčar', N'Muški', 1)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (34, N'12333', N'Bela', CAST(N'2019-06-12T00:00:00.0000000' AS DateTime2), N'Labdrador retriver', N'Ženski', 1)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (36, N'43226', N'Astor', CAST(N'2019-07-07T00:00:00.0000000' AS DateTime2), N'Nemački ovčar', N'Muški', 1)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (37, N'14145', N'Maks', CAST(N'2019-02-28T00:00:00.0000000' AS DateTime2), N'Labrador retriver', N'Muški', 4)
INSERT [dbo].[Pas] ([Id], [BrojZdravstveneKnjizice], [Ime], [DatumRodjenja], [Rasa], [Pol], [ObukaId]) VALUES (39, N'77211', N'Medoks', CAST(N'2019-11-12T00:00:00.0000000' AS DateTime2), N'Labrador retriver', N'Muški', 2)
SET IDENTITY_INSERT [dbo].[Pas] OFF
SET IDENTITY_INSERT [dbo].[Zadatak] ON 

INSERT [dbo].[Zadatak] ([Id], [NazivZadatka], [Datum], [Teren]) VALUES (1, N'Savladavanje naoružanih begunaca', CAST(N'2019-10-10T00:00:00.0000000' AS DateTime2), N'Niš')
INSERT [dbo].[Zadatak] ([Id], [NazivZadatka], [Datum], [Teren]) VALUES (2, N'Obezbeđivanje javnog skupa', CAST(N'2019-11-11T00:00:00.0000000' AS DateTime2), N'Beograd,Terazije')
INSERT [dbo].[Zadatak] ([Id], [NazivZadatka], [Datum], [Teren]) VALUES (3, N'Traganje za preživelima', CAST(N'2019-12-12T00:00:00.0000000' AS DateTime2), N'Beograd, Batajnica')
INSERT [dbo].[Zadatak] ([Id], [NazivZadatka], [Datum], [Teren]) VALUES (4, N'Provera terena', CAST(N'2019-12-31T00:00:00.0000000' AS DateTime2), N'Beograd, Voždovac')
INSERT [dbo].[Zadatak] ([Id], [NazivZadatka], [Datum], [Teren]) VALUES (5, N'Kontrola policijskog časa', CAST(N'2020-03-26T00:00:00.0000000' AS DateTime2), N'Beograd, Centar')
INSERT [dbo].[Zadatak] ([Id], [NazivZadatka], [Datum], [Teren]) VALUES (6, N'Obezbeđivanje aerodroma Nikola Tesla', CAST(N'2020-03-21T00:00:00.0000000' AS DateTime2), N'Beograd')
INSERT [dbo].[Zadatak] ([Id], [NazivZadatka], [Datum], [Teren]) VALUES (7, N'Obezbeđivanje aerodroma Konstantin Veliki', CAST(N'2020-03-09T00:00:00.0000000' AS DateTime2), N'Beograd')
SET IDENTITY_INSERT [dbo].[Zadatak] OFF
/****** Object:  Index [IX_Angazovanje_ZadatakId]    Script Date: 4/2/2020 12:48:08 AM ******/
CREATE NONCLUSTERED INDEX [IX_Angazovanje_ZadatakId] ON [dbo].[Angazovanje]
(
	[ZadatakId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 4/2/2020 12:48:08 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 4/2/2020 12:48:08 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 4/2/2020 12:48:08 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 4/2/2020 12:48:08 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 4/2/2020 12:48:08 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 4/2/2020 12:48:08 AM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUsers_ObukaId]    Script Date: 4/2/2020 12:48:08 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUsers_ObukaId] ON [dbo].[AspNetUsers]
(
	[ObukaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 4/2/2020 12:48:08 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Pas_ObukaId]    Script Date: 4/2/2020 12:48:08 AM ******/
CREATE NONCLUSTERED INDEX [IX_Pas_ObukaId] ON [dbo].[Pas]
(
	[ObukaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (N'') FOR [Discriminator]
GO
ALTER TABLE [dbo].[Angazovanje]  WITH CHECK ADD  CONSTRAINT [FK_Angazovanje_Pas_PasId] FOREIGN KEY([PasId])
REFERENCES [dbo].[Pas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Angazovanje] CHECK CONSTRAINT [FK_Angazovanje_Pas_PasId]
GO
ALTER TABLE [dbo].[Angazovanje]  WITH CHECK ADD  CONSTRAINT [FK_Angazovanje_Zadatak_ZadatakId] FOREIGN KEY([ZadatakId])
REFERENCES [dbo].[Zadatak] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Angazovanje] CHECK CONSTRAINT [FK_Angazovanje_Zadatak_ZadatakId]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_Obuka_ObukaId] FOREIGN KEY([ObukaId])
REFERENCES [dbo].[Obuka] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_Obuka_ObukaId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Pas]  WITH CHECK ADD  CONSTRAINT [FK_Pas_Obuka_ObukaId] FOREIGN KEY([ObukaId])
REFERENCES [dbo].[Obuka] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pas] CHECK CONSTRAINT [FK_Pas_Obuka_ObukaId]
GO
USE [master]
GO
ALTER DATABASE [DatabaseVojniPsiProba] SET  READ_WRITE 
GO
