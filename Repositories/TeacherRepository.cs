using Mapster;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Common;
using WebApplication1.Data;
using WebApplication1.Dtos.TeacherDtos;
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
}