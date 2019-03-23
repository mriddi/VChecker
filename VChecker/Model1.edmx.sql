
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/22/2019 16:58:23
-- Generated from EDMX file: E:\Code\VCheker_1.3\VCheker\VCheker\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [tet];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_EntryReferences]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReferencesSet] DROP CONSTRAINT [FK_EntryReferences];
GO
IF OBJECT_ID(N'[dbo].[FK_NvdEntry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EntrySet] DROP CONSTRAINT [FK_NvdEntry];
GO
IF OBJECT_ID(N'[dbo].[FK_VulnerablesoftwarelistProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductSet] DROP CONSTRAINT [FK_VulnerablesoftwarelistProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_ReferencesReference]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReferenceSet] DROP CONSTRAINT [FK_ReferencesReference];
GO
IF OBJECT_ID(N'[dbo].[FK_EntryVulnerableconfiguration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VulnerableConfigurationSet1] DROP CONSTRAINT [FK_EntryVulnerableconfiguration];
GO
IF OBJECT_ID(N'[dbo].[FK_EntryVulnerablesoftwarelist]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VulnerableSoftwareListSet] DROP CONSTRAINT [FK_EntryVulnerablesoftwarelist];
GO
IF OBJECT_ID(N'[dbo].[FK_Logicaltest1Name1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FactRef1Set] DROP CONSTRAINT [FK_Logicaltest1Name1];
GO
IF OBJECT_ID(N'[dbo].[FK_Logicaltest1Logicaltest2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LogicalTest2Set] DROP CONSTRAINT [FK_Logicaltest1Logicaltest2];
GO
IF OBJECT_ID(N'[dbo].[FK_Logicaltest2Factref]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FactRef2Set] DROP CONSTRAINT [FK_Logicaltest2Factref];
GO
IF OBJECT_ID(N'[dbo].[FK_VulnerableconfigurationLogicaltest1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LogicalTest1Set] DROP CONSTRAINT [FK_VulnerableconfigurationLogicaltest1];
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
IF OBJECT_ID(N'[dbo].[VulnerableConfigurationSet1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VulnerableConfigurationSet1];
GO
IF OBJECT_ID(N'[dbo].[LogicalTest1Set]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LogicalTest1Set];
GO
IF OBJECT_ID(N'[dbo].[NvdSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NvdSet];
GO
IF OBJECT_ID(N'[dbo].[ReferenceSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReferenceSet];
GO
IF OBJECT_ID(N'[dbo].[ProductSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductSet];
GO
IF OBJECT_ID(N'[dbo].[FactRef2Set]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FactRef2Set];
GO
IF OBJECT_ID(N'[dbo].[LogicalTest2Set]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LogicalTest2Set];
GO
IF OBJECT_ID(N'[dbo].[FactRef1Set]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FactRef1Set];
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
    [NvdId] int  NOT NULL
);
GO

-- Creating table 'ReferencesSet'
CREATE TABLE [dbo].[ReferencesSet] (
    [ReferencesId] int IDENTITY(1,1) NOT NULL,
    [Lang] nvarchar(50)  NULL,
    [ReferenceType] nvarchar(50)  NULL,
    [EntryId] nvarchar(50)  NOT NULL,
    [Source] nvarchar(50)  NULL
);
GO

-- Creating table 'VulnerableSoftwareListSet'
CREATE TABLE [dbo].[VulnerableSoftwareListSet] (
    [VulnerableSoftwareListId] int IDENTITY(1,1) NOT NULL,
    [EntryId] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'VulnerableConfigurationSet1'
CREATE TABLE [dbo].[VulnerableConfigurationSet1] (
    [VulnerableConfigurationId] int IDENTITY(1,1) NOT NULL,
    [EntryId] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'LogicalTest1Set'
CREATE TABLE [dbo].[LogicalTest1Set] (
    [LogicalTest1Id] int IDENTITY(1,1) NOT NULL,
    [Negate1] nvarchar(50)  NULL,
    [Operator1] nvarchar(50)  NULL,
    [VulnerableConfigurationId] int  NOT NULL
);
GO

-- Creating table 'NvdSet'
CREATE TABLE [dbo].[NvdSet] (
    [NvdId] int IDENTITY(1,1) NOT NULL,
    [PubDate] nvarchar(50)  NULL
);
GO

-- Creating table 'ReferenceSet'
CREATE TABLE [dbo].[ReferenceSet] (
    [ReferenceId] int IDENTITY(1,1) NOT NULL,
    [Href] nvarchar(200)  NOT NULL,
    [Lang] nvarchar(50)  NULL,
    [Text] nvarchar(200)  NULL,
    [ReferencesId] int  NOT NULL
);
GO

-- Creating table 'ProductSet'
CREATE TABLE [dbo].[ProductSet] (
    [ProductId] int IDENTITY(1,1) NOT NULL,
    [ProductN] nvarchar(200)  NOT NULL,
    [VulnerableSoftwareListId] int  NOT NULL
);
GO

-- Creating table 'FactRef2Set'
CREATE TABLE [dbo].[FactRef2Set] (
    [FactRef2Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(200)  NOT NULL,
    [LogicalTest2Id] int  NOT NULL
);
GO

-- Creating table 'LogicalTest2Set'
CREATE TABLE [dbo].[LogicalTest2Set] (
    [LogicalTest2Id] int IDENTITY(1,1) NOT NULL,
    [Negate2] nvarchar(50)  NULL,
    [Operator2] nvarchar(50)  NULL,
    [LogicalTest1Id] int  NOT NULL
);
GO

-- Creating table 'FactRef1Set'
CREATE TABLE [dbo].[FactRef1Set] (
    [FactRed1Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(200)  NOT NULL,
    [LogicalTest1Id] int  NOT NULL
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

-- Creating primary key on [ReferencesId] in table 'ReferencesSet'
ALTER TABLE [dbo].[ReferencesSet]
ADD CONSTRAINT [PK_ReferencesSet]
    PRIMARY KEY CLUSTERED ([ReferencesId] ASC);
GO

-- Creating primary key on [VulnerableSoftwareListId] in table 'VulnerableSoftwareListSet'
ALTER TABLE [dbo].[VulnerableSoftwareListSet]
ADD CONSTRAINT [PK_VulnerableSoftwareListSet]
    PRIMARY KEY CLUSTERED ([VulnerableSoftwareListId] ASC);
GO

-- Creating primary key on [VulnerableConfigurationId] in table 'VulnerableConfigurationSet1'
ALTER TABLE [dbo].[VulnerableConfigurationSet1]
ADD CONSTRAINT [PK_VulnerableConfigurationSet1]
    PRIMARY KEY CLUSTERED ([VulnerableConfigurationId] ASC);
GO

-- Creating primary key on [LogicalTest1Id] in table 'LogicalTest1Set'
ALTER TABLE [dbo].[LogicalTest1Set]
ADD CONSTRAINT [PK_LogicalTest1Set]
    PRIMARY KEY CLUSTERED ([LogicalTest1Id] ASC);
GO

-- Creating primary key on [NvdId] in table 'NvdSet'
ALTER TABLE [dbo].[NvdSet]
ADD CONSTRAINT [PK_NvdSet]
    PRIMARY KEY CLUSTERED ([NvdId] ASC);
GO

-- Creating primary key on [ReferenceId] in table 'ReferenceSet'
ALTER TABLE [dbo].[ReferenceSet]
ADD CONSTRAINT [PK_ReferenceSet]
    PRIMARY KEY CLUSTERED ([ReferenceId] ASC);
GO

-- Creating primary key on [ProductId] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [PK_ProductSet]
    PRIMARY KEY CLUSTERED ([ProductId] ASC);
GO

-- Creating primary key on [FactRef2Id] in table 'FactRef2Set'
ALTER TABLE [dbo].[FactRef2Set]
ADD CONSTRAINT [PK_FactRef2Set]
    PRIMARY KEY CLUSTERED ([FactRef2Id] ASC);
GO

-- Creating primary key on [LogicalTest2Id] in table 'LogicalTest2Set'
ALTER TABLE [dbo].[LogicalTest2Set]
ADD CONSTRAINT [PK_LogicalTest2Set]
    PRIMARY KEY CLUSTERED ([LogicalTest2Id] ASC);
GO

-- Creating primary key on [FactRed1Id] in table 'FactRef1Set'
ALTER TABLE [dbo].[FactRef1Set]
ADD CONSTRAINT [PK_FactRef1Set]
    PRIMARY KEY CLUSTERED ([FactRed1Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

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

-- Creating foreign key on [NvdId] in table 'EntrySet'
ALTER TABLE [dbo].[EntrySet]
ADD CONSTRAINT [FK_NvdEntry]
    FOREIGN KEY ([NvdId])
    REFERENCES [dbo].[NvdSet]
        ([NvdId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NvdEntry'
CREATE INDEX [IX_FK_NvdEntry]
ON [dbo].[EntrySet]
    ([NvdId]);
GO

-- Creating foreign key on [VulnerableSoftwareListId] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [FK_VulnerablesoftwarelistProduct]
    FOREIGN KEY ([VulnerableSoftwareListId])
    REFERENCES [dbo].[VulnerableSoftwareListSet]
        ([VulnerableSoftwareListId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VulnerablesoftwarelistProduct'
CREATE INDEX [IX_FK_VulnerablesoftwarelistProduct]
ON [dbo].[ProductSet]
    ([VulnerableSoftwareListId]);
GO

-- Creating foreign key on [ReferencesId] in table 'ReferenceSet'
ALTER TABLE [dbo].[ReferenceSet]
ADD CONSTRAINT [FK_ReferencesReference]
    FOREIGN KEY ([ReferencesId])
    REFERENCES [dbo].[ReferencesSet]
        ([ReferencesId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReferencesReference'
CREATE INDEX [IX_FK_ReferencesReference]
ON [dbo].[ReferenceSet]
    ([ReferencesId]);
GO

-- Creating foreign key on [EntryId] in table 'VulnerableConfigurationSet1'
ALTER TABLE [dbo].[VulnerableConfigurationSet1]
ADD CONSTRAINT [FK_EntryVulnerableconfiguration]
    FOREIGN KEY ([EntryId])
    REFERENCES [dbo].[EntrySet]
        ([EntryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EntryVulnerableconfiguration'
CREATE INDEX [IX_FK_EntryVulnerableconfiguration]
ON [dbo].[VulnerableConfigurationSet1]
    ([EntryId]);
GO

-- Creating foreign key on [EntryId] in table 'VulnerableSoftwareListSet'
ALTER TABLE [dbo].[VulnerableSoftwareListSet]
ADD CONSTRAINT [FK_EntryVulnerablesoftwarelist]
    FOREIGN KEY ([EntryId])
    REFERENCES [dbo].[EntrySet]
        ([EntryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EntryVulnerablesoftwarelist'
CREATE INDEX [IX_FK_EntryVulnerablesoftwarelist]
ON [dbo].[VulnerableSoftwareListSet]
    ([EntryId]);
GO

-- Creating foreign key on [LogicalTest1Id] in table 'FactRef1Set'
ALTER TABLE [dbo].[FactRef1Set]
ADD CONSTRAINT [FK_Logicaltest1Name1]
    FOREIGN KEY ([LogicalTest1Id])
    REFERENCES [dbo].[LogicalTest1Set]
        ([LogicalTest1Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Logicaltest1Name1'
CREATE INDEX [IX_FK_Logicaltest1Name1]
ON [dbo].[FactRef1Set]
    ([LogicalTest1Id]);
GO

-- Creating foreign key on [LogicalTest1Id] in table 'LogicalTest2Set'
ALTER TABLE [dbo].[LogicalTest2Set]
ADD CONSTRAINT [FK_Logicaltest1Logicaltest2]
    FOREIGN KEY ([LogicalTest1Id])
    REFERENCES [dbo].[LogicalTest1Set]
        ([LogicalTest1Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Logicaltest1Logicaltest2'
CREATE INDEX [IX_FK_Logicaltest1Logicaltest2]
ON [dbo].[LogicalTest2Set]
    ([LogicalTest1Id]);
GO

-- Creating foreign key on [LogicalTest2Id] in table 'FactRef2Set'
ALTER TABLE [dbo].[FactRef2Set]
ADD CONSTRAINT [FK_Logicaltest2Factref]
    FOREIGN KEY ([LogicalTest2Id])
    REFERENCES [dbo].[LogicalTest2Set]
        ([LogicalTest2Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Logicaltest2Factref'
CREATE INDEX [IX_FK_Logicaltest2Factref]
ON [dbo].[FactRef2Set]
    ([LogicalTest2Id]);
GO

-- Creating foreign key on [VulnerableConfigurationId] in table 'LogicalTest1Set'
ALTER TABLE [dbo].[LogicalTest1Set]
ADD CONSTRAINT [FK_VulnerableconfigurationLogicaltest1]
    FOREIGN KEY ([VulnerableConfigurationId])
    REFERENCES [dbo].[VulnerableConfigurationSet1]
        ([VulnerableConfigurationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VulnerableconfigurationLogicaltest1'
CREATE INDEX [IX_FK_VulnerableconfigurationLogicaltest1]
ON [dbo].[LogicalTest1Set]
    ([VulnerableConfigurationId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------