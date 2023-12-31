using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http;
using Okaz.Client.Services;
using Okaz.Client.Extensions;
using Okaz.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

{
  builder.Services.AddAutoMapperProfiles();
  builder.Services.AddRazorPages();
  builder.Services.AddServerSideBlazor();

  builder.Services.RegisterHttpClientServices();
}

var app = builder.Build();
{
  // Configure the HTTP request pipeline.
  if (!app.Environment.IsDevelopment())
  {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
  }

  app.UseHttpsRedirection();

  app.UseStaticFiles();

  app.UseRouting();

  app.MapBlazorHub();
  app.MapFallbackToPage("/_Host");

  app.Run();
}