

using System.Numerics;
using ProjectTo.Modules.GraphicApi.DataModels;
using ProjectTo.Modules.Scene;

namespace ProjectTo.Modules.GuiEditor.InputOutput;

public interface IForm
{
     void DrawInput();
     bool AttachOutput(IForm form);
     string GetInputType();
     Guid GetParentId();
     void SetTitle(string title);
     InputDto? GetInputDto();
     Guid GetOutputParent();
     void DrawOutput();
     void SetUniforms(ShaderHelper shaderHelper);

}