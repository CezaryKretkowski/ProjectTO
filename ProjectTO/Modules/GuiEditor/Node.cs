using System.Numerics;
using ImGuiNET;
using OpenTK.Graphics.ES20;

namespace ProjectTo.Modules.GuiEditor;

public class Node
{
    protected record DrawCircleStruct(Vector2 Point, float Radius,uint Color);
    public String ID { get; init; }
    public string Title { get; set; } = "New Node";
    public Vector4 HeaderColor { get; set; } = new Vector4(0.16f, 0.29f, 0.48f, 1.0f);
    public Vector4 BorderColor { get; set; } = new Vector4(0.7f, 0.7f, 0.7f, 1f);
    public Vector4 BackGroundColor { get; set; } = new Vector4(0.24f, 0.24f, 0.27f, 1f);

    protected List<DrawCircleStruct> _circleDrawList;

    protected Vector2 _winPos = new Vector2(50,50);
    
    
    public Node(Guid id,Vector2 winMenu)
    {
        _winPos = winMenu;
        ID = id.ToString();
        _circleDrawList = new List<DrawCircleStruct>();
    }
    public Node(Guid id)
    {
        ID = id.ToString();
        _circleDrawList = new List<DrawCircleStruct>();
    }

    protected void AddCircle(DrawCircleStruct param)
    {
        _circleDrawList.Add(param);
    }

    protected virtual void DrawInput()
    {
        
        
        
        
        float param = 0.01f;
        ImGui.InputFloat("Input 1",ref param);
        var point = ImGui.GetWindowPos();
        point.X -= 8.0f;
        point.Y += 9.0f;
        AddCircle(new DrawCircleStruct(point,6.0f,ImGui.GetColorU32(new Vector4(0.93f,0.84f,0.35f,1))));
        
        
    }

    protected virtual void DrawNodeContent()
    {
        DrawInput();
    }

    public void DrawNode()
    {
        

        ImGui.SetNextWindowPos(_winPos);
        
        ImGui.PushStyleColor(ImGuiCol.ChildBg,BackGroundColor);
        ImGui.PushStyleColor(ImGuiCol.Border,BorderColor);

        ImGui.PushStyleVar(ImGuiStyleVar.ChildRounding,6.0f);
      
      
        ImGui.BeginChild(ID,new Vector2(200,100),ImGuiChildFlags.Border);
            var winPos = ImGui.GetWindowPos();
            
            ImGui.PushStyleColor(ImGuiCol.Button,HeaderColor);
            ImGui.PushStyleColor(ImGuiCol.ButtonHovered,HeaderColor*1.5f);
            ImGui.PushStyleColor(ImGuiCol.ButtonActive,HeaderColor*0.7f);
            ImGui.ArrowButton("##DragIdle"+ID, ImGuiDir.Down);


            if (ImGui.IsItemActive()&&ImGui.IsMouseDragging(ImGuiMouseButton.Left))
            {
    
                var delta = ImGui.GetIO().MousePos;
                winPos.X = delta.X;
                winPos.Y = delta.Y;
     
            }
            ImGui.PopStyleColor(3);
            ImGui.SameLine();
            ImGui.Text(Title); 
            ImGui.Text(""); 
         
            
            ImGui.BeginChild("ChildFrame"+ID);
                DrawNodeContent();
            ImGui.EndChild();
       
            
        ImGui.EndChild(); 
        
        ImGui.GetWindowDrawList().AddRectFilled(new Vector2(_winPos.X,_winPos.Y),new Vector2(_winPos.X+200,_winPos.Y+35),ImGui.GetColorU32(HeaderColor),6.0f);
        ImGui.GetWindowDrawList().AddRect(new Vector2(_winPos.X,_winPos.Y),new Vector2(_winPos.X+200,_winPos.Y+35),ImGui.GetColorU32(BorderColor),6.0f);
        foreach (var circle in _circleDrawList)
        {
            ImGui.GetWindowDrawList().AddCircleFilled(circle.Point,circle.Radius,circle.Color);
        }
        _circleDrawList.Clear();
        _winPos = winPos;
        ImGui.PopStyleVar();
        ImGui.PopStyleColor(2);
        
      

    }
}