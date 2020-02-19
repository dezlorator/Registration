using Google.Apis.Customsearch.v1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using EasyCaching.Core.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Registration.Interfaces;
using Registration.Models;
using Registration.Repository;
using Registration.Services;
using Registration.Validator;
using System;
using static Google.Apis.Services.BaseClientService;
using EasyCaching.Core;
using Microsoft.Extensions.Caching.Distributed;

namespace Registration
{
    public class Startup
    {
        #region fields
        private const string corsPolicyForRegistration = "CorsPolicyForRegistration";
        private const string corsPolicyForGame = "CorsPolicyForGame";
        private const string CachingProvider = "redis1";
        #endregion

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddControllers();

            services.AddDbContext<AuthenticationContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("GuessWhatGoogleGame")));

            services.AddIdentity<UserIdentityChanged, IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IInitializer<User>, UserInitializer>();
            services.AddTransient<IQuestionGameService, GameQuestionService>();
            services.AddTransient<IQuestionRepository, QuestionsRepository>();
            services.AddTransient<IAnswerRepository, AnswerRepository>();
            services.AddTransient<IGetPhotoFromGoogleService, GetPhotoFromGoogleService>();
            services.AddTransient<IDownloadImageService, DownloadImageService>();
            services.AddTransient<IRandomService, RandomService>();
            services.AddTransient<Random>();
            services.AddDistributedMemoryCache();
            services.AddTransient<CustomsearchService>((p) => {
                return new CustomsearchService(new Initializer { ApiKey = "AIzaSyAdIkUnMWWPtet-61OpGWV14GZ2SitCcoI" });
            });
            services.Configure<ServerURLSettings>(Configuration.GetSection("Server"));
            services.AddTransient<ICacheService, CachingFromByteService>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyMethod();
                });
                options.AddPolicy(corsPolicyForRegistration, builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                });
                options.AddPolicy(corsPolicyForGame, builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod();
                });
            });

            //services.ConfigureApplicationCookie(options =>
            //{
            //    // Cookie settings
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            //    options.LoginPath = "/api/applicationUser/SingIn";
            //    options.SlidingExpiration = true;
            //});

            services.AddEasyCaching(options =>
            {
                //use redis cache
                options.UseRedis(redisConfig =>
                {
                    //Setup Endpoint
                    redisConfig.DBConfig.Endpoints.Add(new ServerEndPoint("localhost", 6379));

                    //Setup password if applicable
                    //if (!string.IsNullOrEmpty(serverPassword))
                    //{
                    //    redisConfig.DBConfig.Password = serverPassword;
                    //}

                    //Allow admin operations
                    redisConfig.DBConfig.AllowAdmin = true;
                },
                    CachingProvider);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
