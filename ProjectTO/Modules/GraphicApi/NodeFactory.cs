using System.CodeDom;
using ProjectTo.Modules.GraphicApi.DataModels;
using ProjectTo.Modules.GuiEditor;
using ProjectTO.Modules.GuiEditor.Shader;
using System.Numerics;
using ProjectTo.Modules.GuiEditor.InputOutput;
using ProjectTo.Modules.InputManager;

namespace ProjectTo.Modules.GraphicApi;

public class NodeFactory
{
    private static NodeFactory? _instance;
    public static NodeFactory Instance { get => _instance ??= new NodeFactory(); }
    private NodeFactory() { }

    public Node CreateNode(NodeDto node, Vector2 pos, Shader parent) { 
        switch (node.ShaderType) {
            case Types.In: return CreateInNode(node, pos, parent);
            case Types.Function: return CreateFunctionNode(node, pos, parent);
            case Types.Uniform: return CreateUniformNode(node, pos, parent);
            case Types.Out: return CreateOutNode(node, pos, parent);
        }
        throw new Exception("Field to create Node");
    }
    public FunctionNode CreateFunctionNode(NodeDto node,Vector2 pos,Shader parent) {
        var id = Guid.NewGuid();
        
        var functionNode = new FunctionNode(id,pos, parent);
        var y = (node.Inputs.Count + node.Outputs.Count) * 50;
        
        float x = 200f;
        foreach(var i in node.Inputs )
        {
            if (i.DataTypeDto.CType.Contains("Vector"))
                x = 300;
            AddInput(functionNode,i);
        }
        functionNode.Size = new Vector2(x, y);
        var output =node.Outputs.Select(x => x).FirstOrDefault();
        if (output != null)
            functionNode._output =AddOutput(functionNode,output);
        functionNode.SetTitle(node.Name);
        
        return functionNode;
    }

    private InNode CreateInNode(NodeDto node, Vector2 pos, Shader parent)
    {
        var id = Guid.NewGuid();
        var inode =new InNode(id, pos,parent);
        inode.SetTitle(node.Name);
        var output = node.Outputs.Select(x => x).FirstOrDefault();
        if (output != null)
        {
            inode._output = AddOutput(inode, output);
            inode._output.SetTitle(output.DataTypeDto.GlslType);
        }

        return inode;
    }
    
    private OutNode CreateOutNode(NodeDto node, Vector2 pos, Shader parent)
    {
        var id = Guid.NewGuid();
        return new OutNode(id, pos,parent);
    }
    private UniformNode CreateUniformNode(NodeDto node, Vector2 pos, Shader parent)
    {
        var id = Guid.NewGuid();
        return new UniformNode(id, pos,parent);
    }

    private void AddInput(Node node, InputDto dto)
    {
        switch (dto.DataTypeDto.CType)
        {
            case "float": node.Inputs.Add(Input<float>.CreateInputInstance(node,dto)); break;
            case "Vector3": node.Inputs.Add(Input<Vector3>.CreateInputInstance(node,dto)); break;
            case "Vector2": node.Inputs.Add(Input<Vector2>.CreateInputInstance(node,dto)); break;
            case "Vector4": node.Inputs.Add(Input<Vector4>.CreateInputInstance(node,dto)); break;
            default: node.Inputs.Add(Input<String>.CreateInputInstance(node,dto)) ;break;
        }
    }

    private IForm AddOutput(Node node, OutputDto output)
    {
        switch (output.DataTypeDto.CType)
        {
            case "float": return new Output<float>(node,output);
            case "Vector4": return new Output<Vector4>(node,output);
            case "Vector3": return new Output<Vector3>(node,output);
            case "Vector2": return new Output<Vector2>(node,output);
            default: return new Output<string>(node,output);
        }
    }
}