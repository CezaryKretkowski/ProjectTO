using ProjectTo.Modules.Scene;

namespace ProjectTo.Modules.InputManager;

public interface IInputHandler<T>
{
    T HandleInput(string name,T? argument);
    void SetArgument(T argument);
    void SetUniform(ShaderHelper shaderHelper,string name);
}