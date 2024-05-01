using ImGuiNET;
using ProjectTo.Gui.Interfaces;
using ProjectTo.Modules.GuiEditor;
using ProjectTO.Modules.GuiEditor.Shader;

namespace ProjectTo.Modules.MainWindow;

public class MainMenuBar : IGui
{
    public void OnRender()
    {
        ImGui.BeginMainMenuBar();
        if (ImGui.BeginMenu("File"))
        {
            if (ImGui.BeginMenu("New Node"))
            {
                
                ImGui.EndMenu();
            }

            if (ImGui.BeginMenu("Save"))
            {
                if (ImGui.MenuItem("Save fragment shader"))
                {
                    
                    var compileResult = ShaderEditorGui.Instance.CompileResult;
                    if (compileResult.Success){
                         SaveShader.Instance.SaveFile("Fragment", compileResult.VertSource, ".glsl");
                    }
                }
                if (ImGui.MenuItem("Save vertex shader"))
                {
                    var compileResult = ShaderEditorGui.Instance.CompileResult;
                    
                   if (compileResult.Success)
                   {
                        SaveShader.Instance.SaveFile("Vertex", compileResult.VertSource, ".glsl");
                   }
                }

                ImGui.EndMenu();
            }

            ImGui.EndMenu();
        }
        
        ImGui.EndMainMenuBar();
        SaveShader.Instance.ImGuiDialog();
    }
}