create table [dbo].[BuyerLeads] (
    [BuyerLeadId] [int] not null identity,
    [Type] [nvarchar](max) null,
    [LeadName] [nvarchar](max) null,
    [priorApproval] [bit] not null,
    [Min] [int] not null,
    [Max] [int] not null,
    [Bed] [int] not null,
    [Bath] [int] not null,
    [SqFootage] [int] not null,
    [Floors] [int] not null,
    [RealEstateAgent_RealEstateAgentId] [int] null,
    primary key ([BuyerLeadId])
);
create table [dbo].[RealEstateAgents] (
    [RealEstateAgentId] [int] not null identity,
    [FirstName] [nvarchar](max) null,
    [LastName] [nvarchar](max) null,
    [Email] [nvarchar](max) null,
    [Password] [nvarchar](max) null,
    [Alias] [nvarchar](max) null,
    primary key ([RealEstateAgentId])
);
create table [dbo].[SellerLeads] (
    [SellerLeadId] [int] not null identity,
    [Type] [nvarchar](max) null,
    [LeadName] [nvarchar](max) null,
    [Min] [int] not null,
    [Max] [int] not null,
    [Bed] [int] not null,
    [Bath] [int] not null,
    [SqFootage] [int] not null,
    [Floors] [int] not null,
    [RealEstateAgent_RealEstateAgentId] [int] null,
    primary key ([SellerLeadId])
);
create table [dbo].[Users] (
    [UserId] [int] not null identity,
    [FirstName] [nvarchar](max) null,
    [LastName] [nvarchar](max) null,
    [Email] [nvarchar](max) null,
    [Password] [nvarchar](max) null,
    [Alias] [nvarchar](max) null,
    primary key ([UserId])
);
alter table [dbo].[BuyerLeads] add constraint [RealEstateAgent_BuyerLeads] foreign key ([RealEstateAgent_RealEstateAgentId]) references [dbo].[RealEstateAgents]([RealEstateAgentId]);
alter table [dbo].[SellerLeads] add constraint [SellerLead_RealEstateAgent] foreign key ([RealEstateAgent_RealEstateAgentId]) references [dbo].[RealEstateAgents]([RealEstateAgentId]);
