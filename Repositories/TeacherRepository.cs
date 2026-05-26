using Mapster;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Common;
using WebApplication1.Data;
using WebApplication1.Dtos.TeacherDtos;
using WebApplication1.Entities;
using WebApplication1.Repositories.Interfaces;

namespace WebApplication1.Repositories;

public class TeacherRepository(AppDbContext context) : ITeacherRepository
{
    public async Task<Result<List<TeacherDto>>> GetAllAsync()
    {
        var teachers = await context.Teachers
            .Include(t => t.Classes)
                .ThenInclude(c => c.Course)
            .ToListAsync();
            
        var teachersDto = teachers.Adapt<List<TeacherDto>>();
        return Result<List<TeacherDto>>.Success(teachersDto);
    }

    public async Task<Result<TeacherDto>> GetByIdAsync(Guid id)
    {
        var teacher = await context.Teachers
            .FirstOrDefaultAsync(t => t.Id.Equals(id));
        
        if(teacher == null)
        {
            return Result<TeacherDto>.Failure(new ApiError
            {
                Message = $"No se encuentra un profesor con el id '{id}'",
                Code = "NOT_FOUND"
            }
            );
        }
        
        var teacherDto = teacher.Adapt<TeacherDto>();
        return Result<TeacherDto>.Success(teacherDto);
    }
    
    public async Task<Result<TeacherDto>> CreateAsync(TeacherRequest request)
    {
        var teacher = new Teacher{ Name = request.Name };
        await context.Teachers.AddAsync(teacher);
        await context.SaveChangesAsync();
        
        var createdDto = teacher.Adapt<TeacherDto>();
        return Result<TeacherDto>.Success(createdDto);
    }
}