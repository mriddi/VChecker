
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/25/2019 00:44:42
-- Generated from EDMX file: C:\Users\User\Documents\GitHub\VChecker\VChecker\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [{0}];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_NvdEntry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EntrySet] DROP CONSTRAINT [FK_NvdEntry];
GO
IF OBJECT_ID(N'[dbo].[FK_EntryReferences]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReferencesSet] DROP CONSTRAINT [FK_EntryReferences];
GO
IF OBJECT_ID(N'[dbo].[FK_EntryVulnerableConfiguration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VulnerableConfigurationSet] DROP CONSTRAINT [FK_EntryVulnerableConfiguration];
GO
IF OBJECT_ID(N'[dbo].[FK_EntryVulnerableSoftwareList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VulnerableSoftwareListSet] DROP CONSTRAINT [FK_EntryVulnerableSoftwareList];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[EntrySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EntrySet];
GO
IF OBJECT_ID(N'[dbo].[ReferencesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReferencesSet];
GO
IF OBJECT_ID(N'[dbo].[VulnerableSoftwareListSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VulnerableSoftwareListSet];
GO
IF OBJECT_ID(N'[dbo].[NvdSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NvdSet];
GO
IF OBJECT_ID(N'[dbo].[VulnerableConfigurationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VulnerableConfigurationSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'EntrySet'
CREATE TABLE [dbo].[EntrySet] (
    [EntryId] nvarchar(50)  NOT NULL,
    [Summary] nvarchar(3000)  NULL,
    [LastmodifiedDateTime] nvarchar(50)  NULL,
    [PublishedDateTime] nvarchar(50)  NULL,
    [NvdPubDate] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ReferencesSet'
CREATE TABLE [dbo].[ReferencesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Href] nvarchar(200)  NOT NULL,
    [EntryId] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'VulnerableSoftwareListSet'
CREATE TABLE [dbo].[VulnerableSoftwareListSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Product] nvarchar(200)  NOT NULL,
    [EntryId] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'NvdSet'
CREATE TABLE [dbo].[NvdSet] (
    [PubDate] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'VulnerableConfigurationSet'
CREATE TABLE [dbo].[VulnerableConfigurationSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(200)  NOT NULL,
    [EntryId] nvarchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [EntryId] in table 'EntrySet'
ALTER TABLE [dbo].[EntrySet]
ADD CONSTRAINT [PK_EntrySet]
    PRIMARY KEY CLUSTERED ([EntryId] ASC);
GO

-- Creating primary key on [Id] in table 'ReferencesSet'
ALTER TABLE [dbo].[ReferencesSet]
ADD CONSTRAINT [PK_ReferencesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VulnerableSoftwareListSet'
ALTER TABLE [dbo].[VulnerableSoftwareListSet]
ADD CONSTRAINT [PK_VulnerableSoftwareListSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [PubDate] in table 'NvdSet'
ALTER TABLE [dbo].[NvdSet]
ADD CONSTRAINT [PK_NvdSet]
    PRIMARY KEY CLUSTERED ([PubDate] ASC);
GO

-- Creating primary key on [Id] in table 'VulnerableConfigurationSet'
ALTER TABLE [dbo].[VulnerableConfigurationSet]
ADD CONSTRAINT [PK_VulnerableConfigurationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [NvdPubDate] in table 'EntrySet'
ALTER TABLE [dbo].[EntrySet]
ADD CONSTRAINT [FK_NvdEntry]
    FOREIGN KEY ([NvdPubDate])
    REFERENCES [dbo].[NvdSet]
        ([PubDate])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NvdEntry'
CREATE INDEX [IX_FK_NvdEntry]
ON [dbo].[EntrySet]
    ([NvdPubDate]);
GO

-- Creating foreign key on [EntryId] in table 'ReferencesSet'
ALTER TABLE [dbo].[ReferencesSet]
ADD CONSTRAINT [FK_EntryReferences]
    FOREIGN KEY ([EntryId])
    REFERENCES [dbo].[EntrySet]
        ([EntryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EntryReferences'
CREATE INDEX [IX_FK_EntryReferences]
ON [dbo].[ReferencesSet]
    ([EntryId]);
GO

-- Creating foreign key on [EntryId] in table 'VulnerableConfigurationSet'
ALTER TABLE [dbo].[VulnerableConfigurationSet]
ADD CONSTRAINT [FK_EntryVulnerableConfiguration]
    FOREIGN KEY ([EntryId])
    REFERENCES [dbo].[EntrySet]
        ([EntryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EntryVulnerableConfiguration'
CREATE INDEX [IX_FK_EntryVulnerableConfiguration]
ON [dbo].[VulnerableConfigurationSet]
    ([EntryId]);
GO

-- Creating foreign key on [EntryId] in table 'VulnerableSoftwareListSet'
ALTER TABLE [dbo].[VulnerableSoftwareListSet]
ADD CONSTRAINT [FK_EntryVulnerableSoftwareList]
    FOREIGN KEY ([EntryId])
    REFERENCES [dbo].[EntrySet]
        ([EntryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EntryVulnerableSoftwareList'
CREATE INDEX [IX_FK_EntryVulnerableSoftwareList]
ON [dbo].[VulnerableSoftwareListSet]
    ([EntryId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------