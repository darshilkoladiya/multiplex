using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multiplex.Migrations
{
    [Migration(1)]
    public class CreateCustomer_1 : Migration
    {
        public override void Up()
        {
            Create.Table("Customer")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("MobileNo").AsString()
                .WithColumn("Email").AsString()
                .WithColumn("Password").AsString()
                .WithColumn("CityId").AsInt32()
                .WithColumn("StateId").AsInt32()
                .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("UpdatedOn").AsDateTime().Nullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);


            Insert.IntoTable("Customer").Row(new { Name = "John", MobileNo = "1122334455", Email = "john@gmail.com", Password = "123", CityId = 1, StateId = 1 });
        }

        public override void Down()
        {
        }



    }
}