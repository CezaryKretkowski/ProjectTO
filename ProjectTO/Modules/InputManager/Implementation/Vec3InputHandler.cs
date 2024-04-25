using System.Numerics;
using ImGuiNET;
using ProjectTo.Modules.Scene;

namespace ProjectTo.Modules.InputManager.Implementation;

public class Vec3InputHandler: IInputHandler<Vector3>
{
    private Vector3 args;
    public Vector3 HandleInput(string name, Vector3 argument)
    {
        ImGui.InputFloat3(name, ref argument);
        args = argument;
        return argument;
    }

    public void SetArgument(Vector3 arument)
    {
        args = arument;
    }

    public void SetUniform(ShaderHelper shaderHelper,string name)
    {
        var ars = new OpenTK.Mathematics.Vector3();
        ars.X = args.X;
        ars.Y = args.Y;
        args.Z = args.Z;
        shaderHelper.SetVector3(name,ars);
    }
}