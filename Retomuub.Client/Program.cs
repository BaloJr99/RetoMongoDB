using MongoDB.Driver;
using Retomuub.Bussiness.Interfaces;
using Retomuub.Bussiness.Services;
using Retomuub.Data.Model;

var builder = WebApplication.CreateBuilder(args);

ConfigureMongoDB();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IIngredientCollection, IngredientService>();
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

void ConfigureMongoDB(){
    var client = new MongoClient("mongodb+srv://testUser:zNW3wMQsbUWXc0Nk@retomuub.psqtodl.mongodb.net/?retryWrites=true&w=majority");
    var db = client.GetDatabase("retomuub");

    var ingredientCollection = db.GetCollection<Ingredient>("Ingredients");
    builder.Services.AddSingleton(ingredientCollection);
    builder.Services.AddSingleton<IngredientService>();
}   