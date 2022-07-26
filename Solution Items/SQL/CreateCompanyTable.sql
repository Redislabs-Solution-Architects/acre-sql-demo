﻿SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[company](
	[symbol] [nvarchar](25) NOT NULL,
	[company] [nvarchar](100) NULL,
	[country] [nvarchar](50) NULL,
	[rank] [int] NULL,
	[marketcap] [float] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[company] ADD PRIMARY KEY CLUSTERED 
(
	[symbol] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
