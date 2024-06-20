using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MudBlazor.Services;
using WuwaDB.Authentication;
using WuwaDB.Components;
using WuwaDB.Components.Pages;
using WuwaDB.DBAccess.DataContext;
using WuwaDB.DBAccess.Entities.Account;
using WuwaDB.DBAccess.Entities.Login;
using WuwaDB.DBAccess.Repository;
using WuwaDB.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.AddMvc();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddDbContextFactory<WuwaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AdminRepository>();
builder.Services.AddScoped<SharedRepository>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthentication>();
builder.Services.AddScoped<CustomAuthentication>();
builder.Services.AddAuthenticationCore();
builder.Services.AddSingleton<LastestUrl>();
builder.Services.AddScoped<UrlChangeListenerService>();
builder.Services.AddHostedService<LoginUrlService>();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
    // Add other logging providers as needed
});

builder.Services.AddMudServices(x =>
    x.PopoverOptions.ThrowOnDuplicateProvider = false);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
using (var scope = app.Services.CreateScope())
{
    var userRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();
    var adminRepository = scope.ServiceProvider.GetRequiredService<AdminRepository>();
    var CheckLoginUrl = await userRepository.GetDataAsync<Login_Info>();
    if(CheckLoginUrl == null)
    {
        CheckLoginUrl = new();
        CheckLoginUrl.LoginUrl = Guid.NewGuid();
        CheckLoginUrl.LastUpdated = DateTime.UtcNow;
        await adminRepository.SavesAsync(CheckLoginUrl);
    }
}
using (var scope = app.Services.CreateScope())
{
    var userRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();
    var adminRepository = scope.ServiceProvider.GetRequiredService<AdminRepository>();
    var checkAdmin = await userRepository.GetDataAsync<Admin>();
    if (checkAdmin is null)
    {
        await adminRepository.CreateUserDataAsync(username: "ZRoku69696989", password: "ZKyu69696989", role: "Admin", additionalProp:null);
    }

}
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

