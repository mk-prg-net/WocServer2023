/*
    mko, 26.3.2021
    Systemwocs haben Grundlegende Bedeutung.
    Systemwocs haben immer implizit den Author **_G00D_** und sind auf dem Node **_BIGBANG_** erstellt worden
*/
CREATE TABLE [dbo].[SystemWocs]
(	
    [Id]          BIGINT         NOT NULL,    

    /* Kulturneutraler Name */
    CNT           NVARCHAR(255)  NOT NULL,

    PRIMARY KEY CLUSTERED ([Id] ASC)
)
