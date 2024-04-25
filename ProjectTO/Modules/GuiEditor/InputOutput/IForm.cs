

using System.Numerics;
using ProjectTo.Modules.GraphicApi.DataModels;
using ProjectTo.Modules.Scene;

namespace ProjectTo.Modules.GuiEditor.InputOutput;

public interface IForm
{
     void DrawInput();
     bool AttachOutput(IForm form);
     Type GetTType();
     void TryDetach();
     Guid GetParentId();

     void SetTitle(string title);
     string GetTitle();
     InputDto? GetInputDto();

     Guid GetOutputParent();
     void DrawOutpute();

     void SetUnforms(ShaderHelper shaderHelper);

}