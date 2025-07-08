using System;
using GameStore.API.Data;
using GameStore.API.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.EndPoints;

public static class GenreEndPoints
{

    public static RouteGroupBuilder MapGenresEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("genres");

        group.MapGet("/", async (GameStoreContext dbContext) =>


            await dbContext.Genres
            .Select(x => x.ToDto())
            .AsNoTracking()
            .ToListAsync()

        );

return group;
}


}
