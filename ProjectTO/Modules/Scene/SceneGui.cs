using System.Numerics;
using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using ProjectTo.Gui.Interfaces;
using ProjectTo.Modules.GuiEditor;
using ProjectTo.RenderEngine;

namespace ProjectTo.Modules.Scene;

public class SceneGui(FrameBuffer frameBuffer) : IGui
{
    private readonly FrameBuffer _frameBuffer = frameBuffer;
    private readonly SceneInspector _sceneInspector = new SceneInspector();
    private bool init = false;
    private bool daraw = false;

    public void OnRender()
    {
        _sceneInspector.OnRender();
        ImGui.Begin("Scene");
        ImGui.BeginChild("Render"); 
        _frameBuffer.Bind();
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        var compierResutl = ShaderEditorGui.Instance.CompileResult;
        if (!compierResutl.Success)
        {
            _sceneInspector.InitScene = false;
            _sceneInspector.DrawScene = false;
        }

        if (_sceneInspector.DrawScene)
        {
            if (_sceneInspector.InitScene)
            {
                if(compierResutl.Success)
                    _sceneInspector.Init(compierResutl);
                _sceneInspector.InitScene = false;
            }
            if(compierResutl.Success)
                _sceneInspector.Draw();
        }

        _frameBuffer.Unbind();
        
        ImGui.Image(_frameBuffer.Texture,ImGui.GetWindowSize(),new Vector2(0,1),new Vector2(1,0));

        ImGui.EndChild();
        ImGui.End();
    }

   

}