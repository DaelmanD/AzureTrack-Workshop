using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RMotownFestival.DAL
{
    public class MotownDbContext : DbContext
    {
        public MotownDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var description = "A music festival is a festival oriented towards music that is sometimes presented with a theme such as musical genre, nationality or locality of musicians, or holiday. They are commonly held outdoors, and are often inclusive of other attractions such as food and merchandise vending machines, performance art, and social activities. Large music festivals such as Lollapalooza are constructed around well known main stage acts and lesser known musicians and bands on side stages. Many festivals are annual, or repeat at some other interval, and have modular staging of many types. Each year Lollapalooza often features multiple acts on its main and side stages.";
            var stages = new List<Stage>() {
                         new Stage { Id = 1, Name = "Main Stage", Description = description },
                         new Stage { Id = 2, Name = "Orange Room", Description = description },
                         new Stage { Id = 3, Name = "StarDust", Description = description },
                         new Stage { Id = 4, Name = "Pink Room", Description = description }
            };

            var artists = new List<Artist>() {
                      new Artist { Id = 1, Name = "Diana Ross", ImagePath = "dianaross.jpg", Website = new Uri("http://www.dianaross.co.uk/indexb.html") },
                      new Artist { Id = 2, Name = "The Commodores", ImagePath = "thecommodores.jpg", Website = new Uri("http://en.wikipedia.org/wiki/Commodores") },
                      new Artist { Id = 3, Name = "Stevie Wonder", ImagePath = "steviewonder.jpg", Website = new Uri("http://www.steviewonder.net/") },
                      new Artist { Id = 4, Name = "Lionel Richie", ImagePath = "lionelrichie.jpg", Website = new Uri("http://lionelrichie.com/") },
                      new Artist { Id = 5, Name = "Marvin Gaye", ImagePath = "marvingaye.jpg", Website = new Uri("http://www.marvingayepage.net/") }
            };

            modelBuilder.Entity<Artist>().HasData(artists);
            modelBuilder.Entity<Stage>().HasData(stages);
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Stage> Stages { get; set; }
    }
}
