using Microsoft.AspNetCore.Mvc;
using WebApplication1.Common;
using WebApplication1.Dtos.TeacherDtos;
using WebApplication1.Repositories.Interfaces;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeacherController : ControllerBase
{
    private readonly ITeacherRepository _teacherRepository;

    public TeacherController(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<TeacherDto>>>> GetAll()
    {
        var result = await _teacherRepository.GetAllAsync();
        return Ok(ApiResponse<List<TeacherDto>>.FromResult(result));
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<TeacherDto>>> GetById(Guid id)
    {
        var result = await _teacherRepository.GetByIdAsync(id);
        if (result.IsFailure)
        {
            if (result.Error?.Code == "NOT_FOUND")
            {
                return NotFound(ApiResponse<TeacherDto>
                .FromResult(result));
            }
            return BadRequest(ApiResponse<TeacherDto>
                .FromResult(result));
        }
        
        return Ok(ApiResponse<TeacherDto>.FromResult(result));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<ApiResponse<TeacherDto>>> Create(TeacherRequest request)
    {
        var result = await _teacherRepository.CreateAsync(request);
        if (result.IsFailure || result.Value == null)
        {
            return BadRequest(ApiResponse<TeacherDto>.FromResult(result));
        }
        
        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Value.Id },
            ApiResponse<TeacherDto>.FromResult(result)
        );
    }
}