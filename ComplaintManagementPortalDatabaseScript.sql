USE [master]
GO
/****** Object:  Database [ComplaintManagementPortal]    Script Date: 8/22/2020 6:48:37 PM ******/
CREATE DATABASE [ComplaintManagementPortal]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ComplaintManagementPortal', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ComplaintManagementPortal.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ComplaintManagementPortal_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ComplaintManagementPortal_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ComplaintManagementPortal] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ComplaintManagementPortal].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ComplaintManagementPortal] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ComplaintManagementPortal] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ComplaintManagementPortal] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ComplaintManagementPortal] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ComplaintManagementPortal] SET ARITHABORT OFF 
GO
ALTER DATABASE [ComplaintManagementPortal] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ComplaintManagementPortal] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ComplaintManagementPortal] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ComplaintManagementPortal] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ComplaintManagementPortal] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ComplaintManagementPortal] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ComplaintManagementPortal] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ComplaintManagementPortal] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ComplaintManagementPortal] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ComplaintManagementPortal] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ComplaintManagementPortal] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ComplaintManagementPortal] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ComplaintManagementPortal] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ComplaintManagementPortal] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ComplaintManagementPortal] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ComplaintManagementPortal] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ComplaintManagementPortal] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ComplaintManagementPortal] SET RECOVERY FULL 
GO
ALTER DATABASE [ComplaintManagementPortal] SET  MULTI_USER 
GO
ALTER DATABASE [ComplaintManagementPortal] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ComplaintManagementPortal] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ComplaintManagementPortal] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ComplaintManagementPortal] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ComplaintManagementPortal] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ComplaintManagementPortal', N'ON'
GO
ALTER DATABASE [ComplaintManagementPortal] SET QUERY_STORE = OFF
GO
USE [ComplaintManagementPortal]
GO
/****** Object:  Table [dbo].[Complaints]    Script Date: 8/22/2020 6:48:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Complaints](
	[ComplaintID] [int] IDENTITY(1,1) NOT NULL,
	[ComplaintStatusID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Refund] [bit] NOT NULL,
	[CountryID] [int] NOT NULL,
	[Details] [nvarchar](max) NOT NULL,
	[ContactMethod] [int] NOT NULL,
	[ComplaintLoggedTime] [datetime] NULL,
 CONSTRAINT [PK_Complains] PRIMARY KEY CLUSTERED 
(
	[ComplaintID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComplaintsDepartments]    Script Date: 8/22/2020 6:48:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComplaintsDepartments](
	[ComplaintsDepartmentsID] [int] IDENTITY(1,1) NOT NULL,
	[ComplaintID] [int] NOT NULL,
	[DepartmentsID] [int] NOT NULL,
 CONSTRAINT [PK_ComplaintsDepartments] PRIMARY KEY CLUSTERED 
(
	[ComplaintsDepartmentsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComplaintStatus]    Script Date: 8/22/2020 6:48:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComplaintStatus](
	[ComplaintStatusID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ComplaintStatus] PRIMARY KEY CLUSTERED 
(
	[ComplaintStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactMethods]    Script Date: 8/22/2020 6:48:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactMethods](
	[ContactMethodID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_ContactMethods] PRIMARY KEY CLUSTERED 
(
	[ContactMethodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 8/22/2020 6:48:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[CountryID] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 8/22/2020 6:48:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[DepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genders]    Script Date: 8/22/2020 6:48:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genders](
	[GenderID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[GenderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemUsers]    Script Date: 8/22/2020 6:48:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemUsers](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](20) NOT NULL,
	[CountryID] [int] NOT NULL,
	[GenderID] [int] NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[UserTypeID] [int] NOT NULL,
 CONSTRAINT [PK_SystemUsers] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTypes]    Script Date: 8/22/2020 6:48:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTypes](
	[UserTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserTypes] PRIMARY KEY CLUSTERED 
(
	[UserTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Complaints] ON 

INSERT [dbo].[Complaints] ([ComplaintID], [ComplaintStatusID], [UserID], [Refund], [CountryID], [Details], [ContactMethod], [ComplaintLoggedTime]) VALUES (14, 2, 2, 1, 2, N'okokokkokkokoko', 2, CAST(N'2020-08-22T17:36:57.480' AS DateTime))
SET IDENTITY_INSERT [dbo].[Complaints] OFF
GO
SET IDENTITY_INSERT [dbo].[ComplaintStatus] ON 

INSERT [dbo].[ComplaintStatus] ([ComplaintStatusID], [Name]) VALUES (1, N'Resolved')
INSERT [dbo].[ComplaintStatus] ([ComplaintStatusID], [Name]) VALUES (2, N'Pending Resolution')
INSERT [dbo].[ComplaintStatus] ([ComplaintStatusID], [Name]) VALUES (3, N'Dismissed')
SET IDENTITY_INSERT [dbo].[ComplaintStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[ContactMethods] ON 

INSERT [dbo].[ContactMethods] ([ContactMethodID], [Name]) VALUES (1, N'Phone')
INSERT [dbo].[ContactMethods] ([ContactMethodID], [Name]) VALUES (2, N'Email')
INSERT [dbo].[ContactMethods] ([ContactMethodID], [Name]) VALUES (5, N'Email And Phone')
SET IDENTITY_INSERT [dbo].[ContactMethods] OFF
GO
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (1, N'Jordan')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (2, N'UAE')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (3, N'KSA')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (4, N'Egypt')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (5, N'UK')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (6, N'China')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (7, N'USA')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (8, N'Germany')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (9, N'Oman')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (10, N'Algeria')
INSERT [dbo].[Countries] ([CountryID], [CountryName]) VALUES (11, N'Turkey')
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO
SET IDENTITY_INSERT [dbo].[Departments] ON 

INSERT [dbo].[Departments] ([DepartmentID], [Name]) VALUES (1, N'Shipping Department')
INSERT [dbo].[Departments] ([DepartmentID], [Name]) VALUES (2, N'Sales Department')
INSERT [dbo].[Departments] ([DepartmentID], [Name]) VALUES (3, N'Reception Department')
INSERT [dbo].[Departments] ([DepartmentID], [Name]) VALUES (4, N'Customer Service Department')
INSERT [dbo].[Departments] ([DepartmentID], [Name]) VALUES (5, N'Supply Department')
INSERT [dbo].[Departments] ([DepartmentID], [Name]) VALUES (6, N'Showroom')
SET IDENTITY_INSERT [dbo].[Departments] OFF
GO
SET IDENTITY_INSERT [dbo].[Genders] ON 

INSERT [dbo].[Genders] ([GenderID], [Name]) VALUES (1, N'Male')
INSERT [dbo].[Genders] ([GenderID], [Name]) VALUES (2, N'Female')
SET IDENTITY_INSERT [dbo].[Genders] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemUsers] ON 

INSERT [dbo].[SystemUsers] ([UserID], [UserName], [Password], [FirstName], [LastName], [Email], [PhoneNumber], [CountryID], [GenderID], [DateOfBirth], [UserTypeID]) VALUES (2, N'admin', N'admin12345', N'Yaseen', N'Alnadi', N'Alnadi.yaseen@gmail.com', N'0786914883', 1, 1, CAST(N'1997-09-09' AS Date), 2)
INSERT [dbo].[SystemUsers] ([UserID], [UserName], [Password], [FirstName], [LastName], [Email], [PhoneNumber], [CountryID], [GenderID], [DateOfBirth], [UserTypeID]) VALUES (9, N'ibrahim', N'ibrahim12345', N'ibrahim', N'barada', N'ibrahim@Pwc.com', N'96278999', 1, 1, CAST(N'1988-05-05' AS Date), 1)
SET IDENTITY_INSERT [dbo].[SystemUsers] OFF
GO
SET IDENTITY_INSERT [dbo].[UserTypes] ON 

INSERT [dbo].[UserTypes] ([UserTypeID], [Name]) VALUES (1, N'Customer')
INSERT [dbo].[UserTypes] ([UserTypeID], [Name]) VALUES (2, N'Admin')
SET IDENTITY_INSERT [dbo].[UserTypes] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_SystemUsers]    Script Date: 8/22/2020 6:48:41 PM ******/
ALTER TABLE [dbo].[SystemUsers] ADD  CONSTRAINT [IX_SystemUsers] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_SystemUsers_1]    Script Date: 8/22/2020 6:48:41 PM ******/
ALTER TABLE [dbo].[SystemUsers] ADD  CONSTRAINT [IX_SystemUsers_1] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Complaints]  WITH CHECK ADD  CONSTRAINT [FK_Complaints_ComplaintStatus] FOREIGN KEY([ComplaintStatusID])
REFERENCES [dbo].[ComplaintStatus] ([ComplaintStatusID])
GO
ALTER TABLE [dbo].[Complaints] CHECK CONSTRAINT [FK_Complaints_ComplaintStatus]
GO
ALTER TABLE [dbo].[Complaints]  WITH CHECK ADD  CONSTRAINT [FK_Complaints_ContactMethods] FOREIGN KEY([ContactMethod])
REFERENCES [dbo].[ContactMethods] ([ContactMethodID])
GO
ALTER TABLE [dbo].[Complaints] CHECK CONSTRAINT [FK_Complaints_ContactMethods]
GO
ALTER TABLE [dbo].[Complaints]  WITH CHECK ADD  CONSTRAINT [FK_Complaints_Countries] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Countries] ([CountryID])
GO
ALTER TABLE [dbo].[Complaints] CHECK CONSTRAINT [FK_Complaints_Countries]
GO
ALTER TABLE [dbo].[Complaints]  WITH CHECK ADD  CONSTRAINT [FK_Complaints_SystemUsers] FOREIGN KEY([UserID])
REFERENCES [dbo].[SystemUsers] ([UserID])
GO
ALTER TABLE [dbo].[Complaints] CHECK CONSTRAINT [FK_Complaints_SystemUsers]
GO
ALTER TABLE [dbo].[ComplaintsDepartments]  WITH CHECK ADD  CONSTRAINT [FK_ComplaintsDepartments_Complaints] FOREIGN KEY([ComplaintID])
REFERENCES [dbo].[Complaints] ([ComplaintID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ComplaintsDepartments] CHECK CONSTRAINT [FK_ComplaintsDepartments_Complaints]
GO
ALTER TABLE [dbo].[ComplaintsDepartments]  WITH CHECK ADD  CONSTRAINT [FK_ComplaintsDepartments_Departments] FOREIGN KEY([DepartmentsID])
REFERENCES [dbo].[Departments] ([DepartmentID])
GO
ALTER TABLE [dbo].[ComplaintsDepartments] CHECK CONSTRAINT [FK_ComplaintsDepartments_Departments]
GO
ALTER TABLE [dbo].[Genders]  WITH CHECK ADD  CONSTRAINT [FK_Genders_Genders] FOREIGN KEY([GenderID])
REFERENCES [dbo].[Genders] ([GenderID])
GO
ALTER TABLE [dbo].[Genders] CHECK CONSTRAINT [FK_Genders_Genders]
GO
ALTER TABLE [dbo].[SystemUsers]  WITH CHECK ADD  CONSTRAINT [FK_SystemUsers_Countries] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Countries] ([CountryID])
GO
ALTER TABLE [dbo].[SystemUsers] CHECK CONSTRAINT [FK_SystemUsers_Countries]
GO
ALTER TABLE [dbo].[SystemUsers]  WITH CHECK ADD  CONSTRAINT [FK_SystemUsers_Genders] FOREIGN KEY([GenderID])
REFERENCES [dbo].[Genders] ([GenderID])
GO
ALTER TABLE [dbo].[SystemUsers] CHECK CONSTRAINT [FK_SystemUsers_Genders]
GO
ALTER TABLE [dbo].[SystemUsers]  WITH CHECK ADD  CONSTRAINT [FK_SystemUsers_SystemUsers] FOREIGN KEY([UserID])
REFERENCES [dbo].[SystemUsers] ([UserID])
GO
ALTER TABLE [dbo].[SystemUsers] CHECK CONSTRAINT [FK_SystemUsers_SystemUsers]
GO
ALTER TABLE [dbo].[SystemUsers]  WITH CHECK ADD  CONSTRAINT [FK_SystemUsers_SystemUsers1] FOREIGN KEY([UserTypeID])
REFERENCES [dbo].[UserTypes] ([UserTypeID])
GO
ALTER TABLE [dbo].[SystemUsers] CHECK CONSTRAINT [FK_SystemUsers_SystemUsers1]
GO
USE [master]
GO
ALTER DATABASE [ComplaintManagementPortal] SET  READ_WRITE 
GO
