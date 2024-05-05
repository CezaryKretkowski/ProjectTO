using System.Numerics;
using ImGuiNET;
using ProjectTo.Modules.Scene;

namespace ProjectTo.Modules.InputManager.Implementation;

public class Vec2InputHandler : IInputHandler
{
    private Vector2 _args;
    public void HandleInput(string name)
    {
        ImGui.InputFloat2(name, ref _args);
    }
    public void SetUniform(ShaderHelper shaderHelper,string name)
    {
        throw new NotImplementedException();
    }

    public int GetLocationSize()
    {
        return 2;
    }
}