using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using ImGuiNET;
using Microsoft.VisualBasic;
using OpenTK.Graphics.OpenGL4;
using ProjectTo.Modules.GraphicApi.DataModels;
using ProjectTo.Modules.GuiEditor;
using ProjectTo.Modules.GuiEditor.InputOutput;

namespace ProjectTO.Modules.GuiEditor.Shader;

public enum ShaderProfile
{
    Vertex,
    Fragment,
}

public class Compiler
{
    private List<Node> _inNodes;
    private List<Node> _functionNodes;
    private List<Node> _outNodes;
    private List<Node> _uniformNodes;
    private List<Guid> _ids;
    private List<IForm> _inputs;
    private Dictionary<Guid, Node> _nd;
    private List<string> Lines { get; set; }

    public void Init(Dictionary<Guid,Node> nd)
    {
        _nd = nd;
        var nodes = nd.Values.ToList();
        _inNodes = nodes.Select(x => x).Where(x => x.Entity.ShaderType == Types.In).ToList();
        _outNodes = nodes.Select(x => x).Where(x => x.Entity.ShaderType == Types.Out).ToList();
        _uniformNodes = nodes.Select(x => x).Where(x => x.Entity.ShaderType == Types.Uniform).ToList();
        _functionNodes = nodes.Select(x => x).Where(x => x.Entity.ShaderType == Types.Function).ToList();
        foreach (var node in _functionNodes)
        {
            _inputs = _inputs.Concat(node.Inputs).ToList();
        }
    }

    public Compiler()
    {
        _inNodes = new List<Node>();
        _functionNodes = new List<Node>();
        _outNodes = new List<Node>();
        _uniformNodes = new List<Node>();
        _ids = new List<Guid>();
        _inputs = new List<IForm>();
        Lines = new List<string>();
    }

    private void BuildHeader(StringBuilder builder,ShaderProfile profile)
    {
        int i = 0;
        foreach (var inNode in _inNodes)
        {
            var output = inNode.Entity.Outputs.Select(x => x).Single();
            var prefix = profile == ShaderProfile.Vertex ? $"layout (location = {i}) " : "";
            var line = $"{prefix}in {output.DataTypeDto.GlslType} {RemoveSpace(inNode.Title)};";
            builder.AppendLine(line);
        }
        builder.AppendLine("");
        foreach (var uniformNode in _uniformNodes)
        {
            var output = uniformNode.Entity.Outputs.Select(x => x).Single();
            var line = $"uniform {output.DataTypeDto.GlslType} {RemoveSpace(uniformNode.Title)};";
            builder.AppendLine(line);
        }
        foreach (var outNode in _outNodes)
        {
            var output = outNode.Entity.Inputs.Select(x => x).Single();
            var line = $"out {output.DataTypeDto.GlslType} {RemoveSpace(outNode.Title)};";
            builder.AppendLine(line);
        }
    }


    public static string RemoveSpace(string value)
    {
        value = value.Replace(' ', '_');
        return value;
    }

    private string BuildLine(Node node)
    {
        var argList = new List<string>();
        foreach (var input in node.Inputs)
        {
            var parentId = input.GetOutputParent();
            if (parentId == Guid.Empty)
            {
                var item = input.GetInputDto();
                if (item != null) argList.Add(item.Dv);
            }
            else
            {
                var parentNode = _nd[parentId];
                var parentTitle = RemoveSpace(parentNode.Title);
                if (parentNode.Entity.ShaderType == Types.Function)
                {
                    var output = parentNode.Entity.Id;
                    argList.Add(parentTitle + "_" + output);
                }
                else
                {
                    argList.Add(parentTitle);
                }
            }
        }

        var dto = node.Entity;

        if (node.Entity.ShaderType == Types.Function)
        {
            object?[] tab = argList.ToArray();

             var value = string.Format(dto.ToStringFormat, tab);
            if(node.Output == null)
                return RemoveSpace(dto.Name) + " = " + value;
            else
            {
                var glslType = node.Entity.Outputs.Single().DataTypeDto.GlslType;
                return glslType+" "+RemoveSpace(dto.Name) +"_"+dto.Id+ " = " + value;
            }
        }
        else
        {
            return RemoveSpace(dto.Name) + " = " + argList.Single()+";";
        }

    }


    private void BuildMethode(StringBuilder builder)
    {
        _ids.Clear();
        var list = _functionNodes.Concat(_outNodes);
        foreach (var node in list)
        {
            builder.AppendLine(BuildLine(node));
        }

    }

 
    public string CompileWrite(ShaderProfile profile)
    {
        if (!_functionNodes.Any() && !_outNodes.Any())
            return string.Empty;
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("#version 330 core");
        BuildHeader(builder,profile);
        builder.AppendLine("");
        builder.AppendLine("void main(){");
        BuildMethode(builder);
        builder.AppendLine("}");
       
        return builder.ToString();
    }

   public static CompileResult TryCreatProgram(string sourceVert,string sourceFrag)
    {
        try
        {
            Console.WriteLine(sourceFrag);
            Console.WriteLine(sourceVert);
            if (string.IsNullOrEmpty(sourceFrag))
            {
                throw new Exception("Faild to compile shaeders is empty!");
            }
            if (string.IsNullOrEmpty(sourceVert))
            {
                throw new Exception("Faild to compile shaeders is empty!");
            }
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, sourceVert);
            CompileShader(vertexShader);
            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, sourceFrag);
            CompileShader(fragmentShader);
            int program = GL.CreateProgram();
            
            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);


            LinkProgram(program);


            GL.DetachShader(program, vertexShader);
            GL.DetachShader(program, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);
        }
        catch (Exception e)
        {
            return new CompileResult(false,e.Message,"","");

        }

        return new CompileResult(true,"Success",sourceVert,sourceFrag);
    }
    
    
    private static void CompileShader(int shader)
    {
           
        GL.CompileShader(shader);


        GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
        if (code != (int)All.True)
        {
            var infoLog = GL.GetShaderInfoLog(shader);
            throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
        }
    }

    private static void LinkProgram(int program)
    {
         
        GL.LinkProgram(program);


        GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
        if (code != (int)All.True)
        {
            throw new Exception($"Error occurred whilst linking Program({program})");
        }
    }
}