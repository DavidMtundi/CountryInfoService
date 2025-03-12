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

    public Task UpdateAsync(Country entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Country entity)
    {
        throw new NotImplementedException();
    }
}