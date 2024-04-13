using System.Numerics;
using ProjectTo.Modules.GuiEditor.InputOutput;
using ProjectTO.Modules.GuiEditor.Shader;

namespace ProjectTo.Modules.GuiEditor;

public class InNode:Node
{
    IForm _output;
    public InNode(Guid id, Vector2 winMenu, Shader menu) : base(id, winMenu, menu)
    {
        this.HeaderColor = new Vector4(1.0f,0.0f,0.0f,0.0f);
        ID = id;
        winMenu = winMenu;
        menu = menu;
    }

    public InNode(Guid id, Shader menu) : base(id, menu)
    {
        this.HeaderColor = new Vector4(1.0f, 0.0f, 0.0f, 0.0f);
        ID = id;
     
    }
    protected override void DrawNodeContent()
    {

        // foreach (var input in _inputs)
        // {
        //     input.DrawInput();
        // }
        //_output.DrawInput();
    }
    public override List<IForm> Inputs => null;
    public override void TryAttached()
    {
        _parent.TryAttach(_output);
    }
}