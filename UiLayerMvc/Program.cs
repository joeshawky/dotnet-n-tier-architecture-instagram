using BusinessLayer.AutoMapperProfiles;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authentication.Cookies;
using UiLayerMvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opts =>
    {
        opts.Cookie.Name = ".User.auth";
        opts.ExpireTimeSpan = TimeSpan.FromDays(7);
        opts.SlidingExpiration = false;
        opts.LoginPath = "/Account/Login";
        opts.LogoutPath = "/Account/Logout";
        opts.AccessDeniedPath = "/Home/Accessdenied";

    });

builder.Services.AddSignalR();

builder.Services.AddAutoMapper(typeof(CommentMapper));
builder.Services.AddAutoMapper(typeof(ImageMapper));
builder.Services.AddAutoMapper(typeof(UserMapper));
builder.Services.AddAutoMapper(typeof(ImageMapper));


builder.Services.AddScoped<ICommentDal, EfCommentDal>();
builder.Services.AddScoped<IRiskyCommentDal, EfRiskyCommentDal>();
builder.Services.AddScoped<ICommentDal, EfCommentDal>();
builder.Services.AddScoped<ICommentLikeDal, EfCommentLike>();
builder.Services.AddScoped<IPostDal, EfPostDal>();
builder.Services.AddScoped<IPostSaveDal, EfPostSaveDal>();
builder.Services.AddScoped<IPostLikeDal, EfPostLikeDal>();
builder.Services.AddScoped<IUserDal, EfUserDal>();
builder.Services.AddScoped<IImageDal, EfImageDal>();
builder.Services.AddScoped<IProfileImageDal, EfProfileImageDal>();
builder.Services.AddScoped<IChatInstanceDal, EfChatInstanceDal>();
builder.Services.AddScoped<IFollowInstanceDal, EfFollowInstance>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    app.UseHttpsRedirection();
}


app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
