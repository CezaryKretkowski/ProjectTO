using System.Numerics;
using ProjectTo.Modules.GuiEditor.InputOutput;
using ProjectTO.Modules.GuiEditor.Shader;

namespace ProjectTo.Modules.GuiEditor;

public class FunctionNode:Node
{
    public FunctionNode(Guid id, Vector2 winMenu, Shader menu) : base(id, winMenu, menu)
    {
        var input1 = new Input<float>(this);
        var input2 = new Input<string>(this);
        _inputs.Add(input1);
        _inputs.Add(input2);
        _output = new Output<float>(this);
    }

    public FunctionNode(Guid id, Shader menu) : base(id, menu)
    {
    }
}