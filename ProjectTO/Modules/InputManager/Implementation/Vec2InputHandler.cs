using System.Numerics;
using ImGuiNET;
using ProjectTo.Modules.Scene;

namespace ProjectTo.Modules.InputManager.Implementation;

public class Vec2InputHandler : IInputHandler<Vector2>
{
    private Vector2 args;
    public Vector2 HandleInput(string name, Vector2 argument)
    {
        ImGui.InputFloat2(name, ref argument);
        return argument;
    }

    public void SetArgument(Vector2 argyumeny)
    {
        args = argyumeny;
    }

    public void SetUniform(ShaderHelper shaderHelper,string name)
    {
        throw new NotImplementedException();
    }
}