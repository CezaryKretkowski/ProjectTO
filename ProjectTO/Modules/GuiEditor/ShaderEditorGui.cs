using System.Numerics;
using ImGuiNET;
using ProjectTo.Gui.Interfaces;
using ProjectTo.Modules.GraphicApi.DataModels;
using ProjectTo.Modules.GuiEditor.InputOutput;
using ProjectTO.Modules.GuiEditor.Shader;

namespace ProjectTo.Modules.GuiEditor;

public class ShaderEditorGui : IGui
{
    private Vector2 _nodePose = new Vector2(0,0);
    private readonly Dictionary<Guid, Node> _nodes = new Dictionary<Guid, Node>();

    private string[] Items { get; set; }
    private int _displayIndex = -1;
    private readonly VertexShader vertexShader;
    private readonly FramgentShader fragmentShader;

    public ShaderEditorGui()
    {
        vertexShader = new VertexShader();  
        fragmentShader = new FramgentShader();
        NodeDto node = new NodeDto() { 
        Name = "type",
        ShaderType = Types.In
        };
        NodeDto node1 = new NodeDto()
        {
            Name = "type1",
            ShaderType = Types.Function
        };
        vertexShader.AndNode(node,new Vector2(50,50));
        vertexShader.AndNode(node1,new Vector2(50,50));
        Items = new[] { "Vertex Shader", "Fragment Shader" };
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
        
        ImGui.Begin("Inspector");

        for(var i = 0; i<Items.Length;i++)
        {
                ImGui.Selectable(Items[i],(i==_displayIndex));
                if (ImGui.IsItemClicked())
                {
                    _displayIndex = i;
                }
        }
        

        ImGui.End();
        ImGui.Begin("Node");
        
            ImGui.PushStyleColor(ImGuiCol.ChildBg,new Vector4(0.24f, 0.24f, 0.27f, 0.68f));
            ImGui.BeginChild("menu");
                DisplayGrid(32);
                if(_displayIndex==0) 
                    vertexShader.DrawNodes();
                else
                    fragmentShader.DrawNodes();                
                
                
     
            ImGui.EndChild();
            ImGui.PopStyleColor();
        
        ImGui.End();
    }
}