using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapitest.Repository.ArtifactGeneration.Models;

public class Artifact
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Type { get; set; }
    
    public string InitialDetails { get; set; }
    
    public DateTime CreatedDate { get; set; }
}