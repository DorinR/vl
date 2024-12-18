using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fragments.Models;

namespace webapitest.Repository.ArtifactGeneration.Models;

public class Artifact
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Type { get; set; }
    
    public string InitialDetails { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public List<Fragment> Fragments { get; set; }
}