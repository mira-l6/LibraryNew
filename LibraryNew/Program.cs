using GroqSharp;
using LibraryNew.Data;
using LibraryNew.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using LibraryNew.Services;

namespace LibraryNew
{
    public class Program
    {
        public static async Task Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<LibraryDbContext>();
            builder.Services.AddControllersWithViews();


            //var apiKey = builder.Configuration["ApiKey"];
            //var apiModel = "llama-3.1-70b-versatile";

            //builder.Services.AddSingleton<IGroqClient>(gc =>
            //new GroqClient(apiKey, apiModel)
            //.SetTemperature(0.5)
            //.SetMaxTokens(512)
            //.SetTopP(1)
            //.SetStop("NONE")
            //.SetStructuredRetryPolicy(5));


            builder.Services.AddSingleton<ITimeNow,TimeService>();
            builder.Services.AddSingleton<IGreeting, Greeting>();
            builder.Services.AddSingleton<IQuoteGenerate, QuoteGenerate>();

            builder.Services.AddHttpClient<GeminiService>();

            // builder.Services.AddScoped;
            // builder.Services.AddTransient



            var app = builder.Build();

            // Enable Developer Exception Page temporarily
            app.UseDeveloperExceptionPage();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            //Roles

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

                var roles = new[] {
                    "Admin",
                    "User"
                };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                var adminEmail = "admin@gmail.com";
                var adminPassword = "Admin_123";

                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var admin = new IdentityUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail
                    };

                    var finalResult = await userManager.CreateAsync(admin, adminPassword);
                    if (finalResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(admin, "Admin");
                    }
                }

            }

            app.Run();


                }
            }
        }



    
