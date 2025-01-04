using Microsoft.EntityFrameworkCore;
using webapitest.Data;
using webapitest.Repository.Fragment.Interfaces;
using webapitest.Repository.Fragment.Models;

namespace webapitest.Repository.Fragment;

public class FragmentRepository : IFragmentRepository
{
    private readonly PostgresDbContext _dbContext;

    public FragmentRepository(PostgresDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<List<FragmentModel>> GetArtifactFragments(int artifactId)
    {
        var fragments = await _dbContext.Fragments.Where(f => f.ArtifactId == artifactId).ToListAsync();

        return fragments;
    }

    public async Task<List<FragmentModel>> GetEmptyArtifactFragments(int artifactId)
    {
        var fragments = await _dbContext.Fragments
            .Where(f => f.ArtifactId == artifactId && string.IsNullOrWhiteSpace(f.Value)).ToListAsync();

        return fragments;
    }

    public async Task<int> AddFragmentValue(int fragmentId, string value)
    {
        var fragmentToUpdate = await _dbContext.Fragments.FindAsync(fragmentId);

        if (fragmentToUpdate == null) throw new Exception("Fragment not found");

        fragmentToUpdate.Value = value;
        await _dbContext.SaveChangesAsync();

        return fragmentToUpdate.Id;
    }
}