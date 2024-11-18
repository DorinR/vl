using Microsoft.EntityFrameworkCore;
using webapitest.Data;
using webapitest.Repository.ArtifactGeneration.Interfaces;
using webapitest.Repository.ArtifactGeneration.Models;

namespace webapitest.Repository.ArtifactGeneration;

public class ArtifactGenerationRepository : IArtifactGenerationRepository
{
    private readonly PostgresDbContext _dbContext;
    
    public ArtifactGenerationRepository(PostgresDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<string> CreateArtifact(string initialDetails, string artifactType)
    {
        var newArtifact = new Artifact()
        {
            Type = artifactType,
            InitialDetails = initialDetails,
            CreatedDate = DateTime.UtcNow
        };
        
        _dbContext.Artifacts.Add(newArtifact);
        await _dbContext.SaveChangesAsync();
        
        return newArtifact.Id.ToString();
    }
}