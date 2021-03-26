/*
	mko, 26.3.2021
	Ablage von Namen/Meldungen/Kurztexten in Spanisch, die Wocs zugeordnet sind
*/
CREATE TABLE [dbo].[PlainTextES]
(
	[WocId] bigint NOT NULL PRIMARY KEY,
	[Txt] NVarchar(4000) Not Null		
)
