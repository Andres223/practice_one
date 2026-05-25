using WebApplication1.Dtos.CourseDtos;

namespace WebApplication1.Dtos.ClassDto;

public class ClassDtoWithCourse
{
    public Guid Id { get; set; }
    public string Periodo { get; set; } = string.Empty;
    public CourseDto Course { get; set; } = null!;
}