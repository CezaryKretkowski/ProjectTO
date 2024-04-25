using ImGuiNET;
using ProjectTo.Modules.Scene;

namespace ProjectTo.Modules.InputManager.Implementation;

public class FloatInputHandler : IInputHandler<float>
{
    private float args;
    public float HandleInput(string name,float argument)
    {
        ImGui.InputFloat(name, ref argument);
        args = argument;
        return argument;
    }

    public void SetArgument(float argument)
    {
        args = argument;
    }

    public void SetUniform(ShaderHelper shaderHelper,string name)
    {
        shaderHelper.SetFloat(name,args);
    }
}