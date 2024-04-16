using System.Numerics;
using ProjectTo.Modules.GuiEditor.InputOutput;
using ProjectTO.Modules.GuiEditor.Shader;
using ProjectTo.Modules.InputManager.Implementation;

namespace ProjectTo.Modules.GuiEditor;

public class FunctionNode:Node
{
    readonly List<IForm> _inputs;
    public IForm? _output =null;

    public void SetTitle(string title)
    {
        Title = title;
    }

    public FunctionNode(Guid id, Vector2 winMenu, Shader menu) : base(id, winMenu, menu)
    {

        _inputs = new List<IForm> { };

        
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

        if (_output != null)
        {
            _output.DrawInput();
            Console.WriteLine("work");
        }
    }

    public override void TryAttached()
    {
        if(_output!=null)
            _parent.TryAttach(_output);
    }
    
    
}