using Mango.Web.Services;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

string identityServUrl = new
            ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build()
            .GetSection("ServiceUrls")["IdentityServer"];



// Add services to the container.
builder.Services.AddControllersWithViews();

//Add implementation of HTTPClient

builder.Services.AddHttpClient<IProductService, ProductService>();
builder.Services.AddHttpClient<ICartService, CartService>();
builder.Services.AddHttpClient<ICouponService, CouponService>();


//Add implementation of ProductService, CartService
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICouponService, CouponService>();




//Add Authentification
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
                .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = identityServUrl;
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.ClientId = "mango";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code";

                    options.ClaimActions.MapJsonKey("role", "role", "role");
                    options.ClaimActions.MapJsonKey("sub", "sub", "sub");
                    options.TokenValidationParameters.NameClaimType = "name";
                    options.TokenValidationParameters.RoleClaimType = "role";
                    options.Scope.Add("mango");
                    options.SaveTokens = true;

                });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.Run();
