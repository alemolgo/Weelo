using AutoMapper;
using co_weelo_testproject_bl.Implements;
using co_weelo_testproject_bl.Interfaces;
using co_weelo_testproject_dal.Implements;
using co_weelo_testproject_dal.Interfaces;
using co_weelo_testproject_dal.ModelData;
using co_weelo_testproject_service.Configuration;
using co_weelo_testproject_service.Implements;
using co_weelo_testproject_service.Interfaces;
using co_weelo_testproject_sl.Automapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace co_weelo_testproject_sl
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "*";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200",
                                                          "http://www.contoso.com");
                                  });
            });
            services.AddControllers();


            //Configure Authentication with JWT
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                var key = Encoding.ASCII.GetBytes(Configuration["JwtConfig:Secret"]);

                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //RequireExpirationTime = false,
                    //ValidateLifetime = true
                };
            });


            //Object of business injected
            services.AddTransient<IOwnerBl, OwnerBl>();
            services.AddTransient<IOwnerDal, OwnerDal>();
            services.AddTransient<IPropertyBl, PropertyBl>();
            services.AddTransient<IPropertyDal, PropertyDal>();
            services.AddTransient<IPropertyImageBl, PropertyImageBl>();
            services.AddTransient<IPropertyImageDal, PropertyImageDal>();
            services.AddTransient<IPropertyTraceBl, PropertyTraceBl>();
            services.AddTransient<IPropertyTraceDal, PropertyTraceDal>();
            services.AddTransient<IAuthService, AuthService>();
            // services.AddScoped<IAuthService, AuthService>();



            services.AddDbContext<WEELOContext>(options => options.UseSqlServer(Configuration.GetConnectionString("WeeloDataBase")));

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfileModule());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "co_weelo_testproject_sl", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "co_weelo_testproject_masters_netcore_sl v1"));

            app.UseCors(builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

            app.UseExceptionHandler("/error");

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}