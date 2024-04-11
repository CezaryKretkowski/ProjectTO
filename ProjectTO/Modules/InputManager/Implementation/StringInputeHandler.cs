using ImGuiNET;

namespace ProjectTo.Modules.InputManager.Implementation;

public class StringInputHandler : IInputHandler<string>
{
    public string HandleInput(string name)
    {
        ImGui.Text(name);
        return name;
    }
}