using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Okaz.API.Models.Repositories;
using Okaz.Okaz.API.Extensions;
using Okaz.Okaz.API.Models;
using Okaz.Okaz.API.Models.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//  Add services to the container.
builder.Services.AddDbContextPool<OkazDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("OkazDB")));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.AddAutoMapperProfiles();
builder.Services.AddRepositories();
// builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  _ = app.UseSwagger();
  _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
