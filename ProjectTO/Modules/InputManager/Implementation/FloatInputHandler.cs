using ImGuiNET;

namespace ProjectTo.Modules.InputManager.Implementation;

public class FloatInputHandler : IInputHandler<float>
{
    public float HandleInput(string name,float argument)
    {
        ImGui.InputFloat(name, ref argument);
        return argument;
    }
}