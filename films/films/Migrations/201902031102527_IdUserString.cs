namespace films.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdUserString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Films", "IdUser", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Films", "IdUser", c => c.Int(nullable: false));
        }
    }
}
