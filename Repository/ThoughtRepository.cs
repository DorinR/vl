using Microsoft.EntityFrameworkCore;
using webapitest.Data;
using webapitest.Repository.Interfaces;
using webapitest.Repository.Models;
using webapitest.Repository.Models.Distortions;

namespace webapitest.Repository;

public class ThoughtRepository : IThoughtRepository
{
    private readonly PostgresDbContext _dbContext;

    public ThoughtRepository(PostgresDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<string> AddThought(string thought, Guid userId)
    {
        var newThought = new Thought { Content = thought, DateCreated = DateTime.UtcNow, UserId = userId };

        _dbContext.Add(newThought);
        await _dbContext.SaveChangesAsync();

        return newThought.Id.ToString();
    }

    public async Task AddDistortionsToThought(string thoughtId, List<DistortionDto> distortionsToAttach)
    {
        var thought = await _dbContext.Thoughts.FindAsync(Guid.Parse(thoughtId));

        if (thought == null) throw new Exception("Thought doesn't exist");

        foreach (var distortionToAdd in distortionsToAttach)
        {
            var associatedDistortion = await _dbContext.Distortion.FindAsync(Guid.Parse(distortionToAdd.Id));

            if (associatedDistortion != null) associatedDistortion.Thoughts.Add(thought);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<string> DeleteThought(string thoughtId)
    {
        var thought = await _dbContext.Thoughts.FindAsync(Guid.Parse(thoughtId));

        if (thought == null) throw new Exception("Thought doesn't exist");

        _dbContext.Thoughts.Remove(thought);
        await _dbContext.SaveChangesAsync();

        return thought.Id.ToString();
    }

    public async Task<List<Thought>> RetrieveThoughts(Guid userId)
    {
        var result = await _dbContext.Thoughts.Include(t => t.Distortions).Where(t => t.UserId.Equals(userId))
            .ToListAsync();

        return result;
    }
}