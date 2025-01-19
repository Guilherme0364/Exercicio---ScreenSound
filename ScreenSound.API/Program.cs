using System.Text.Json.Serialization;
using Azure.Messaging;
using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Shared.Modelos.Modelos;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>(); // Esse service extingue a necessidade de declarar o dal em toda função (rota)
builder.Services.AddTransient<DAL<Musica>>();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

app.MapGet("/artistas", ([FromServices] DAL<Artista> dal) =>
{
    dal.Listar();
    return Results.Ok();
});

app.MapGet("/artistas/{nome}", ([FromServices] DAL<Artista> dal, string nome) =>
{ 
    var artista = dal.RecuperarPor(a => a.Nome.ToLower().Equals(nome.ToLower())); // Captura a iteração única "a" e compara o nome passado na rota

    if (artista is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(artista);
});

app.MapPost("/artista", ([FromServices] DAL<Artista> dal, [FromBody] Artista artista) => // "FromBody" indica que receberemos a informação no corpo da requisição
{
    dal.Adicionar(artista);
    return Results.Ok();
});

app.MapDelete("/artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
{
    var artista = dal.RecuperarPor(a => a.Id.Equals(id));
    if (artista is null)
    {
        return Results.NotFound();
    }
    dal.Deletar(artista);
    return Results.NoContent();
});

app.MapPut("artistas", ([FromServices] DAL<Artista> dal, [FromBody] Artista artista ) =>
{
    var artistaAAtualizar = dal.RecuperarPor(a => a.Id.Equals(artista.Id)); // Compara o ID do artista passado na rota com algum do banco de dados
    if(artistaAAtualizar is null)
    {
        return Results.NotFound();
    }
    artistaAAtualizar.Nome = artista.Nome;
    artistaAAtualizar.Bio = artista.Bio;
    artistaAAtualizar.FotoPerfil = artista.FotoPerfil;

    dal.Atualizar(artistaAAtualizar);
    return Results.Ok();
});

// Rotas de musica

app.MapGet("/musica", ([FromServices] DAL<Musica> dal) =>
{    
    return Results.Ok(dal.Listar());
});

app.MapPost("/musica", ([FromServices] DAL<Musica> dal, [FromBody]Musica musica) =>
{
    dal.Adicionar(musica);
    return Results.Ok();
});

app.MapPut("/musicas", ([FromServices] DAL<Musica> dal, [FromBody]Musica musica) =>
{
    var musicaAAtulizar = dal.RecuperarPor(a => a.Id.Equals(musica.Id));
    if(musicaAAtulizar is null)
    {
        return Results.NotFound();
    }
    musicaAAtulizar.Nome = artista.Nome;
    musicaAAtulizar.Bio = artista.Bio;
    musicaAAtulizar.FotoPerfil = artista.FotoPerfil;

    dal.Atualizar(artistaAAtualizar);
    return Results.Ok();
});

app.Run();
