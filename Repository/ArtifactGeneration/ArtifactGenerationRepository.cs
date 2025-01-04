using webapitest.Business.ArtifactGeneration;
using webapitest.Data;
using webapitest.Repository.ArtifactGeneration.Interfaces;
using webapitest.Repository.ArtifactGeneration.Models;
using webapitest.Repository.Fragment.Models;

namespace webapitest.Repository.ArtifactGeneration;

public class ArtifactGenerationRepository : IArtifactGenerationRepository
{
    private readonly PostgresDbContext _dbContext;

    public ArtifactGenerationRepository(PostgresDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<int> InitializeArtifact(string initialDetails, string artifactType,
        List<FragmentSpec> fragmentSpecs)
    {
        var newArtifact = new ArtifactModel
        {
            Type = artifactType,
            InitialDetails = initialDetails,
            CreatedDate = DateTime.UtcNow,
            Fragments = fragmentSpecs.Select(spec => new FragmentModel
            {
                Code = spec.Code,
                Description = spec.Description,
                Value = spec.Value ?? ""
            }).ToList()
        };

        _dbContext.Artifacts.Add(newArtifact);
        await _dbContext.SaveChangesAsync();

        return newArtifact.Id;
    }
}