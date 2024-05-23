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
    public static NodeFactory Instance => _instance ??= new NodeFactory();
    private NodeFactory() { }

    public Node CreateNode(NodeDto node, Vector2 pos, Shader parent)
    {
        return node.ShaderType switch
        {
            Types.In => CreateInNode(node, pos, parent, Guid.NewGuid()),
            Types.Function => CreateFunctionNode(node, pos, parent, Guid.NewGuid()),
            Types.Uniform => CreateUniformNode(node, pos, parent, Guid.NewGuid()),
            Types.Out => CreateOutNode(node, pos, parent, Guid.NewGuid()),
            _ => throw new Exception("Field to create Node")
        };
    }

    private FunctionNode CreateFunctionNode(NodeDto node,Vector2 pos,Shader parent,Guid id) {

        node.guid = id;
        var functionNode = new FunctionNode(id,pos, parent);
        functionNode.Entity = node;
        var y = 50+(node.Inputs.Count * 50);

        
        float x = 150;
        foreach(var i in node.Inputs )
        {
            functionNode.Inputs.Add(Input.CreateInputInstance(functionNode,i));
        }
        functionNode.Size = new Vector2(x, y);
        var output =node.Outputs.FirstOrDefault();
        
        if (output != null)
            functionNode.Output =Output.CrateOutput(functionNode,output);
        functionNode.SetTitle(node.Name);
        
        return functionNode;
    }

    private InNode CreateInNode(NodeDto node, Vector2 pos, Shader parent,Guid id)
    {
        node.guid = id;
        var inode =new InNode(id, pos,parent);
        inode.Entity = node;
        inode.SetTitle(node.Name);
        var output = node.Outputs.Select(x => x).FirstOrDefault();

        inode.Size = new Vector2(150, 100);
        if (output == null) 
            return inode;
        
        inode.Output = Output.CrateOutput(inode, output);
        inode.Output.SetTitle(output.DataTypeDto.GlslType);

        return inode;
    }
    
    private OutNode CreateOutNode(NodeDto node, Vector2 pos, Shader parent,Guid id)
    {
        node.guid = id;
        var outNode = new OutNode(id, pos,parent);
        outNode.Entity = node;
        outNode.SetTitle(node.Name);
        var input = node.Inputs.Select(x => x).FirstOrDefault();
        outNode.Size=new Vector2(150, 100);
        
        if (input == null) return outNode;
        outNode.Inputs.Add(Input.CreateInputInstance(outNode,input));
        return outNode;
    }
    private UniformNode CreateUniformNode(NodeDto node, Vector2 pos, Shader parent,Guid id)
    {
        node.guid = id;
        var uniformNode =  new UniformNode(id, pos,parent);
        uniformNode.Entity = node;
        uniformNode.SetTitle(node.Name);
        var output = node.Outputs.Select(x => x).FirstOrDefault();

        uniformNode.Size = new Vector2(150, 100);
        if (output == null) 
            return  uniformNode;
        
        uniformNode.Output = Output.CrateOutput( uniformNode, output);
        uniformNode.Output.SetTitle(output.DataTypeDto.GlslType);
        uniformNode.Output = uniformNode.Output;

        return uniformNode;
    }
    
}