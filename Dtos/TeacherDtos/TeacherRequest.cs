using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.TeacherDtos;

public class TeacherRequest
{
    [Required(ErrorMessage = "El nombre es requerido.")]
    [MinLength(3, ErrorMessage = "El nombre debe contener mínimo 3 caracteres.")]
    public string Name { get; set; } = string.Empty;
}