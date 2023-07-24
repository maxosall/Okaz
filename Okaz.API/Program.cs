using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Okaz.API.Extensions;
using Okaz.API.Models;
using Okaz.API.Models.Repositories;
using Okaz.API.Models.Repositories;
using Okaz.API.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//  Add services to the container.
builder.Services.AddDbContext<OkazDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("OkazDB")));


builder.Services.AddAutoMapperProfiles();
builder.Services.AddRepositories();

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

// app.UseCors(policy =>
//   policy.WithOrigins("https://localhost:7200","http://localhost:5014")
//   .AllowAnyMethod()
//   .WithHeaders(HeaderNames.ContentType)
// );
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
