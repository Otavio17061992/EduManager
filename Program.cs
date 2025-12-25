using EduManager.InfraEstrutura.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; 
using EduManager.Models.Entities.Dominios;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração do DbContext
builder.Services.AddDbContext<EduManagerContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity(options => 
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