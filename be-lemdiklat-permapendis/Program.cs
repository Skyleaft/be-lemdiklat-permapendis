using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using be_lemdiklat_permapendis.Data;
using be_lemdiklat_permapendis.Services;
using be_lemdiklat_permapendis.Endpoints;
using be_lemdiklat_permapendis.Repositories;
using Core.Systems;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDBContext, AppDbContext>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddTransient<IEncryptionService, EncryptionService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAvatarService, AvatarService>();
builder.Services.AddTransient<IAuthService, AuthService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/api/auth/login";
        options.LogoutPath = "/api/auth/logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

#if !AOT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Lemdiklat Permapendis API", Version = "v1" });
    c.AddSecurityDefinition("Cookie", new()
    {
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Cookie",
        Description = "Cookie authentication"
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();

});
#endif



#if AOT
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});
#endif

var app = builder.Build();

#if !AOT
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lemdiklat Permapendis API v1");
        c.RoutePrefix = "swagger";
        c.DocumentTitle = "Lemdiklat Permapendis API Documentation";
    });
}
#endif

app.UseAuthentication();
app.UseAuthorization();



app.MapUserEndpoints();
app.MapAuthEndpoints();
app.MapGet("/ping", () => $"Phoonk!! - {DateTime.Now}");

app.MapGet("/openapi.json", () => Results.File("openapi.json", "application/json"));

app.Run();

