using CursoBackend.Automappers;
using CursoBackend.DTOs;
using CursoBackend.Models;
using CursoBackend.Repository;
using CursoBackend.Services;
using CursoBackend.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddSingleton<IPeopleService, People2Service>();
builder.Services.AddKeyedSingleton<IPeopleService, PeopleService>("peopleService");
builder.Services.AddKeyedSingleton<IPeopleService, People2Service>("people2Service");

builder.Services.AddKeyedSingleton<IRandomService, RandomService>("randomSingleton");
builder.Services.AddKeyedScoped<IRandomService, RandomService>("randomScoped");
builder.Services.AddKeyedTransient<IRandomService, RandomService>("randomTransient");

builder.Services.AddScoped<IPostsService, PostsService>();
builder.Services.AddKeyedScoped<ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>, BeerService>("beerService");

builder.Services.AddHttpClient<IPostsService, PostsService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["BaseUrlPosts"]);
});

//Repository
builder.Services.AddScoped<IRepository<Beer>, BeerRepository>();

//Entity Framework
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

//Validadores
builder.Services.AddScoped<IValidator<BeerInsertDto>, BeerInsertValidator>();
builder.Services.AddScoped<IValidator<BeerUpdateDto>, BeerUpdateValidator>();

//Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
