using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ProjectTo.Common;
using ProjectTo.Gui.Interfaces;
using ProjectTo.Modules.GraphicApi;
using ProjectTo.Modules.GuiEditor;
using ProjectTo.Modules.MainWindow;
using ProjectTo.Modules.Scene;
using ProjectTo.RenderEngine;


namespace ProjectTo.Window;

public class Window : GameWindow
{
    private static Window? _instance;
    private readonly ImGuiController _controller;
    private readonly FrameBuffer _frameBuffer;
    private readonly List<IGui> _modules;
    private readonly DataBaseInterface _dataBaseInterface;

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
        _modules = new List<IGui>();
        _dataBaseInterface = DataBaseInterface.Instance;
    }
    
    public static Window Instance(int width, int height, string title)
    {
        return _instance ??= new Window(width, height, title);
    }
    public static Window Instance()
    {
        return _instance ??= new Window(800, 600, "test");
    }
    
    protected override void OnLoad()
    {
        base.OnLoad();
        _modules.Add(new SceneGui(_frameBuffer));
        _modules.Add(new MainMenuBar());
        _modules.Add(new ShaderEditorGui());
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
        
        foreach (var module in _modules)
        {
            module.OnRender();
        }

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
        _controller.WindowResized(ClientSize.X, ClientSize.Y);
       
    }
    protected sealed override void OnUnload()
    {
        base.OnUnload();
       
    }
}