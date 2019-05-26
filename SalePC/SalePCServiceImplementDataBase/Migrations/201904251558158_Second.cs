namespace SalePCServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PCs", "Client_Id", c => c.Int());
            AddColumn("dbo.StockHardwares", "PC_Id", c => c.Int());
            CreateIndex("dbo.PCs", "Client_Id");
            CreateIndex("dbo.StockHardwares", "PC_Id");
            AddForeignKey("dbo.StockHardwares", "PC_Id", "dbo.PCs", "Id");
            AddForeignKey("dbo.PCs", "Client_Id", "dbo.Clients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PCs", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.StockHardwares", "PC_Id", "dbo.PCs");
            DropIndex("dbo.StockHardwares", new[] { "PC_Id" });
            DropIndex("dbo.PCs", new[] { "Client_Id" });
            DropColumn("dbo.StockHardwares", "PC_Id");
            DropColumn("dbo.PCs", "Client_Id");
        }
    }
}
