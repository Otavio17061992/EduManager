using EduManager.InfraEstrutura.Data;
using EduManager.Models.Entities.Dominios;
using EduManager.Models.Entities.Interfaces;
using EduManager.Models.Entities.Metodos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração do DbContext
builder.Services.AddDbContext<EduManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<EduManager.Models.Entities.Metodos.AccountMetodos>();
builder.Services.AddScoped<EduManager.Models.Entities.Metodos.AlunoMetodos>();

builder.Services.AddScoped<IAccountRepository, AccountMetodos>();
builder.Services.AddScoped<IAccountRepository, AccountMetodos>();
builder.Services.AddScoped<IAlunosRepository, AlunoMetodos>();
builder.Services.AddScoped<ITurmaRepository, TurmaMetodos>();
builder.Services.AddScoped<IProfessorRepository, ProfessorMetodos>();


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<EduManagerContext>()
.AddDefaultTokenProviders();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// O mapeamento de rota é o Endpoint final.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

app.Run();