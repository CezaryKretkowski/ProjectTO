using System.Numerics;
using ImGuiNET;
using ProjectTo.Modules.GraphicApi.DataModels;

namespace ProjectTo.Modules.GuiEditor.InputOutput;

public class Output<T>(Node parent,OutputDto dto) : IForm
{
    public Vector2 Point;
    private bool IsDrawing { get; set; }
    private string Name { get; set; } = "Output";

    private void DrawBezierOn()
    {
        var point2 = ImGui.GetMousePos();
        parent.AddBezier(new Node.DrawBezierStruct(Point,point2,ImGui.GetColorU32(new Vector4(0.93f,0.84f,0.35f,1))));
    }

    public void DrawInput()
    {
        ImGui.Text(Name);
        ImGui.Text("            ");
        ImGui.SameLine();
        var mg = ImGui.GetCursorPos().Y;
        ImGui.Text("Output"); 
        var point = ImGui.GetWindowPos();
        point.X += parent.Size.X-8.0f;
        point.Y += mg+10.0f;
        parent.AddCircle(new Node.DrawCircleStruct(point,6.0f,ImGui.GetColorU32(new Vector4(0.93f,0.84f,0.35f,1))));
        Point = point;
        
        DrawLine();
        if (ImGui.IsMouseDown(ImGuiMouseButton.Left) && IsDrawing == true)
        {
            DrawBezierOn();
        }
        else if(IsDrawing)
        {
            parent.TryAttach = true;
            IsDrawing = false;
        }
        else
        {
            IsDrawing = false;
        }
        TryDetach();
    }

    private void DrawLine()
    {
        var mousePosition = ImGui.GetMousePos();
        if (mousePosition.X > Point.X - 6.0f && mousePosition.X < Point.X + 6.0f &&
            mousePosition.Y > Point.Y - 6.0f && mousePosition.Y < Point.Y + 6.0f &&
            ImGui.IsMouseDown(ImGuiMouseButton.Left))
        {
            IsDrawing = true;
        }
    }

    public bool AttachOutput(IForm i)
    {
        return true;
    }

    public Type GetTType()
    {
        return typeof(T);
    }

    public void TryDetach() { }

    public Guid GetParentId()
    {
        return parent.Id;
    }

    public void SetTitle(string title)
    {
        Name = title;
    }
}