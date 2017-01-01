CREATE TABLE [Concept] (
    [Id]         BIGINT       NOT NULL,
    [Owner]      BIGINT       NOT NULL,
    [Label]      NVARCHAR (50) NOT NULL,
    [Width]      FLOAT (53)   NOT NULL,
    [Height]     FLOAT (53)   NOT NULL,
    [X]          FLOAT (53)   NOT NULL,
    [Y]          FLOAT (53)   NOT NULL,
    [Foreground] INT          NOT NULL,
    [Background] INT          NOT NULL
)