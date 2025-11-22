using Microsoft.EntityFrameworkCore;
using Inventario.Data;

public class Program
{
    /// <summary>
    /// Punto de entrada principal de la aplicación.
    /// </summary>
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    /// <summary>
    /// Configura y construye el host de la aplicación.
    /// Define servicios, middleware y el contexto de base de datos.
    /// </summary>
    /// <param name="args">Argumentos de la línea de comandos</param>
    /// <returns>IHostBuilder configurado</returns>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices((context, services) =>
                {
                    var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                    // Configuración del contexto de base de datos con MySQL
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseMySql(
                            connectionString, // Usa la variable obtenida de la configuración
                            ServerVersion.AutoDetect(connectionString) // Usa la variable para AutoDetect también
                        )
                    );

                    // Registro de los controladores para la API
                    services.AddControllers();

                    // Configuración de política CORS para permitir frontend en Angular (localhost:4200)
                    services.AddCors(options =>
                    {
                        // Define la política CORS para desarrollo (localhost)
                        options.AddPolicy("CorsPolicy", builder =>
                        {
                            // En desarrollo (lo lee de appsettings), se permite localhost.
                            // En Azure, puedes usar un comodín (*) temporalmente para facilitar las pruebas.
                            var allowedOrigins = context.Configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>();
                            
                            builder.WithOrigins(allowedOrigins)
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                            
                            // Política de producción: si la URL de origen no está en la lista (como localhost), permitimos cualquier origen
                            // Esto simplifica la prueba cuando el frontend esté en Azure Static Web Apps.
                            if (allowedOrigins == null || allowedOrigins.Length == 0)
                            {
                                builder.AllowAnyOrigin() 
                                        .AllowAnyMethod()
                                        .AllowAnyHeader();
                            }
                        });
                    });
                });

                webBuilder.Configure(app =>
                {
                    // Activar la política CORS definida anteriormente
                    app.UseCors("CorsPolicy");

                    app.UseRouting();

                    // Habilita archivos estáticos desde wwwroot
                    app.UseStaticFiles();

                    // Mapeo de endpoints de controladores
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });
            });
}
