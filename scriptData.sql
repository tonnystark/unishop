USE [UNISHOP]
GO
/****** Object:  Table [dbo].[Footers]    Script Date: 20/04/2017 2:31:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Footers](
	[ID] [varchar](50) NOT NULL,
	[Content] [nvarchar](max) NULL,
 CONSTRAINT [PK_Footers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MenuGroup]    Script Date: 20/04/2017 2:31:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuGroup](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](250) NULL,
 CONSTRAINT [PK_MenuGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Menus]    Script Date: 20/04/2017 2:31:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Menus](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[URL] [nvarchar](500) NULL,
	[DisplayOrder] [int] NULL,
	[GroupID] [int] NULL,
	[Target] [varchar](10) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 20/04/2017 2:31:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orders]    Script Date: 20/04/2017 2:31:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Orders](
	[ID] [int] NOT NULL,
	[CustomerName] [nvarchar](250) NOT NULL,
	[CustomerAddress] [nvarchar](250) NOT NULL,
	[CustomerEmail] [nvarchar](250) NOT NULL,
	[CustomerMobile] [varchar](50) NOT NULL,
	[CustomerMessage] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[PaymentMethod] [nvarchar](250) NULL,
	[PaymentStatus] [nvarchar](250) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pages]    Script Date: 20/04/2017 2:31:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pages](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Content] [nvarchar](max) NULL,
	[MetaKeyword] [nvarchar](250) NULL,
	[MetaDescription] [nvarchar](250) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PostCategories]    Script Date: 20/04/2017 2:31:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostCategories](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Alias] [nvarchar](250) NOT NULL,
	[ParentID] [int] NULL,
	[DisplayOrder] [int] NULL,
	[Image] [nvarchar](500) NULL,
	[MetaKeyword] [nvarchar](250) NULL,
	[MetaDescription] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[Status] [bit] NOT NULL,
	[HomeFlag] [bit] NULL,
 CONSTRAINT [PK_PostCategories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Posts]    Script Date: 20/04/2017 2:31:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Alias] [nvarchar](250) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[Image] [nvarchar](500) NULL,
	[Description] [nvarchar](500) NULL,
	[Content] [nvarchar](max) NULL,
	[MetaKeyword] [nvarchar](250) NULL,
	[MetaDescription] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[Status] [bit] NOT NULL,
	[HomeFlag] [bit] NULL,
	[HotFlag] [bit] NULL,
	[ViewCount] [int] NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PostTags]    Script Date: 20/04/2017 2:31:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PostTags](
	[PostID] [int] NOT NULL,
	[TagID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_PostTags] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC,
	[TagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductCategories]    Script Date: 20/04/2017 2:31:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategories](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Alias] [nvarchar](250) NOT NULL,
	[ParentID] [int] NULL,
	[DisplayOrder] [int] NULL,
	[Image] [nvarchar](500) NULL,
	[MetaKeyword] [nvarchar](250) NULL,
	[MetaDescription] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[Status] [bit] NOT NULL,
	[HomeFlag] [bit] NULL,
 CONSTRAINT [PK_ProductCategories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 20/04/2017 2:31:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Alias] [nvarchar](250) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[Image] [nvarchar](500) NULL,
	[MoreImage] [xml] NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Promotion] [decimal](18, 2) NULL,
	[Warranty] [int] NULL,
	[Description] [nvarchar](500) NULL,
	[Content] [nvarchar](max) NULL,
	[MetaKeyword] [nvarchar](250) NULL,
	[MetaDescription] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[Status] [bit] NOT NULL,
	[HomeFlag] [bit] NULL,
	[HotFlag] [bit] NULL,
	[ViewCount] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductTags]    Script Date: 20/04/2017 2:31:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductTags](
	[ProductID] [int] NOT NULL,
	[TagID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ProductTags] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC,
	[TagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Slides]    Script Date: 20/04/2017 2:31:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Slides](
	[ID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Image] [nvarchar](500) NOT NULL,
	[URL] [nvarchar](500) NOT NULL,
	[DisplayOrder] [int] NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Slides] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SupportOnline]    Script Date: 20/04/2017 2:31:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupportOnline](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Department] [nvarchar](250) NULL,
	[Skype] [nvarchar](250) NULL,
	[Mobile] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NULL,
	[Yahoo] [nvarchar](250) NULL,
	[Facebook] [nvarchar](250) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_SupportOnline] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SystemConfigs]    Script Date: 20/04/2017 2:31:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SystemConfigs](
	[ID] [int] NOT NULL,
	[Code] [varchar](50) NULL,
	[ValueString] [nvarchar](250) NULL,
	[ValueInt] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 20/04/2017 2:31:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tags](
	[ID] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Type] [varchar](50) NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VisitorStatistics]    Script Date: 20/04/2017 2:31:02 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VisitorStatistics](
	[ID] [uniqueidentifier] NOT NULL,
	[VisitedDate] [datetime] NOT NULL,
	[IPAdress] [varchar](50) NOT NULL,
 CONSTRAINT [PK_VisitorStatistics] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[MenuGroup]  WITH CHECK ADD  CONSTRAINT [FK_MenuGroup_Menus] FOREIGN KEY([ID])
REFERENCES [dbo].[Menus] ([ID])
GO
ALTER TABLE [dbo].[MenuGroup] CHECK CONSTRAINT [FK_MenuGroup_Menus]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([ID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Products]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_PostCategories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[PostCategories] ([ID])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_PostCategories]
GO
ALTER TABLE [dbo].[PostTags]  WITH CHECK ADD  CONSTRAINT [FK_PostTags_Posts] FOREIGN KEY([PostID])
REFERENCES [dbo].[Posts] ([ID])
GO
ALTER TABLE [dbo].[PostTags] CHECK CONSTRAINT [FK_PostTags_Posts]
GO
ALTER TABLE [dbo].[PostTags]  WITH CHECK ADD  CONSTRAINT [FK_PostTags_Tags] FOREIGN KEY([TagID])
REFERENCES [dbo].[Tags] ([ID])
GO
ALTER TABLE [dbo].[PostTags] CHECK CONSTRAINT [FK_PostTags_Tags]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductCategories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[ProductCategories] ([ID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_ProductCategories]
GO
ALTER TABLE [dbo].[ProductTags]  WITH CHECK ADD  CONSTRAINT [FK_ProductTags_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ID])
GO
ALTER TABLE [dbo].[ProductTags] CHECK CONSTRAINT [FK_ProductTags_Products]
GO
ALTER TABLE [dbo].[ProductTags]  WITH CHECK ADD  CONSTRAINT [FK_ProductTags_Tags] FOREIGN KEY([TagID])
REFERENCES [dbo].[Tags] ([ID])
GO
ALTER TABLE [dbo].[ProductTags] CHECK CONSTRAINT [FK_ProductTags_Tags]
GO
