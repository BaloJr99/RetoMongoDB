using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Retomuub.Bussiness.Automapper;
using Retomuub.Bussiness.Interfaces;
using Retomuub.Bussiness.Jwt;
using Retomuub.Bussiness.Services;
using Retomuub.Client.Middleware;
using Retomuub.Common;
using Retomuub.Data.Model;

var builder = WebApplication.CreateBuilder(args);


ConfigureMongoDB();
builder.Services.AddControllersWithViews().AddNewtonsoftJson();;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters(){
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();

builder.Services.AddSingleton(new MapperConfiguration( cfg => {
    cfg.AddProfile(new AutomapperProfile());
}).CreateMapper());
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IIngredientCollection, IngredientService>();
builder.Services.AddScoped<IMealCollection, MealService>();
builder.Services.AddScoped<IUserCollection, UserService>();
builder.Services.AddSession();
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

app.UseSession();  

app.UseMiddleware<AuthenticationMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void ConfigureMongoDB(){
    var client = new MongoClient("mongodb+srv://testUser:zNW3wMQsbUWXc0Nk@retomuub.psqtodl.mongodb.net/?retryWrites=true&w=majority");
    var db = client.GetDatabase("retomuub");

    var ingredientCollection = db.GetCollection<Ingredient>("Ingredients");
    builder.Services.AddSingleton(ingredientCollection);
    builder.Services.AddSingleton<IngredientService>();

    var mealCollection = db.GetCollection<Meal>("Meals");
    builder.Services.AddSingleton(mealCollection);
    builder.Services.AddSingleton<MealService>();

    var userCollection = db.GetCollection<User>("Users");
    builder.Services.AddSingleton(userCollection);
    builder.Services.AddSingleton<UserService>();
}   