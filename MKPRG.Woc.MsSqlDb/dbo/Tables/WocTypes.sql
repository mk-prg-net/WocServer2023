/*
    mko, 26.3.2016
    Diese Wocs stehen für Klassen von Wocs.
    Hier werden keine Author und Node- Id definiert, da diese immer fest sind für Systemwocs
*/
CREATE TABLE [dbo].[WocTypes]
(
    [Id]          BIGINT         NOT NULL,

    /* Kulturneutraler Name */
    CNT           NVARCHAR(255)  NOT NULL,

    PRIMARY KEY CLUSTERED ([Id] ASC)
)
