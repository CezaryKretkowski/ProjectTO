using ProjectTo.Modules.GraphicApi.DataModels;
using ProjectTo.Modules.GuiEditor;
using ProjectTO.Modules.GuiEditor.Shader;
using System.Numerics;

namespace ProjectTo.Modules.GraphicApi;

public class NodeFactory
{
    private static NodeFactory _instance;
    public static NodeFactory Instance { get => _instance ??= new NodeFactory(); }
    private NodeFactory() { }

    public Node CreateNode(NodeDto node, Vector2 pos, Shader parent) { 
        switch (node.ShaderType) {
            case Types.In: return CreateInNode(node, pos, parent);
            case Types.Function: return CreateFunctionNode(node, pos, parent);
        }
        throw new Exception("Field to create Node");
    }
    public FunctionNode CreateFunctionNode(NodeDto node,Vector2 pos,Shader parent) {
        var id = Guid.NewGuid();
        return new FunctionNode(id,pos, parent);
    }

    public InNode CreateInNode(NodeDto node, Vector2 pos, Shader parent)
    {
        var id = Guid.NewGuid();
        return new InNode(id, pos,parent);
    }
}