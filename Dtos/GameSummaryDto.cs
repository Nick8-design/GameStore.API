namespace GameStore.API.Dtos;

public record class GameSummary
(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
