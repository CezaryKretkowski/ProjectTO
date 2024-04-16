using System.Numerics;
using ImGuiNET;
using ProjectTo.Modules.GuiEditor.InputOutput;

namespace ProjectTo.Modules.GuiEditor;

public partial class Node
{
    protected Vector4 HeaderColor { get; set; } = new Vector4(0.16f, 0.29f, 0.48f, 1.0f);
    private Vector4 BorderColor { get; set; } = new Vector4(0.7f, 0.7f, 0.7f, 1f);
    private Vector4 BackGroundColor { get; set; } = new Vector4(0.24f, 0.24f, 0.27f, 1f);
    private readonly List<DrawCircleStruct> _circleDrawList;
    private readonly List<DrawBezierStruct> _bezierDrawList;


    #region Designe methods
    
    private void DrawHeaderAndCircle()
    {
        ImGui.GetWindowDrawList().AddRectFilled(new Vector2(_winPos.X, _winPos.Y),
            new Vector2(_winPos.X + Size.X, _winPos.Y + 35), ImGui.GetColorU32(HeaderColor), 6.0f);
        ImGui.GetWindowDrawList().AddRect(new Vector2(_winPos.X, _winPos.Y),
            new Vector2(_winPos.X + Size.X, _winPos.Y + 35), ImGui.GetColorU32(BorderColor), 6.0f);
        foreach (var circle in _circleDrawList)
            ImGui.GetWindowDrawList().AddCircleFilled(circle.Point, circle.Radius, circle.Color);
        
        foreach (var line in _bezierDrawList)
            ImGui.GetWindowDrawList().AddLine(line.Point1, line.Point2, line.Color);
    }
    private void DrawSubMenu()
    {
        if (_displaySuMenu)
        {
            if (ImGui.BeginPopupContextItem("item" + ID))
            {
                DrawSubMenuContent();
                if (ImGui.MenuItem("Remove"))
                    Parent.Remove(ID);
                ImGui.EndPopup();
            }
        }
        
        if (!ImGui.IsPopupOpen("item" + ID) && ImGui.IsMouseClicked(ImGuiMouseButton.Left))
        {
            _displaySuMenu = false;
        }
    }

    private void BeginNode()
    {
        ImGui.SetNextWindowPos(_winPos);
        ImGui.PushStyleColor(ImGuiCol.ChildBg, BackGroundColor);
        ImGui.PushStyleColor(ImGuiCol.Border, BorderColor);
        ImGui.PushStyleVar(ImGuiStyleVar.ChildRounding, 6.0f);
        ImGui.BeginChild(ID.ToString(), Size, ImGuiChildFlags.Border);
        ImGui.PushStyleColor(ImGuiCol.Button, HeaderColor);
        ImGui.PushStyleColor(ImGuiCol.ButtonHovered, HeaderColor * 1.5f);
        ImGui.PushStyleColor(ImGuiCol.ButtonActive, HeaderColor * 0.7f);
    }


    private void BeginNodeContent()
    {
        ImGui.PopStyleColor(3);
        ImGui.SameLine();
        DrawHeaderContent();
        ImGui.Text("");
        ImGui.BeginChild("ChildFrame" + ID);
    }
    private void EndNodeContent()
    {
        ImGui.EndChild();
        ImGui.EndChild();
        DrawHeaderAndCircle();
    }

    private void EndNode()
    {
        _circleDrawList.Clear();
        _bezierDrawList.Clear();
        ImGui.PopStyleVar();
        ImGui.PopStyleColor(2);
    }

    public void AddCircle(DrawCircleStruct param)
    {
        _circleDrawList.Add(param);
    }

    public void AddBezier(DrawBezierStruct bezier)
    {
        _bezierDrawList.Add(bezier);
    }
    #endregion

    
    
    #region Virtual methode
    public virtual List<IForm> Inputs => new();
    protected virtual void DrawNodeContent() { }
    protected virtual void DrawSubMenuContent() { }
    protected virtual void TryAttached() { }

    #endregion
    

    #region private classes
        public record DrawCircleStruct(Vector2 Point, float Radius, uint Color);
        public record DrawBezierStruct(Vector2 Point1, Vector2 Point2, uint Color);
    #endregion

}
