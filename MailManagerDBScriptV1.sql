USE [MailManager]
GO
/****** Object:  Table [dbo].[MailAttachments]    Script Date: 10/26/2019 12:16:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailAttachments](
	[AttachmentId] [int] IDENTITY(1,1) NOT NULL,
	[MailId] [int] NULL,
	[Attachment] [varchar](50) NULL,
 CONSTRAINT [PK_MailAttachments] PRIMARY KEY CLUSTERED 
(
	[AttachmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mails]    Script Date: 10/26/2019 12:16:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mails](
	[MailId] [int] IDENTITY(1,1) NOT NULL,
	[MailDate] [datetime] NULL,
	[FromEmail] [varchar](50) NULL,
	[ToEmail] [varchar](max) NULL,
	[Subject] [varchar](250) NULL,
	[Message] [nvarchar](max) NULL,
	[IsAttachmentPresent] [bit] NULL,
	[EmailStatus] [varchar](10) NULL,
	[MailFolder] [varchar](10) NULL,
	[IsRead] [bit] NULL,
 CONSTRAINT [PK_Mails] PRIMARY KEY CLUSTERED 
(
	[MailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAccounts]    Script Date: 10/26/2019 12:16:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccounts](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[EmailId] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_UserAccounts] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[MailAttachments] ON 

INSERT [dbo].[MailAttachments] ([AttachmentId], [MailId], [Attachment]) VALUES (31, 83, N'image1.jpg')
INSERT [dbo].[MailAttachments] ([AttachmentId], [MailId], [Attachment]) VALUES (32, 83, N'image2.png')
INSERT [dbo].[MailAttachments] ([AttachmentId], [MailId], [Attachment]) VALUES (34, 84, N'image1.jpg')
INSERT [dbo].[MailAttachments] ([AttachmentId], [MailId], [Attachment]) VALUES (35, 84, N'image2.png')
INSERT [dbo].[MailAttachments] ([AttachmentId], [MailId], [Attachment]) VALUES (36, 102, N'image1.jpg')
INSERT [dbo].[MailAttachments] ([AttachmentId], [MailId], [Attachment]) VALUES (37, 102, N'image2.png')
INSERT [dbo].[MailAttachments] ([AttachmentId], [MailId], [Attachment]) VALUES (38, 103, N'image1.jpg')
INSERT [dbo].[MailAttachments] ([AttachmentId], [MailId], [Attachment]) VALUES (39, 103, N'image2.png')
INSERT [dbo].[MailAttachments] ([AttachmentId], [MailId], [Attachment]) VALUES (40, 104, N'image1.jpg')
INSERT [dbo].[MailAttachments] ([AttachmentId], [MailId], [Attachment]) VALUES (41, 104, N'image2.png')
INSERT [dbo].[MailAttachments] ([AttachmentId], [MailId], [Attachment]) VALUES (42, 105, N'image1.jpg')
INSERT [dbo].[MailAttachments] ([AttachmentId], [MailId], [Attachment]) VALUES (43, 105, N'image2.png')
SET IDENTITY_INSERT [dbo].[MailAttachments] OFF
SET IDENTITY_INSERT [dbo].[Mails] ON 

INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (83, CAST(N'2019-10-25T23:01:43.440' AS DateTime), N'sanjota.k@gmail.com', N'anand.kawatekar@gmail.com', N'Hello', N'Hi,

How are you', 0, N'SENT', N'TRASH', 0)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (84, CAST(N'2019-10-25T23:01:43.423' AS DateTime), N'sanjota.k@gmail.com', N'anand.kawatekar@gmail.com', N'Hello', N'Hi,

How are you', 0, N'RECEIVED', N'INBOX', 1)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (85, CAST(N'2019-10-25T23:02:12.440' AS DateTime), N'sanjota.k@gmail.com', N'vivek.g@gmail.com', N'testdraft', N'', 0, N'DRAFT', N'DRAFT', 0)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (86, CAST(N'2019-10-25T23:05:30.807' AS DateTime), N'anand.kawatekar@gmail.com', N'sanjota.k@gmail.com', N'Re:Hello', N'Hello Sanjota,

I am fine, how are you doing

---On 2019-10-25T23:01:43.423, sanjota.k@gmail.com Wrote:
  Hi,

How are you', 0, N'SENT', N'SENT', 0)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (87, CAST(N'2019-10-25T23:05:30.800' AS DateTime), N'anand.kawatekar@gmail.com', N'sanjota.k@gmail.com', N'Re:Hello', N'Hello Sanjota,

I am fine, how are you doing

---On 2019-10-25T23:01:43.423, sanjota.k@gmail.com Wrote:
  Hi,

How are you', 0, N'RECEIVED', N'TRASH', 1)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (88, CAST(N'2019-10-25T23:20:18.540' AS DateTime), N'sanjota.k@gmail.com', N'testmail@gmail.com', N'', N'', 0, N'DRAFT', N'TRASH', 0)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (89, CAST(N'2019-10-25T23:47:28.370' AS DateTime), N'anand.kawatekar@gmail.com', N'sadanand.k@gmail.com', N'draft test', N'Hi', 0, N'DRAFT', N'DRAFT', 0)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (90, CAST(N'2019-10-25T23:48:40.930' AS DateTime), N'anand.kawatekar@gmail.com', N'vivek.g@gmail.com', N'test for trash', N'Hi vivek,

How are you?', 0, N'DRAFT', N'TRASH', 0)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (91, CAST(N'2019-10-25T23:50:12.113' AS DateTime), N'anand.kawatekar@gmail.com', N'testmail@gmail.com,preetam@gmail.com', N'multiple mail test', N'Hi test', 0, N'SENT', N'SENT', 0)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (92, CAST(N'2019-10-25T23:50:12.107' AS DateTime), N'anand.kawatekar@gmail.com', N'testmail@gmail.com', N'multiple mail test', N'Hi test', 0, N'RECEIVED', N'INBOX', 0)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (93, CAST(N'2019-10-25T23:50:12.147' AS DateTime), N'anand.kawatekar@gmail.com', N'preetam@gmail.com', N'multiple mail test', N'Hi test', 0, N'RECEIVED', N'INBOX', 0)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (94, CAST(N'2019-10-25T23:51:56.417' AS DateTime), N'testmail@gmail.com', N'sujata.t@gmail.com', N'Hi', N'Hi  Sujata,

Reply if you get this mail', 0, N'SENT', N'SENT', 0)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (95, CAST(N'2019-10-25T23:51:56.410' AS DateTime), N'testmail@gmail.com', N'sujata.t@gmail.com', N'Hi', N'Hi  Sujata,

Reply if you get this mail', 0, N'RECEIVED', N'INBOX', 1)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (96, CAST(N'2019-10-25T23:53:39.833' AS DateTime), N'aravind.k@gmail.com', N'anand.kawatekar@gmail.com', N'Hello Anand', N'Hey,

How are you?', 0, N'SENT', N'SENT', 0)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (97, CAST(N'2019-10-25T23:53:39.827' AS DateTime), N'aravind.k@gmail.com', N'anand.kawatekar@gmail.com', N'Hello Anand', N'Hey,

How are you?', 0, N'RECEIVED', N'INBOX', 0)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (98, CAST(N'2019-10-25T23:54:40.477' AS DateTime), N'sujata.t@gmail.com', N'testmail@gmail.com', N'Re:Hi', N'Hello,

thanks, got your mail

---On 2019-10-25T23:51:56.41, testmail@gmail.com Wrote:
  Hi  Sujata,

Reply if you get this mail', 0, N'SENT', N'SENT', 0)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (99, CAST(N'2019-10-25T23:54:40.473' AS DateTime), N'sujata.t@gmail.com', N'testmail@gmail.com', N'Re:Hi', N'Hello,

thanks, got your mail

---On 2019-10-25T23:51:56.41, testmail@gmail.com Wrote:
  Hi  Sujata,

Reply if you get this mail', 0, N'RECEIVED', N'INBOX', 0)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (100, CAST(N'2019-10-25T23:55:54.117' AS DateTime), N'sujata.t@gmail.com', N'anand.kawatekar@gmail.com', N'Fr:Hi', N'Hi Anand,
Here is the forwarded mail.

-----Forwarded Message-----
From: testmail@gmail.com
Date: 2019-10-25T23:51:56.41
To: sujata.t@gmail.com
  Hi  Sujata,

Reply if you get this mail', 0, N'SENT', N'SENT', 0)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (101, CAST(N'2019-10-25T23:55:54.110' AS DateTime), N'sujata.t@gmail.com', N'anand.kawatekar@gmail.com', N'Fr:Hi', N'Hi Anand,
Here is the forwarded mail.

-----Forwarded Message-----
From: testmail@gmail.com
Date: 2019-10-25T23:51:56.41
To: sujata.t@gmail.com
  Hi  Sujata,

Reply if you get this mail', 0, N'RECEIVED', N'INBOX', 0)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (102, CAST(N'2019-10-26T00:03:48.060' AS DateTime), N'anand.kawatekar@gmail.com', N'uma.k@gmail.com', N'Test mail with attachment', N'Hi,

PFA', 0, N'SENT', N'SENT', 0)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (103, CAST(N'2019-10-26T00:03:48.047' AS DateTime), N'anand.kawatekar@gmail.com', N'uma.k@gmail.com', N'Test mail with attachment', N'Hi,

PFA', 0, N'RECEIVED', N'INBOX', 0)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (104, CAST(N'2019-10-26T00:09:47.587' AS DateTime), N'anand.kawatekar@gmail.com', N'sadanand.k@gmail.com', N'Fr:Hello', N'Hi
test forwarded mail

-----Forwarded Message-----
From: sanjota.k@gmail.com
Date: 2019-10-25T23:01:43.423
To: anand.kawatekar@gmail.com
  Hi,

How are you', 0, N'SENT', N'SENT', 0)
INSERT [dbo].[Mails] ([MailId], [MailDate], [FromEmail], [ToEmail], [Subject], [Message], [IsAttachmentPresent], [EmailStatus], [MailFolder], [IsRead]) VALUES (105, CAST(N'2019-10-26T00:09:47.570' AS DateTime), N'anand.kawatekar@gmail.com', N'sadanand.k@gmail.com', N'Fr:Hello', N'Hi
test forwarded mail

-----Forwarded Message-----
From: sanjota.k@gmail.com
Date: 2019-10-25T23:01:43.423
To: anand.kawatekar@gmail.com
  Hi,

How are you', 0, N'RECEIVED', N'INBOX', 0)
SET IDENTITY_INSERT [dbo].[Mails] OFF
SET IDENTITY_INSERT [dbo].[UserAccounts] ON 

INSERT [dbo].[UserAccounts] ([UserId], [FirstName], [LastName], [EmailId], [Password], [CreatedDate]) VALUES (1, N'Anand ', N'Kawatekar', N'anand.kawatekar@gmail.com', N'1234', NULL)
INSERT [dbo].[UserAccounts] ([UserId], [FirstName], [LastName], [EmailId], [Password], [CreatedDate]) VALUES (2, N'TestFirst', N'TestLast', N'testmail@gmail.com', N'1234', NULL)
INSERT [dbo].[UserAccounts] ([UserId], [FirstName], [LastName], [EmailId], [Password], [CreatedDate]) VALUES (3, N'Sadanand', N'Kkawatekar', N'sadanand.k@gmail.com', N'1234', NULL)
INSERT [dbo].[UserAccounts] ([UserId], [FirstName], [LastName], [EmailId], [Password], [CreatedDate]) VALUES (4, N'Sanjota', N'Kawatekar', N'sanjota.k@gmail.com', N'1234', NULL)
INSERT [dbo].[UserAccounts] ([UserId], [FirstName], [LastName], [EmailId], [Password], [CreatedDate]) VALUES (5, N'Vivek', N'G', N'vivek.g@gmail.com', N'1234', NULL)
INSERT [dbo].[UserAccounts] ([UserId], [FirstName], [LastName], [EmailId], [Password], [CreatedDate]) VALUES (6, N'Manjunath', N'Tungal', N'manju.t@gmail.com', N'1234', NULL)
INSERT [dbo].[UserAccounts] ([UserId], [FirstName], [LastName], [EmailId], [Password], [CreatedDate]) VALUES (7, N'Uma', N'Kawatekar', N'uma.k@gmail.com', N'1234', NULL)
INSERT [dbo].[UserAccounts] ([UserId], [FirstName], [LastName], [EmailId], [Password], [CreatedDate]) VALUES (8, N'sujata', N'T', N'sujata.t@gmail.com', N'1234', NULL)
INSERT [dbo].[UserAccounts] ([UserId], [FirstName], [LastName], [EmailId], [Password], [CreatedDate]) VALUES (9, N'preetam', N'T', N'preetam@gmail.com', N'1234', NULL)
INSERT [dbo].[UserAccounts] ([UserId], [FirstName], [LastName], [EmailId], [Password], [CreatedDate]) VALUES (10, N'Aravind', N'Kawatekar', N'aravind.k@gmail.com', N'1234', NULL)
SET IDENTITY_INSERT [dbo].[UserAccounts] OFF
