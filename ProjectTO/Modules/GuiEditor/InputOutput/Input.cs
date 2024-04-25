using System.Numerics;
using System.Runtime.CompilerServices;
using ImGuiNET;
using ProjectTo.Modules.GraphicApi.DataModels;
using ProjectTo.Modules.InputManager;
using ProjectTo.Modules.Scene;

namespace ProjectTo.Modules.GuiEditor.InputOutput;

public class Input<T> :IForm
{
    private InputDto? _dto;
    private T? Value { get; set; }

    private Vector2 _point;
    private string Name { get; set; } = "Input"+Guid.NewGuid().ToString();
    private Output<T>? Output { get; set; }
    private readonly Node _parent;

    private Input(Node parent)
    {
        _parent = parent;

        
    }

    private void DrawLine()
    {
        if (Output != null)
        {
                _parent.AddBezier(new Node.DrawBezierStruct(_point, Output.Point,
                    ImGui.GetColorU32(new Vector4(0.93f, 0.84f, 0.35f, 1))));
        }
    }

    private void DrawForm()
    {
       
        ImGui.Text(" "+_dto.DataTypeDto.GlslType);
        ImGui.Text("");
        DrawLine();
    }


    public void DrawInput()
    {
        var mg = ImGui.GetCursorPos().Y;
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
        return _parent.Id;
    }

    public void SetTitle(string title)
    {
        Name = title;
    }

    public string GetTitle()
    {
        return Name;
    }

    public InputDto? GetInputDto()
    {
        return _dto!;
    }

    public Guid GetOutputParent()
    {
        if (Output == null) return Guid.Empty;;
        return Output.GetParentId();
    }

    public void DrawOutpute()
    {
        throw new NotImplementedException();
    }

    public void SetUnforms(ShaderHelper shaderHelper)
    {
        throw new NotImplementedException();
    }


    // public int GetOutputID()
    // {
    //     
    //     return 0;
    // }

    public static Input<T> CreateInputInstance(Node parent,InputDto dto)
    {
        
        //Do zrobienia dorzucić tu ten mechanizm 
        var input = new Input<T>(parent);
        input.Name = dto.Name;
        input._dto = dto;
        return input;
    }
}