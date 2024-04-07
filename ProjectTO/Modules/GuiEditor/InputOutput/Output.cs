using System.Numerics;
using ImGuiNET;

namespace ProjectTo.Modules.GuiEditor.InputOutput;

public class Output<T> : IForm
{
    protected readonly Node _parent;
    public Vector2 _point;
    private bool IsDrawing { get; set; }

    private void DrawBazierOn()
    {
        var point2 = ImGui.GetMousePos();
        _parent.AddBezier(new Node.DrawBezierStruct(_point,point2,ImGui.GetColorU32(new Vector4(0.93f,0.84f,0.35f,1))));
    }

    public Output(Node parent)
    {
        _parent = parent;
    }

    public void DrawInput()
    {
        ImGui.Text("    ");
        ImGui.Text("            ");
        ImGui.SameLine();
        float mg = ImGui.GetCursorPos().Y;
        ImGui.Text("Output"); 
        
        var point = ImGui.GetWindowPos();
        point.X += _parent.Size.X-8.0f;
        point.Y += mg+10.0f;
        _parent.AddCircle(new Node.DrawCircleStruct(point,6.0f,ImGui.GetColorU32(new Vector4(0.93f,0.84f,0.35f,1))));
        _point = point;
        
        DrawLine();
        if (ImGui.IsMouseDown(ImGuiMouseButton.Left) && IsDrawing == true)
        {
            DrawBazierOn();
        }
        else if(IsDrawing)
        {
            _parent.TryAttach = true;
            IsDrawing = false;
        }
        else
        {
            IsDrawing = false;
        }
        TryDetach();
    }

    public void DrawLine()
    {
        var mousePosition = ImGui.GetMousePos();
        if (mousePosition.X > _point.X - 6.0f && mousePosition.X < _point.X + 6.0f &&
            mousePosition.Y > _point.Y - 6.0f && mousePosition.Y < _point.Y + 6.0f &&
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

    public void TryDetach()
    {
        
    }

    public Guid GetParentId()
    {
        return _parent.ID;
    }
}