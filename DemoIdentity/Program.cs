using DemoIdentity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyIdentiryDbContext>(o => o.UseSqlServer(connection));

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();


builder.Services
    .AddIdentity<MyUser, MyRol>(
     op =>
     {
         //Password
         op.Password.RequireDigit = true;
         op.Password.RequireLowercase = true;
         op.Password.RequireUppercase = true;
         op.Password.RequiredLength = 8;
         op.Password.RequireNonAlphanumeric = false;
         //Require Email confirmed
         op.SignIn.RequireConfirmedEmail = false;

         //Lockout
         op.Lockout.AllowedForNewUsers = true;
         op.Lockout.MaxFailedAccessAttempts = 8;
     }
    )
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<MyIdentiryDbContext>()
    .AddApiEndpoints();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.MapIdentityApi<MyUser>();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
