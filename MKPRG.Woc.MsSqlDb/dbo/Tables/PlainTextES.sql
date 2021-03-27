/*
	mko, 26.3.2021
	Ablage von Namen/Meldungen/Kurztexten in Spanisch, die Wocs zugeordnet sind
*/
CREATE TABLE [dbo].[PlainTextES]
(
	[WocId] bigint NOT NULL ,
	[FK_AuthorId] bigint NOT NULL,
	[FK_NodeId] bigint NOT NULL,
	[Txt] NVarchar(4000) Not Null,

	PRIMARY KEY CLUSTERED (FK_NodeId ASC, FK_AuthorId ASC, [WocId] ASC),
	foreign key (FK_NodeId) references dbo.Nodes(Id),
	foreign key (FK_AuthorId) references dbo.Authors(Id)
)
