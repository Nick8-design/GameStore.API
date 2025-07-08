using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Dtos;

public record class CreateGameDto
(
 [Required][StringLength(30)]  string Name,
 int GenreId,
 [Range(1,200)]   decimal Price,
    DateOnly ReleaseDate
);