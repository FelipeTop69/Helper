using Business;
using Business.Services;
using Data.Factories;
using Entity.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Base de Datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(connectionString));

//Entitys
builder.Services.AddScoped<IDataFactoryGlobal, GlobalFactory>();

builder.Services.AddScoped<PersonBusiness>();
builder.Services.AddScoped<RoleBusiness>();
builder.Services.AddScoped<UserRoleBusiness>();
builder.Services.AddScoped<UserBusiness>();

//Mapper
builder.Services.AddAutoMapper(typeof(GeneralMapper));


//Swagger
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
