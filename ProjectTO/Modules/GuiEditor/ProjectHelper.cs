using System.Text.Json;
using ProjectTO.Modules.GuiEditor.Shader;

namespace ProjectTo.Modules.GuiEditor;

public class ProjectHelper
{
    public VertexShader VertexShaderToSave { get; set; }
    public FragmentShader FragmentShaderToSave { get; set; }

    public void SaveShaderProject(string name,ProjectHelper projectHelper)
    {
        string var = JsonSerializer.Serialize(ShaderEditorGui.Instance.vertexShader);
        Console.WriteLine(var);
    }

}