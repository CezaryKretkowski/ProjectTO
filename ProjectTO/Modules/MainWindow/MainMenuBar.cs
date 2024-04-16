using ImGuiNET;
using ProjectTo.Gui.Interfaces;

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
                ImGui.EndMenu();
            }

            ImGui.EndMenu();
        }
        
        ImGui.EndMainMenuBar();
    }
}