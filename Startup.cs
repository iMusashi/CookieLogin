using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace AutomobileCMS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            var x = Configuration["Jwt:Audience"];
            services.Configure<JwtTokenOptions>(Configuration.GetSection("Jwt"));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            List<string> s = new List<string>();
            var a = (IEnumerable<string>)s;
            a.Where(a => EF.Functions.Like(a.N))

            //var tokenValidationParameters = new TokenValidationParameters()
            //{
            //    ValidateIssuer = true,
            //    ValidateAudience = true,
            //    ValidateLifetime = true,
            //    ValidateIssuerSigningKey = true,
            //    ValidIssuer = Configuration["Jwt:Issuer"],
            //    ValidAudience = Configuration["Jwt:Audience"],
            //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            //};
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options =>
            //   {
            //       options.Cookie.HttpOnly = true;
            //       options.Cookie.Path = "/";
            //       options.LoginPath = "/";
            //       options.SlidingExpiration = true;
            //       options.Cookie.Expiration = new System.TimeSpan(10, 0, 0, 0);
            //       options.Cookie.Name = ".AspNetCore.WmsCookie";
            //   });
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = tokenValidationParameters;
            //});

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("CanAccessContact", policy => policy.RequireAssertion(context =>
            //    context.User.IsInRole("Admin") && context.User.IsInRole("DSA")));
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            //var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            //var options = new TokenProviderOptions
            //{
            //    Audience = Configuration["Jwt:Audience"],
            //    Issuer = Configuration["Jwt:Issuer"],
            //    SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
            //};

            //app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));
            //app.UseMiddleware<JWTInHeaderMiddleware>();
            //app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Security}/{action=Index}/{id?}");
            });
        }
    }
}