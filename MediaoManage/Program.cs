using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MediaoManage.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddPageRoute("/Medias", "Medias");
});
builder.Services.AddRouting(options => options.LowercaseUrls = true);
// Add services to the container.
//builder.Services.AddRazorPages();
builder.Services.AddDbContext<MediaoManageContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MediaoManageContext") ?? throw new InvalidOperationException("Connection string 'MediaoManageContext' not found.")));

var app = builder.Build();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
