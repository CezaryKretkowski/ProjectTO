using System.Numerics;
using ImGuiNET;
using OpenTK.Graphics.ES20;
using ProjectTo.Modules.GuiEditor.InputOutput;
using ProjectTO.Modules.GuiEditor.Shader;

namespace ProjectTo.Modules.GuiEditor;

public class Node
{
    private Shader _parent;
    public record DrawCircleStruct(Vector2 Point, float Radius,uint Color);
    public record DrawBezierStruct(Vector2 Point1,Vector2 Point2,uint Color);

    public Guid ID { get; init; }
    private string Title { get; set; } = "New Node";
    private Vector4 HeaderColor { get; set; } = new Vector4(0.16f, 0.29f, 0.48f, 1.0f);
    private Vector4 BorderColor { get; set; } = new Vector4(0.7f, 0.7f, 0.7f, 1f);
    private Vector4 BackGroundColor { get; set; } = new Vector4(0.24f, 0.24f, 0.27f, 1f);
    public Vector2 Size { get; set; } = new Vector2(200,150);
    private Vector2 _prevWinPos;
 
    public bool TryAttach { get; set; }

    private List<DrawCircleStruct> _circleDrawList;
    private List<DrawBezierStruct> _bezierDrawList;
    private Vector2 _winPos;

    protected List<IForm> _inputs;
    protected IForm _output;
    
    public List<IForm> Inputs
    {
        get { return _inputs; }
    }

    public Node(Guid id,Vector2 winMenu,Shader menu)
    {
        var winPos = ImGui.GetWindowPos();
        _prevWinPos =winMenu;
        _winPos = winMenu;
        ID = id;
        _circleDrawList = new List<DrawCircleStruct>();
        _bezierDrawList = new List<DrawBezierStruct>();
        _inputs = new List<IForm>();
        _parent = menu;
    }
    public Node(Guid id,Shader menu)
    {
        var winPos = ImGui.GetWindowPos();
        _winPos = new Vector2(50,50);
        _prevWinPos =new Vector2(50,50);
        ID = id;
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

    private void DrawHeaderAndCircle()
    {
        ImGui.GetWindowDrawList().AddRectFilled(new Vector2(_winPos.X,_winPos.Y),new Vector2(_winPos.X+Size.X,_winPos.Y+35),ImGui.GetColorU32(HeaderColor),6.0f);
        ImGui.GetWindowDrawList().AddRect(new Vector2(_winPos.X,_winPos.Y),new Vector2(_winPos.X+Size.X,_winPos.Y+35),ImGui.GetColorU32(BorderColor),6.0f);
        foreach (var circle in _circleDrawList)
            ImGui.GetWindowDrawList().AddCircleFilled(circle.Point,circle.Radius,circle.Color);
        

        foreach (var line in _bezierDrawList)
            ImGui.GetWindowDrawList().AddLine(line.Point1,line.Point2,line.Color);
    }

    public void DrawNode()
    {
        ImGui.SetNextWindowPos(_winPos);
        ImGui.PushStyleColor(ImGuiCol.ChildBg,BackGroundColor);
        ImGui.PushStyleColor(ImGuiCol.Border,BorderColor);
        ImGui.PushStyleVar(ImGuiStyleVar.ChildRounding,6.0f);   
        var winPos =_prevWinPos;
        var prevWinPos = ImGui.GetWindowPos();
        var parentWinSize = ImGui.GetWindowSize();
        var isDraging = false;
        Vector2 mousePos = new Vector2(0, 0);
        ImGui.BeginChild(ID.ToString(),Size,ImGuiChildFlags.Border);
            ImGui.PushStyleColor(ImGuiCol.Button,HeaderColor);
            ImGui.PushStyleColor(ImGuiCol.ButtonHovered,HeaderColor*1.5f);
            ImGui.PushStyleColor(ImGuiCol.ButtonActive,HeaderColor*0.7f);
            ImGui.ArrowButton("##DragIdle"+ID, ImGuiDir.Down);
            if (ImGui.IsItemActive()&&ImGui.IsMouseDragging(ImGuiMouseButton.Left))
            {
                isDraging = true;
                mousePos = SaveMousPos(prevWinPos,parentWinSize);
                var delta = ImGui.GetWindowPos() - _prevWinPos;
                _prevWinPos = mousePos - delta;
            }
            ImGui.PopStyleColor(3);
            ImGui.SameLine();
            ImGui.Text(Title); 
            ImGui.Text(""); 
            ImGui.BeginChild("ChildFrame"+ID);
            DrawNodeContent();
            ImGui.EndChild();
        ImGui.EndChild(); 
        DrawHeaderAndCircle();
        

        if (TryAttach)
        {
            _parent.TryAttach(_output);
            TryAttach = false;
        }

        _circleDrawList.Clear();
        _bezierDrawList.Clear();
        if(!isDraging)
            _winPos =prevWinPos+winPos;
        else
            _winPos = mousePos;
        
        ImGui.PopStyleVar();
        ImGui.PopStyleColor(2);
        
    }

    private Vector2 SaveMousPos(Vector2 winPos,Vector2 size)
    {
        var mousePos = ImGui.GetMousePos();
        if (mousePos.X < winPos.X)
            mousePos.X = winPos.X;
        if (mousePos.X > winPos.X+(size.X-Size.X))
            mousePos.X = winPos.X +size.X-Size.X;
        if (mousePos.Y < winPos.Y)
            mousePos.Y = winPos.Y;
        if (mousePos.Y > winPos.Y+(size.Y-Size.Y))
            mousePos.Y = winPos.Y +size.Y-Size.Y;
        return mousePos;
    }
}