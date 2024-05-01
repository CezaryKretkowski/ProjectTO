using ImGuiNET;
using ProjectTo.Modules.Scene;

namespace ProjectTo.Modules.InputManager.Implementation;

public class FloatInputHandler : IInputHandler
{
    private float _args;
    public void HandleInput(string name)
    {
        ImGui.InputFloat(name, ref _args);
    }
    public void SetUniform(ShaderHelper shaderHelper,string name)
    {
        shaderHelper.SetFloat(name,_args);
    }
}