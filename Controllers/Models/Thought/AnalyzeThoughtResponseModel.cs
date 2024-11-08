namespace webapitest.Controllers.Models;

public class AnalyzeThoughtResponseModel
{
    public List<string> DistortionsDetected { get; set; }

    public string Reformulation { get; set; }
}