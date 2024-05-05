using ImGuiNET;
using ProjectTo.Modules.Scene;

namespace ProjectTo.Modules.InputManager.Implementation;

public class StringInputHandler : IInputHandler
{
    public void  HandleInput(string name)
    {
        ImGui.Text(name);
    }
    
    public void SetUniform(ShaderHelper shaderHelper,string name)
    {
        
    }

    public int GetLocationSize()
    {
        return 0;
    }
}