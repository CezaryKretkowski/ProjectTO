using System.Numerics;
using ImGuiNET;
using OpenTK.Graphics.ES20;
using ProjectTo.Modules.GuiEditor.InputOutput;
using ProjectTO.Modules.GuiEditor.Shader;

namespace ProjectTo.Modules.GuiEditor;

public class Node
{
    public Shader _parent;
    public record DrawCircleStruct(Vector2 Point, float Radius,uint Color);
    public record DrawBezierStruct(Vector2 Point1,Vector2 Point2,uint Color);
    public String ID { get; init; }
    public string Title { get; set; } = "New Node";
    public Vector4 HeaderColor { get; set; } = new Vector4(0.16f, 0.29f, 0.48f, 1.0f);
    public Vector4 BorderColor { get; set; } = new Vector4(0.7f, 0.7f, 0.7f, 1f);
    public Vector4 BackGroundColor { get; set; } = new Vector4(0.24f, 0.24f, 0.27f, 1f);
    public Vector2 Size { get; set; } = new Vector2(200,150);
    
    public bool TryAttach { get; set; }

    protected List<DrawCircleStruct> _circleDrawList;
    protected List<DrawBezierStruct> _bezierDrawList;
    protected Vector2 _winPos = new Vector2(50,50);

    protected List<IForm> _inputs;
    protected IForm _output;

    public List<IForm> Inputs
    {
        get { return _inputs; }
    }

    public Node(Guid id,Vector2 winMenu,Shader menu)
    {
        _winPos = winMenu;
        ID = id.ToString();
        _circleDrawList = new List<DrawCircleStruct>();
        _bezierDrawList = new List<DrawBezierStruct>();
        _inputs = new List<IForm>();
        var input1 = new Input<float>(this);
        var input2 = new Input<string>(this);
        _inputs.Add(input1);
        _inputs.Add(input2);
        _output = new Output<float>(this);
        _parent = menu;
    }
    public Node(Guid id,Shader menu)
    {
        ID = id.ToString();
        _circleDrawList = new List<DrawCircleStruct>();
        _bezierDrawList = new List<DrawBezierStruct>();
        _parent = menu;
    }

    public void AddCircle(DrawCircleStruct param)
    {
        _circleDrawList.Add(param);
    }

    public void AddBezier(DrawBezierStruct bezier)
    {
        _bezierDrawList.Add(bezier);
    }


    protected virtual void DrawNodeContent()
    {
       
        foreach (var input in _inputs)
        {
            input.DrawInput();
        }
        _output.DrawInput();
    }

    public void DrawNode()
    {
        

        ImGui.SetNextWindowPos(_winPos);
        
        ImGui.PushStyleColor(ImGuiCol.ChildBg,BackGroundColor);
        ImGui.PushStyleColor(ImGuiCol.Border,BorderColor);

        ImGui.PushStyleVar(ImGuiStyleVar.ChildRounding,6.0f);
      
      
        ImGui.BeginChild(ID,Size,ImGuiChildFlags.Border);
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

        foreach (var line in _bezierDrawList)
        {
            ImGui.GetWindowDrawList().AddLine(line.Point1,line.Point2,line.Color);
        }

        if (TryAttach)
        {
            _parent.TryAttach(_output);

            TryAttach = false;
        }

        _circleDrawList.Clear();
        _bezierDrawList.Clear();
        _winPos = winPos;
        ImGui.PopStyleVar();
        ImGui.PopStyleColor(2);
        
      

    }
}