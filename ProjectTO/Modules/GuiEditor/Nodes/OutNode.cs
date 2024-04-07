using System.Numerics;
using ProjectTO.Modules.GuiEditor.Shader;

namespace ProjectTo.Modules.GuiEditor;

public class OutNode : Node
{
    public OutNode(Guid id, Vector2 winMenu, Shader menu) : base(id, winMenu, menu)
    {
    }

    public OutNode(Guid id, Shader menu) : base(id, menu)
    {
    }
}