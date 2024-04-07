using System.Numerics;
using ProjectTO.Modules.GuiEditor.Shader;

namespace ProjectTo.Modules.GuiEditor;

public class UniformNode : Node
{
    public UniformNode(Guid id, Vector2 winMenu, Shader menu) : base(id, winMenu, menu)
    {
    }

    public UniformNode(Guid id, Shader menu) : base(id, menu)
    {
    }
}