using System;
using GameStore.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{

    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
new {
    Id = 1,
    Name = "Adult",
},
new {
    Id = 2,
    Name = "Family",
},
new {
    Id = 3,
    Name = "All ages",
},
new {
    Id = 4,
    Name = "Children",
},
new {
    Id = 5,
    Name = "Fighting",
},
new {
    Id = 6,
    Name = "Racing",
}


        );
    }

}
