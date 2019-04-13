namespace SalePCServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StockHardwares", "PC_Id", "dbo.PCs");
            DropForeignKey("dbo.PCs", "Client_Id", "dbo.Clients");
            DropIndex("dbo.PCs", new[] { "Client_Id" });
            DropIndex("dbo.StockHardwares", new[] { "PC_Id" });
            DropColumn("dbo.PCs", "Client_Id");
            DropColumn("dbo.StockHardwares", "PC_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StockHardwares", "PC_Id", c => c.Int());
            AddColumn("dbo.PCs", "Client_Id", c => c.Int());
            CreateIndex("dbo.StockHardwares", "PC_Id");
            CreateIndex("dbo.PCs", "Client_Id");
            AddForeignKey("dbo.PCs", "Client_Id", "dbo.Clients", "Id");
            AddForeignKey("dbo.StockHardwares", "PC_Id", "dbo.PCs", "Id");
        }
    }
}
