namespace QuanLyCongViec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CongViecs",
                c => new
                    {
                        CongViecId = c.Int(nullable: false, identity: true),
                        Ten = c.String(),
                        MoTa = c.String(),
                        NgayKetThuc = c.DateTime(nullable: false),
                        NgayBatDau = c.DateTime(nullable: false),
                        NguoiThucHien = c.Int(nullable: false),
                        NguoiDuocGiao = c.Int(nullable: false),
                        ChaId = c.Int(),
                    })
                .PrimaryKey(t => t.CongViecId)
                .ForeignKey("dbo.CongViecs", t => t.ChaId)
                .Index(t => t.ChaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CongViecs", "ChaId", "dbo.CongViecs");
            DropIndex("dbo.CongViecs", new[] { "ChaId" });
            DropTable("dbo.CongViecs");
        }
    }
}
