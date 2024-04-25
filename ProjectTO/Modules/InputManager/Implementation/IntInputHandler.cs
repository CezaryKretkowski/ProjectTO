using ImGuiNET;
using ProjectTo.Modules.Scene;

namespace ProjectTo.Modules.InputManager.Implementation;

public class IntInputHandler : IInputHandler<Int32>
{            //IntInputHandler
    private int args;
    public int HandleInput(string name, int argument)
    {
        ImGui.InputInt(name, ref argument);
        return argument;
    }

    public void SetArgument(int argument)
    {
        args = argument;
    }

    public void SetUniform(ShaderHelper shaderHelper,string name)
    {
         shaderHelper.SetInt(name,args);
    }
}