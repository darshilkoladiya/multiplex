using Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
   public class MultiplexContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Multiplexes> multiplexes { get; set; }
        public DbSet<Cities> cities { get; set; }
        public DbSet<States> states { get; set; }
        public DbSet<Screen> screens { get; set; }
        public DbSet<SeatType> seatTypes { get; set; }
        public DbSet<Movie> movies { get; set; }
        public DbSet<Language>languages { get; set; }
        public DbSet<Genres_Movie> genresMovies { get; set; }
        public DbSet<Formate_Movie> formateMovies { get; set; }
        public DbSet<Language_Movie> languageMovies  { get; set; }
        public DbSet<Movie_Multiplex> movieMultiplexes { get; set; }
        public DbSet<Show> shows { get; set; }
        public DbSet<ShowPrice> showPrices { get; set; }

        public System.Data.Entity.DbSet<Data.Models.ShowPriceModel> ShowPriceModels { get; set; }
    }
}
