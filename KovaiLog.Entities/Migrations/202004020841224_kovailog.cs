namespace KovaiLog.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kovailog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contents",
                c => new
                    {
                        ContentId = c.Int(nullable: false, identity: true),
                        ContentTitle = c.String(nullable: false, maxLength: 50),
                        ContentType = c.Int(nullable: false),
                        ContentDesc = c.String(nullable: false, maxLength: 1000),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ContentId)
                .ForeignKey("dbo.TypeMasters", t => t.ContentType, cascadeDelete: true)
                .Index(t => t.ContentType);
            
            CreateTable(
                "dbo.TypeMasters",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        TypeName = c.String(nullable: false, maxLength: 50),
                        TypeColor = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.TypeId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                        Role = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contents", "ContentType", "dbo.TypeMasters");
            DropIndex("dbo.Users", new[] { "Name" });
            DropIndex("dbo.Contents", new[] { "ContentType" });
            DropTable("dbo.Users");
            DropTable("dbo.TypeMasters");
            DropTable("dbo.Contents");
        }
    }
}
