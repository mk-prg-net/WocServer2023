﻿/*
	mko, 26.3.2021
	Ablage von Namen/Meldungen/Kurztexten in Englisch, die Wocs zugeordnet sind
*/
CREATE TABLE [dbo].[PlainTextEN]
(
	[WocId] bigint NOT NULL PRIMARY KEY,
	[Txt] NVarchar(4000) Not Null		
)
