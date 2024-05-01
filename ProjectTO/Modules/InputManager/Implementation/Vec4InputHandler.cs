using System.Numerics;
using ImGuiNET;
using ProjectTo.Modules.Scene;

namespace ProjectTo.Modules.InputManager.Implementation;

public class Vec4InputHandler :IInputHandler
{
    private Vector4 _args;
    public void HandleInput(string name)
    {
        ImGui.InputFloat4(name, ref _args);
    }
    
    public void SetUniform(ShaderHelper shaderHelper,string name)
    {   Console.WriteLine(_args);
        var argument = new OpenTK.Mathematics.Vector4(_args.X, _args.Y, _args.Z, _args.W);
        shaderHelper.SetVector4(name,argument);
    }
}