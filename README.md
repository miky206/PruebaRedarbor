# PruebaRedarbor
Gestion de candidatos</br>
<h1>Test de conocimientos C#</h1>
Un reclutador necesita administrar una base de candidatos de un proceso
selectivo. Utilizando de base el modelo relacional representado encima, desarrolla una aplicación web utilizando arquitectura MVC que permita al reclutador realizar las siguientes acciones:</br>

• Consultar una lista de candidatos inscritos.</br>
• Inscribir nuevos candidatos y sus experiencias profesionales.</br>
• Editar la inscripción de un candidato y sus experiencias profesionales.</br>
• Eliminar un candidato.</br>

<h2>Requisitos</h2></br>
• Utiliza .Net Core.</br>
• Utiliza Entity Framework como ORM para las consultas y persistencia de
datos.</br>
• Desarrolla la aplicación utilizando de base el modelo relacional mostrado</br>
al inicio de este documento.</br>
• Desarrolla el modelo de entidades utilizando Code-First con Entity Framework.</br>
• Utiliza SQL in-memory o SQL Express (localdb)</br>
• No pongas el foco en Frontend, lo importante es el Backend!</br>

<h2>Consejos</h2></br>
• Procura utilizar tus conocimientos y buenas prácticas de OOP, DDD, SOLID e Clean Code.</br>
• En caso de tener conocimiento del patrón arquitectural CQRS, utilízalo!</br>
• Nuestro objetivo es conocer tu estilo de programación, aquí no existe
correcto o incorrecto.</br>
• Y principalmente, diviértete!</br>


<h2>Preparación del entorno</h2>
Instalación de docker y mediante consola ejecutar los siguientes comandos desde la consola:</br>

docker pull mcr.microsoft.com/azure-sql-edge </br>
para acabar de configurar el contenedor:</br>
docker run -d --name MsSQLServer -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=r00t.R00T" -p 1433:1433 mcr.microsoft.com/azure-sql-edge


<h3>Scripts de configuración de la bbdd</h3>

USE [master]
GO

/****** Object:  Database [PruebaRA]    Script Date: 23/12/2022 13:22:29 ******/

/*******N'/var/opt/mssql/data/PruebaRA.mdf' cambiar por la ruta que queremos usar para la bbdd******/
/*******N'/var/opt/mssql/data/PruebaRA_log.ldf'  cambiar por la ruta que queremos usar para la bbdd******/
CREATE DATABASE [PruebaRA]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PruebaRA', FILENAME = N'/var/opt/mssql/data/PruebaRA.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PruebaRA_log', FILENAME = N'/var/opt/mssql/data/PruebaRA_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PruebaRA].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [PruebaRA] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [PruebaRA] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [PruebaRA] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [PruebaRA] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [PruebaRA] SET ARITHABORT OFF 
GO

ALTER DATABASE [PruebaRA] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [PruebaRA] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [PruebaRA] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [PruebaRA] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [PruebaRA] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [PruebaRA] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [PruebaRA] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [PruebaRA] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [PruebaRA] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [PruebaRA] SET  DISABLE_BROKER 
GO

ALTER DATABASE [PruebaRA] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [PruebaRA] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [PruebaRA] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [PruebaRA] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [PruebaRA] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [PruebaRA] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [PruebaRA] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [PruebaRA] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [PruebaRA] SET  MULTI_USER 
GO

ALTER DATABASE [PruebaRA] SET PAGE_VERIFY NONE  
GO

ALTER DATABASE [PruebaRA] SET DB_CHAINING OFF 
GO

ALTER DATABASE [PruebaRA] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [PruebaRA] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [PruebaRA] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [PruebaRA] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [PruebaRA] SET DATA_RETENTION ON 
GO

ALTER DATABASE [PruebaRA] SET QUERY_STORE = OFF
GO

ALTER DATABASE [PruebaRA] SET  READ_WRITE 
GO




añadir usuario de la bbdd </br>
USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [test]    Script Date: 23/12/2022 13:23:01 ******/
CREATE LOGIN [test] WITH PASSWORD=N'wVduhiBvkELLfiX1BZ87/D4eDhDVpcrx4bocD+Gla4U=', DEFAULT_DATABASE=[PruebaRA], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [test] DISABLE
GO

ALTER SERVER ROLE [sysadmin] ADD MEMBER [test]
GO

<h3>Configuración de la bbdd en el proyecto</h3>
En el archivo appsettings.json hay que configurar la cadena de conexión al servidor instalado anteriormente.
