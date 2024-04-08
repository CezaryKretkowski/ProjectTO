namespace ProjectTo.Modules.GraphicApi;

public enum Types
{
    Function,
    In,
    Out,
    Uniform
}

public class NodeDto
{
    public Types ShaderType { get; set; }
    public string Name { get; set; } = string.Empty;
}