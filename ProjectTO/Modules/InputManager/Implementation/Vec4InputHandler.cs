using System.Numerics;
using ImGuiNET;
using ProjectTo.Modules.Scene;

namespace ProjectTo.Modules.InputManager.Implementation;

public class Vec4InputHandler :IInputHandler<Vector4>
{
    private OpenTK.Mathematics.Vector4 args;
    public Vector4 HandleInput(string name, Vector4 argument)
    {
        ImGui.InputFloat4(name, ref argument);
        SetArgument(argument);
        return argument;
    }

    public void SetArgument(Vector4 ars)
    {
        args = new OpenTK.Mathematics.Vector4(ars.X, ars.Y, ars.Z, ars.W);
        
    }

    public void SetUniform(ShaderHelper shaderHelper,string name)
    {   Console.WriteLine(args);
        shaderHelper.SetVector4(name,args);
    }
}