using System.Numerics;
using ProjectTO.Modules.GuiEditor.Shader;

namespace ProjectTo.Modules.GuiEditor;

public class InNode:Node
{
    public InNode(Guid id, Vector2 winMenu, Shader menu) : base(id, winMenu, menu)
    {
    }

    public InNode(Guid id, Shader menu) : base(id, menu)
    {
    }
}