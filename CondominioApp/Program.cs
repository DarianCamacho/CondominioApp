using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Google.Cloud.Firestore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


string path = "C:\\Users\\delga\\source\\repos\\CondominioApp\\CondominioApp\\firebase-config.json";
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


