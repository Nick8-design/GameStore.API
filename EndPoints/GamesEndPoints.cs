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


    private static readonly List<GameSummary> games = [
    new(
1,
"Candy Crush",
"All Ages",
46.0M,
new DateOnly(2018,8,8)
),
new(
2,
"Snake Game",
"Adult",
6.0M,
new DateOnly(1999,8,8)
),
new(
3,
"Box Puzzle",
"Family",
100.0M,
new DateOnly(2023,8,8)
),


];


    public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app)
    {

        var route = app.MapGroup("games");


        //GET /games
        route.MapGet("/", () => games);

        //Get /games/1
        route.MapGet("/{id}", (int id,GameStoreContext dbContext) =>
        {


            var game = dbContext.Games.Find(id);


            return game is null ? Results.NotFound() : Results.Ok(game);

        }
        )
        .WithName("GateGame");






        //POST /games
        route.MapPost("/", (CreateGameDto newGame,GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();
          
            dbContext.Games.Add(game);
            dbContext.SaveChanges();



            return Results.CreatedAtRoute("GateGame", new { id = game.Id }, game.ToGameDetailsDto());

        });





        //PUT /games
        route.MapPut("/{id}", (int id, UpdateGameDto updateGameDto,GameStoreContext dbCOntext) =>
        {
            var exidtingGame = dbCOntext.Games.Find(id);


            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameSummary(
                id,
                updateGameDto.Name,
                updateGameDto.Genre,
                updateGameDto.Price,
                updateGameDto.ReleaseDate
            );
            return Results.NoContent();

        });


        //DEl 
        route.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(g => g.Id == id);
            return Results.NoContent();
        }
        );

        return route;




    } 



}
