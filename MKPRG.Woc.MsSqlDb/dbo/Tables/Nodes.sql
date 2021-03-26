CREATE TABLE [dbo].[Nodes] (
    /* Woc- Kopf */
    [Id]         BIGINT         NOT NULL,
    [FK_NodeId]   BIGINT         NOT NULL,
    [FK_AuthorId] BIGINT         NOT NULL,
    [Version]	  int			 NOT NULL,

    /* Nutzdaten */
    [Name]       NVARCHAR (255) NOT NULL,
    [IP6Address] NVARCHAR (255) NULL,
    [IP4Address] NVARCHAR (255) NULL,

    PRIMARY KEY CLUSTERED ([Id] ASC),
    foreign key (FK_NodeId) references dbo.Nodes(Id),
	foreign key (FK_AuthorId) references dbo.Authors(Id)
);

