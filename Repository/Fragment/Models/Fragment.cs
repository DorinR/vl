using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapitest.Repository.Fragment.Models;

public class Fragment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } 
    
    public string Code { get; set; }
    
    public string Description { get; set; }
    
    public string Value { get; set; }
}