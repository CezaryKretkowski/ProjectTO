using System.Numerics;
using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using ProjectTo.Gui.Interfaces;
using ProjectTo.Modules.GuiEditor;
using ProjectTO.Modules.Scene;
using ProjectTo.RenderEngine;

namespace ProjectTo.Modules.Scene;

public class SceneGui(FrameBuffer frameBuffer) : IGui
{
    private readonly FrameBuffer _frameBuffer = frameBuffer;
    private readonly SceneInspector _sceneInspector = new SceneInspector();
    private bool isActive;
    public void OnRender(FrameEventArgs e)
    {
        var compilerResult = ShaderEditorGui.Instance.CompileResult;
        _sceneInspector.OnRender(compilerResult.Success);
        ImGui.Begin("Scene");
        isActive = ImGui.IsWindowFocused();
        if(isActive)
            Camera.Instance.OnUpdateFrame(e);
        ImGui.BeginChild("Render"); 
        _frameBuffer.Bind();
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        
        if (!compilerResult.Success)
        {
            _sceneInspector.InitScene = false;
            _sceneInspector.DrawScene = false;
        }

        if (_sceneInspector.SceneIsReady)
        {
            if (_sceneInspector.DrawScene)
            {
                if (_sceneInspector.InitScene)
                {
                    if (compilerResult.Success)
                        _sceneInspector.Init(compilerResult);
                    _sceneInspector.InitScene = false;
                }

                if (compilerResult.Success)
                    _sceneInspector.Draw();
            }
        }

        _frameBuffer.Unbind();
        
        ImGui.Image(_frameBuffer.Texture,ImGui.GetWindowSize(),new Vector2(0,1),new Vector2(1,0));

        ImGui.EndChild();
        ImGui.End();
    }

   

}