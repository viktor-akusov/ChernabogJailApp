using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ChernabogJailApp.Data;
using ChernabogJailApp.Areas.Identity.Data;
using System.Diagnostics;
using ChernabogJailApp.Email;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ChernabogJailAppContextConnection") ?? throw new InvalidOperationException("Connection string 'ChernabogJailAppContextConnection' not found.");

builder.Services.AddDbContext<ChernabogJailAppContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDefaultIdentity<ChernabogJailAppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ChernabogJailAppContext>();

builder.Services.AddSingleton<IEmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddSession();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

Debug.WriteLine("SomeText");

app.Run();
