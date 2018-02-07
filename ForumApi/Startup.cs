using ForumApi.Authorization;
using ForumApi.Interfaces;
using ForumApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForumApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = "Basic";
            }).AddCustomAuthentication("Basic", "This is my lovely authentication scheme", o => { });
            //services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, AuthorizationHandler>("Basic", options => {});
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IAnswerRepository, AnswerRepository>();
            services.AddTransient<IVoteRepository, VotesRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseMvc();
        }
    }
}
