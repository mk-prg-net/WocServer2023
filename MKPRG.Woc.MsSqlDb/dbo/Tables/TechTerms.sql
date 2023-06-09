﻿/*
    mko, 21.6.2021

*/
CREATE TABLE [dbo].[TechTerms] (
    [Id]          BIGINT         NOT NULL,
    [FK_NodeId]   BIGINT         NOT NULL,
    [FK_AuthorId] BIGINT         NOT NULL,
    IsBranch      BIT            NOT NULL,
    [Version]	  int			 NOT NULL,
    [CNT]         nvarchar(512)  Not Null, /* Culture Neutral Name */
    PRIMARY KEY CLUSTERED ([Id] ASC, FK_NodeId ASC, FK_AuthorId ASC),
    foreign key (FK_NodeId) references dbo.Nodes(Id),
	foreign key (FK_AuthorId) references dbo.Authors(Id)

);

