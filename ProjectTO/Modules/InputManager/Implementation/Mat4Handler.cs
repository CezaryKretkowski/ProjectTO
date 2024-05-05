using ImGuiNET;
using OpenTK.Mathematics;
using ProjectTo.Modules.Scene;
using ProjectTO.Modules.Scene;

namespace ProjectTo.Modules.InputManager.Implementation;

public class Mat4Handler :IInputHandler
{
    Guid id = Guid.NewGuid();
    private Matrix4 matrix = Matrix4.Zero;
    public void HandleInput(string name)
    {
        ImGui.Text(name);
        ImGui.SameLine();
        if (ImGui.Button("Set"))
        {
            ImGui.OpenPopup(name+id);
        }

        if (ImGui.BeginPopupContextItem(name+id))
        {
            if (ImGui.MenuItem("Use Identity matrix"))
            {
                matrix = Matrix4.Identity;
            }
            if (ImGui.MenuItem("Use Projection"))
            {
                matrix = Camera.Instance.GetProjectionMatrix();
            }
            if (ImGui.MenuItem("Use view"))
            {
                matrix = Camera.Instance.GetViewMatrix();
            }

            ImGui.End();
        }
    }

    public void SetUniform(ShaderHelper shaderHelper, string name)
    {
        shaderHelper.SetMatrix4(name,matrix);
    }

    public int GetLocationSize()
    {
        return 0;
    }
}