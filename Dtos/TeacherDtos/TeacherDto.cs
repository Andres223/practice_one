using WebApplication1.Dtos.ClassDto;

namespace WebApplication1.Dtos.TeacherDtos;

public class TeacherDto
{
    public Guid Id { get; set; } 
    public string Name { get; set; } = string.Empty;

    public ICollection<ClassDtoWithCourse> Classes { get; set; } = [];
}