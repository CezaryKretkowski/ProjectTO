namespace ProjectTo.Modules.GraphicApi.DataModels;

public enum Types
{
    Function,
    In,
    Out,
    Uniform
}

public class NodeDto
{
    public NodeDto()
    {
        Inputs = new List<InputDto>();
        Outputs = new List<OutputDto>();
    }

    public Types ShaderType { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Id { get; set; }

    public List<InputDto> Inputs { get; set; } 
    public List<OutputDto> Outputs { get; set; } 
    
}