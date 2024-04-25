using ImGuiNET;
using ProjectTo.Modules.Scene;

namespace ProjectTo.Modules.InputManager.Implementation;

public class StringInputHandler : IInputHandler<string>
{
    private string args;
    public string HandleInput(string name,string argument)
    {
        ImGui.Text(name);
        return name;
    }

    public void SetArgument(string argument)
    {
        args = argument;
    }

    public void SetUniform(ShaderHelper shaderHelper,string name)
    {
        
    }
}