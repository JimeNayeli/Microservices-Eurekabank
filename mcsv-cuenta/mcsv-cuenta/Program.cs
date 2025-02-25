using mcsv_cuenta.Data;
using mcsv_cuenta.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Habilitar CORS para cualquier origen
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy => policy.AllowAnyOrigin()  // ⚠️ Permite cualquier IP u origen
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Agregar DbContext con la cadena de conexión
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar servicios
builder.Services.AddScoped<CuentaService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🛑 IMPORTANTE: Agregar `app.UseCors()` antes de Authorization
app.UseRouting();
app.UseCors("CorsPolicy");  // Habilita CORS para cualquier IP

app.UseAuthorization();

app.MapControllers();

app.Run();
