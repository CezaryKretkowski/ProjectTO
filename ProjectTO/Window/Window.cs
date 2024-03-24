using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ProjectTo.Common;
using ProjectTo.RenderEngine;
using Vector2 = System.Numerics.Vector2;

namespace ProjectTo.Window;

public class Window : GameWindow
{
    private static Window? _instance;
    private readonly ImGuiController _controller;
    private FrameBuffer _frameBuffer;

    private Window(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings()
    {
        ClientSize = new Vector2i(width, height),
        Title = title, Profile = ContextProfile.Core,
        Vsync = VSyncMode.On,
        APIVersion = new Version(4, 0)
    })
    {
        _controller = new ImGuiController(ClientSize.X, ClientSize.Y);
        _frameBuffer = new FrameBuffer(ClientSize.X, ClientSize.Y); 
    }
    
    public static Window Instance(int width, int height, string title)
    {
        return _instance ??= new Window(width, height, title);
    }
    
    protected override void OnLoad()
    {
        base.OnLoad();
        
        GL.ClearColor(0.0f, 0.0f, 0.4f, 0.0f);
        GL.Enable(EnableCap.DepthTest);
        GL.DepthFunc(DepthFunction.Less);
        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha,BlendingFactor.OneMinusSrcAlpha);
    }
    
    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        _controller.Update(this, (float)e.Time);
        GL.ClearColor(0.0f, 0.0f, 0.4f, 0.0f);
        ImGui.DockSpaceOverViewport();
        ImGui.BeginMainMenuBar();
        ImGui.MenuItem("File");
        ImGui.MenuItem("Save");
        ImGui.EndMainMenuBar();        
        ImGui.Begin("Scene");
        ImGui.BeginChild("Render"); 
        _frameBuffer.Bind();
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        
        _frameBuffer.Unbind();
        
        ImGui.Image(_frameBuffer.Texture,ImGui.GetWindowSize(),new Vector2(0,1),new Vector2(1,0));

        ImGui.EndChild();
        ImGui.End();

        GL.Finish();
        _controller.Render();
   
        SwapBuffers();
    }
    
    protected sealed override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);
        if (!IsFocused&&!MouseState.IsButtonDown(MouseButton.Right))
        {
            return;
        }
        var input = KeyboardState;
        if (input.IsKeyDown(Keys.Escape))
        {
            Close();
        }
        
    }
    protected sealed override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
       
    }
    protected sealed override void OnUnload()
    {
        base.OnUnload();
       
    }
}