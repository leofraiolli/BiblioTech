using BiblioTech.Web.Contracts;
using BiblioTech.Web.Repositories;
using BiblioTech.Web.Services;
using BiblioTech.Web.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Injeção de Dependência (SOLID - Dependency Inversion Principle)
builder.Services.AddSingleton<IAutorRepository>(new InMemoryAutorRepository(popularDadosIniciais: true));
builder.Services.AddSingleton<ILivroRepository>(sp =>
    new InMemoryLivroRepository(sp.GetRequiredService<IAutorRepository>(), popularDadosIniciais: true));
builder.Services.AddSingleton<IEmprestimoRepository, InMemoryEmprestimoRepository>();

builder.Services.AddScoped<ILivroValidator, LivroValidator>();
builder.Services.AddScoped<AutorService>();
builder.Services.AddScoped<LivroService>();
builder.Services.AddScoped<EmprestimoService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
