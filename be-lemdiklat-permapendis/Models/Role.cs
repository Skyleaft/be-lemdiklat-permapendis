﻿using System.ComponentModel.DataAnnotations;

namespace be_lemdiklat_permapendis.Models;

public class Role
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string? Name { get; set; }
    public int Level { get; set; }
}