using ImGuiNET;
using OpenTK.Mathematics;
using ProjectTo.Modules.Scene;
using ProjectTO.Modules.Scene;
using Vector4 = System.Numerics.Vector4;

namespace ProjectTo.Modules.InputManager.Implementation;

public class Mat4Handler :IInputHandler
{
    Guid id = Guid.NewGuid();
    private Matrix4 matrix = Matrix4.Zero;
    private int _updateIndex = 0;
    private string _use ="Empty";
    public void HandleInput(string name)
    {
        ImGui.Text(name);
        ImGui.SameLine();
        if (ImGui.Button("Set###"+id))
        {
            ImGui.OpenPopup(name+id);
        }
        ImGui.SameLine();
        if (_use.Equals("Empty"))
        {
            ImGui.PushStyleColor(ImGuiCol.Text,new Vector4(1.0f,0.0f,0.0f,1.0f));
            ImGui.Text(_use);
            
        }
        else
        {
            ImGui.PushStyleColor(ImGuiCol.Text,new Vector4(0.0f,1.0f,0.0f,1.0f));
            ImGui.Text(_use);
        }
        ImGui.PopStyleColor();
        if (ImGui.BeginPopupContextItem(name+id))
        {
            if (ImGui.MenuItem("Use Identity matrix"))
            {
                matrix = Matrix4.Identity;
                _use = "Identity";
            }
            if (ImGui.MenuItem("Use Projection"))
            {
                _updateIndex = 2;
                _use = "Projection";
            }
            if (ImGui.MenuItem("Use view"))
            {
                _updateIndex = 1;
                _use = "View";

            }

            ImGui.End();
        }
    }

    public void SetUniform(ShaderHelper shaderHelper, string name)
    {
        if (_updateIndex == 1)
            matrix = Camera.Instance.GetViewMatrix();
        if(_updateIndex == 2)
            matrix = Camera.Instance.GetProjectionMatrix();
        shaderHelper.SetMatrix4(name,matrix);
    }

    public int GetLocationSize()
    {
        return 0;
    }
}