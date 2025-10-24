using be_lemdiklat_permapendis.Services;
using be_lemdiklat_permapendis.Dto;
using be_lemdiklat_permapendis.Models;

namespace be_lemdiklat_permapendis.Endpoints;

public static class UserEndpoint
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var users = app.MapGroup("/api/users");
        
        users.MapPost("/find", async (FindRequest request, IUserService userService) => 
            await userService.Find(request));
            
        users.MapGet("/{id}", async (string id, IUserService userService) =>
        {
            if (!Guid.TryParse(id, out var guid)) return Results.BadRequest();
            var user = await userService.Get(guid);
            return user is not null ? Results.Ok(user) : Results.NotFound();
        });
                
        users.MapPost("/", async (CreateUserRequest request, IUserService userService) =>
        {
            await userService.Create(request);
            return Results.Created();
        });
        
        users.MapPut("/{id}", async (string id, UserProfile profile, IUserService userService) =>
        {
            if (!Guid.TryParse(id, out var guid)) return Results.BadRequest();
            await userService.Update(guid, profile);
            return Results.NoContent();
        });
        
        users.MapDelete("/{id}", async (string id, IUserService userService) =>
        {
            if (!Guid.TryParse(id, out var guid)) return Results.BadRequest();
            await userService.Delete(guid);
            return Results.NoContent();
        });
    }
}
