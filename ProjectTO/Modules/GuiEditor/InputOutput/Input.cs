using System.Numerics;
using System.Runtime.CompilerServices;
using ImGuiNET;

namespace ProjectTo.Modules.GuiEditor.InputOutput;

public class Input<T> :IForm
{
    
    public T Value
    {
        get { return _value;}
        set { _value = value; }
    }

    protected Vector2 _point;
    public T? _value;
    public string Name { get; init; } = "Input1";
    public  Output<T>? Output { get; set; }
    protected readonly Node _parent;

    public Input(Node parent)
    {
        _parent = parent;
    }

    public void DrawLine()
    {
        if (Output != null)
        {
           
                _parent.AddBezier(new Node.DrawBezierStruct(_point, Output._point,
                    ImGui.GetColorU32(new Vector4(0.93f, 0.84f, 0.35f, 1))));
        }
    }

    private void DrawForm()
    {
        if (typeof(float) == typeof(T))
        {
            float var=0.0f;
            ImGui.InputFloat(Name,ref var);
            _value = (T)Convert.ChangeType(var, typeof(T));;
        }

        if (typeof(T)==typeof(string))
        {
            ImGui.Text(Name);
        }
        DrawLine();
    }
    
    public void DrawInput()
    {
        float param = 0.01f;
        float mg = ImGui.GetCursorPos().Y;
        DrawForm();
        var point = ImGui.GetWindowPos();
        point.X -= 8.0f;
        point.Y += mg+10.0f;
        _parent.AddCircle(new Node.DrawCircleStruct(point,6.0f,ImGui.GetColorU32(new Vector4(0.93f,0.84f,0.35f,1))));
        _point = point;
        TryDetach();
       
    }

    public bool AttachOutput(IForm form)
    {
        var mousePosition = ImGui.GetMousePos();
        if (mousePosition.X > _point.X - 6.0f && mousePosition.X < _point.X + 6.0f &&
            mousePosition.Y > _point.Y - 6.0f && mousePosition.Y < _point.Y + 6.0f &&
            this.GetTType() == form.GetTType())
        {
            
            Output = (Output<T>?)form;
            return true;
        }
        else
        {
            return false;
        }

    }

    public Type GetTType()
    {
        return typeof(T);
    }

    public void TryDetach()
    {
        var mousePosition = ImGui.GetMousePos();
        if (mousePosition.X > _point.X - 6.0f && mousePosition.X < _point.X + 6.0f &&
            mousePosition.Y > _point.Y - 6.0f && mousePosition.Y < _point.Y + 6.0f &&
            ImGui.IsMouseClicked(ImGuiMouseButton.Right))
        {
            Output = null;
        }
    }

    public Guid GetParentId()
    {
        return _parent.ID;
    }
}