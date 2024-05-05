using System.Numerics;
using ImGuiNET;
using ProjectTo.Modules.GuiEditor;

namespace ProjectTo.Modules.Scene;

public class ObjectHelper
{
    public Mesh Mesh { get; set; }
    public string path = "";
    private bool isLoaded=false;
    private InNode node;
    private int _lenght=0;
    public int GetVericesLenght()
    {
        return _lenght;
    }

    public void InitFromFile()
    {
    }

    public ObjectHelper()
    {
    }

    public void Draw()
    {
        if (string.IsNullOrEmpty(path))
        {
            ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1.0f, 0.0f, 0.0f, 1.0f));
            ImGui.Text("Object not Set");
            ImGui.SameLine();
            ImGui.PopStyleColor();
            if (ImGui.Button("Chose file"))
            {
                SaveShader.Instance.GetFilePath(".obj");
                isLoaded = true;
            }

            if (!string.IsNullOrEmpty(SaveShader.Instance.GetPath())&&isLoaded)
            {
                path=SaveShader.Instance.GetPath();
                Mesh = MeshLoader.LoadObj(path,true);
                
                isLoaded = false;
            }

        }
        else
        {
            ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(0.0f, 1.0f, 0.0f, 1.0f));
            var shortCut = path.Substring(0, 11);
            ImGui.Text(shortCut+"...");
            ImGui.PopStyleColor();
            ImGui.SameLine();
            if (ImGui.Button("Change file"))
            {
            }
            
        }
    }

    public void OpenMenu(InNode node)
    {
        this.node = node;
        ImGui.OpenPopup("Object chooser");
    }

    public void DrawMenu()
    {
        if (ImGui.BeginPopupContextItem("Object chooser"))
        {
            if (ImGui.BeginMenu("Static data"))
            {
                if (ImGui.MenuItem("Vertices"))
                {
                    node.DataBuffer = StaticDataMesh.Vertices;
                    _lenght = node.DataBuffer.Length;
                }
                if (ImGui.MenuItem("Normals"))
                {
                    node.DataBuffer = StaticDataMesh.Normals;
                }
                if (ImGui.MenuItem("uvs"))
                {
                    node.DataBuffer = StaticDataMesh.Uvs;
                }

                ImGui.EndMenu();
            }

            if (!string.IsNullOrEmpty(path))
            {
                if (ImGui.BeginMenu("Object data"))
                {
                    if (ImGui.MenuItem("Vertices"))
                    {
                        node.DataBuffer = Mesh.Vertices;
                        _lenght = node.DataBuffer.Length;
                        
                    }
                    if (ImGui.MenuItem("Normals"))
                    {
                        node.DataBuffer = Mesh.Normals;
                    }
                    if (ImGui.MenuItem("uvs"))
                    {
                        node.DataBuffer = Mesh.Uvs;
                    }
                    ImGui.EndMenu();
                }
            }

            ImGui.EndPopup();
        }
    }
}