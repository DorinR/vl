using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapitest.Repository.Fragment.Models;

namespace webapitest.Repository.ArtifactGeneration.Models;

public class ArtifactModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Type { get; set; }
    public string InitialDetails { get; set; }
    public DateTime CreatedDate { get; set; }
    public ICollection<FragmentModel> Fragments { get; set; }
}