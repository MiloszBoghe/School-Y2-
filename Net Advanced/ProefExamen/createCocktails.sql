/* Check if database already exists and delete it if it does exist*/
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'Cocktails') 
BEGIN
DROP DATABASE Cocktails
END
GO

CREATE DATABASE Cocktails
GO


USE [Cocktails]
GO

/****** Object: Table [dbo].[Cocktails] Script Date: 02-Dec-17 10:55:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cocktails] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (50)   NOT NULL,
    [Description]  VARCHAR (250)  NULL,
    [Instructions] VARCHAR (1000) NOT NULL
);

INSERT [dbo].[Cocktails] ([Name], [Description], [Instructions]) VALUES (N'White lady', N'Een cocktail die gin, citroensap en eiwit als voornaamste ingredienten heeft. Verrassende cocktail voor de echte fijnproever. ', N'Wanneer je White lady gaat maken, is het van belang dat het glas koud is. Zet deze dan ongeveer een kwartier in de koelkast. Vervolgens kan het glas met de opening in de grenadinesuiker worden geduwd. Doordat het glas koud is, zal de suiker aan de rand van het glas blijven kleven. Vervolgens kunnen alle ingrediënten in het glas worden gedaan. Het eiwit moet als laatste worden toegevoegd. Het drankje moet flink geschud worden en het eiwit moet tot schuim worden geklopt. Pak er vervolgens een cocktailglas bij en schenk het daarin. Vergeet niet om de zeef te gebruiken. Je kunt de cocktail garneren met een citroentje en doe er een aantal rietjes in.')

INSERT [dbo].[Cocktails] ([Name], [Description], [Instructions])  VALUES (N'Screwdriver', N'Het is een lekkere cocktail die sinaasappel als basis heeft. Veel mensen vinden sinaasappel lekker en daarom valt deze cocktail ook bij veel mensen in de smaak.', N'Het is het handigst om deze cocktail zonder hulp van een blender of shaker te maken. De Wodka moet als eerste in een glas worden geschonken, doe er vier ijsblokjes bij. Doe daarna de sinaasappelsap in het glas. Roer alles een keer goed door. Doe bij voorkeur het eerst in een ander glas en laat het door een zeef in een longdrink-glas lopen. Vervolgens kunnen de overige ijsblokjes worden toegevoegd. Alles moet dan nog een keer flink omgeroerd worden. Als de cocktail klaar is, kun je deze nog garneren met een schijfje sinaasappel aan de rand van het glas. Vergeet ook niet de rietjes in het glas.')

INSERT [dbo].[Cocktails] ([Name], [Description], [Instructions]) VALUES (N'Cuba Libre', N'Een Pina Colada is een heerlijke cocktail die bestaat uit witte rum, kokosmelk en ananassap. Het is een cocktail die bij uitstek past op het strand.',N'De Pina Colada kun je het beste met een shaker of een blender maken. Je kunt dan verse ananas gebruiken voor de sap. In de shaker moet je eerst ijs doen, doe er vervolgens een barmaatje aan ananassap, een barmaatje kokosmelk en witte rum bij. Ook de vloeibare room moet erbij. Dat zal er voor zorgen dat de cocktail lekker romig zal worden. Het is belangrijk om alles even goed te shaken. Vervolgens kun je het glas, waar al ijsblokjes inzitten, vullen met de cocktail. Wanneer je het mengsel hebt verdeeld kun je de cocktail garneren met een ananas aan de rand en eventueel een kers bovenop.')

INSERT [dbo].[Cocktails] ([Name], [Description], [Instructions]) VALUES (N'Caipirinha', N'De nationale cocktail van Brazilië, perfect om van te genieten tijdens het WK.', N'Doe de stukjes limoen in een glas. Voeg de suiker toe en roer goed. Vul het glas met ijs, en giet daarover de cachaça.')


/****** Object: Table [dbo].[Ingredients] Script Date: 02-Dec-17 10:56:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Ingredients] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (50) NOT NULL,
    [Unit] VARCHAR (10) NULL
);

INSERT [dbo].[Ingredients] ([Name], [Unit]) VALUES (N'Grenadinesuiker', null)
INSERT [dbo].[Ingredients] ([Name], [Unit]) VALUES (N'Gin', N'CL')
INSERT [dbo].[Ingredients] ([Name], [Unit]) VALUES (N'Citroensap', N'CL')
INSERT [dbo].[Ingredients] ([Name], [Unit]) VALUES (N'Losgeklopt eiwit', null)
INSERT [dbo].[Ingredients] ([Name], [Unit]) VALUES (N'Ijsblokjes', null)
INSERT [dbo].[Ingredients] ([Name], [Unit]) VALUES (N'Cointreau', N'CL')
INSERT [dbo].[Ingredients] ([Name], [Unit]) VALUES (N'Wodka', N'CL')
INSERT [dbo].[Ingredients] ([Name], [Unit]) VALUES (N'Sinaasappelsap', N'CL')
INSERT [dbo].[Ingredients] ([Name], [Unit]) VALUES (N'Stukjes sinaasappel', null)
INSERT [dbo].[Ingredients] ([Name], [Unit]) VALUES (N'Witte rum', N'ML')
INSERT [dbo].[Ingredients] ([Name], [Unit]) VALUES (N'Vloeibare room', N'EL')
INSERT [dbo].[Ingredients] ([Name], [Unit]) VALUES (N'Ananassap', N'ML')	
INSERT [dbo].[Ingredients] ([Name], [Unit]) VALUES (N'Kokosmelk', N'ML')
INSERT [dbo].[Ingredients] ([Name], [Unit]) VALUES (N'Bruine suiker', N'KL')
INSERT [dbo].[Ingredients] ([Name], [Unit]) VALUES (N'Limoen', null)
INSERT [dbo].[Ingredients] ([Name], [Unit]) VALUES (N'Cachaça', N'ML')



/****** Object: Table [dbo].[CocktailIngredients] Script Date: 02-Dec-17 10:56:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CocktailIngredients] (
    [CocktailId]   INT          NOT NULL,
    [IngredientId] INT          NOT NULL,
    [Quantity]     DECIMAL (18) NULL
);


INSERT [dbo].[CocktailIngredients] ([CocktailId], [IngredientId], [Quantity]) VALUES (1, 1, null)
INSERT [dbo].[CocktailIngredients] ([CocktailId], [IngredientId], [Quantity]) VALUES (1, 2, 2)
INSERT [dbo].[CocktailIngredients] ([CocktailId], [IngredientId], [Quantity]) VALUES (1, 3, 1)
INSERT [dbo].[CocktailIngredients] ([CocktailId], [IngredientId], [Quantity]) VALUES (1, 4, 1)
INSERT [dbo].[CocktailIngredients] ([CocktailId], [IngredientId], [Quantity]) VALUES (1, 5, null)
INSERT [dbo].[CocktailIngredients] ([CocktailId], [IngredientId], [Quantity]) VALUES (2, 5, null)
INSERT [dbo].[CocktailIngredients] ([CocktailId], [IngredientId], [Quantity]) VALUES (2, 8, 10)
INSERT [dbo].[CocktailIngredients] ([CocktailId], [IngredientId], [Quantity]) VALUES (2, 9, null)
INSERT [dbo].[CocktailIngredients] ([CocktailId], [IngredientId], [Quantity]) VALUES (3, 10, 30)
INSERT [dbo].[CocktailIngredients] ([CocktailId], [IngredientId], [Quantity]) VALUES (3, 11, 1)
INSERT [dbo].[CocktailIngredients] ([CocktailId], [IngredientId], [Quantity]) VALUES (3, 12, 30)
INSERT [dbo].[CocktailIngredients] ([CocktailId], [IngredientId], [Quantity]) VALUES (3, 13, 30)
INSERT [dbo].[CocktailIngredients] ([CocktailId], [IngredientId], [Quantity]) VALUES (4, 5, null)
INSERT [dbo].[CocktailIngredients] ([CocktailId], [IngredientId], [Quantity]) VALUES (4, 14, 2)
INSERT [dbo].[CocktailIngredients] ([CocktailId], [IngredientId], [Quantity]) VALUES (4, 15, 1)
INSERT [dbo].[CocktailIngredients] ([CocktailId], [IngredientId], [Quantity]) VALUES (4, 16, 60)