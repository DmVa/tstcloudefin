using Microsoft.OpenApi;
using Vendor.Logic;
using Vendor.Logic.Interfaces;
using Vendor.Logic.Settings;

namespace Vendor.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.Configure<VendorSettings>(builder.Configuration.GetSection("VendorSettings"));
            builder.Services.AddSingleton<ILoaderFactory, LoaderFactory>();
            builder.Services.AddScoped<IVendorService, VendorService>();

           
            // only for development env.
            const string angularDevClient = "AngularDevClient";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(angularDevClient, policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vendor API", Version = "v1" });
                c.EnableAnnotations();
            });

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddExceptionHandler<VendorApiExceptionHandler>();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(c =>
                {

                    c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_1;
                });

                app.UseSwaggerUI();
            }

            // app.UseAuthorization();

            app.UseCors(angularDevClient);

            app.UseExceptionHandler();

            app.MapControllers();

            app.Run();
        }
    }
}
