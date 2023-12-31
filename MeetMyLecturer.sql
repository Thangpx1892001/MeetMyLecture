USE [master]
GO
/****** Object:  Database [MeetMyLecturer]    Script Date: 10/23/2023 9:58:02 PM ******/
CREATE DATABASE [MeetMyLecturer]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MeetMyLecturer', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MeetMyLecturer.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MeetMyLecturer_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MeetMyLecturer_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MeetMyLecturer] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MeetMyLecturer].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MeetMyLecturer] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MeetMyLecturer] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MeetMyLecturer] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MeetMyLecturer] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MeetMyLecturer] SET ARITHABORT OFF 
GO
ALTER DATABASE [MeetMyLecturer] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [MeetMyLecturer] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MeetMyLecturer] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MeetMyLecturer] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MeetMyLecturer] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MeetMyLecturer] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MeetMyLecturer] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MeetMyLecturer] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MeetMyLecturer] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MeetMyLecturer] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MeetMyLecturer] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MeetMyLecturer] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MeetMyLecturer] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MeetMyLecturer] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MeetMyLecturer] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MeetMyLecturer] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MeetMyLecturer] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MeetMyLecturer] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MeetMyLecturer] SET  MULTI_USER 
GO
ALTER DATABASE [MeetMyLecturer] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MeetMyLecturer] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MeetMyLecturer] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MeetMyLecturer] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MeetMyLecturer] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MeetMyLecturer] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [MeetMyLecturer] SET QUERY_STORE = ON
GO
ALTER DATABASE [MeetMyLecturer] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MeetMyLecturer]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 10/23/2023 9:58:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[fullname] [nvarchar](255) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[dob] [date] NOT NULL,
	[role] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Account__3213E83F87E927D2] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 10/23/2023 9:58:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[student_id] [int] NOT NULL,
	[slot_id] [int] NOT NULL,
	[subject_id] [int] NOT NULL,
	[description] [nvarchar](300) NOT NULL,
	[reason] [nvarchar](255) NULL,
	[created_at] [datetime] NOT NULL,
	[status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Booking__3213E83FB070D362] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 10/23/2023 9:58:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[booking_id] [int] NOT NULL,
	[star] [int] NOT NULL,
	[comment] [nvarchar](300) NOT NULL,
	[created_at] [datetime] NOT NULL,
 CONSTRAINT [PK__Feedback__3213E83FE9C2E41C] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 10/23/2023 9:58:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[booking_id] [int] NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[time] [datetime] NOT NULL,
	[is_read] [bit] NOT NULL,
	[created_at] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Request]    Script Date: 10/23/2023 9:58:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Request](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[student_id] [int] NOT NULL,
	[lecturer_id] [int] NOT NULL,
	[subject_id] [int] NOT NULL,
	[description] [nvarchar](255) NOT NULL,
	[created_at] [datetime] NOT NULL,
	[status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Request__3213E83FAC4B1368] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Slot]    Script Date: 10/23/2023 9:58:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Slot](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[lecturer_id] [int] NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[location] [nvarchar](255) NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[limit_booking] [int] NOT NULL,
	[start_datetime] [datetime] NOT NULL,
	[end_datetime] [datetime] NOT NULL,
	[created_at] [datetime] NOT NULL,
	[status] [nvarchar](50) NOT NULL,
	[mode] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Slot__3213E83FE5DA4D08] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 10/23/2023 9:58:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[subject_code] [nvarchar](50) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[created_at] [datetime] NOT NULL,
 CONSTRAINT [PK__Subject__3213E83F8C5B2782] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject_Lecturer]    Script Date: 10/23/2023 9:58:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject_Lecturer](
	[subject_id] [int] NOT NULL,
	[lecturer_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[subject_id] ASC,
	[lecturer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Account] FOREIGN KEY([id])
REFERENCES [dbo].[Account] ([id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Account]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Slot] FOREIGN KEY([slot_id])
REFERENCES [dbo].[Slot] ([id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Slot]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Subject] FOREIGN KEY([subject_id])
REFERENCES [dbo].[Subject] ([id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Subject]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Booking] FOREIGN KEY([booking_id])
REFERENCES [dbo].[Booking] ([id])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_Booking]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Booking] FOREIGN KEY([booking_id])
REFERENCES [dbo].[Booking] ([id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Booking]
GO
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Account] FOREIGN KEY([student_id])
REFERENCES [dbo].[Account] ([id])
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_Account]
GO
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Account1] FOREIGN KEY([lecturer_id])
REFERENCES [dbo].[Account] ([id])
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_Account1]
GO
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Subject] FOREIGN KEY([subject_id])
REFERENCES [dbo].[Subject] ([id])
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_Subject]
GO
ALTER TABLE [dbo].[Slot]  WITH CHECK ADD  CONSTRAINT [FK_Slot_Account] FOREIGN KEY([lecturer_id])
REFERENCES [dbo].[Account] ([id])
GO
ALTER TABLE [dbo].[Slot] CHECK CONSTRAINT [FK_Slot_Account]
GO
ALTER TABLE [dbo].[Subject_Lecturer]  WITH CHECK ADD  CONSTRAINT [FK_Subject_Lecturer_Account] FOREIGN KEY([lecturer_id])
REFERENCES [dbo].[Account] ([id])
GO
ALTER TABLE [dbo].[Subject_Lecturer] CHECK CONSTRAINT [FK_Subject_Lecturer_Account]
GO
ALTER TABLE [dbo].[Subject_Lecturer]  WITH CHECK ADD  CONSTRAINT [FK_Subject_Lecturer_Subject] FOREIGN KEY([subject_id])
REFERENCES [dbo].[Subject] ([id])
GO
ALTER TABLE [dbo].[Subject_Lecturer] CHECK CONSTRAINT [FK_Subject_Lecturer_Subject]
GO
USE [master]
GO
ALTER DATABASE [MeetMyLecturer] SET  READ_WRITE 
GO
