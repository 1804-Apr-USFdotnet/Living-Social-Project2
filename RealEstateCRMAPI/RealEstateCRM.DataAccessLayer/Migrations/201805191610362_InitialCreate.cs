namespace RealEstateCRM.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Leads",
                c => new
                    {
                        LeadId = c.Int(nullable: false, identity: true),
                        LeadType = c.String(nullable: false),
                        LeadName = c.String(nullable: false),
                        PriorApproval = c.Boolean(nullable: false),
                        Min = c.Int(),
                        Max = c.Int(),
                        Bed = c.Int(),
                        Bath = c.Int(),
                        SqFootage = c.Int(),
                        Floors = c.Int(),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zipcode = c.Int(),
                        UserId = c.Int(),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        RealEstateAgent_RealEstateAgentId = c.Int(),
                    })
                .PrimaryKey(t => t.LeadId)
                .ForeignKey("dbo.RealEstateAgents", t => t.RealEstateAgent_RealEstateAgentId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RealEstateAgent_RealEstateAgentId);
            
            CreateTable(
                "dbo.RealEstateAgents",
                c => new
                    {
                        RealEstateAgentId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Alias = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.RealEstateAgentId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Alias = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leads", "UserId", "dbo.Users");
            DropForeignKey("dbo.Leads", "RealEstateAgent_RealEstateAgentId", "dbo.RealEstateAgents");
            DropIndex("dbo.Leads", new[] { "RealEstateAgent_RealEstateAgentId" });
            DropIndex("dbo.Leads", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.RealEstateAgents");
            DropTable("dbo.Leads");
        }
    }
}
