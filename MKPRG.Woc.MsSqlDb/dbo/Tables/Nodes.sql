CREATE TABLE [dbo].[Nodes] (
    /* 
        Woc- Kopf
        Nodes haben nur eine einfache ID, werden nur einmal unter dieser definiert.
    */
    [Id]         BIGINT         NOT NULL,

    /* Nutzdaten */
    [Name]       NVARCHAR (255) NOT NULL,
    [IP6Address] NVARCHAR (255) NULL,
    [IP4Address] NVARCHAR (255) NULL,

    PRIMARY KEY CLUSTERED ([Id] ASC),
);

