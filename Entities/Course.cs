using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Entities;

public class Course
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    [Required(ErrorMessage = "El nombre es requerido.")]
    [MinLength(3, ErrorMessage = "El nombre debe contener mínimo 3 caracteres.")]
    public string Name { get; set; } = string.Empty;

    public ICollection<Class> Classes { get; } = new List<Class>();
}