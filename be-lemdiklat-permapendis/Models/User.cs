﻿using System.ComponentModel.DataAnnotations;

namespace be_lemdiklat_permapendis.Models;

public class User
{
    public Guid Id { get; set; }= Guid.CreateVersion7();
    
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;
    
    [MaxLength(255)]
    public string Password { get; set; } = string.Empty;
    
    [MaxLength(255)]
    public string PasswordSalt { get; set; } = string.Empty;
    
    public int RoleId { get; set; }
    public Role? Role { get; set; }
    public Guid UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }
}