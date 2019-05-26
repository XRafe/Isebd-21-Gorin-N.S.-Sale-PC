namespace SalePCServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockHardwares", "PC_Id", c => c.Int());
            CreateIndex("dbo.StockHardwares", "PC_Id");
            AddForeignKey("dbo.StockHardwares", "PC_Id", "dbo.PCs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockHardwares", "PC_Id", "dbo.PCs");
            DropIndex("dbo.StockHardwares", new[] { "PC_Id" });
            DropColumn("dbo.StockHardwares", "PC_Id");
        }
    }
}
