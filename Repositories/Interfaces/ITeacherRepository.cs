using WebApplication1.Common;
using WebApplication1.Dtos.TeacherDtos;

namespace WebApplication1.Repositories.Interfaces;

public interface ITeacherRepository
{
    Task<Result<List<TeacherDto>>> GetAllAsync();
    Task<Result<TeacherDto>> GetByIdAsync(Guid id);
    Task<Result<TeacherDto>> CreateAsync(TeacherRequest request);
    Task<Result<TeacherDto>> UpdateAsync(Guid id, TeacherRequest request);
    Task<Result<bool>> DeleteAsync(Guid id);
}