USE [master]
GO
/****** Object:  Database [AnycmdMessage]    Script Date: 2014/12/23 8:00:42 ******/
CREATE DATABASE [AnycmdMessage]
GO
ALTER DATABASE [AnycmdMessage] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AnycmdMessage].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AnycmdMessage] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AnycmdMessage] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AnycmdMessage] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AnycmdMessage] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AnycmdMessage] SET ARITHABORT OFF 
GO
ALTER DATABASE [AnycmdMessage] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AnycmdMessage] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [AnycmdMessage] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AnycmdMessage] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AnycmdMessage] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AnycmdMessage] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AnycmdMessage] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AnycmdMessage] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AnycmdMessage] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AnycmdMessage] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AnycmdMessage] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AnycmdMessage] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AnycmdMessage] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AnycmdMessage] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AnycmdMessage] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AnycmdMessage] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AnycmdMessage] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AnycmdMessage] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AnycmdMessage] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AnycmdMessage] SET  MULTI_USER 
GO
ALTER DATABASE [AnycmdMessage] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AnycmdMessage] SET DB_CHAINING OFF 
GO
EXEC sys.sp_db_vardecimal_storage_format N'AnycmdMessage', N'ON'
GO
USE [AnycmdMessage]
GO
/****** Object:  Table [dbo].[ClientEvent]    Script Date: 2014/12/23 8:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ClientEvent](
	[Id] [uniqueidentifier] NOT NULL,
	[MessageId] [varchar](50) NOT NULL,
	[MessageType] [varchar](50) NOT NULL,
	[EventSourceType] [varchar](50) NULL,
	[EventSubjectCode] [varchar](50) NULL,
	[ClientType] [varchar](50) NOT NULL,
	[ClientId] [varchar](36) NOT NULL,
	[Verb] [varchar](20) NOT NULL,
	[Ontology] [varchar](20) NOT NULL,
	[CatalogCode] [varchar](50) NULL,
	[LocalEntityId] [varchar](36) NOT NULL,
	[InfoFormat] [varchar](10) NOT NULL,
	[InfoId] [nvarchar](max) NOT NULL,
	[InfoValue] [nvarchar](max) NULL,
	[ReceivedOn] [datetime] NOT NULL,
	[CreateOn] [datetime] NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[ReasonPhrase] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[UserName] [varchar](50) NULL,
	[IsDumb] [bit] NOT NULL,
	[Version] [varchar](10) NOT NULL,
	[QueryList] [varchar](500) NULL,
 CONSTRAINT [PK_ClientEvent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DistributedMessage]    Script Date: 2014/12/23 8:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DistributedMessage](
	[Id] [uniqueidentifier] NOT NULL,
	[MessageId] [varchar](50) NOT NULL,
	[MessageType] [varchar](50) NOT NULL,
	[EventSourceType] [varchar](50) NULL,
	[EventSubjectCode] [varchar](50) NULL,
	[ClientType] [varchar](50) NOT NULL,
	[ClientId] [varchar](36) NOT NULL,
	[Verb] [varchar](20) NOT NULL,
	[Ontology] [varchar](20) NOT NULL,
	[CatalogCode] [varchar](50) NULL,
	[LocalEntityId] [varchar](36) NOT NULL,
	[InfoFormat] [varchar](10) NOT NULL,
	[InfoId] [nvarchar](max) NOT NULL,
	[InfoValue] [nvarchar](max) NULL,
	[ReceivedOn] [datetime] NOT NULL,
	[CreateOn] [datetime] NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[ReasonPhrase] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[UserName] [varchar](50) NULL,
	[IsDumb] [bit] NOT NULL,
	[Version] [varchar](10) NOT NULL,
	[QueryList] [varchar](500) NULL,
 CONSTRAINT [PK_DistributedMessage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DistributeFailingMessage]    Script Date: 2014/12/23 8:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DistributeFailingMessage](
	[Id] [uniqueidentifier] NOT NULL,
	[MessageId] [varchar](50) NOT NULL,
	[MessageType] [varchar](50) NOT NULL,
	[EventSourceType] [varchar](50) NULL,
	[EventSubjectCode] [varchar](50) NULL,
	[ClientType] [varchar](50) NOT NULL,
	[ClientId] [varchar](36) NOT NULL,
	[Verb] [varchar](20) NOT NULL,
	[Ontology] [varchar](20) NOT NULL,
	[CatalogCode] [varchar](50) NULL,
	[LocalEntityId] [varchar](36) NOT NULL,
	[InfoFormat] [varchar](10) NOT NULL,
	[InfoId] [nvarchar](max) NOT NULL,
	[InfoValue] [nvarchar](max) NULL,
	[ReceivedOn] [datetime] NOT NULL,
	[CreateOn] [datetime] NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[ReasonPhrase] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[UserName] [varchar](50) NULL,
	[IsDumb] [bit] NOT NULL,
	[Version] [varchar](10) NOT NULL,
	[QueryList] [varchar](500) NULL,
 CONSTRAINT [PK_DistributeFailingMessage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DistributeMessage]    Script Date: 2014/12/23 8:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DistributeMessage](
	[Id] [uniqueidentifier] NOT NULL,
	[MessageId] [varchar](50) NOT NULL,
	[MessageType] [varchar](50) NOT NULL,
	[EventSourceType] [varchar](50) NULL,
	[EventSubjectCode] [varchar](50) NULL,
	[ClientType] [varchar](50) NOT NULL,
	[ClientId] [varchar](36) NOT NULL,
	[Verb] [varchar](20) NOT NULL,
	[Ontology] [varchar](20) NOT NULL,
	[CatalogCode] [varchar](50) NULL,
	[LocalEntityId] [varchar](36) NOT NULL,
	[InfoFormat] [varchar](10) NOT NULL,
	[InfoId] [nvarchar](max) NOT NULL,
	[InfoValue] [nvarchar](max) NULL,
	[ReceivedOn] [datetime] NOT NULL,
	[CreateOn] [datetime] NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[ReasonPhrase] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[UserName] [varchar](50) NULL,
	[IsDumb] [bit] NOT NULL,
	[Version] [varchar](10) NOT NULL,
	[QueryList] [varchar](500) NULL,
 CONSTRAINT [PK_DistributeMessage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HandledCommand]    Script Date: 2014/12/23 8:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HandledCommand](
	[Id] [uniqueidentifier] NOT NULL,
	[MessageId] [varchar](50) NOT NULL,
	[MessageType] [varchar](50) NOT NULL,
	[EventSourceType] [varchar](50) NULL,
	[EventSubjectCode] [varchar](50) NULL,
	[ClientType] [varchar](50) NOT NULL,
	[ClientId] [varchar](36) NOT NULL,
	[Verb] [varchar](20) NOT NULL,
	[Ontology] [varchar](20) NOT NULL,
	[CatalogCode] [varchar](50) NULL,
	[LocalEntityId] [varchar](36) NOT NULL,
	[InfoFormat] [varchar](10) NOT NULL,
	[InfoId] [nvarchar](max) NOT NULL,
	[InfoValue] [nvarchar](max) NULL,
	[ReceivedOn] [datetime] NOT NULL,
	[CreateOn] [datetime] NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[ReasonPhrase] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[UserName] [varchar](50) NULL,
	[IsDumb] [bit] NOT NULL,
	[Version] [varchar](10) NOT NULL,
	[QueryList] [varchar](500) NULL,
 CONSTRAINT [PK_HandledCommand] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HandleFailingCommand]    Script Date: 2014/12/23 8:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HandleFailingCommand](
	[Id] [uniqueidentifier] NOT NULL,
	[MessageId] [varchar](50) NOT NULL,
	[MessageType] [varchar](50) NOT NULL,
	[EventSourceType] [varchar](50) NULL,
	[EventSubjectCode] [varchar](50) NULL,
	[ClientType] [varchar](50) NOT NULL,
	[ClientId] [varchar](36) NOT NULL,
	[Verb] [varchar](20) NOT NULL,
	[Ontology] [varchar](20) NOT NULL,
	[CatalogCode] [varchar](50) NULL,
	[LocalEntityId] [varchar](36) NOT NULL,
	[InfoFormat] [varchar](10) NOT NULL,
	[InfoId] [nvarchar](max) NOT NULL,
	[InfoValue] [nvarchar](max) NULL,
	[ReceivedOn] [datetime] NOT NULL,
	[CreateOn] [datetime] NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[ReasonPhrase] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[UserName] [varchar](50) NULL,
	[IsDumb] [bit] NOT NULL,
	[Version] [varchar](10) NOT NULL,
	[QueryList] [varchar](500) NULL,
 CONSTRAINT [PK_HandleFailingCommand] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LocalEvent]    Script Date: 2014/12/23 8:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LocalEvent](
	[Id] [uniqueidentifier] NOT NULL,
	[MessageId] [varchar](50) NOT NULL,
	[MessageType] [varchar](50) NOT NULL,
	[EventSourceType] [varchar](50) NULL,
	[EventSubjectCode] [varchar](50) NULL,
	[ClientType] [varchar](50) NOT NULL,
	[ClientId] [varchar](36) NOT NULL,
	[Verb] [varchar](20) NOT NULL,
	[Ontology] [varchar](20) NOT NULL,
	[CatalogCode] [varchar](50) NULL,
	[LocalEntityId] [varchar](36) NOT NULL,
	[InfoFormat] [varchar](10) NOT NULL,
	[InfoId] [nvarchar](max) NOT NULL,
	[InfoValue] [nvarchar](max) NULL,
	[ReceivedOn] [datetime] NOT NULL,
	[CreateOn] [datetime] NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[ReasonPhrase] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[UserName] [varchar](50) NULL,
	[IsDumb] [bit] NOT NULL,
	[Version] [varchar](10) NOT NULL,
	[QueryList] [varchar](500) NULL,
 CONSTRAINT [PK_LocalEvent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ReceivedMessage]    Script Date: 2014/12/23 8:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReceivedMessage](
	[Id] [uniqueidentifier] NOT NULL,
	[MessageId] [varchar](50) NOT NULL,
	[MessageType] [varchar](50) NOT NULL,
	[EventSourceType] [varchar](50) NULL,
	[EventSubjectCode] [varchar](50) NULL,
	[ClientType] [varchar](50) NOT NULL,
	[ClientId] [varchar](36) NOT NULL,
	[Verb] [varchar](20) NOT NULL,
	[Ontology] [varchar](20) NOT NULL,
	[CatalogCode] [varchar](50) NULL,
	[LocalEntityId] [varchar](36) NOT NULL,
	[InfoFormat] [varchar](10) NOT NULL,
	[InfoId] [nvarchar](max) NOT NULL,
	[InfoValue] [nvarchar](max) NULL,
	[ReceivedOn] [datetime] NOT NULL,
	[CreateOn] [datetime] NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[ReasonPhrase] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[UserName] [varchar](50) NULL,
	[IsDumb] [bit] NOT NULL,
	[Version] [varchar](10) NOT NULL,
	[QueryList] [varchar](500) NULL,
 CONSTRAINT [PK_ReceivedMessage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UnacceptedMessage]    Script Date: 2014/12/23 8:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UnacceptedMessage](
	[Id] [uniqueidentifier] NOT NULL,
	[MessageId] [varchar](50) NOT NULL,
	[MessageType] [varchar](50) NOT NULL,
	[EventSourceType] [varchar](50) NULL,
	[EventSubjectCode] [varchar](50) NULL,
	[ClientType] [varchar](50) NOT NULL,
	[ClientId] [varchar](36) NOT NULL,
	[Verb] [varchar](20) NOT NULL,
	[Ontology] [varchar](20) NOT NULL,
	[CatalogCode] [varchar](50) NULL,
	[LocalEntityId] [varchar](36) NOT NULL,
	[InfoFormat] [varchar](10) NOT NULL,
	[InfoId] [nvarchar](max) NOT NULL,
	[InfoValue] [nvarchar](max) NULL,
	[ReceivedOn] [datetime] NOT NULL,
	[CreateOn] [datetime] NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[ReasonPhrase] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[UserName] [varchar](50) NULL,
	[IsDumb] [bit] NOT NULL,
	[Version] [varchar](10) NOT NULL,
	[QueryList] [varchar](500) NULL,
 CONSTRAINT [PK_UnacceptedMessage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[ClientEvent] ADD  CONSTRAINT [DF_ClientEvent_IsDumb]  DEFAULT ((0)) FOR [IsDumb]
GO
ALTER TABLE [dbo].[ClientEvent] ADD  CONSTRAINT [DF_ClientEvent_Version]  DEFAULT ('v1') FOR [Version]
GO
ALTER TABLE [dbo].[DistributedMessage] ADD  CONSTRAINT [DF_DistributedCommand_IsDumb]  DEFAULT ((0)) FOR [IsDumb]
GO
ALTER TABLE [dbo].[DistributedMessage] ADD  CONSTRAINT [DF_DistributedCommand_Version]  DEFAULT ('v1') FOR [Version]
GO
ALTER TABLE [dbo].[DistributeFailingMessage] ADD  CONSTRAINT [DF_DistributeFailingCommand_IsDumb]  DEFAULT ((0)) FOR [IsDumb]
GO
ALTER TABLE [dbo].[DistributeFailingMessage] ADD  CONSTRAINT [DF_DistributeFailingCommand_Version]  DEFAULT ('v1') FOR [Version]
GO
ALTER TABLE [dbo].[DistributeMessage] ADD  CONSTRAINT [DF_DistributeCommand_IsDumb]  DEFAULT ((0)) FOR [IsDumb]
GO
ALTER TABLE [dbo].[DistributeMessage] ADD  CONSTRAINT [DF_DistributeCommand_Version]  DEFAULT ('v1') FOR [Version]
GO
ALTER TABLE [dbo].[HandledCommand] ADD  CONSTRAINT [DF_ExecutedCommand_IsDumb]  DEFAULT ((0)) FOR [IsDumb]
GO
ALTER TABLE [dbo].[HandledCommand] ADD  CONSTRAINT [DF_ExecutedCommand_Version]  DEFAULT ('v1') FOR [Version]
GO
ALTER TABLE [dbo].[HandleFailingCommand] ADD  CONSTRAINT [DF_ExecuteFailingCommand_IsDumb]  DEFAULT ((0)) FOR [IsDumb]
GO
ALTER TABLE [dbo].[HandleFailingCommand] ADD  CONSTRAINT [DF_ExecuteFailingCommand_Version]  DEFAULT ('v1') FOR [Version]
GO
ALTER TABLE [dbo].[LocalEvent] ADD  CONSTRAINT [DF_LocalEvent_IsDumb]  DEFAULT ((0)) FOR [IsDumb]
GO
ALTER TABLE [dbo].[LocalEvent] ADD  CONSTRAINT [DF_LocalEvent_Version]  DEFAULT ('v1') FOR [Version]
GO
ALTER TABLE [dbo].[ReceivedMessage] ADD  CONSTRAINT [DF_ReceivedCommand_IsDumb]  DEFAULT ((0)) FOR [IsDumb]
GO
ALTER TABLE [dbo].[ReceivedMessage] ADD  CONSTRAINT [DF_ReceivedCommand_Version]  DEFAULT ('v1') FOR [Version]
GO
ALTER TABLE [dbo].[UnacceptedMessage] ADD  CONSTRAINT [DF_ReceiveFailingCommand_IsDumb]  DEFAULT ((0)) FOR [IsDumb]
GO
ALTER TABLE [dbo].[UnacceptedMessage] ADD  CONSTRAINT [DF_ReceiveFailingCommand_Version]  DEFAULT ('v1') FOR [Version]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'MessageId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'MessageType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件源类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'EventSourceType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件主题码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'EventSubjectCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'ClientType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'ClientId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'Verb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本体码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'Ontology'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'CatalogCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本地数据标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'LocalEntityId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息格式。如：json、xml' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'InfoFormat'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'InfoId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'InfoValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'ReceivedOn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'CreateOn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命令时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'TimeStamp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'原因短语' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'ReasonPhrase'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命令状态描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是哑的' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'IsDumb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'api版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端事件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClientEvent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'MessageId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'MessageType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件源类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'EventSourceType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件主题码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'EventSubjectCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'ClientType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'ClientId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'Verb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本体码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'Ontology'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'CatalogCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本地数据标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'LocalEntityId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息格式。如：json、xml' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'InfoFormat'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'InfoId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'InfoValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'ReceivedOn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'CreateOn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命令时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'TimeStamp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'原因短语' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'ReasonPhrase'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命令状态描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是哑的' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'IsDumb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'api版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已成功分发的命令' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributedMessage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'MessageId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'MessageType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件源类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'EventSourceType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件主题码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'EventSubjectCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'ClientType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'ClientId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'Verb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本体码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'Ontology'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'CatalogCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本地数据标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'LocalEntityId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息格式。如：json、xml' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'InfoFormat'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'InfoId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'InfoValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'ReceivedOn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'CreateOn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命令时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'TimeStamp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'原因短语' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'ReasonPhrase'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命令状态描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是哑的' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'IsDumb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'api版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分发失败的命令' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeFailingMessage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'MessageId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'MessageType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件源类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'EventSourceType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件主题码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'EventSubjectCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'ClientType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'ClientId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'Verb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本体码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'Ontology'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'CatalogCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本地数据标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'LocalEntityId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息格式。如：json、xml' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'InfoFormat'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'InfoId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'InfoValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'ReceivedOn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'CreateOn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命令时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'TimeStamp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'原因短语' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'ReasonPhrase'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命令状态描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是哑的' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'IsDumb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'api版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'待分发的命令' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DistributeMessage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'MessageId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'MessageType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件源类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'EventSourceType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件主题码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'EventSubjectCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'ClientType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'ClientId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'Verb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本体码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'Ontology'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'CatalogCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本地数据标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'LocalEntityId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息格式。如：json、xml' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'InfoFormat'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'InfoId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'InfoValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'ReceivedOn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'CreateOn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命令时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'TimeStamp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'原因短语' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'ReasonPhrase'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命令状态描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是哑的' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'IsDumb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'api版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'成功执行的命令' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandledCommand'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'MessageId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'MessageType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件源类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'EventSourceType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件主题码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'EventSubjectCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'ClientType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'ClientId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'Verb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本体码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'Ontology'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'CatalogCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本地数据标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'LocalEntityId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息格式。如：json、xml' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'InfoFormat'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'InfoId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'InfoValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'ReceivedOn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'CreateOn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命令时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'TimeStamp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'原因短语' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'ReasonPhrase'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命令状态描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是哑的' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'IsDumb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'api版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'执行失败的命令' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HandleFailingCommand'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'MessageId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'MessageType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件源类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'EventSourceType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件主题码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'EventSubjectCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'ClientType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'ClientId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'Verb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本体码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'Ontology'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'CatalogCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本地数据标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'LocalEntityId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息格式。如：json、xml' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'InfoFormat'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'InfoId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'InfoValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'ReceivedOn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'CreateOn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命令时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'TimeStamp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'原因短语' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'ReasonPhrase'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命令状态描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是哑的' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'IsDumb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'api版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本地事件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalEvent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'MessageId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'MessageType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件源类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'EventSourceType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件主题码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'EventSubjectCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'ClientType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'ClientId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'Verb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本体码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'Ontology'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'CatalogCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本地数据标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'LocalEntityId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息格式。如：json、xml' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'InfoFormat'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'InfoId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'InfoValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'ReceivedOn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'CreateOn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命令时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'TimeStamp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'原因短语' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'ReasonPhrase'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命令状态描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是哑的' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'IsDumb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'api版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收成功的命令' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReceivedMessage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'MessageId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'MessageType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件源类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'EventSourceType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事件主题码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'EventSubjectCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'ClientType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'ClientId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'Verb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本体码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'Ontology'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'CatalogCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本地数据标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'LocalEntityId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息格式。如：json、xml' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'InfoFormat'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'InfoId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'InfoValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'ReceivedOn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'CreateOn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命令时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'TimeStamp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'原因短语' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'ReasonPhrase'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命令状态描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是哑的' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'IsDumb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'api版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收失败的命令' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UnacceptedMessage'
GO
USE [master]
GO
ALTER DATABASE [AnycmdMessage] SET  READ_WRITE 
GO
