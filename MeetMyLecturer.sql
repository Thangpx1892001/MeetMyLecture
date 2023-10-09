USE [master]
GO
/****** Object:  Database [MajorMapper]    Script Date: 10/9/2023 5:11:45 PM ******/
CREATE DATABASE [MajorMapper]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MajorMapper', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MajorMapper.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MajorMapper_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MajorMapper_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MajorMapper] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MajorMapper].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MajorMapper] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MajorMapper] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MajorMapper] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MajorMapper] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MajorMapper] SET ARITHABORT OFF 
GO
ALTER DATABASE [MajorMapper] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MajorMapper] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MajorMapper] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MajorMapper] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MajorMapper] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MajorMapper] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MajorMapper] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MajorMapper] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MajorMapper] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MajorMapper] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MajorMapper] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MajorMapper] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MajorMapper] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MajorMapper] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MajorMapper] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MajorMapper] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MajorMapper] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MajorMapper] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MajorMapper] SET  MULTI_USER 
GO
ALTER DATABASE [MajorMapper] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MajorMapper] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MajorMapper] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MajorMapper] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MajorMapper] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MajorMapper] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [MajorMapper] SET QUERY_STORE = OFF
GO
USE [MajorMapper]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 10/9/2023 5:11:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Gender] [nvarchar](50) NULL,
	[DoB] [date] NULL,
	[Role] [int] NOT NULL,
	[Address] [nvarchar](200) NULL,
	[Phone] [int] NULL,
	[Status] [nvarchar](20) NOT NULL,
	[Turn] [int] NULL,
	[CreateDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 10/9/2023 5:11:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[SlotId] [int] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 10/9/2023 5:11:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookingId] [int] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[Star] [int] NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Major]    Script Date: 10/9/2023 5:11:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Major](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CreateDateTime] [date] NOT NULL,
	[UpdateDateTime] [date] NOT NULL,
 CONSTRAINT [PK_Major] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 10/9/2023 5:11:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookingId] [int] NULL,
	[NotificationContent] [nvarchar](max) NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[time] [date] NOT NULL,
	[IsRead] [bit] NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 10/9/2023 5:11:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[OrderType] [nvarchar](1) NOT NULL,
	[RelatiedId] [int] NOT NULL,
	[OrderId] [int] NOT NULL,
	[TransactionId] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[Description] [nvarchar](1) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonalityType]    Script Date: 10/9/2023 5:11:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonalityType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
	[UpdateDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_PersonalityType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonalityType_Major]    Script Date: 10/9/2023 5:11:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonalityType_Major](
	[PersonalityTypeId] [int] NOT NULL,
	[MajorId] [int] NOT NULL,
 CONSTRAINT [PK_PersonalityType_Major] PRIMARY KEY CLUSTERED 
(
	[PersonalityTypeId] ASC,
	[MajorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 10/9/2023 5:11:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AssetsName] [nvarchar](1) NOT NULL,
	[Type] [int] NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReviewTest]    Script Date: 10/9/2023 5:11:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReviewTest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Star] [int] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[TestResultId] [int] NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_ReviewAnalysis] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 10/9/2023 5:11:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](20) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Score]    Script Date: 10/9/2023 5:11:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Score](
	[TestResultId] [int] NOT NULL,
	[PersonalityTypeId] [int] NOT NULL,
	[Result] [int] NOT NULL,
 CONSTRAINT [PK_Score] PRIMARY KEY CLUSTERED 
(
	[TestResultId] ASC,
	[PersonalityTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Slot]    Script Date: 10/9/2023 5:11:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Slot](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConsultantId] [int] NOT NULL,
	[StartDateTime] [datetime] NOT NULL,
	[EndDateTime] [datetime] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Slot] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Test]    Script Date: 10/9/2023 5:11:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Status] [bit] NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Test] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Test_Question]    Script Date: 10/9/2023 5:11:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test_Question](
	[TestId] [int] NOT NULL,
	[QuestionId] [int] NOT NULL,
	[PersonalityTypeId] [int] NOT NULL,
	[Score] [int] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Test_Question] PRIMARY KEY CLUSTERED 
(
	[TestId] ASC,
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestResult]    Script Date: 10/9/2023 5:11:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestResult](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TestId] [int] NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_TestResult] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Role] FOREIGN KEY([Role])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Role]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Account] FOREIGN KEY([UserId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Account]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Slot] FOREIGN KEY([SlotId])
REFERENCES [dbo].[Slot] ([Id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Slot]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Booking] FOREIGN KEY([BookingId])
REFERENCES [dbo].[Booking] ([Id])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_Booking]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Booking] FOREIGN KEY([BookingId])
REFERENCES [dbo].[Booking] ([Id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Booking]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Account] FOREIGN KEY([UserId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Account]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Booking] FOREIGN KEY([RelatiedId])
REFERENCES [dbo].[Booking] ([Id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Booking]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_TestResult] FOREIGN KEY([RelatiedId])
REFERENCES [dbo].[TestResult] ([Id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_TestResult]
GO
ALTER TABLE [dbo].[PersonalityType_Major]  WITH CHECK ADD  CONSTRAINT [FK_PersonalityType_Major_Major] FOREIGN KEY([MajorId])
REFERENCES [dbo].[Major] ([Id])
GO
ALTER TABLE [dbo].[PersonalityType_Major] CHECK CONSTRAINT [FK_PersonalityType_Major_Major]
GO
ALTER TABLE [dbo].[PersonalityType_Major]  WITH CHECK ADD  CONSTRAINT [FK_PersonalityType_Major_PersonalityType] FOREIGN KEY([PersonalityTypeId])
REFERENCES [dbo].[PersonalityType] ([Id])
GO
ALTER TABLE [dbo].[PersonalityType_Major] CHECK CONSTRAINT [FK_PersonalityType_Major_PersonalityType]
GO
ALTER TABLE [dbo].[ReviewTest]  WITH CHECK ADD  CONSTRAINT [FK_ReviewAnalysis_TestResult] FOREIGN KEY([TestResultId])
REFERENCES [dbo].[TestResult] ([Id])
GO
ALTER TABLE [dbo].[ReviewTest] CHECK CONSTRAINT [FK_ReviewAnalysis_TestResult]
GO
ALTER TABLE [dbo].[Score]  WITH CHECK ADD  CONSTRAINT [FK_Score_PersonalityType] FOREIGN KEY([PersonalityTypeId])
REFERENCES [dbo].[PersonalityType] ([Id])
GO
ALTER TABLE [dbo].[Score] CHECK CONSTRAINT [FK_Score_PersonalityType]
GO
ALTER TABLE [dbo].[Score]  WITH CHECK ADD  CONSTRAINT [FK_Score_TestResult] FOREIGN KEY([TestResultId])
REFERENCES [dbo].[TestResult] ([Id])
GO
ALTER TABLE [dbo].[Score] CHECK CONSTRAINT [FK_Score_TestResult]
GO
ALTER TABLE [dbo].[Slot]  WITH CHECK ADD  CONSTRAINT [FK_Slot_Account] FOREIGN KEY([ConsultantId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Slot] CHECK CONSTRAINT [FK_Slot_Account]
GO
ALTER TABLE [dbo].[Test]  WITH CHECK ADD  CONSTRAINT [FK_Test_Account] FOREIGN KEY([UserId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Test] CHECK CONSTRAINT [FK_Test_Account]
GO
ALTER TABLE [dbo].[Test_Question]  WITH CHECK ADD  CONSTRAINT [FK_Test_Question_PersonalityType] FOREIGN KEY([PersonalityTypeId])
REFERENCES [dbo].[PersonalityType] ([Id])
GO
ALTER TABLE [dbo].[Test_Question] CHECK CONSTRAINT [FK_Test_Question_PersonalityType]
GO
ALTER TABLE [dbo].[Test_Question]  WITH CHECK ADD  CONSTRAINT [FK_Test_Question_Question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([Id])
GO
ALTER TABLE [dbo].[Test_Question] CHECK CONSTRAINT [FK_Test_Question_Question]
GO
ALTER TABLE [dbo].[Test_Question]  WITH CHECK ADD  CONSTRAINT [FK_Test_Question_Test] FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([Id])
GO
ALTER TABLE [dbo].[Test_Question] CHECK CONSTRAINT [FK_Test_Question_Test]
GO
ALTER TABLE [dbo].[TestResult]  WITH CHECK ADD  CONSTRAINT [FK_TestResult_Test] FOREIGN KEY([TestId])
REFERENCES [dbo].[Test] ([Id])
GO
ALTER TABLE [dbo].[TestResult] CHECK CONSTRAINT [FK_TestResult_Test]
GO
USE [master]
GO
ALTER DATABASE [MajorMapper] SET  READ_WRITE 
GO
