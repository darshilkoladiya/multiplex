using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multiplex.Migrations
{
    [Migration(9)]
    public class CreateNewScreen_3 : Migration
    {
        public override void Up()
        {
            Create.Table("Screen_Multiplex_Mapping")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("MultiplexId").AsInt32()
                .WithColumn("Name").AsString()
                .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("UpdatedOn").AsDateTime().Nullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);

        }

        public override void Down()
        {
            
        }



    }
}