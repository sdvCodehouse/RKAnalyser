CREATE TABLE [dbo].[ExerciseSessions] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NULL,
    [Time]        DATETIME       NULL,
    [Distance]    FLOAT (53)     NOT NULL,
    [Duration]    TIME (7)       NULL,
    [SessionType] INT            NOT NULL,
    [Elevation]   FLOAT (53)     NULL,
    [Calories]    INT            NOT NULL,
    [Speed]       TIME (7)       NOT NULL,
    CONSTRAINT [PK_dbo.ExerciseSessions] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO


CREATE INDEX [IX_ExerciseSessions_Time] ON [dbo].[ExerciseSessions] ([Time])
