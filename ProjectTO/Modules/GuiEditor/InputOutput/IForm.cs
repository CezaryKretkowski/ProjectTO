

using System.Numerics;

namespace ProjectTo.Modules.GuiEditor.InputOutput;

public interface IForm
{
     void DrawInput();
     bool AttachOutput(IForm form);
     Type GetTType();
}