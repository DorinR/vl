using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapitest.Repository.ArtifactGeneration.Models;

namespace webapitest.Repository.Fragment.Models;

public class FragmentModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Code { get; set; }

    public string Description { get; set; }

    public string Value { get; set; }

    public int ArtifactId { get; set; }
    public ArtifactModel Artifact { get; set; }
}