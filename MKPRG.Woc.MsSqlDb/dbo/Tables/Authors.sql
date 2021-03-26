CREATE TABLE [dbo].[Authors] (
    /* Woc Kopf */
    [Id]        BIGINT         NOT NULL,
    [FK_NodeId]   BIGINT         NOT NULL,
    [FK_AuthorId] BIGINT         NOT NULL,
    [Version]	  int			 NOT NULL,

    /* Nutzdaten */
    [FirstName] NVARCHAR (255) NULL,
    [LastName]  NVARCHAR (255) NOT NULL,
    [Birthday]  DATE           NULL,
    [City]      NVARCHAR (255) NULL,

    PRIMARY KEY CLUSTERED ([Id] ASC),
    foreign key (FK_NodeId) references dbo.Nodes(Id),
	foreign key (FK_AuthorId) references dbo.Authors(Id)
);

