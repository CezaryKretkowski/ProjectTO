using System.Numerics;
using ImGuiNET;
using ProjectTo.Modules.GuiEditor.InputOutput;
using ProjectTO.Modules.GuiEditor.Shader;

namespace ProjectTo.Modules.GuiEditor;

public class UniformNode : Node
{
    private bool _editTitle = false;
    private string _bufferTitle = string.Empty;
    public void SetTitle(string title)
    {
        Title = title;
    }
    public UniformNode(Guid id, Vector2 winMenu, Shader menu) : base(id, winMenu, menu)
    {
        this.HeaderColor = new Vector4(0.7f, 0.8f, 0.0f, 1.0f);
        Id = id;
    }
    
    
    protected override void DrawNodeContent()
    {
        Output?.DrawInput();
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


    protected override void TryAttached()
    {
        if (Output != null) Parent.TryAttach(Output);
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