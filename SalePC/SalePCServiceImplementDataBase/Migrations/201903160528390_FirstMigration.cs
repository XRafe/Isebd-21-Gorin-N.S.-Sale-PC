namespace SalePCServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        PCId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.PCs", t => t.PCId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.PCId);
            
            CreateTable(
                "dbo.PCs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PCName = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .Index(t => t.Client_Id);
            
            CreateTable(
                "dbo.PCHardwares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PCId = c.Int(nullable: false),
                        HardwareId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PCs", t => t.PCId, cascadeDelete: true)
                .ForeignKey("dbo.Hardwares", t => t.HardwareId, cascadeDelete: true)
                .Index(t => t.PCId)
                .Index(t => t.HardwareId);
            
            CreateTable(
                "dbo.Hardwares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HardwareName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StockHardwares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StockId = c.Int(nullable: false),
                        HardwareId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hardwares", t => t.HardwareId, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .Index(t => t.StockId)
                .Index(t => t.HardwareId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StockName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockHardwares", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockHardwares", "HardwareId", "dbo.Hardwares");
            DropForeignKey("dbo.PCHardwares", "HardwareId", "dbo.Hardwares");
            DropForeignKey("dbo.PCs", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.Orders", "PCId", "dbo.PCs");
            DropForeignKey("dbo.PCHardwares", "PCId", "dbo.PCs");
            DropForeignKey("dbo.Orders", "ClientId", "dbo.Clients");
            DropIndex("dbo.StockHardwares", new[] { "HardwareId" });
            DropIndex("dbo.StockHardwares", new[] { "StockId" });
            DropIndex("dbo.PCHardwares", new[] { "HardwareId" });
            DropIndex("dbo.PCHardwares", new[] { "PCId" });
            DropIndex("dbo.PCs", new[] { "Client_Id" });
            DropIndex("dbo.Orders", new[] { "PCId" });
            DropIndex("dbo.Orders", new[] { "ClientId" });
            DropTable("dbo.Stocks");
            DropTable("dbo.StockHardwares");
            DropTable("dbo.Hardwares");
            DropTable("dbo.PCHardwares");
            DropTable("dbo.PCs");
            DropTable("dbo.Orders");
            DropTable("dbo.Clients");
        }
    }
}
