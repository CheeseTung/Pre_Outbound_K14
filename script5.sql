USE [master]
GO
/****** Object:  Database [FPTEIC_DB]    Script Date: 9/13/2023 10:30:54 PM ******/
CREATE DATABASE [FPTEIC_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FPTEIC_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\FPTEIC_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FPTEIC_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\FPTEIC_DB_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [FPTEIC_DB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FPTEIC_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FPTEIC_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FPTEIC_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FPTEIC_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FPTEIC_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FPTEIC_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [FPTEIC_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FPTEIC_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FPTEIC_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FPTEIC_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FPTEIC_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FPTEIC_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FPTEIC_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FPTEIC_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FPTEIC_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FPTEIC_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FPTEIC_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FPTEIC_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FPTEIC_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FPTEIC_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FPTEIC_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FPTEIC_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FPTEIC_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FPTEIC_DB] SET RECOVERY FULL 
GO
ALTER DATABASE [FPTEIC_DB] SET  MULTI_USER 
GO
ALTER DATABASE [FPTEIC_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FPTEIC_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FPTEIC_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FPTEIC_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FPTEIC_DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FPTEIC_DB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FPTEIC_DB] SET QUERY_STORE = ON
GO
ALTER DATABASE [FPTEIC_DB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [FPTEIC_DB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/13/2023 10:30:55 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommonMaster]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommonMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Master_Code] [char](10) NOT NULL,
	[Master_Type] [nchar](10) NOT NULL,
	[Master_Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[UpdateUser] [int] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_CommonMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[ISO_Code] [char](2) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[ISO_Code3] [char](3) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[ISO_Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAQ]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAQ](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](500) NOT NULL,
	[Question] [nvarchar](max) NOT NULL,
	[Answer] [nvarchar](max) NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdateUser] [int] NOT NULL,
 CONSTRAINT [PK_FAQ] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Program_Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL,
	[ProgramContent] [int] NOT NULL,
	[Facilities] [int] NOT NULL,
	[PartnerSupport] [int] NOT NULL,
	[Extracurricular] [int] NOT NULL,
	[StaffSupport] [int] NOT NULL,
	[Feedback] [nvarchar](500) NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FPT_Campus]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FPT_Campus](
	[Campus_Code] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
	[Phone] [varchar](50) NULL,
 CONSTRAINT [PK_FPT_Campus] PRIMARY KEY CLUSTERED 
(
	[Campus_Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NotiType] [int] NOT NULL,
	[NotiContent] [nvarchar](max) NOT NULL,
	[Receiver_Usr] [int] NOT NULL,
	[UpdateUsr] [int] NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Partner]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partner](
	[Partner_Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[User_Id] [int] NOT NULL,
	[Country] [char](2) NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Phone] [varchar](50) NULL,
	[Image] [nvarchar](500) NOT NULL,
	[Website] [nvarchar](500) NULL,
	[IsActive] [bit] NOT NULL,
	[UpdateUser] [int] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[Description] [nvarchar](max) NULL,
	[StartDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Partner] PRIMARY KEY CLUSTERED 
(
	[Partner_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Partner_Contact]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partner_Contact](
	[Contact_Id] [int] IDENTITY(1,1) NOT NULL,
	[Partner_Id] [int] NOT NULL,
	[Contact_Level] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[LinkedIn] [nvarchar](500) NULL,
	[Twitter] [nvarchar](500) NULL,
	[Facebook] [nvarchar](500) NULL,
 CONSTRAINT [PK_Partner_Contact] PRIMARY KEY CLUSTERED 
(
	[Contact_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Partner_Document]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partner_Document](
	[Document_Id] [int] IDENTITY(1,1) NOT NULL,
	[Partner_Id] [int] NOT NULL,
	[Program_Id] [int] NULL,
	[Type] [char](10) NOT NULL,
	[Path] [nvarchar](500) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Start_Date] [date] NOT NULL,
	[End_Date] [date] NULL,
	[Status] [int] NOT NULL,
	[UpdateUser] [int] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Partner_Document] PRIMARY KEY CLUSTERED 
(
	[Document_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Partner_History]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partner_History](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Partner_Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Start_Date] [date] NOT NULL,
	[End_Date] [date] NOT NULL,
	[Documents] [nvarchar](500) NULL,
	[Image] [nvarchar](500) NULL,
	[UpdateUser] [int] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Partner_History] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment_Log]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment_Log](
	[Log_Id] [int] IDENTITY(1,1) NOT NULL,
	[Register_Id] [int] NOT NULL,
	[Program_Id] [int] NOT NULL,
	[Payment_Value] [decimal](10, 2) NOT NULL,
	[Payment_Date] [date] NOT NULL,
	[Status] [int] NOT NULL,
	[UpdateUser] [int] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Payment_Log] PRIMARY KEY CLUSTERED 
(
	[Log_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post_Comments]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post_Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Post_Id] [int] NOT NULL,
	[CommentText] [nvarchar](500) NOT NULL,
	[UpdateUser] [int] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Post_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Program]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Program](
	[Program_Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [char](10) NOT NULL,
	[Categorize] [char](10) NOT NULL,
	[IsStudyExchange] [int] NULL,
	[User_Id] [int] NULL,
	[Country] [char](2) NULL,
	[Title] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Participants] [nvarchar](500) NOT NULL,
	[Start_Date] [date] NOT NULL,
	[End_date] [date] NOT NULL,
	[FaceBook_Link] [nvarchar](500) NULL,
	[Register_StartDate] [date] NOT NULL,
	[Register_EndDate] [date] NOT NULL,
	[Payment_Value] [decimal](10, 2) NULL,
	[Payment_StartDate] [date] NULL,
	[Payment_EndDate] [date] NULL,
	[Payment_Description] [nvarchar](500) NULL,
	[Image] [nvarchar](500) NULL,
	[Status] [int] NOT NULL,
	[UpdateUser] [int] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Program] PRIMARY KEY CLUSTERED 
(
	[Program_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Program_Cooperation]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Program_Cooperation](
	[Cooperation_Id] [int] IDENTITY(1,1) NOT NULL,
	[Program_Id] [int] NOT NULL,
	[Partner_Id] [int] NOT NULL,
	[MaxNumberRegister] [int] NOT NULL,
	[NumberOfRegister] [int] NOT NULL,
	[Partner_Contact] [int] NOT NULL,
 CONSTRAINT [PK_Program_Cooperation] PRIMARY KEY CLUSTERED 
(
	[Cooperation_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Program_Log]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Program_Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Program_Id] [int] NOT NULL,
	[LogPath] [nvarchar](500) NOT NULL,
	[UpdateUser] [int] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Program_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Program_Management]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Program_Management](
	[Program_Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL,
	[Manage_Role] [int] NOT NULL,
	[ManageId] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ManageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Program_Posts]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Program_Posts](
	[Pots_Id] [int] IDENTITY(1,1) NOT NULL,
	[Program_Id] [int] NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[PostContent] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](500) NULL,
	[UpdateUser] [int] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Program_Posts] PRIMARY KEY CLUSTERED 
(
	[Pots_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Register]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Register](
	[Register_Id] [int] IDENTITY(1,1) NOT NULL,
	[Program_Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL,
	[Register_Date] [date] NOT NULL,
	[Register_Status] [int] NOT NULL,
	[Request_Change] [nvarchar](500) NULL,
	[Request_Cancel] [nvarchar](500) NULL,
	[UpdateUser] [int] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[Partner_Id] [int] NULL,
 CONSTRAINT [PK_Register] PRIMARY KEY CLUSTERED 
(
	[Register_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Register_Answer]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Register_Answer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuestionID] [int] NOT NULL,
	[RegisterID] [int] NOT NULL,
	[AnswerContent] [nvarchar](max) NULL,
 CONSTRAINT [PK_Register_Answer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Register_Question]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Register_Question](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Program_Id] [int] NOT NULL,
	[QuestionType] [int] NOT NULL,
	[QuestionContent] [nvarchar](max) NOT NULL,
	[isRequire] [bit] NULL,
 CONSTRAINT [PK_Register_Question] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudyExchange]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudyExchange](
	[Exchange_Id] [int] IDENTITY(1,1) NOT NULL,
	[Program_Id] [int] NOT NULL,
	[Course_Name] [nvarchar](255) NOT NULL,
	[FPT_Course] [nvarchar](255) NOT NULL,
	[Max_Mark] [decimal](10, 1) NOT NULL,
	[Pass_Mark] [decimal](10, 1) NOT NULL,
 CONSTRAINT [PK_StudyExchange] PRIMARY KEY CLUSTERED 
(
	[Exchange_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudyResult]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudyResult](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Exchange_Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL,
	[Result_Mark] [decimal](10, 1) NOT NULL,
	[Condition] [bit] NOT NULL,
	[Condition_Reason] [nvarchar](255) NULL,
	[Subject_Status] [bit] NOT NULL,
	[UpdateUser] [int] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_StudyResult] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usr_Account]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usr_Account](
	[User_Id] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](10) NOT NULL,
	[Campus] [int] NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[Gender] [int] NOT NULL,
	[DOB] [date] NULL,
	[Major] [varchar](50) NULL,
	[RollNumber] [varchar](50) NULL,
	[Image] [nvarchar](500) NOT NULL,
	[Phone] [varchar](50) NULL,
	[ID_Number] [varchar](50) NULL,
	[Passport] [varchar](50) NULL,
	[Passport_StartDate] [date] NULL,
	[Passport_EndDate] [date] NULL,
	[Facebook] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[UpdateUser] [int] NULL,
	[UpdateDate] [datetime] NULL,
	[ID_Number_StDate] [date] NULL,
 CONSTRAINT [PK_Usr_Account] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usr_Account_Role]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usr_Account_Role](
	[Role_Code] [varchar](10) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Role_Name] [nvarchar](500) NULL,
 CONSTRAINT [PK_Usr_Account_Role] PRIMARY KEY CLUSTERED 
(
	[Role_Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usr_Role_Permission]    Script Date: 9/13/2023 10:30:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usr_Role_Permission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](10) NOT NULL,
	[Permission_Code] [varchar](255) NULL,
	[IsEnable] [bit] NOT NULL,
 CONSTRAINT [PK_Usr_Role_Permission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Partner] ADD  DEFAULT (getdate()) FOR [StartDate]
GO
ALTER TABLE [dbo].[StudyResult] ADD  CONSTRAINT [DF_StudyResult_Result_Mark]  DEFAULT ((0.0)) FOR [Result_Mark]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Program] FOREIGN KEY([Program_Id])
REFERENCES [dbo].[Program] ([Program_Id])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_Program]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Usr_Account] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Usr_Account] ([User_Id])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_Usr_Account]
GO
ALTER TABLE [dbo].[Partner]  WITH CHECK ADD  CONSTRAINT [FK_Partner_Country] FOREIGN KEY([Country])
REFERENCES [dbo].[Country] ([ISO_Code])
GO
ALTER TABLE [dbo].[Partner] CHECK CONSTRAINT [FK_Partner_Country]
GO
ALTER TABLE [dbo].[Partner]  WITH CHECK ADD  CONSTRAINT [FK_Partner_Usr_Account] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Usr_Account] ([User_Id])
GO
ALTER TABLE [dbo].[Partner] CHECK CONSTRAINT [FK_Partner_Usr_Account]
GO
ALTER TABLE [dbo].[Partner_Contact]  WITH CHECK ADD  CONSTRAINT [FK_Partner_Contact_Partner] FOREIGN KEY([Partner_Id])
REFERENCES [dbo].[Partner] ([Partner_Id])
GO
ALTER TABLE [dbo].[Partner_Contact] CHECK CONSTRAINT [FK_Partner_Contact_Partner]
GO
ALTER TABLE [dbo].[Partner_Document]  WITH CHECK ADD  CONSTRAINT [FK_Partner_Document_Partner] FOREIGN KEY([Partner_Id])
REFERENCES [dbo].[Partner] ([Partner_Id])
GO
ALTER TABLE [dbo].[Partner_Document] CHECK CONSTRAINT [FK_Partner_Document_Partner]
GO
ALTER TABLE [dbo].[Partner_Document]  WITH CHECK ADD  CONSTRAINT [FK_Partner_Document_Program] FOREIGN KEY([Program_Id])
REFERENCES [dbo].[Program] ([Program_Id])
GO
ALTER TABLE [dbo].[Partner_Document] CHECK CONSTRAINT [FK_Partner_Document_Program]
GO
ALTER TABLE [dbo].[Partner_History]  WITH CHECK ADD  CONSTRAINT [FK_Partner_History_Partner] FOREIGN KEY([Partner_Id])
REFERENCES [dbo].[Partner] ([Partner_Id])
GO
ALTER TABLE [dbo].[Partner_History] CHECK CONSTRAINT [FK_Partner_History_Partner]
GO
ALTER TABLE [dbo].[Partner_History]  WITH CHECK ADD  CONSTRAINT [FK_Partner_History_Usr_Account] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Usr_Account] ([User_Id])
GO
ALTER TABLE [dbo].[Partner_History] CHECK CONSTRAINT [FK_Partner_History_Usr_Account]
GO
ALTER TABLE [dbo].[Payment_Log]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Log_Program] FOREIGN KEY([Program_Id])
REFERENCES [dbo].[Program] ([Program_Id])
GO
ALTER TABLE [dbo].[Payment_Log] CHECK CONSTRAINT [FK_Payment_Log_Program]
GO
ALTER TABLE [dbo].[Payment_Log]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Log_Register] FOREIGN KEY([Register_Id])
REFERENCES [dbo].[Register] ([Register_Id])
GO
ALTER TABLE [dbo].[Payment_Log] CHECK CONSTRAINT [FK_Payment_Log_Register]
GO
ALTER TABLE [dbo].[Post_Comments]  WITH CHECK ADD  CONSTRAINT [FK_Post_Comments_Program_Posts] FOREIGN KEY([Post_Id])
REFERENCES [dbo].[Program_Posts] ([Pots_Id])
GO
ALTER TABLE [dbo].[Post_Comments] CHECK CONSTRAINT [FK_Post_Comments_Program_Posts]
GO
ALTER TABLE [dbo].[Program]  WITH CHECK ADD  CONSTRAINT [FK_Program_Usr_Account] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Usr_Account] ([User_Id])
GO
ALTER TABLE [dbo].[Program] CHECK CONSTRAINT [FK_Program_Usr_Account]
GO
ALTER TABLE [dbo].[Program_Cooperation]  WITH CHECK ADD  CONSTRAINT [FK_Program_Cooperation_Partner] FOREIGN KEY([Partner_Id])
REFERENCES [dbo].[Partner] ([Partner_Id])
GO
ALTER TABLE [dbo].[Program_Cooperation] CHECK CONSTRAINT [FK_Program_Cooperation_Partner]
GO
ALTER TABLE [dbo].[Program_Cooperation]  WITH CHECK ADD  CONSTRAINT [FK_Program_Cooperation_Program] FOREIGN KEY([Program_Id])
REFERENCES [dbo].[Program] ([Program_Id])
GO
ALTER TABLE [dbo].[Program_Cooperation] CHECK CONSTRAINT [FK_Program_Cooperation_Program]
GO
ALTER TABLE [dbo].[Program_Log]  WITH CHECK ADD  CONSTRAINT [FK_Program_Log_Program] FOREIGN KEY([Program_Id])
REFERENCES [dbo].[Program] ([Program_Id])
GO
ALTER TABLE [dbo].[Program_Log] CHECK CONSTRAINT [FK_Program_Log_Program]
GO
ALTER TABLE [dbo].[Program_Management]  WITH CHECK ADD  CONSTRAINT [FK_Program_Management_Program] FOREIGN KEY([Program_Id])
REFERENCES [dbo].[Program] ([Program_Id])
GO
ALTER TABLE [dbo].[Program_Management] CHECK CONSTRAINT [FK_Program_Management_Program]
GO
ALTER TABLE [dbo].[Program_Management]  WITH CHECK ADD  CONSTRAINT [FK_Program_Management_Usr_Account] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Usr_Account] ([User_Id])
GO
ALTER TABLE [dbo].[Program_Management] CHECK CONSTRAINT [FK_Program_Management_Usr_Account]
GO
ALTER TABLE [dbo].[Program_Posts]  WITH CHECK ADD  CONSTRAINT [FK_Program_Posts_Program] FOREIGN KEY([Program_Id])
REFERENCES [dbo].[Program] ([Program_Id])
GO
ALTER TABLE [dbo].[Program_Posts] CHECK CONSTRAINT [FK_Program_Posts_Program]
GO
ALTER TABLE [dbo].[Register]  WITH CHECK ADD  CONSTRAINT [FK_Register_Program] FOREIGN KEY([Program_Id])
REFERENCES [dbo].[Program] ([Program_Id])
GO
ALTER TABLE [dbo].[Register] CHECK CONSTRAINT [FK_Register_Program]
GO
ALTER TABLE [dbo].[Register]  WITH CHECK ADD  CONSTRAINT [FK_Register_Usr_Account] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Usr_Account] ([User_Id])
GO
ALTER TABLE [dbo].[Register] CHECK CONSTRAINT [FK_Register_Usr_Account]
GO
ALTER TABLE [dbo].[Register_Answer]  WITH CHECK ADD  CONSTRAINT [FK_Register_Answer_Register] FOREIGN KEY([RegisterID])
REFERENCES [dbo].[Register] ([Register_Id])
GO
ALTER TABLE [dbo].[Register_Answer] CHECK CONSTRAINT [FK_Register_Answer_Register]
GO
ALTER TABLE [dbo].[Register_Answer]  WITH CHECK ADD  CONSTRAINT [FK_Register_Answer_Register_Question] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Register_Question] ([Id])
GO
ALTER TABLE [dbo].[Register_Answer] CHECK CONSTRAINT [FK_Register_Answer_Register_Question]
GO
ALTER TABLE [dbo].[Register_Question]  WITH CHECK ADD  CONSTRAINT [FK_Register_Question_Program] FOREIGN KEY([Program_Id])
REFERENCES [dbo].[Program] ([Program_Id])
GO
ALTER TABLE [dbo].[Register_Question] CHECK CONSTRAINT [FK_Register_Question_Program]
GO
ALTER TABLE [dbo].[StudyExchange]  WITH CHECK ADD  CONSTRAINT [FK_StudyExchange_Program] FOREIGN KEY([Program_Id])
REFERENCES [dbo].[Program] ([Program_Id])
GO
ALTER TABLE [dbo].[StudyExchange] CHECK CONSTRAINT [FK_StudyExchange_Program]
GO
ALTER TABLE [dbo].[StudyResult]  WITH CHECK ADD  CONSTRAINT [FK_StudyResult_StudyExchange] FOREIGN KEY([Exchange_Id])
REFERENCES [dbo].[StudyExchange] ([Exchange_Id])
GO
ALTER TABLE [dbo].[StudyResult] CHECK CONSTRAINT [FK_StudyResult_StudyExchange]
GO
ALTER TABLE [dbo].[StudyResult]  WITH CHECK ADD  CONSTRAINT [FK_StudyResult_Usr_Account] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Usr_Account] ([User_Id])
GO
ALTER TABLE [dbo].[StudyResult] CHECK CONSTRAINT [FK_StudyResult_Usr_Account]
GO
ALTER TABLE [dbo].[Usr_Account]  WITH CHECK ADD  CONSTRAINT [FK_Usr_Account_FPT_Campus] FOREIGN KEY([Campus])
REFERENCES [dbo].[FPT_Campus] ([Campus_Code])
GO
ALTER TABLE [dbo].[Usr_Account] CHECK CONSTRAINT [FK_Usr_Account_FPT_Campus]
GO
ALTER TABLE [dbo].[Usr_Account]  WITH CHECK ADD  CONSTRAINT [FK_Usr_Account_Usr_Account_Role] FOREIGN KEY([Role])
REFERENCES [dbo].[Usr_Account_Role] ([Role_Code])
GO
ALTER TABLE [dbo].[Usr_Account] CHECK CONSTRAINT [FK_Usr_Account_Usr_Account_Role]
GO
ALTER TABLE [dbo].[Usr_Role_Permission]  WITH CHECK ADD  CONSTRAINT [FK_Usr_Role_Permission_Usr_Account_Role] FOREIGN KEY([Role])
REFERENCES [dbo].[Usr_Account_Role] ([Role_Code])
GO
ALTER TABLE [dbo].[Usr_Role_Permission] CHECK CONSTRAINT [FK_Usr_Role_Permission_Usr_Account_Role]
GO
USE [master]
GO
ALTER DATABASE [FPTEIC_DB] SET  READ_WRITE 
GO
