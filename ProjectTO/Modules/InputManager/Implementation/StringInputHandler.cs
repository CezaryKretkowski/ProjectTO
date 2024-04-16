using ImGuiNET;

namespace ProjectTo.Modules.InputManager.Implementation;

public class StringInputHandler : IInputHandler<string>
{
    public string HandleInput(string name,string argument)
    {
        ImGui.Text(name);
        return name;
    }
}