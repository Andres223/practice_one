using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entities;

public class Class
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;
    [Required(ErrorMessage = "El periodo es requerido.")]
    public string Periodo { get; set; } = string.Empty;

    public ICollection<StudentClass> StudentClasses { get; } = [];
}