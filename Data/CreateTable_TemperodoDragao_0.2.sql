/* =========================================================
   TABLES
========================================================= */

-- CATEGORY
CREATE TABLE Category (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

-- DIFFICULTY
CREATE TABLE Difficulty (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Level NVARCHAR(50) NOT NULL
);

-- INGREDIENT
CREATE TABLE Ingredient (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

-- MEASUREMENTS
CREATE TABLE Measurement (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Unit NVARCHAR(50) NOT NULL
);

-- USER
CREATE TABLE [User] (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL,
    Password NVARCHAR(500) NOT NULL,         
    IsAdmin BIT NOT NULL CONSTRAINT DF_User_IsAdmin DEFAULT (0),
    Status INT NOT NULL CONSTRAINT DF_User_Status DEFAULT (0),

    CONSTRAINT UQ_User_Email UNIQUE (Email)
);

-- RECIPE
CREATE TABLE Recipe (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL,
    PreparationMethod NVARCHAR(MAX) NOT NULL,
    PreparationTime INT NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Status INT NOT NULL CONSTRAINT DF_Recipe_Status DEFAULT (0),
    CreatedAt DATETIME NOT NULL CONSTRAINT DF_Recipe_CreatedAt DEFAULT (GETDATE()), 
    ImagePath NVARCHAR(500) NULL,                                                    

    UserId INT NOT NULL,
    CategoryId INT NOT NULL,
    DifficultyId INT NOT NULL
);

-- RECIPE INGREDIENT
CREATE TABLE RecipeIngredient (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    RecipeId INT NOT NULL,
    IngredientId INT NOT NULL,
    Quantity DECIMAL(10,2) NOT NULL,
    MeasurementId INT NOT NULL
);

-- FAVORITES
CREATE TABLE Favorite (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    UserId INT NOT NULL,
    RecipeId INT NOT NULL
);

-- COMMENT
CREATE TABLE Comment (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Content NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME NOT NULL CONSTRAINT DF_Comment_CreatedAt DEFAULT (GETDATE()), 
    UserId INT NOT NULL,
    RecipeId INT NOT NULL
);

-- RATING
CREATE TABLE Rating (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Score INT NOT NULL,

    UserId INT NOT NULL,
    RecipeId INT NOT NULL,

    CONSTRAINT CHK_Rating_Score CHECK (Score >= 1 AND Score <= 5), 
    CONSTRAINT UQ_Rating UNIQUE (UserId, RecipeId)
);

GO

/* =========================================================
   FOREIGN KEYS
========================================================= */

-- RECIPE
ALTER TABLE Recipe
ADD CONSTRAINT FK_Recipe_User 
FOREIGN KEY (UserId) REFERENCES [User](Id);

ALTER TABLE Recipe
ADD CONSTRAINT FK_Recipe_Category 
FOREIGN KEY (CategoryId) REFERENCES Category(Id);

ALTER TABLE Recipe
ADD CONSTRAINT FK_Recipe_Difficulty 
FOREIGN KEY (DifficultyId) REFERENCES Difficulty(Id);

-- RECIPE INGREDIENT
ALTER TABLE RecipeIngredient
ADD CONSTRAINT FK_RecipeIngredient_Recipe 
FOREIGN KEY (RecipeId) REFERENCES Recipe(Id) ON DELETE CASCADE;

ALTER TABLE RecipeIngredient
ADD CONSTRAINT FK_RecipeIngredient_Ingredient 
FOREIGN KEY (IngredientId) REFERENCES Ingredient(Id);

ALTER TABLE RecipeIngredient
ADD CONSTRAINT FK_RecipeIngredient_Measurement 
FOREIGN KEY (MeasurementId) REFERENCES Measurement(Id);

-- FAVORITES
ALTER TABLE Favorite
ADD CONSTRAINT FK_Favorite_User 
FOREIGN KEY (UserId) REFERENCES [User](Id) ON DELETE CASCADE;

ALTER TABLE Favorite
ADD CONSTRAINT FK_Favorite_Recipe 
FOREIGN KEY (RecipeId) REFERENCES Recipe(Id) ON DELETE CASCADE;

ALTER TABLE Favorite
ADD CONSTRAINT UQ_Favorite UNIQUE (UserId, RecipeId);

-- COMMENT
ALTER TABLE Comment
ADD CONSTRAINT FK_Comment_User 
FOREIGN KEY (UserId) REFERENCES [User](Id) ON DELETE CASCADE;

ALTER TABLE Comment
ADD CONSTRAINT FK_Comment_Recipe 
FOREIGN KEY (RecipeId) REFERENCES Recipe(Id) ON DELETE CASCADE;

-- RATING
ALTER TABLE Rating
ADD CONSTRAINT FK_Rating_User 
FOREIGN KEY (UserId) REFERENCES [User](Id) ON DELETE CASCADE;

ALTER TABLE Rating
ADD CONSTRAINT FK_Rating_Recipe 
FOREIGN KEY (RecipeId) REFERENCES Recipe(Id) ON DELETE CASCADE;

GO

