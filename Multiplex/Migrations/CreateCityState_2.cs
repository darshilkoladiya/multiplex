using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multiplex.Migrations
{
    [Migration(2)]
    public class CreateCityState_2 : Migration
    {
        public override void Up()
        {
            Create.Table("Cities")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("StateId").AsInt32().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("UpdatedOn").AsDateTime().Nullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);

            Insert.IntoTable("Cities").Row(new { StateId = 1, Name = "Surat" });
            Insert.IntoTable("Cities").Row(new { StateId = 1, Name = "Rajkot" });
            Insert.IntoTable("Cities").Row(new { StateId = 2, Name = "Udaipur" });
            Insert.IntoTable("Cities").Row(new { StateId = 2, Name = "Jesalmer" });
            Insert.IntoTable("Cities").Row(new { StateId = 3, Name = "Mumbai" });
            Insert.IntoTable("Cities").Row(new { StateId = 3, Name = "Pune" });


            Create.Table("States")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("UpdatedOn").AsDateTime().Nullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);


            Insert.IntoTable("States").Row(new { Name = "Gujarat" });
            Insert.IntoTable("States").Row(new { Name = "Rajasthan" });
            Insert.IntoTable("States").Row(new { Name = "Maharashtra" });

            Create.Table("Multiplexes")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Address").AsString().NotNullable()
                .WithColumn("CityId").AsInt32().NotNullable()
                .WithColumn("StateId").AsInt32().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("UpdatedOn").AsDateTime().Nullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);

            Insert.IntoTable("Multiplexes").Row(new { Name = "Raj Imparial", Address="Nana Varachha", CityId=1, StateId=1 });

        }

        public override void Down()
        {
            
        }



    }
}