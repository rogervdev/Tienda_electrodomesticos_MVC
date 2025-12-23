using Tienda_electrodomesticos_MVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<ProductoApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7008/"); // cambia por la URL de tu API
});

builder.Services.AddHttpClient<CategoriaApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7008/"); // tu URL de la API
});

builder.Services.AddHttpClient<UsuarioApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7008/"); // URL de tu API
});

builder.Services.AddHttpClient<CarritoApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7008/"); // tu API
});



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
