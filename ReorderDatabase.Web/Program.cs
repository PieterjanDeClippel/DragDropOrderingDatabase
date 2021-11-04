using Microsoft.AspNetCore.SpaServices.AngularCli;
using ReorderDatabase.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddReorderDatabase(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("Notebook");
});
builder.Services.AddSpaStaticFiles(options =>
{
    options.RootPath = "ClientApp/dist";
});

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
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";
    if (app.Environment.IsDevelopment())
    {
        spa.UseAngularCliServer(npmScript: "start");
    }
});

app.Run();

// dotnet --version
// dotnet ef --version
// dotnet tool update dotnet-ef --version 6.0.0-rc.2.21480.5 --global
// dotnet ef migrations add AddNotes
// dotnet ef database update