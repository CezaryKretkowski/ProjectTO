using System.Numerics;
using ImGuiNET;
using ProjectTo.Gui.Interfaces;

namespace ProjectTo.Modules.GuiEditor;

public class ShaderEditorGui : IGui
{
    private void DisplayGrid(int gridSize)
    {
        var size = Window.Window.Instance().Size;
        var gap = size.X / gridSize;
        uint color = ImGui.GetColorU32(new Vector4(0.58f,0.58f,0.58f,1));
        float x = 0;
        for (var i = 0; i < gridSize; i++)
        {
            ImGui.GetWindowDrawList().AddLine(new Vector2(x+gap,0),new Vector2(x+gap,size.Y),color);
            ImGui.GetWindowDrawList().AddLine(new Vector2(0,x+gap),new Vector2(size.X,x+gap),color);
            x += gap;
        }
    }

    public void OnRender()
    {
        ImGui.Begin("Node");
        
        ImGui.PushStyleColor(ImGuiCol.ChildBg,new Vector4(0.24f, 0.24f, 0.27f, 0.78f));
        ImGui.BeginChild("menu");
        DisplayGrid(16);
        ImGui.PopStyleVar(); 
        ImGui.EndChild();
        
        ImGui.End();
    }
}