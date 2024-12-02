

using system.dto;

namespace system.Endpoints;

public static class GamesEndpoints
{
const string GetGameEndpointName ="GetGame";
private static readonly List<Data> games =[
  new(
    1,
    "fifa 23",
    "sports",
    69.99M,
    new DateOnly (2023,03,11)
  ),
  new(
    2,
    "fifa 24",
    "sports",
    60.99M,
    new DateOnly (2024,03,11)
  )
];
public static RouteGroupBuilder MapGamesEndpoints( this WebApplication app)
{
    var group = app.MapGroup("games")
    .WithParameterValidation();
    group.MapGet("/",()=> games) ;
    group.MapGet("/{id}",(int id)=>{
  Data? game = games.Find(game=>game.ID ==id);
  return game is null ? Results.NotFound(): Results.Ok(game);
  }) 
 .WithName(GetGameEndpointName);
group.MapPost("/",(CreateGameDto newGame)=>{
   Data game =new(
    games.Count +1,
    newGame.Name,
    newGame.Genre,
    newGame.Price,
    newGame.ReleaseDate);

  games.Add(game);
  return Results.CreatedAtRoute(GetGameEndpointName,new{id= game.ID},game); 
    
})
;

group.MapPut("/{id}",(int id,UpdateGamedto updateGame)=> 
{
  var index = games.FindIndex(game => game.ID == id);
  if(index== -1){
    return Results.NotFound();
  }
  games[index] = new Data(
    id,
    updateGame.Name,
    updateGame.Genre,
    updateGame.Price,
    updateGame.ReleaseDate

  );
  return Results.NoContent();
});
  group.MapDelete("/{id}",(int id)=>{
  games.RemoveAll(game => game.ID == id);
  return Results.NoContent();
});
return group;

}
}
