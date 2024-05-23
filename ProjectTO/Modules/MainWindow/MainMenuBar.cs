using System.Text;
using ImGuiNET;
using OpenTK.Windowing.Common;
using ProjectTo.Gui.Interfaces;
using ProjectTo.Modules.GuiEditor;
using ProjectTO.Modules.GuiEditor.Shader;

namespace ProjectTo.Modules.MainWindow;

public class MainMenuBar : IGui
{
    private bool isLoaded = false;
    private bool isvertex = false;
    public void OnRender(FrameEventArgs e)
    {
        ImGui.BeginMainMenuBar();
        if (ImGui.BeginMenu("File"))
        {
            if (ImGui.BeginMenu("Load"))
            {
                if (ImGui.MenuItem("Load vertex"))
                {
                    SaveShader.Instance.GetFilePath(".json");
                    isvertex = true;
                    isLoaded = true;
                }
                if (ImGui.MenuItem("Load fragment"))
                {
                    SaveShader.Instance.GetFilePath(".json");
                    isvertex = false;
                    isLoaded = true;
                }
                
                ImGui.EndMenu();
            }
            if (ImGui.BeginMenu("Save"))
            {
                if (ImGui.MenuItem("Save vertex"))
                {
                    SaveShader.Instance.SaveFile("Vertex Shader",ShaderEditorGui.Instance.vertexShader.Serialize(),".json");
                }
                if (ImGui.MenuItem("Save fragment"))
                {
                    SaveShader.Instance.SaveFile("Vertex Shader",ShaderEditorGui.Instance.fragmentShader.Serialize(),".json");
                }

                ImGui.EndMenu();
            }

            if (ImGui.BeginMenu("Export"))
            {
                if (ImGui.MenuItem("Export fragment shader"))
                {
                    
                    var compileResult = ShaderEditorGui.Instance.CompileResult;
                    if (compileResult.Success){
                         SaveShader.Instance.SaveFile("Fragment", compileResult.FragSource, ".glsl");
                    }
                }
                if (ImGui.MenuItem("Export vertex shader"))
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
        if (!SaveShader.Instance.IsPathReady&&isLoaded)
        {
            var path=SaveShader.Instance.GetPath();
            var content = File.ReadAllText(path);
            if(isvertex)
                ShaderEditorGui.Instance.vertexShader.Deserialize(content);
            else
                ShaderEditorGui.Instance.fragmentShader.Deserialize(content);
            isLoaded = false;
        }
        SaveShader.Instance.ImGuiDialog();
    }
}