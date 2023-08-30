using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multiplex.Migrations
{
    [Migration(15)]
    public class CreateTable_5 : Migration
    {
        public override void Up()
        {
            Create.Table("Movie")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Image").AsString().NotNullable()
                .WithColumn("ReleaseDate").AsDateTime().NotNullable()
                .WithColumn("IsActive").AsBoolean().NotNullable()
                .WithColumn("IsDeleted").AsBoolean().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("UpdatedOn").AsDateTime().Nullable();

            Create.Table("Language")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("IsActive").AsBoolean().NotNullable()
                .WithColumn("IsDeleted").AsBoolean().NotNullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("UpdatedOn").AsDateTime().Nullable();

            Create.Table("Genres_Movie_Mapping")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("MovieId").AsInt32().NotNullable()
                .WithColumn("GenresId").AsInt32().NotNullable();
            
            Create.Table("FormateType_Movie_Mapping")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("MovieId").AsInt32().NotNullable()
                .WithColumn("FormateId").AsInt32().NotNullable();
            
            
            Create.Table("Language_Movie_Mapping")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("MovieId").AsInt32().NotNullable()
                .WithColumn("LanguageId").AsInt32().NotNullable();


        }
        public override void Down()
        {

        }
    }
}