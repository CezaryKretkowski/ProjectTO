namespace ProjectTo.Modules.InputManager;

public interface IInputHandler<T>
{
    T HandleInput(string name,T? argument);
}