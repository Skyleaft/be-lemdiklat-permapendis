using Microsoft.EntityFrameworkCore;
using be_lemdiklat_permapendis.Data;
using be_lemdiklat_permapendis.Services;
using be_lemdiklat_permapendis.Endpoints;
using be_lemdiklat_permapendis.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDBContext, AppDbContext>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddTransient<IEncryptionService, EncryptionService>();
builder.Services.AddTransient<IUserService, UserService>();

#if !AOT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
    app.UseSwaggerUI();
}
#endif



app.MapUserEndpoints();
app.MapGet("/ping", () => $"Phoonk!! - {DateTime.Now}");

app.MapGet("/openapi.json", () => Results.File("openapi.json", "application/json"));

app.Run();

