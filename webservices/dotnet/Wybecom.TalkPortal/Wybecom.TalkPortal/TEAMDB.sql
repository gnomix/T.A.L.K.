/****** Object:  ForeignKey [FK_ACDManagerAddress_ACDManagerAddressGroup]    Script Date: 02/28/2011 15:42:52 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ACDManagerAddress_ACDManagerAddressGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[ACDManagerAddress]'))
ALTER TABLE [dbo].[ACDManagerAddress] DROP CONSTRAINT [FK_ACDManagerAddress_ACDManagerAddressGroup]
GO
/****** Object:  ForeignKey [FK_Calls_Calls]    Script Date: 02/28/2011 15:42:52 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calls_Calls]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calls]'))
ALTER TABLE [dbo].[Calls] DROP CONSTRAINT [FK_Calls_Calls]
GO
/****** Object:  ForeignKey [FK_Team_ACDManagerAddressGroup]    Script Date: 02/28/2011 15:42:52 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Team_ACDManagerAddressGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Team]'))
ALTER TABLE [dbo].[Team] DROP CONSTRAINT [FK_Team_ACDManagerAddressGroup]
GO
/****** Object:  ForeignKey [FK_TeamMemberTeam_Team]    Script Date: 02/28/2011 15:42:52 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TeamMemberTeam_Team]') AND parent_object_id = OBJECT_ID(N'[dbo].[TeamMemberTeam]'))
ALTER TABLE [dbo].[TeamMemberTeam] DROP CONSTRAINT [FK_TeamMemberTeam_Team]
GO
/****** Object:  ForeignKey [FK_TeamMemberTeam_TeamMember]    Script Date: 02/28/2011 15:42:52 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TeamMemberTeam_TeamMember]') AND parent_object_id = OBJECT_ID(N'[dbo].[TeamMemberTeam]'))
ALTER TABLE [dbo].[TeamMemberTeam] DROP CONSTRAINT [FK_TeamMemberTeam_TeamMember]
GO
/****** Object:  Table [dbo].[TeamMemberTeam]    Script Date: 02/28/2011 15:42:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TeamMemberTeam]') AND type in (N'U'))
DROP TABLE [dbo].[TeamMemberTeam]
GO
/****** Object:  Table [dbo].[Team]    Script Date: 02/28/2011 15:42:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Team]') AND type in (N'U'))
DROP TABLE [dbo].[Team]
GO
/****** Object:  Table [dbo].[ACDManagerAddress]    Script Date: 02/28/2011 15:42:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACDManagerAddress]') AND type in (N'U'))
DROP TABLE [dbo].[ACDManagerAddress]
GO
/****** Object:  Table [dbo].[Calls]    Script Date: 02/28/2011 15:42:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Calls]') AND type in (N'U'))
DROP TABLE [dbo].[Calls]
GO
/****** Object:  Table [dbo].[Codif]    Script Date: 02/28/2011 15:42:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Codif]') AND type in (N'U'))
DROP TABLE [dbo].[Codif]
GO
/****** Object:  Table [dbo].[ACDManagerAddressGroup]    Script Date: 02/28/2011 15:42:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACDManagerAddressGroup]') AND type in (N'U'))
DROP TABLE [dbo].[ACDManagerAddressGroup]
GO
/****** Object:  Table [dbo].[TeamMember]    Script Date: 02/28/2011 15:42:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TeamMember]') AND type in (N'U'))
DROP TABLE [dbo].[TeamMember]
GO
/****** Object:  Default [DF_ACDManagerAddress_acdmanageraddressid]    Script Date: 02/28/2011 15:42:52 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ACDManagerAddress_acdmanageraddressid]') AND parent_object_id = OBJECT_ID(N'[dbo].[ACDManagerAddress]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ACDManagerAddress_acdmanageraddressid]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ACDManagerAddress] DROP CONSTRAINT [DF_ACDManagerAddress_acdmanageraddressid]
END


End
GO
/****** Object:  Default [DF_ACDManagerAddressGroup_acdmanageraddressgroupid]    Script Date: 02/28/2011 15:42:52 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ACDManagerAddressGroup_acdmanageraddressgroupid]') AND parent_object_id = OBJECT_ID(N'[dbo].[ACDManagerAddressGroup]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ACDManagerAddressGroup_acdmanageraddressgroupid]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ACDManagerAddressGroup] DROP CONSTRAINT [DF_ACDManagerAddressGroup_acdmanageraddressgroupid]
END


End
GO
/****** Object:  Default [DF_Codif_active]    Script Date: 02/28/2011 15:42:52 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Codif_active]') AND parent_object_id = OBJECT_ID(N'[dbo].[Codif]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Codif_active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Codif] DROP CONSTRAINT [DF_Codif_active]
END


End
GO
/****** Object:  Default [DF_Team_teamid]    Script Date: 02/28/2011 15:42:52 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Team_teamid]') AND parent_object_id = OBJECT_ID(N'[dbo].[Team]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Team_teamid]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Team] DROP CONSTRAINT [DF_Team_teamid]
END


End
GO
/****** Object:  Default [DF_TeamMember_teammemberid]    Script Date: 02/28/2011 15:42:52 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TeamMember_teammemberid]') AND parent_object_id = OBJECT_ID(N'[dbo].[TeamMember]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TeamMember_teammemberid]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TeamMember] DROP CONSTRAINT [DF_TeamMember_teammemberid]
END


End
GO
/****** Object:  Default [DF_TeamMember_issupervisor]    Script Date: 02/28/2011 15:42:52 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TeamMember_issupervisor]') AND parent_object_id = OBJECT_ID(N'[dbo].[TeamMember]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TeamMember_issupervisor]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TeamMember] DROP CONSTRAINT [DF_TeamMember_issupervisor]
END


End
GO
/****** Object:  Default [DF_TeamMember_islogged]    Script Date: 02/28/2011 15:42:52 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TeamMember_islogged]') AND parent_object_id = OBJECT_ID(N'[dbo].[TeamMember]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TeamMember_islogged]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TeamMember] DROP CONSTRAINT [DF_TeamMember_islogged]
END


End
GO
/****** Object:  Table [dbo].[TeamMember]    Script Date: 02/28/2011 15:42:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TeamMember]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TeamMember](
	[teammemberid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[teammembername] [nvarchar](50) COLLATE French_CI_AS NOT NULL,
	[teammemberextension] [nvarchar](10) COLLATE French_CI_AS NOT NULL,
	[issupervisor] [bit] NOT NULL,
	[islogged] [bit] NULL,
 CONSTRAINT [PK_TeamMember] PRIMARY KEY CLUSTERED 
(
	[teammemberid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[ACDManagerAddressGroup]    Script Date: 02/28/2011 15:42:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACDManagerAddressGroup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ACDManagerAddressGroup](
	[acdmanageraddressgroupid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[acdmanageraddressgroupname] [nvarchar](50) COLLATE French_CI_AS NOT NULL,
 CONSTRAINT [PK_ACDManagerAddressGroup] PRIMARY KEY CLUSTERED 
(
	[acdmanageraddressgroupid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Codif]    Script Date: 02/28/2011 15:42:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Codif]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Codif](
	[codif] [nvarchar](50) COLLATE French_CI_AS NOT NULL,
	[active] [bit] NOT NULL,
	[codifid] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Codif] PRIMARY KEY CLUSTERED 
(
	[codifid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Calls]    Script Date: 02/28/2011 15:42:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Calls]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Calls](
	[extension] [nvarchar](20) COLLATE French_CI_AS NOT NULL,
	[callee] [nvarchar](20) COLLATE French_CI_AS NULL,
	[calltype] [nvarchar](15) COLLATE French_CI_AS NOT NULL,
	[startdatetime] [datetime] NOT NULL,
	[id] [nvarchar](50) COLLATE French_CI_AS NULL,
	[caller] [nvarchar](20) COLLATE French_CI_AS NULL,
	[callid] [bigint] IDENTITY(1,1) NOT NULL,
	[codifid] [int] NULL,
 CONSTRAINT [PK_Calls] PRIMARY KEY CLUSTERED 
(
	[callid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[ACDManagerAddress]    Script Date: 02/28/2011 15:42:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACDManagerAddress]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ACDManagerAddress](
	[acdmanageraddressid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[pattern] [nvarchar](10) COLLATE French_CI_AS NOT NULL,
	[acdmanageraddressgroupid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ACDManagerAddress] PRIMARY KEY CLUSTERED 
(
	[acdmanageraddressid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Team]    Script Date: 02/28/2011 15:42:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Team]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Team](
	[teamid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[teampattern] [nvarchar](10) COLLATE French_CI_AS NOT NULL,
	[teamname] [nvarchar](50) COLLATE French_CI_AS NOT NULL,
	[teamdefaultdestination] [nvarchar](10) COLLATE French_CI_AS NOT NULL,
	[acdmanageraddressgroupid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED 
(
	[teamid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[TeamMemberTeam]    Script Date: 02/28/2011 15:42:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TeamMemberTeam]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TeamMemberTeam](
	[teamid] [uniqueidentifier] NOT NULL,
	[teammemberid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TeamMemberTeam] PRIMARY KEY CLUSTERED 
(
	[teamid] ASC,
	[teammemberid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Default [DF_ACDManagerAddress_acdmanageraddressid]    Script Date: 02/28/2011 15:42:52 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ACDManagerAddress_acdmanageraddressid]') AND parent_object_id = OBJECT_ID(N'[dbo].[ACDManagerAddress]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ACDManagerAddress_acdmanageraddressid]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ACDManagerAddress] ADD  CONSTRAINT [DF_ACDManagerAddress_acdmanageraddressid]  DEFAULT (newid()) FOR [acdmanageraddressid]
END


End
GO
/****** Object:  Default [DF_ACDManagerAddressGroup_acdmanageraddressgroupid]    Script Date: 02/28/2011 15:42:52 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ACDManagerAddressGroup_acdmanageraddressgroupid]') AND parent_object_id = OBJECT_ID(N'[dbo].[ACDManagerAddressGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ACDManagerAddressGroup_acdmanageraddressgroupid]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ACDManagerAddressGroup] ADD  CONSTRAINT [DF_ACDManagerAddressGroup_acdmanageraddressgroupid]  DEFAULT (newid()) FOR [acdmanageraddressgroupid]
END


End
GO
/****** Object:  Default [DF_Codif_active]    Script Date: 02/28/2011 15:42:52 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Codif_active]') AND parent_object_id = OBJECT_ID(N'[dbo].[Codif]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Codif_active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Codif] ADD  CONSTRAINT [DF_Codif_active]  DEFAULT ((1)) FOR [active]
END


End
GO
/****** Object:  Default [DF_Team_teamid]    Script Date: 02/28/2011 15:42:52 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Team_teamid]') AND parent_object_id = OBJECT_ID(N'[dbo].[Team]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Team_teamid]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Team] ADD  CONSTRAINT [DF_Team_teamid]  DEFAULT (newid()) FOR [teamid]
END


End
GO
/****** Object:  Default [DF_TeamMember_teammemberid]    Script Date: 02/28/2011 15:42:52 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TeamMember_teammemberid]') AND parent_object_id = OBJECT_ID(N'[dbo].[TeamMember]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TeamMember_teammemberid]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TeamMember] ADD  CONSTRAINT [DF_TeamMember_teammemberid]  DEFAULT (newid()) FOR [teammemberid]
END


End
GO
/****** Object:  Default [DF_TeamMember_issupervisor]    Script Date: 02/28/2011 15:42:52 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TeamMember_issupervisor]') AND parent_object_id = OBJECT_ID(N'[dbo].[TeamMember]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TeamMember_issupervisor]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TeamMember] ADD  CONSTRAINT [DF_TeamMember_issupervisor]  DEFAULT ((0)) FOR [issupervisor]
END


End
GO
/****** Object:  Default [DF_TeamMember_islogged]    Script Date: 02/28/2011 15:42:52 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_TeamMember_islogged]') AND parent_object_id = OBJECT_ID(N'[dbo].[TeamMember]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TeamMember_islogged]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TeamMember] ADD  CONSTRAINT [DF_TeamMember_islogged]  DEFAULT ((0)) FOR [islogged]
END


End
GO
/****** Object:  ForeignKey [FK_ACDManagerAddress_ACDManagerAddressGroup]    Script Date: 02/28/2011 15:42:52 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ACDManagerAddress_ACDManagerAddressGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[ACDManagerAddress]'))
ALTER TABLE [dbo].[ACDManagerAddress]  WITH CHECK ADD  CONSTRAINT [FK_ACDManagerAddress_ACDManagerAddressGroup] FOREIGN KEY([acdmanageraddressgroupid])
REFERENCES [dbo].[ACDManagerAddressGroup] ([acdmanageraddressgroupid])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ACDManagerAddress_ACDManagerAddressGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[ACDManagerAddress]'))
ALTER TABLE [dbo].[ACDManagerAddress] CHECK CONSTRAINT [FK_ACDManagerAddress_ACDManagerAddressGroup]
GO
/****** Object:  ForeignKey [FK_Calls_Calls]    Script Date: 02/28/2011 15:42:52 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calls_Calls]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calls]'))
ALTER TABLE [dbo].[Calls]  WITH CHECK ADD  CONSTRAINT [FK_Calls_Calls] FOREIGN KEY([codifid])
REFERENCES [dbo].[Codif] ([codifid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Calls_Calls]') AND parent_object_id = OBJECT_ID(N'[dbo].[Calls]'))
ALTER TABLE [dbo].[Calls] CHECK CONSTRAINT [FK_Calls_Calls]
GO
/****** Object:  ForeignKey [FK_Team_ACDManagerAddressGroup]    Script Date: 02/28/2011 15:42:52 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Team_ACDManagerAddressGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Team]'))
ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_ACDManagerAddressGroup] FOREIGN KEY([acdmanageraddressgroupid])
REFERENCES [dbo].[ACDManagerAddressGroup] ([acdmanageraddressgroupid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Team_ACDManagerAddressGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Team]'))
ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [FK_Team_ACDManagerAddressGroup]
GO
/****** Object:  ForeignKey [FK_TeamMemberTeam_Team]    Script Date: 02/28/2011 15:42:52 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TeamMemberTeam_Team]') AND parent_object_id = OBJECT_ID(N'[dbo].[TeamMemberTeam]'))
ALTER TABLE [dbo].[TeamMemberTeam]  WITH CHECK ADD  CONSTRAINT [FK_TeamMemberTeam_Team] FOREIGN KEY([teamid])
REFERENCES [dbo].[Team] ([teamid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TeamMemberTeam_Team]') AND parent_object_id = OBJECT_ID(N'[dbo].[TeamMemberTeam]'))
ALTER TABLE [dbo].[TeamMemberTeam] CHECK CONSTRAINT [FK_TeamMemberTeam_Team]
GO
/****** Object:  ForeignKey [FK_TeamMemberTeam_TeamMember]    Script Date: 02/28/2011 15:42:52 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TeamMemberTeam_TeamMember]') AND parent_object_id = OBJECT_ID(N'[dbo].[TeamMemberTeam]'))
ALTER TABLE [dbo].[TeamMemberTeam]  WITH CHECK ADD  CONSTRAINT [FK_TeamMemberTeam_TeamMember] FOREIGN KEY([teammemberid])
REFERENCES [dbo].[TeamMember] ([teammemberid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TeamMemberTeam_TeamMember]') AND parent_object_id = OBJECT_ID(N'[dbo].[TeamMemberTeam]'))
ALTER TABLE [dbo].[TeamMemberTeam] CHECK CONSTRAINT [FK_TeamMemberTeam_TeamMember]
GO
