using System.Numerics;
using ImGuiNET;
using ProjectTo.Modules.GuiEditor;
using ProjectTo.Modules.Scene;

namespace ProjectTo.Modules.InputManager.Implementation;

public class TwoDSamplerInputHandler : IInputHandler
{
    private Texture texture;
    private Guid id = Guid.NewGuid();
    private bool isLoaded=false;
    private string? path;
    public void HandleInput(string name)
    {
        ImGui.Text(name);
        ImGui.SameLine();
        if (ImGui.Button("Set ###" + id)){
            SaveShader.Instance.GetFilePath(".png");
            isLoaded = true;
        }

        if (path != null)
        {
            ImGui.PushStyleColor(ImGuiCol.Text,new Vector4(0.0f,1.0f,0.0f,1.0f));
            ImGui.Text(path.Substring(0, 7) + "...");
            ImGui.PopStyleColor();
        }

        if (!SaveShader.Instance.IsPathReady && isLoaded)
        {
            path = SaveShader.Instance.GetPath();
            texture = Texture.LoadFromFile(path);
            
            isLoaded = false;
        }

    }

    public void SetUniform(ShaderHelper shaderHelper, string name)
    {
        texture.Use();
    }

    public int GetLocationSize()
    {
        return 0;
    }
}