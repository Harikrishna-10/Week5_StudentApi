using System.ComponentModel.DataAnnotations;

namespace StudentApi.DTOs;

public class TeacherCreateDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Subject { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}