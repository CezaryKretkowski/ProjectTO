using ImGuiNET;
using ProjectTo.Modules.Scene;

namespace ProjectTo.Modules.InputManager.Implementation;

public class IntInputHandler : IInputHandler
{         
    private int _args;
    public void HandleInput(string name)
    {
        ImGui.InputInt(name, ref _args);
    }
    public void SetUniform(ShaderHelper shaderHelper,string name)
    {
         shaderHelper.SetInt(name,_args);
    }

    public int GetLocationSize()
    {
        return 1;
    }
}