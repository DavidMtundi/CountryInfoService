using System.Globalization;
using System.Net;
using AutoMapper;
using CountryInfoService.Core.Interfaces;
using CountryInfoService.Data;
using CountryInfoService.Models;
using CountryInfoService.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using ServiceReference;

namespace CountryInfoService.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CountryController:ControllerBase
{
    private readonly ICountryService _countryService;
    private readonly IMapper _mapper;
    private readonly ILogger<CountryController> _logger;

    public CountryController(
        ICountryService countryService,
        IMapper mapper,
        ILogger<CountryController> logger)
    {
        _countryService = countryService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<CountryResponseDto>> CreateCountryInfo(
        [FromBody] CountryCreateDto createDto)
    {
        try
        {
            var result = await _countryService.GetAndSaveCountryInfoAsync(createDto.Name);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Client error occurred");
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Server error occurred");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CountryResponseDto>> GetById(int id)
    {
        try
        {
            var country = await _countryService.GetCountryByIdAsync(id);
            if (country == null)
            {
                _logger.LogWarning("Country with ID {CountryId} not found", id);
                return NotFound($"Country with ID {id} not found");
            }
            
            _logger.LogInformation("Successfully retrieved country with ID {CountryId}", id);
            return Ok(country);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving country with ID {CountryId}", id);
            return StatusCode(500, "Internal server error occurred while retrieving the country");
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CountryCreateDto updateDto)
    {
        try
        {
            await _countryService.UpdateCountryAsync(id, updateDto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Country not found");
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating country with ID {CountryId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _countryService.DeleteCountryAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Country not found");
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting country with ID {CountryId}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}