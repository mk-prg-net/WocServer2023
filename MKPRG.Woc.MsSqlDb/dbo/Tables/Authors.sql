CREATE TABLE [dbo].[Authors] (
    /*
        Woc Kopf
        Autoren haben nur eine einfache ID, werden nur einmal unter dieser definiert.
        Autoren sind Systemwocs, und haben immer implizit den Author **_G00D_** und sind auf dem Node **_BIGBANG_** erstellt worden

    */
    [Id]        BIGINT         NOT NULL,

    /* Nutzdaten */
    [FirstName] NVARCHAR (255) NULL,
    [LastName]  NVARCHAR (255) NOT NULL,
    [Birthday]  DATE           NULL,
    [City]      NVARCHAR (255) NULL,

    PRIMARY KEY CLUSTERED ([Id] ASC),
    
);

