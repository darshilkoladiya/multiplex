using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multiplex.Migrations
{
    [Migration(10)]
    public class CreatSeatType_4 : Migration
    {
        public override void Up()
        {
            Create.Table("SeatType_Screen_Mapping")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("ScreenId").AsInt32()
                .WithColumn("Seat").AsInt32()
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