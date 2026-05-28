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
                Code = "404"
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

    public async Task<Result<TeacherDto>> UpdateAsync(Guid id, TeacherRequest request)
    {
        var teacher = await context.Teachers.FirstOrDefaultAsync(t => t.Id.Equals(id));
        if (teacher is null)
        {
            return Result<TeacherDto>.Failure(new ApiError
            {
                Message = $"No se encuentra un profesor con el id '{id}'",
                Code = "404"
            });
        }
        
        teacher.Name = request.Name;
        await context.SaveChangesAsync();
        var teacherDto = teacher.Adapt<TeacherDto>();
        return Result<TeacherDto>.Success(teacherDto);
    }

    public async Task<Result<bool>> DeleteAsync(Guid id)
    {
        var teacher = await context.Teachers.FirstOrDefaultAsync(t => t.Id.Equals(id));
        if (teacher is null)
        {
            return Result<bool>.Failure(new ApiError
            {
               Message = $"No existe un profesor con el id '{id}'",
               Code = "404"
            });
        }
        context.Teachers.Remove(teacher);
        await context.SaveChangesAsync();
        return Result<bool>.Success(true);
    }
}