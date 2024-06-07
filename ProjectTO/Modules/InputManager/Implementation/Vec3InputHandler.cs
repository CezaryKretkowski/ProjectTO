using System.Configuration;
using System.Numerics;
using ImGuiNET;
using ProjectTo.Modules.Scene;
using ProjectTO.Modules.Scene;

namespace ProjectTo.Modules.InputManager.Implementation;

public class Vec3InputHandler: IInputHandler
{
    private Vector3 _args;
    public bool useCameraPoistion = false;
    public Guid id = Guid.NewGuid();
    public void HandleInput(string name)
    {
        ImGui.Checkbox("Use Camera Position ###"+id, ref useCameraPoistion);
        if (useCameraPoistion)
        {
            var x = Camera.Instance.Position;
            _args =new Vector3(x.X,x.Y,x.Z);
        }
        else
        {
            ImGui.InputFloat3(name, ref _args);
        }
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