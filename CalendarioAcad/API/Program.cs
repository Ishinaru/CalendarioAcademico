
using API.Data;
using API.Services.Calendarios;
using API.Services.Eventos;
using API.Services.Historicos;
using API.Services.Portarias;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();

            builder.Services.AddScoped<IHistoricoInterface, HistoricoService>();
            builder.Services.AddScoped<IEventoInterface, EventoService>();
            builder.Services.AddScoped<HistoricoService>();
            builder.Services.AddScoped<ICalendarioInterface, CalendarioService>();
            builder.Services.AddScoped<IPortariaInterface, PortariaService>();

            var connectionString = builder.Configuration.GetConnectionString("CalendarioAcademicoContext");

            builder.Services.AddDbContextFactory<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddCors(
                x => x.AddPolicy(
                    Configuration.CorsPolicyName,
                    police => police
                    .WithOrigins([
                        Configuration.BackendUrl,
                        Configuration.FrontendUrl
                        ])
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    ));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(Configuration.CorsPolicyName);

            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
