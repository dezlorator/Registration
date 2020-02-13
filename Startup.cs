using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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

namespace Registration
{
    public class Startup
    {
        #region fields
        private readonly string corsPolicyForRegistration = "CorsPolicyForRegistration";
        private readonly string corsPolicyForGame = "CorsPolicyForGame";
        #endregion

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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
            services.Configure<ServerURLSettings>(Configuration.GetSection("Server"));

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
