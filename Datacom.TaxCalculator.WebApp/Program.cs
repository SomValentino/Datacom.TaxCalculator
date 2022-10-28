using Datacom.TaxCalculator.Domain.Entities;
using Datacom.TaxCalculator.Logic;
using Datacom.TaxCalculator.Infrastructure;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IEnumerable<TaxTableEntry>>(options =>
{
    var lst = builder.Configuration.GetSection("TaxTableEntry").Get<List<TaxTableEntry>>();
    return lst;
});
builder.Services.AddTaxCalculatorInfrastructureInstaller();
builder.Services.AddTaxCalculatorLogicInstaller();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
