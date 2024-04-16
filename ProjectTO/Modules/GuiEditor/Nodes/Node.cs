using System.Data.Entity.Core.Metadata.Edm;
using System.Numerics;
using ImGuiNET;
using OpenTK.Graphics.ES20;
using ProjectTo.Modules.GuiEditor.InputOutput;
using ProjectTO.Modules.GuiEditor.Shader;

namespace ProjectTo.Modules.GuiEditor;

public partial class Node
{
    protected readonly Shader Parent;
    
    private bool _displaySuMenu = false;
    public Guid ID { get; init; }
    protected string Title { get; set; } = "New Node";
    public Vector2 Size { get; set; } = new Vector2(200, 150);
    private Vector2 _prevWinPos;
    private Vector2 _winPos;
    public bool TryAttach { get; set; }
    

    protected Node(Guid id, Vector2 winMenu, Shader menu)
    {
        var winPos = ImGui.GetWindowPos();
        _prevWinPos = winMenu;
        _winPos = winMenu;
        ID = id;
        _circleDrawList = new List<DrawCircleStruct>();
        _bezierDrawList = new List<DrawBezierStruct>();
        Parent = menu;
    }

    protected Node(Guid id, Shader menu)
    {
        var winPos = ImGui.GetWindowPos();
        _winPos = new Vector2(50, 50);
        _prevWinPos = new Vector2(50, 50);
        ID = id;
        _circleDrawList = new List<DrawCircleStruct>();
        _bezierDrawList = new List<DrawBezierStruct>();
        Parent = menu;
    }
    
    

    protected virtual void DrawHeaderContent()
    {
        ImGui.Text(Title);
    }

    public void DrawNode()
    {      
        var winPos = _prevWinPos;
        var prevWinPos = ImGui.GetWindowPos();
        var parentWinSize = ImGui.GetWindowSize();
        var isDragging = false;
        var mousePos = new Vector2(0, 0);
        BeginNode();
        
            ImGui.ArrowButton("##DragIdle" + ID, ImGuiDir.Down);
            if (ImGui.IsItemActive() && ImGui.IsMouseDragging(ImGuiMouseButton.Left))
            {
                isDragging = true;
                mousePos = SaveMousePos(prevWinPos, parentWinSize);
                var delta = ImGui.GetWindowPos() - _prevWinPos;
                _prevWinPos = mousePos - delta;
            }
        
            if (ImGui.IsItemClicked(ImGuiMouseButton.Right))
                _displaySuMenu = true;
        
            BeginNodeContent();
                DrawNodeContent();
            EndNodeContent();
            
        EndNode();

        _winPos = !isDragging ? prevWinPos + winPos : mousePos;
        
        DrawSubMenu();
        
        if (!TryAttach) return;
        TryAttached();
        TryAttach = false;

    }

    
    private Vector2 SaveMousePos(Vector2 winPos, Vector2 size)
    {
        var mousePos = ImGui.GetMousePos();
        if (mousePos.X < winPos.X)
            mousePos.X = winPos.X;
        if (mousePos.X > winPos.X + (size.X - Size.X))
            mousePos.X = winPos.X + size.X - Size.X;
        if (mousePos.Y < winPos.Y)
            mousePos.Y = winPos.Y;
        if (mousePos.Y > winPos.Y + (size.Y - Size.Y))
            mousePos.Y = winPos.Y + size.Y - Size.Y;
        return mousePos;
    }

    

    
}