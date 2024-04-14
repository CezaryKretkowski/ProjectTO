using System.Numerics;
using ImGuiNET;
using ProjectTo.Modules.GuiEditor.InputOutput;
using ProjectTO.Modules.GuiEditor.Shader;

namespace ProjectTo.Modules.GuiEditor;

public class InNode:Node
{
    IForm _output;
    private bool _editTitle = false;
    private string _bufferTitle = string.Empty;
    public InNode(Guid id, Vector2 winMenu, Shader menu) : base(id, winMenu, menu)
    {
        this.HeaderColor = new Vector4(0.7f,0.0f,0.0f,1.0f);
        _output = new Output<float>(this);

        ID = id;

    }

    public InNode(Guid id, Shader menu) : base(id, menu)
    {
        this.HeaderColor = new Vector4(0.7f, 0.0f, 0.0f, 1.0f);
        ID = id;
     
    }
    protected override void DrawNodeContent()
    {
        _output.DrawInput();
    }

    protected override void DrawHeaderContent()
    {
   
        if (_editTitle)
        {
            ImGui.InputText("##MyInputText", ref _bufferTitle, 1024);
            
        }
        else
        {
            ImGui.Text(Title);
        }

        if (ImGui.IsKeyPressed(ImGuiKey.Enter))
        {
            Title = _bufferTitle;
            _editTitle = false;
        }
    }

    public override List<IForm> Inputs => null;
    public override void TryAttached()
    {
        _parent.TryAttach(_output);
    }

    public override void DrawSubMenuContent()
    {
        if(!_editTitle)
            if (ImGui.MenuItem("Rename"))
            {
                _editTitle = true;
            }

        if (_editTitle)
        {
            if (ImGui.MenuItem("Save"))
            {
                Title = _bufferTitle;
                _editTitle = false;
            }

            if (ImGui.MenuItem("Cancel"))
                _editTitle = false;
        }
    }
}