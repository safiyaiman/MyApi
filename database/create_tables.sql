
CREATE TABLE [dbo].[tblProducts](
    [Id] [int] NOT NULL,
    [Title] [nvarchar](200) NOT NULL,
    [Description] [nvarchar](max) NULL,
    [Price] [decimal](18, 2) NOT NULL,
    [DiscountPercentage] [float] NOT NULL,
    [Rating] [float] NOT NULL,
    [Stock] [int] NOT NULL,
    [Brand] [nvarchar](200) NULL,
    [Category] [nvarchar](200) NULL,
    [Thumbnail] [nvarchar](500) NULL
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO

