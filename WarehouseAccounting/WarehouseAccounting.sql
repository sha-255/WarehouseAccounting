USE [master]
GO
CREATE DATABASE [WarehouseAccounting]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WarehouseAccounting_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER1\MSSQL\DATA\WarehouseAccounting.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'WarehouseAccounting_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER1\MSSQL\DATA\WarehouseAccounting.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [WarehouseAccounting] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WarehouseAccounting].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WarehouseAccounting] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WarehouseAccounting] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WarehouseAccounting] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WarehouseAccounting] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WarehouseAccounting] SET ARITHABORT OFF 
GO
ALTER DATABASE [WarehouseAccounting] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WarehouseAccounting] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WarehouseAccounting] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WarehouseAccounting] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WarehouseAccounting] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WarehouseAccounting] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WarehouseAccounting] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WarehouseAccounting] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WarehouseAccounting] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WarehouseAccounting] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WarehouseAccounting] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WarehouseAccounting] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WarehouseAccounting] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WarehouseAccounting] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WarehouseAccounting] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WarehouseAccounting] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WarehouseAccounting] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WarehouseAccounting] SET RECOVERY FULL 
GO
ALTER DATABASE [WarehouseAccounting] SET  MULTI_USER 
GO
ALTER DATABASE [WarehouseAccounting] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WarehouseAccounting] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WarehouseAccounting] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WarehouseAccounting] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WarehouseAccounting] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WarehouseAccounting] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [WarehouseAccounting] SET QUERY_STORE = OFF
GO
USE [WarehouseAccounting]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accessories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Material] [varchar](50) NULL,
	[Price] [int] NULL,
	[Quantity] [int] NULL,
	[Warehouse] [varchar](50) NULL,
 CONSTRAINT [PK_Фурнитура] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialsForFurniture](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaterialName] [varchar](50) NULL,
	[MaterialPrice] [int] NULL,
	[Quantity] [int] NULL,
	[Warehouse] [varchar](50) NULL,
 CONSTRAINT [PK_Материалы для мебели] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialsForTheCountertop](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaterialName] [varchar](50) NULL,
	[MaterialPrice] [int] NULL,
	[Quantity] [int] NULL,
	[Warehouse] [varchar](50) NULL,
 CONSTRAINT [PK_Материалы для столешницы] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Warehouse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Responsible] [varchar](50) NULL,
	[Address] [varchar](50) NULL,
 CONSTRAINT [PK_Склад] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Warehouse]  WITH CHECK ADD  CONSTRAINT [FK_Склад_Материалы для мебели] FOREIGN KEY([Id])
REFERENCES [dbo].[MaterialsForFurniture] ([Id])
GO
ALTER TABLE [dbo].[Warehouse] CHECK CONSTRAINT [FK_Склад_Материалы для мебели]
GO
ALTER TABLE [dbo].[Warehouse]  WITH CHECK ADD  CONSTRAINT [FK_Склад_Материалы для столешницы] FOREIGN KEY([Id])
REFERENCES [dbo].[MaterialsForTheCountertop] ([Id])
GO
ALTER TABLE [dbo].[Warehouse] CHECK CONSTRAINT [FK_Склад_Материалы для столешницы]
GO
ALTER TABLE [dbo].[Warehouse]  WITH CHECK ADD  CONSTRAINT [FK_Склад_Фурнитура] FOREIGN KEY([Id])
REFERENCES [dbo].[Accessories] ([Id])
GO
ALTER TABLE [dbo].[Warehouse] CHECK CONSTRAINT [FK_Склад_Фурнитура]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Nastya1]
    @Category NVARCHAR(50),
    @MaterialName NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Category = 'FurnitureMaterials'
    BEGIN
        -- Запрос для получения данных о материалах для мебели
        SELECT [Material Name], [Quantity (pieces)], Warehouse
        FROM [Материалы для мебели]
        WHERE [Material Name] = ISNULL(@MaterialName, [Material Name]);
    END
    ELSE IF @Category = 'CountertopMaterials'
    BEGIN
        -- Запрос для получения данных о материалах для столешниц
        SELECT [Material Name], [Quantity (pieces)], Warehouse
        FROM [Материалы для столешницы]
        WHERE [Material Name] = ISNULL(@MaterialName, [Material Name]);
    END
    ELSE IF @Category = 'Hardware'
    BEGIN
        -- Запрос для получения данных о фурнитуре
        SELECT Name, Quantity, Warehouse
        FROM [Фурнитура]
        WHERE Name = ISNULL(@MaterialName, Name);
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[Nastya2]    Script Date: 08.05.2024 13:09:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Nastya2]
    @Category NVARCHAR(50),
    @MaterialName NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Category = 'Мебель'
    BEGIN
        -- Запрос для получения данных о материалах для мебели
        SELECT [Material Name], [Quantity (pieces)], Warehouse
        FROM [Материалы для мебели]
        WHERE [Material Name] = ISNULL(@MaterialName, [Material Name]);
    END
    ELSE IF @Category = 'Столешницы'
    BEGIN
        -- Запрос для получения данных о материалах для столешниц
        SELECT [Material Name], [Quantity (pieces)], Warehouse
        FROM [Материалы для столешницы]
        WHERE [Material Name] = ISNULL(@MaterialName, [Material Name]);
    END
    ELSE IF @Category = 'Фурнитуры'
    BEGIN
        -- Запрос для получения данных о фурнитуре
        SELECT Name, Quantity, Warehouse
        FROM [Фурнитура]
        WHERE Name = ISNULL(@MaterialName, Name);
    END
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NastyaData]
AS
BEGIN
    SET NOCOUNT ON;

    
    SELECT * FROM [Материалы для мебели];
END;
GO
USE [master]
GO
ALTER DATABASE [WarehouseAccounting] SET  READ_WRITE 
GO
