using System.Numerics;
using System.Runtime.CompilerServices;
using ImGuiNET;
using ProjectTo.Modules.GraphicApi.DataModels;
using ProjectTo.Modules.InputManager;
using ProjectTo.Modules.Scene;

namespace ProjectTo.Modules.GuiEditor.InputOutput;

public class Input :IForm
{
    private readonly InputDto _dto;
    
    private Vector2 _point;
    private string Name { get; set; } = "Input"+Guid.NewGuid().ToString();
    private Output? Output { get; set; }
    private readonly Node _parent;

    private Input(Node parent,InputDto dto)
    {
        _parent = parent;
        _dto = dto;
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
            GetInputType() == form.GetInputType())
        {
            Output = (Output)form;
            return true;
        }
        return false;
    }

    public string GetInputType()
    {
        return _dto.DataTypeDto.GlslType;
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

    public InputDto? GetInputDto()
    {
        return _dto;
    }
    public Guid GetOutputParent()
    {
        if (Output == null) return Guid.Empty;;
        return Output.GetParentId();
    }

    public static Input CreateInputInstance(Node parent,InputDto dto)
    {
        var input = new Input(parent,dto);
        input.Name = dto.Name;
        return input;
    }

    #region NotImplemented

        public void DrawOutput() { }
        public void SetUniforms(ShaderHelper shaderHelper) { }
        public int GetLocationSize()
        {
            return 0;
        }

        #endregion
}