using ProjectTo.Modules.Scene;

namespace ProjectTo.Modules.InputManager;

public interface IInputHandler
{
    void HandleInput(string name);
    void SetUniform(ShaderHelper shaderHelper,string name);
    int GetLocationSize();
}