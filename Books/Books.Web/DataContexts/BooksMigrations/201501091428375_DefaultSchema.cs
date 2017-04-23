namespace Books.Web.DataContexts.BooksMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DefaultSchema : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.Books", newSchema: "library");
        }
        
        public override void Down()
        {
            MoveTable(name: "library.Books", newSchema: "dbo");
        }
    }
}
