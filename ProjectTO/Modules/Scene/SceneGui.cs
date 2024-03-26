using System.Numerics;
using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using ProjectTo.Gui.Interfaces;
using ProjectTo.RenderEngine;

namespace ProjectTo.Modules.Scene;

public class SceneGui(FrameBuffer frameBuffer) : IGui
{
    private readonly FrameBuffer _frameBuffer = frameBuffer;

    public void OnRender()
    {
        ImGui.Begin("Scene");
        ImGui.BeginChild("Render"); 
        _frameBuffer.Bind();
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        
        _frameBuffer.Unbind();
        
        ImGui.Image(_frameBuffer.Texture,ImGui.GetWindowSize(),new Vector2(0,1),new Vector2(1,0));

        ImGui.EndChild();
        ImGui.End();
    }

   

}