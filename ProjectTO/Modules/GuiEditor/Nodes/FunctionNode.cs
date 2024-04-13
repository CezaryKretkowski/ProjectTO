using System.Numerics;
using ProjectTo.Modules.GuiEditor.InputOutput;
using ProjectTO.Modules.GuiEditor.Shader;
using ProjectTo.Modules.InputManager.Implementation;

namespace ProjectTo.Modules.GuiEditor;

public class FunctionNode:Node
{
    readonly List<IForm> _inputs;
    IForm _output;
    public FunctionNode(Guid id, Vector2 winMenu, Shader menu) : base(id, winMenu, menu)
    {
        var input1 = new Input<float>(this,new FloatInputHandler());
        var input2 = new Input<string>(this,new StringInputHandler());
        _inputs = new List<IForm>
        {
            input1,
            input2
        };
        _output = new Output<float>(this);
        
    }

    public FunctionNode(Guid id, Shader menu) : base(id, menu)
    {
       

    }

    public override List<IForm> Inputs => _inputs;

    protected override void DrawNodeContent()
    {
        foreach (var input in _inputs)
        {
             input.DrawInput();
        }
        _output.DrawInput();
    }

    public override void TryAttached()
    {
        _parent.TryAttach(_output);
    }
}