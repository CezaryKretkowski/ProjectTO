using System.Numerics;
using ImGuiNET;

namespace ProjectTo.Modules.InputManager.Implementation;

public class Vec3InputHandler: IInputHandler<Vector3>
{
    public Vector3 HandleInput(string name, Vector3 argument)
    {
        ImGui.InputFloat3(name, ref argument);
        return argument;
    }
}