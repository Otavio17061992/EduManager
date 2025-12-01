using EduManager.InfraEstrutura.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; // Já estava aqui

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração do DbContext
builder.Services.AddDbContext<EduManagerContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddDefaultIdentity<IdentityUser>(options => 
//     options.SignIn.RequireConfirmedAccount = false) // Mude a opção conforme sua regra
//     .AddEntityFrameworkStores<EduManagerContext>();

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