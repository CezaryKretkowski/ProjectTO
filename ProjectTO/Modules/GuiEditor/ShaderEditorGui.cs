using System.Numerics;
using ImGuiNET;
using ProjectTo.Gui.Interfaces;
using ProjectTo.Modules.GuiEditor.InputOutput;

namespace ProjectTo.Modules.GuiEditor;

public class ShaderEditorGui : IGui
{
    private Vector2 _nodePose = new Vector2(0,0);
    private readonly Dictionary<Guid, Node> _nodes = new Dictionary<Guid, Node>();

    public void TryAttach(IForm output)
    {
        foreach (var node in _nodes.Values)
        {
            foreach (var input in node.Inputs)
            {
                input.AttachOutput(output);
            }
        }
    }

    public ShaderEditorGui()
    {
       
        AndNode(new Vector2(50,50));
        AndNode(new Vector2(50,50));
        
    }
    public void AndNode(Vector2 pos)
    {
        var id = Guid.NewGuid();
        _nodes.Add(id,new Node(id,pos,this));
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

                foreach (var node in _nodes)
                {
                    node.Value.DrawNode();
                }
                
     
            ImGui.EndChild();
            ImGui.PopStyleColor();
        
        ImGui.End();
    }
}