using Bingo;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/ping", () => "pong")
    .WithName("Ping")
    .WithOpenApi();

app.MapGet("/", ([FromQuery(Name = "size")] int? size) => Results.Text(Templates.MainTemplate().Render(new { Size = size ?? 4 }), "text/html"))
    .WithName("Main")
    .WithOpenApi();

app.MapGet("/board", ([FromQuery(Name = "board")] int board) =>
    {
        if (BingoService.Boards.TryGetValue(board, out var boardDto))
        {
            return Results.Text(Templates.BoardTemplate().Render(new { Fields = boardDto.FieldsInRows, Room = board }),
                "text/html");
        }

        return Results.Text(Templates.NotFound().Render(), "text/html");
    })
    .WithName("Board")
    .WithOpenApi();

app.MapPost("/board", (CreateBoardDto dto) =>
    {
        var key = BingoService.AddBoard(new BingoBoard(dto.values));

        if (BingoService.Boards.TryGetValue(key, out var boardDto))
        {
            return Results.Text(Templates.BoardTemplate().Render(new { Fields = boardDto.FieldsInRows, Room = key }),
                "text/html");
        }

        return Results.Text(Templates.InternalErr().Render(), "text/html");
    })
    .WithName("CreateBoard")
    .WithOpenApi();

app.MapPost("/board/{id:int}/{field:int}", (int id, int field) =>
    {
        var board = BingoService.MarkField(id, field);
        if (board != null)
            return Results.Text(Templates.BoardTemplate().Render(new { Fields = board.FieldsInRows, Room = id }), "text/html");
        return Results.Text(Templates.NotFound().Render(), "text/html");
    })
    .WithName("Mark field")
    .WithOpenApi();

app.Run();