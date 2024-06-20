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

using (var scope = app.Services.CreateScope())
{
    var adminRepository = scope.ServiceProvider.GetRequiredService<AdminRepository>();
    var userRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();
    var loginInfo = await userRepository.GetDataAsync<Login_Info>();
    if (loginInfo is null)
    {
        loginInfo = new();
        loginInfo.LoginUrl = Guid.NewGuid();
        loginInfo.LastUpdated = DateTime.UtcNow;
        await adminRepository.SavesAsync(loginInfo);
    }
}

using (var scope = app.Services.CreateScope())
{
    
    var adminRepository = scope.ServiceProvider.GetRequiredService<AdminRepository>();
    var userRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();
    var roleInfo = await userRepository.GetToListAsync<Role>();
    if (roleInfo.Count > 0)
    {
        if (roleInfo.FirstOrDefault(x => x.Name == "Admin") is not null)
        {
            Role Role = new()
            {
                Name = "Admin",

            };
           await adminRepository.SavesAsync(Role);
        }
        if (roleInfo.FirstOrDefault(x => x.Name == "User") is not null)
        {
            Role Role = new()
            {
                Name = "User",

            };
            await adminRepository.SavesAsync(Role);
        }
    }
    else
    {
        Role RoleAdmin = new()
        {
            Name = "Admin",

        };
        await adminRepository.SavesAsync(RoleAdmin);
        Role RoleUser = new()
        {
            Name = "User",
        };
        await adminRepository.SavesAsync(RoleUser);
    }
   
}

using (var scope = app.Services.CreateScope())
{
    var adminRepository = scope.ServiceProvider.GetRequiredService<AdminRepository>();
    var userRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();
    var accountInfo = await userRepository.GetToListAsync<Account>();
    if (accountInfo.Count > 0)
    { 
        var roleId = await userRepository.GetDataAsync<Role>(new { Name = "Admin" });
        if (roleId is not null)
        {
            if (accountInfo.Any(x => x.RoleId == roleId.Id))
            { }
            else
            {
                Admin Admin = new()
                {
                    Username = "ZRokude696989",
                    Password = "ZKyu69696989",
                    RoleId = roleId.Id
                };
                await adminRepository.SavesAsync(Admin);
            }
        }
    }
}
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
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

