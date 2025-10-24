using System.Text.Json.Serialization;
using be_lemdiklat_permapendis.Models;

namespace be_lemdiklat_permapendis.Json;

[JsonSerializable(typeof(User[]))]
[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(Role))]
[JsonSerializable(typeof(UserProfile))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}