using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("google.json"),
});

// Add services to the container.
builder.Services.AddRazorPages();

// Add authentication and authorization services
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";  // Set the default authentication scheme to Cookies, or use JWT Bearer for API-based authentication
    options.DefaultChallengeScheme = "Firebase";  // Use Firebase authentication challenge by default
})
.AddCookie("Cookies");  // Use cookie-based authentication;

var app = builder.Build();

// Enable authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.MapRazorPages();  // Maps Razor Pages routes

app.Run();
