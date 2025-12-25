using Tienda_electrodomesticos_MVC.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache(); // Cache en memoria para sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true; // Más seguro
    options.Cookie.IsEssential = true; // Necesario si usas GDPR
});


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

builder.Services.AddHttpClient("api", c =>
{
    c.BaseAddress = new Uri("https://localhost:7008/api/"); // URL de tu API
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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
