using System;
using GameStore.API.Data;
using GameStore.API.Dtos;
using GameStore.API.Entities;
using GameStore.API.Mapping;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Extensions.Validation;

namespace GameStore.API.EndPoints;

public static class GamesEndPoints
{



    public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app)
    {

        var route = app.MapGroup("games");


        //GET /games
        route.MapGet("/",async (GameStoreContext dbContext) =>
        {


            return await dbContext.Games
            .Include(x => x.Genre)
            .Select(x => x.ToGameDetailsDto())
            .AsNoTracking()
            .ToListAsync()

            ;
            
        }

      );

        //Get /games/1
        route.MapGet("/{id}",async (int id,GameStoreContext dbContext) =>
        {


            var game =  await dbContext.Games.FindAsync(id);


            return game is null ? Results.NotFound() : Results.Ok(game);

        }
        )
        .WithName("GateGame");






        //POST /games
        route.MapPost("/",async (CreateGameDto newGame,GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();
          
            dbContext.Games.Add(game);
         await   dbContext.SaveChangesAsync();



            return Results.CreatedAtRoute("GateGame", new { id = game.Id }, game.ToGameDetailsDto());

        });





        //PUT /games
        route.MapPut("/{id}",async (int id, UpdateGameDto updateGame,GameStoreContext dbCOntext) =>
        {
            var exidtingGame =await dbCOntext.Games.FindAsync(id);


            if (exidtingGame is null)
            {
                return Results.NotFound();
            }

            dbCOntext.Entry(exidtingGame)
            .CurrentValues
            .SetValues(updateGame.ToEntity(id));

         await   dbCOntext.SaveChangesAsync();
            return Results.NoContent();

        });


        //DEl 
        route.MapDelete("/{id}",async (int id,GameStoreContext dbCOntext) =>
        {
         await   dbCOntext.Games
            .Where(g => g.Id == id)
            .ExecuteDeleteAsync();

            // dbCOntext.SaveChanges();
            return Results.NoContent();
        }
        );

        return route;




    } 



}
