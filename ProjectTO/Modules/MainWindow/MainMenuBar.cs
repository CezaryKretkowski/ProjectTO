using ImGuiNET;
using ProjectTo.Gui.Interfaces;

namespace ProjectTo.Modules.MainWindow;

public class MainMenuBar : IGui
{
    public void OnRender()
    {
        ImGui.BeginMainMenuBar();
        ImGui.MenuItem("File");
        ImGui.MenuItem("Save");
        ImGui.EndMainMenuBar();
    }
}