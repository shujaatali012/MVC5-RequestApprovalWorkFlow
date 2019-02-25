namespace RequestApprovalWorkflow.Host.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RequestApprovalWorkflow.Host.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(RequestApprovalWorkflow.Host.Models.ApplicationDbContext context)
        {
            //context.Accounts.AddOrUpdate(account => account.Name, 
            //    new Account { Name = "Account1" },
            //    new Account { Name = "Account2" }, 
            //    new Account { Name = "Account3" });
        }
    }
}
