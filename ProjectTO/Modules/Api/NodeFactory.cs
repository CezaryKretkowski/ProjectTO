using System.CodeDom;
using ProjectTo.Modules.GraphicApi.DataModels;
using ProjectTo.Modules.GuiEditor;
using ProjectTO.Modules.GuiEditor.Shader;
using System.Numerics;
using ProjectTo.Modules.GuiEditor.InputOutput;
using ProjectTo.Modules.InputManager;
using ProjectTo.Modules.InputManager.Implementation;

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

    private FunctionNode CreateFunctionNode(NodeDto node,Vector2 pos,Shader parent) {
        var id = Guid.NewGuid();
        
        var functionNode = new FunctionNode(id,pos, parent);
        functionNode.Entity = node;
        var y = 50+(node.Inputs.Count * 50);

        
        float x = 150;
        foreach(var i in node.Inputs )
        {

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
        inode.Entity = node;
        inode.SetTitle(node.Name);
        var output = node.Outputs.Select(x => x).FirstOrDefault();

        inode.Size = new Vector2(150, 100);
        if (output == null) 
            return inode;
        
        inode.Output = AddOutput(inode, output);
        inode.Output.SetTitle(output.DataTypeDto.GlslType);

        return inode;
    }
    
    private OutNode CreateOutNode(NodeDto node, Vector2 pos, Shader parent)
    {
        var id = Guid.NewGuid();
        var outNode = new OutNode(id, pos,parent);
        outNode.Entity = node;
        outNode.SetTitle(node.Name);
        var input = node.Inputs.Select(x => x).FirstOrDefault();
        outNode.Size=new Vector2(150, 100);
        
        
        if (input == null) return outNode;
        AddInput(outNode,input);
        return outNode;
    }
    private UniformNode CreateUniformNode(NodeDto node, Vector2 pos, Shader parent)
    {
        var id = Guid.NewGuid();
        var uniformNode =  new UniformNode(id, pos,parent);
        uniformNode.Entity = node;
        uniformNode.SetTitle(node.Name);
        var output = node.Outputs.Select(x => x).FirstOrDefault();

        uniformNode.Size = new Vector2(150, 100);
        if (output == null) 
            return  uniformNode;
        
        uniformNode.Output = AddOutput( uniformNode, output);
        uniformNode.Output.SetTitle(output.DataTypeDto.GlslType);
        uniformNode._output = uniformNode.Output;

        return uniformNode;
    }

    private void AddInput(Node node, InputDto dto)
    {
        switch (dto.DataTypeDto.CType)
        {
            case "float": node.Inputs.Add(Input<float>.CreateInputInstance(node,dto)); break;
            case "Vector3": node.Inputs.Add(Input<Vector3>.CreateInputInstance(node,dto)); break;
            case "Vector2": node.Inputs.Add(Input<Vector2>.CreateInputInstance(node,dto)); break;
            case "Vector4": node.Inputs.Add(Input<Vector4>.CreateInputInstance(node,dto)); break;
            case "int": node.Inputs.Add(Input<int>.CreateInputInstance(node,dto)); break;
            default:
                node.Inputs.Add(Input<String>.CreateInputInstance(node,dto)) ;break;
        }
    }

    private IForm AddOutput(Node node, OutputDto output)
    {
        switch (output.DataTypeDto.CType)
        {
            case "float": return new Output<float>(node,output,new FloatInputHandler());
            case "Vector4": return new Output<Vector4>(node,output,new Vec4InputHandler());
            case "Vector3": return new Output<Vector3>(node,output,new Vec3InputHandler());
            case "Vector2": return new Output<Vector2>(node,output,new Vec2InputHandler());
            case "int": return new Output<int>(node,output,new IntInputHandler());
            default: return new Output<string>(node,output,new StringInputHandler());
        }
    }
}