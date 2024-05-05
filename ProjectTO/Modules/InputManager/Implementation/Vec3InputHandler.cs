using System.Numerics;
using ImGuiNET;
using ProjectTo.Modules.Scene;

namespace ProjectTo.Modules.InputManager.Implementation;

public class Vec3InputHandler: IInputHandler
{
    private Vector3 _args;
    public void HandleInput(string name)
    {
        ImGui.InputFloat3(name, ref _args);
    }
    
    public void SetUniform(ShaderHelper shaderHelper,string name)
    {
        var ars = new OpenTK.Mathematics.Vector3(_args.X,_args.Y,_args.Z);
        shaderHelper.SetVector3(name,ars);
    }

    public int GetLocationSize()
    {
        return 3;
    }
}