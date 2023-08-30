using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multiplex.Migrations
{
    [Migration(16)]
    public class CreateTableAndFK_6 : Migration
    {
        public override void Up()
        {
            Create.Table("Movie_Multiplex_Mapping")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("UpdatedOn").AsDateTime().Nullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("MultiplexId").AsInt32().NotNullable()
                .WithColumn("MovieId").AsInt32().NotNullable();

            Create.Table("Show")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("UpdatedOn").AsDateTime().Nullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("Time").AsDateTime().NotNullable()
                .WithColumn("MultiplexId").AsInt32().NotNullable()
                .WithColumn("MovieId").AsInt32().NotNullable()
                .WithColumn("LanguageId").AsInt32().NotNullable()
                .WithColumn("FormateId").AsInt32().NotNullable()
                .WithColumn("ScreenId").AsInt32().NotNullable();

            Create.Table("ShowPrice")
               .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
               .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
               .WithColumn("UpdatedOn").AsDateTime().Nullable().WithDefault(SystemMethods.CurrentDateTime)
               .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(false)
               .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false)
               .WithColumn("ShowId").AsInt32().Nullable()
               .WithColumn("SeatTypeId").AsInt32().Nullable()
               .WithColumn("Price").AsDecimal().Nullable();


            Create.ForeignKey("fk_Multiplex_CityId_Cities_Id").FromTable("Multiplexes").ForeignColumn("CityId").ToTable("Cities").PrimaryColumn("Id");
            Create.ForeignKey("fk_Multiplex_StateId_States_Id").FromTable("Multiplexes").ForeignColumn("StateId").ToTable("States").PrimaryColumn("Id");

            Create.ForeignKey("fk_Customer_CityId_Cities_Id").FromTable("Customer").ForeignColumn("CityId").ToTable("Cities").PrimaryColumn("Id");
            Create.ForeignKey("fk_Customer_StateId_State_Id").FromTable("Customer").ForeignColumn("StateId").ToTable("States").PrimaryColumn("Id");

            Create.ForeignKey("fk_Cities_StateId_State_Id").FromTable("Cities").ForeignColumn("StateId").ToTable("States").PrimaryColumn("Id");

            Create.ForeignKey("fk_Screen_MultiplexId_Multiplexes_Id").FromTable("Screen_Multiplex_Mapping").ForeignColumn("MultiplexId").ToTable("Multiplexes").PrimaryColumn("Id");

            Create.ForeignKey("fk_Genres_MovieId_Movie_Id").FromTable("Genres_Movie_Mapping").ForeignColumn("MovieId").ToTable("Movie").PrimaryColumn("Id");

            Create.ForeignKey("fk_Formate_MovieId_Movie_Id").FromTable("FormateType_Movie_Mapping").ForeignColumn("MovieId").ToTable("Movie").PrimaryColumn("Id");

            Create.ForeignKey("fk_LanguageMovie_MovieId_Movie_Id").FromTable("Language_Movie_Mapping").ForeignColumn("MovieId").ToTable("Movie").PrimaryColumn("Id");
            Create.ForeignKey("fk_LanguageMovie_LanguageId_Language_Id").FromTable("Language_Movie_Mapping").ForeignColumn("LanguageId").ToTable("Language").PrimaryColumn("Id");

            Create.ForeignKey("fk_MovieMultiplex_MultiplexId_Multiplexes_Id").FromTable("Movie_Multiplex_Mapping").ForeignColumn("MultiplexId").ToTable("Multiplexes").PrimaryColumn("Id");
            Create.ForeignKey("fk_MovieMultiplex_MovieId_Movie_Id").FromTable("Movie_Multiplex_Mapping").ForeignColumn("MovieId").ToTable("Movie").PrimaryColumn("Id");

            

            Create.ForeignKey("fk_ShowPrice_ShowId_Show_Id").FromTable("ShowPrice").ForeignColumn("ShowId").ToTable("Show").PrimaryColumn("Id");
            Create.ForeignKey("fk_ShowPrice_SeatTypeId_SeatType_Id").FromTable("ShowPrice").ForeignColumn("SeatTypeId").ToTable("SeatType_Screen_Mapping").PrimaryColumn("Id");

        }

        public override void Down()
        {

        }
    }
}