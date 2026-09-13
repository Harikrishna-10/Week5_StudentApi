namespace StudentApi.DTOs;

public class GradeResponseDto
{
    public int Marks { get; set; }

    public string Strategy { get; set; } = string.Empty;

    public string Grade { get; set; } = string.Empty;
}