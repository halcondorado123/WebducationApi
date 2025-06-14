using Microsoft.OpenApi.Models;
using WebducationApi.Modules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ✅ Registro de AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// ✅ Inyección personalizada
builder.Services.AddInjection(builder.Configuration);

// ✅ Configuración de Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WebducationApi",
        Version = "v1.0",
        Description = "API de gestión de estudiantes",
        Contact = new OpenApiContact
        {
            Name = "Jhonattan Halcón Casallas Felipe",
            Email = "falconfelipedeveloper@gmail.com",
            Url = new Uri("https://briefcase-jhonattancasallas.web.app")
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebducationApi v1.0");
    c.RoutePrefix = "swagger"; // O "" si quieres que sea página raíz
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
