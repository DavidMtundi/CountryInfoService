using CountryInfoService.Core.Interfaces;
using CountryInfoService.Data;
using CountryInfoService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CountryInfoService.Infrastructure.Repositories;

public class CountryRepository : IRepository<Country>
{
    private readonly AppDbContext _context;
    private readonly ILogger<CountryRepository> _logger;

    public CountryRepository(
        AppDbContext context, 
        ILogger<CountryRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Country>> GetAllAsync()
    {
        return await _context.Countries.ToListAsync();
    }

    public async Task<Country?> GetByIdAsync(int id)
    {
        return await _context.Countries.FindAsync(id);
    }

    public async Task<Country> AddAsync(Country entity)
    {
        _context.Countries.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Country entity)
    {
        try
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating country with ID {CountryId}", entity.Id);
            throw;
        }
    }

    public async Task DeleteAsync(Country entity)
    {
        try
        {
            _context.Countries.Remove(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting country with ID {CountryId}", entity.Id);
            throw;
        }
    }
}