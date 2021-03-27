CREATE TABLE [dbo].[Articles]
(
    [Id]          BIGINT         NOT NULL,
    [FK_NodeId]   BIGINT         NOT NULL,
    [FK_AuthorId] BIGINT         NOT NULL,
    IsBranch      BIT            NOT NULL,
    [Version]	  int			 NOT NULL,
    
    /* Für den Textauszeichnungssprache */
    [MarkUpLng]   int            NOT NULL,

    PRIMARY KEY CLUSTERED ([Id] ASC, FK_NodeId ASC, FK_AuthorId ASC),
    foreign key (FK_NodeId) references dbo.Nodes(Id),
	foreign key (FK_AuthorId) references dbo.Authors(Id)
)
