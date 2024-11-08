using Microsoft.EntityFrameworkCore;
using webapitest.Data;
using webapitest.Repository.Models.Distortions;

namespace webapitest.Repository;

public class DistortionRepository
{
    private readonly PostgresDbContext _dbContext;

    public DistortionRepository(PostgresDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> AddDistortion(string DistortionName)
    {
        var newDistortion = new Distortion
        {
            Id = Guid.NewGuid(),
            Name = DistortionName
        };

        _dbContext.Add(newDistortion);
        await _dbContext.SaveChangesAsync();

        return newDistortion.Id.ToString();
    }

    public async Task<List<DistortionDto>> GetAllDistortions()
    {
        var allDistortions = await _dbContext.Distortion.ToListAsync();

        var allDistortionsDto =
            allDistortions.Select(distortion =>
                new DistortionDto { Id = distortion.Id.ToString(), Name = distortion.Name }).ToList();

        return allDistortionsDto;
    }
}