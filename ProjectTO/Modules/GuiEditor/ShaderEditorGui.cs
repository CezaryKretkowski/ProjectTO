using System.Numerics;
using ImGuiNET;
using ProjectTo.Gui.Interfaces;
using ProjectTo.Modules.GuiEditor.InputOutput;
using ProjectTO.Modules.GuiEditor.Shader;

namespace ProjectTo.Modules.GuiEditor;

public class ShaderEditorGui : IGui
{
    private Vector2 _nodePose = new Vector2(0,0);
    private readonly Dictionary<Guid, Node> _nodes = new Dictionary<Guid, Node>();

    
    private readonly VertexShader vertexShader;
    private readonly FramgentShader famgentShader;

    public ShaderEditorGui()
    {
        vertexShader = new VertexShader();  
        vertexShader.AndNode(new Vector2(50,50));
        vertexShader.AndNode(new Vector2(50,50));
        
    }


    private void DisplayGrid(int gridSize)
    {
        var size =Window.Window.Instance().Size;
        var gap = size.X / gridSize;
        var pos = ImGui.GetWindowPos();
        uint color = ImGui.GetColorU32(new Vector4(0.58f,0.58f,0.58f,1));
        float x = pos.X;
        float y = pos.Y;
        for (var i = 0; i < gridSize; i++)
        {
            
            ImGui.GetWindowDrawList().AddLine(new Vector2(x+gap,0),new Vector2(x+gap,size.Y),color);
            ImGui.GetWindowDrawList().AddLine(new Vector2(0,y+gap),new Vector2(size.X,y+gap),color);
            x += gap;
            y += gap;
        }
        
    }



    public void OnRender()
    {
        
        ImGui.Begin("Node");
        
            ImGui.PushStyleColor(ImGuiCol.ChildBg,new Vector4(0.24f, 0.24f, 0.27f, 0.68f));
            ImGui.BeginChild("menu");
                DisplayGrid(32);

                vertexShader.DrawNodes();
                
     
            ImGui.EndChild();
            ImGui.PopStyleColor();
        
        ImGui.End();
    }
}