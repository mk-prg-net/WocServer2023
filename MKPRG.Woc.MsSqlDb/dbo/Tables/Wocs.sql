CREATE TABLE [dbo].[Wocs] (
    [Id]          BIGINT         NOT NULL,
    [FK_NodeId]   BIGINT         NOT NULL,
    [FK_AuthorId] BIGINT         NOT NULL,
    [Version]	  int			 NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    foreign key (FK_NodeId) references dbo.Nodes(Id),
	foreign key (FK_AuthorId) references dbo.Authors(Id)

);

