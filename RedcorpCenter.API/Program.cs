using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RedcorpCenter.API.Mapper;
using RedcorpCenter.Domain;
using RedcorpCenter.Infraestructure;
using RedcorpCenter.Infraestructure.Context;
using System.Security.Cryptography;
using System.Text;
using RedcorpCenter.API.Middleware;
public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //dependecy inyection
        builder.Services.AddScoped<IEmployeeInfraestructure, EmployeeMySQLInfraestructure>();
        builder.Services.AddScoped<IEmployeeDomain, EmployeeDomain>();

        builder.Services.AddScoped<IProjectInfraestructure, ProjectMySQLInfraestructure>();
        builder.Services.AddScoped<IProjectDomain, ProjectDomain>();

        builder.Services.AddScoped<ITaskInfraestructure, TaskMySQLInfraestructure>();
        builder.Services.AddScoped<ITaskDomain, TaskDomain>();

        builder.Services.AddScoped<ITeamInfraestructure, TeamMySQLInfraestructure>();
        builder.Services.AddScoped<ITeamDomain, TeamDomain>();

        builder.Services.AddScoped<ISectionInfraestructure, SectionMySQLInfraestructure>();
        builder.Services.AddScoped<ISectionDomain, SectionDomain>();
        builder.Services.AddAutoMapper(typeof(ModelToResponseSection), typeof(RequestToModelSection));

        builder.Services.AddScoped<ISectionAndEmployeeInfraestructure, SectionAndEmployeeMySQLInfraestructure>();
        builder.Services.AddScoped<ISectionAndEmployeeDomain, SectionAndEmployeeDomain>();

        builder.Services.AddScoped<IEncryptDomain, EncryptDomain>();
        builder.Services.AddScoped<ITokenDomain, TokenDomain>();
        //Conexion a MYSQL
        var connectionString = builder.Configuration.GetConnectionString("redcorpCenterConnection");
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

        //Jwt   
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        });



        builder.Services.AddDbContext<RedcorpCenterDBContext>(
            dbContextOptions =>
            {
                dbContextOptions.UseMySql(connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    options => options.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: System.TimeSpan.FromSeconds(60),
                        errorNumbersToAdd: null)
                );
            });

        builder.Services.AddAutoMapper(typeof(ModelToResponse), typeof(RequestToModel));

        //Cors
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", builder =>
                builder.WithOrigins("https://redcorp-web-app.web.app")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });

        //Cors/*
        /*builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });*/

        var app = builder.Build();


        app.UseCors("AllowAllOrigins");

        //app.UseCors("AllowOrigin");

        using (var scope = app.Services.CreateScope())
        using (var context = scope.ServiceProvider.GetService<RedcorpCenterDBContext>())
        {
            context.Database.EnsureCreated();
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<JwtMiddleware>();

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

