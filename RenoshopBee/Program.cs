using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using RenoshopBee.Data;
using RenoshopBee.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using RenoshopBee.Services;
using RenoshopBee.Interfaces.ProductInterfaces;
using RenoshopBee.Interfaces.UserInterfaces;
using RenoshopBee.Implementation.UserImp;
using RenoshopBee.Interfaces.CartInterfaces;
using RenoshopBee.Implementation.ProductServices;
using RenoshopBee.Implementation.OrderImp;
using RenoshopBee.Interfaces.OrderInterfaces;
using RenoshopBee.Interfaces.OrderItemInterfaces;
using RenoshopBee.Implementation.OrderItemImp;
using RenoshopBee.Interfaces.WishlistInterfaces;
using RenoshopBee.Implementation.WishlistImp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString1")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option => option.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<ICartMethods, CartMethod>();
builder.Services.AddTransient<IProductSection, ProductSectionService>();
builder.Services.AddTransient<IProductSizes, ProductSizeServices>();
builder.Services.AddTransient<IProductImage, ProductImageService>();
builder.Services.AddTransient<IProductReview, ProductReviewServices>();
builder.Services.AddTransient<IProductDate, ProductDateServices>();
builder.Services.AddTransient<IProductContext, ProductContextServices>();
builder.Services.AddTransient<IProductSortTechnique, SortByModifiedDate>();
builder.Services.AddTransient<IProductSortTechnique, SortByPrice>();
builder.Services.AddTransient<IUserServices, UserContextService>();
builder.Services.AddTransient<IOrderServices, OrderServices>();
builder.Services.AddTransient<IOrderItemServices, OrderItemServices>();
builder.Services.AddTransient<IWishlistServices, WishlistServices>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "RememberMe";
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    //options.AccessDeniedPath = "AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});
builder.Services.AddSession();

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
app.UseCookiePolicy();
app.UseRouting();
app.UseAuthentication();;
app.UseSession();
app.UseAuthorization();
app.MapRazorPages();
app.UseCors();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
