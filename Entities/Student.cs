using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entities;

public class Student
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    [Required(ErrorMessage = "El nombre es requerido.")]
    [MinLength(3, ErrorMessage = "El nombre debe contener mínimo 3 caracteres.")]
    public string Name { get; set; } = string.Empty;

    public ICollection<StudentClass> StudentClasses { get; } = [];
}