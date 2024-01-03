using Microsoft.EntityFrameworkCore;
using Talabat.Repository.Context;
using System.IO;
using Talabat.Core.Repository;
using Talabat.Repository;
using System.Text.Json.Serialization;
using Talabat.Api.Hellper;
using StackExchange.Redis;
using Talabat.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entityes.Identity;
using Talabat.Core.Services;
using Talapat.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core;

namespace Talabat.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // this Data Base For The Base Data 
            builder.Services.AddDbContext<AppDbcontext>(
               opt=>opt.UseSqlServer(connectionString)
                );

            // this Data Base For Auth => (Login && Register)
            builder.Services.AddDbContext<AppIdentityDbContext>(
                opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"))   
                );


            //builder.Services.AddScoped(typeof(IGenaricRepo<>),typeof(GenaricRepo<>));
            builder.Services.AddAutoMapper(typeof(MapProfile));
            builder.Services.AddScoped(typeof(IBasketRep), typeof(BasketRepo));
            builder.Services.AddScoped(typeof(ITokenServices), typeof(TokenServices));
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork> ();
            builder.Services.AddScoped<IOrderService,OrderService> ();

            // this connection to the redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(option =>
            {
                var connectionString = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connectionString);
            });
           
           

            // after i seding the data to users this line should be write
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            // this code to comper the token which user inter it with the token is genreated
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options=>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                        ValidateAudience = true,  
                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"])),
};
                });
           


          


            var app = builder.Build();


            // this line to update database after runing project 
            var Scope = app.Services.CreateScope();
            var Services = Scope.ServiceProvider;
            var loggerFactory = Services.GetRequiredService<ILoggerFactory>();  
            try
            {
               
                var dbcontext = Services.GetRequiredService<AppDbcontext>(); //Ask clr to creat opject form AppDbcontext Explicitely
                await dbcontext.Database.MigrateAsync();

                // this line to Seeding the data in database
                await ContextSeeding.SeedingAsync(dbcontext);


                var IdentityDbcontext = Services.GetRequiredService<AppIdentityDbContext>();
                await IdentityDbcontext.Database.MigrateAsync();  // there update the database   

                // this lines to seeding dataUser
                var usermanger = Services.GetRequiredService<UserManager<AppUser>>();
                await AppUserDataSeeding.SeedingUserAsync(usermanger);

            }
            catch (Exception ex)
            {
                // here when do any error show me it in cmd
               var loggr = loggerFactory.CreateLogger<Program>();
                loggr.LogError(ex , ex.Message);
                
            }
           

            app.UseStaticFiles();
           
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}