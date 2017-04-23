namespace Books.Web.DataContexts.IdentityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DefaultSchema : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.AspNetRoles", newSchema: "auth");
            MoveTable(name: "dbo.AspNetUserRoles", newSchema: "auth");
            MoveTable(name: "dbo.AspNetUsers", newSchema: "auth");
            MoveTable(name: "dbo.AspNetUserClaims", newSchema: "auth");
            MoveTable(name: "dbo.AspNetUserLogins", newSchema: "auth");
        }
        
        public override void Down()
        {
            MoveTable(name: "auth.AspNetUserLogins", newSchema: "dbo");
            MoveTable(name: "auth.AspNetUserClaims", newSchema: "dbo");
            MoveTable(name: "auth.AspNetUsers", newSchema: "dbo");
            MoveTable(name: "auth.AspNetUserRoles", newSchema: "dbo");
            MoveTable(name: "auth.AspNetRoles", newSchema: "dbo");
        }
    }
}
