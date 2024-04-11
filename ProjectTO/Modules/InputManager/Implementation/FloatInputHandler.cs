using ImGuiNET;

namespace ProjectTo.Modules.InputManager.Implementation;

public class FloatInputHandler : IInputHandler<float>
{
    public float HandleInput(string name)
    {
        float argument = 0.0f;
        ImGui.InputFloat(name, ref argument);
        return argument;
    }
}