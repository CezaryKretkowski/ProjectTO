using System.Numerics;
using ImGuiNET;
using ProjectTo.Modules.GuiEditor.InputOutput;
using ProjectTO.Modules.GuiEditor.Shader;

namespace ProjectTo.Modules.GuiEditor;

public class OutNode : Node
{
    private bool _editTitle = false;
    private string _bufferTitle = string.Empty;
    private List<IForm> _inputs;
    public override List<IForm> Inputs => _inputs;

    public OutNode(Guid id, Vector2 winMenu, Shader menu) : base(id, winMenu, menu)
    {
        HeaderColor = new Vector4(0.0f, 0.7f,0.0f , 1.0f);
        _inputs = new List<IForm>{};
        Id = id;
    }
    public void SetTitle(string title)
    {
        Title = title;
    }
    
    protected override void DrawNodeContent()
    {
        foreach (var input in Inputs)
        {
            input.DrawInput();
        }
    }
    
    protected override void DrawHeaderContent()
    {
        if (_editTitle)
        {
            ImGui.InputText("##MyInputText"+Id, ref _bufferTitle, 1024);
        }
        else
        {
            ImGui.Text(Title);
        }

        if (ImGui.IsKeyPressed(ImGuiKey.Enter)&&_editTitle)
        {
            Title = _bufferTitle;
            Entity.Name = Title;
            _editTitle = false;
        }
    }
    
    protected override void DrawSubMenuContent()
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
                Entity.Name = Title;
                _editTitle = false;
            }

            if (ImGui.MenuItem("Cancel"))
                _editTitle = false;
        }
    }
}