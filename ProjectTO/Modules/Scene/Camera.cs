using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ProjectTo.Window;
using Window = ProjectTo.Window.Window;

namespace ProjectTO.Modules.Scene;

public class Camera
{
    private static  Camera? instance = null;
    private Vector3 _front = -Vector3.UnitZ;
    
    private Vector2 _lastPos;
    private bool _secondFrame = false;

    private Vector3 _up = Vector3.UnitY;

    private Vector3 _right = Vector3.UnitX;

    public static Camera Instance
    {
        get { return instance ??= new Camera(); }
    }

    private float _pitch;
        
    private float _yaw = -MathHelper.PiOver2; // Without this, you would be started rotated 90 degrees right.
        
    private float _fov = MathHelper.PiOver2;

    private Camera()
    {
    }
    
    public void Init(Vector3 position, float aspectRatio)
    {
        Position = position;
        AspectRatio = aspectRatio;
        var w = Window.Instance().Size;
        _lastPos = new Vector2(w.X/2f, w.Y/2f);
        Window.Instance().MousePosition = _lastPos;
    }
    
    public Vector3 Position { get; set; }
    public float AspectRatio { private get; set; }

    public Vector3 Front => _front;

    public Vector3 Up => _up;

    public Vector3 Right => _right;
    
    public float Pitch
    {
        get => MathHelper.RadiansToDegrees(_pitch);
        set
        {
            var angle = MathHelper.Clamp(value, -89f, 89f);
            _pitch = MathHelper.DegreesToRadians(angle);
            UpdateVectors();
        }
    }
    
    public float Yaw
    {
        get => MathHelper.RadiansToDegrees(_yaw);
        set
        {
            _yaw = MathHelper.DegreesToRadians(value);
            UpdateVectors();
        }
    }

    public float Fov
    {
        get => MathHelper.RadiansToDegrees(_fov);
        set
        {
            var angle = MathHelper.Clamp(value, 1f, 90f);
            _fov = MathHelper.DegreesToRadians(angle);
        }
    }
    
    public Matrix4 GetViewMatrix()
    {
        return Matrix4.LookAt(Position, Position + _front, _up);
    }
    
    public Matrix4 GetProjectionMatrix()
    {
        return Matrix4.CreatePerspectiveFieldOfView(_fov, AspectRatio, 0.01f, 100f);
    }
    
    private void UpdateVectors()
    {

        _front.X = MathF.Cos(_pitch) * MathF.Cos(_yaw);
        _front.Y = MathF.Sin(_pitch);
        _front.Z = MathF.Cos(_pitch) * MathF.Sin(_yaw);
        _front = Vector3.Normalize(_front);
        _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
        _up = Vector3.Normalize(Vector3.Cross(_right, _front));
    }

    public void OnUpdateFrame(FrameEventArgs e)
    {
        var input = Window.Instance().KeyboardState;
        const float cameraSpeed = 1.5f;
        const float sensitivity = 0.2f;
        if ( Window.Instance().MouseState.IsButtonPressed(MouseButton.Right))
        {
            Window.Instance().CursorState = CursorState.Grabbed;
            Window.Instance().MousePosition = _lastPos;
            _secondFrame = true;
        }

        if ( Window.Instance().MouseState.IsButtonDown(MouseButton.Right))
        {
            if (_secondFrame)
            {
                Window.Instance().MousePosition = _lastPos;
                _secondFrame = false;   
            }

            if (input.IsKeyDown(Keys.W))
            {
                this.Position += this.Front * cameraSpeed * (float)e.Time; 
            }

            if (input.IsKeyDown(Keys.S))
            {
                this.Position -= this.Front * cameraSpeed * (float)e.Time; 
            }

            if (input.IsKeyDown(Keys.A))
            {
                this.Position -= this.Right * cameraSpeed * (float)e.Time; 
            }

            if (input.IsKeyDown(Keys.D))
            {
                this.Position += this.Right * cameraSpeed * (float)e.Time; 
            }

            if (input.IsKeyDown(Keys.Space))
            {
                this.Position += this.Up * cameraSpeed * (float)e.Time; 
            }

            if (input.IsKeyDown(Keys.LeftShift))
            {
                this.Position -= this.Up * cameraSpeed * (float)e.Time; 
            }
            
            var _mouse =  Window.Instance().MousePosition;
            var deltaX = _mouse.X - _lastPos.X;
            var deltaY = _mouse.Y - _lastPos.Y;
            _lastPos = new Vector2(_mouse.X, _mouse.Y);
            this.Yaw += deltaX * sensitivity;
            this.Pitch -= deltaY * sensitivity;
        }

        if ( Window.Instance().MouseState.IsButtonReleased(MouseButton.Right))
        {
            Window.Instance().CursorState = CursorState.Normal;
        }
    }
}