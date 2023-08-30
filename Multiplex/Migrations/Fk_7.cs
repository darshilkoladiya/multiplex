using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multiplex.Migrations
{
    [Migration(17)]
    public class Fk_7 : Migration
    {
        public override void Up()
        {
            Create.ForeignKey("fk_Show_MultiplexId_Multiplexes_Id").FromTable("Show").ForeignColumn("MultiplexId").ToTable("Multiplexes").PrimaryColumn("Id");
            Create.ForeignKey("fk_Show_MovieId_Movie_Id").FromTable("Show").ForeignColumn("MovieId").ToTable("Movie").PrimaryColumn("Id");
            Create.ForeignKey("fk_Show_FormateId_Formate_Id").FromTable("Show").ForeignColumn("FormateId").ToTable("FormateType_Movie_Mapping").PrimaryColumn("Id");
            Create.ForeignKey("fk_Show_ScreenId_Screen_Id").FromTable("Show").ForeignColumn("ScreenId").ToTable("Screen_Multiplex_Mapping").PrimaryColumn("Id");
            Create.ForeignKey("fk_Show_LanguageId_LanguageMovie_Id").FromTable("Show").ForeignColumn("LanguageId").ToTable("Language_Movie_Mapping").PrimaryColumn("Id");
        }
        public override void Down()
        {

        }
    }
}