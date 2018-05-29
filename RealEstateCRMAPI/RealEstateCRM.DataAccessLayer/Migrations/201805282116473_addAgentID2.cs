namespace RealEstateCRM.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAgentID2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Leads", name: "RealEstateAgent_RealEstateAgentId", newName: "RealEstateAgentId");
            RenameIndex(table: "dbo.Leads", name: "IX_RealEstateAgent_RealEstateAgentId", newName: "IX_RealEstateAgentId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Leads", name: "IX_RealEstateAgentId", newName: "IX_RealEstateAgent_RealEstateAgentId");
            RenameColumn(table: "dbo.Leads", name: "RealEstateAgentId", newName: "RealEstateAgent_RealEstateAgentId");
        }
    }
}
