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

            if (!SaveShader.Instance.IsPathReady&&isLoaded)
            {
                path=SaveShader.Instance.GetPath();
               // Console.WriteLine("Path:"+path);
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
                SaveShader.Instance.GetFilePath(".obj");
                isLoaded = true;
            }
            if (!SaveShader.Instance.IsPathReady&&isLoaded)
            {
                path=SaveShader.Instance.GetPath();
                Console.WriteLine("Path:"+path);
                Mesh = MeshLoader.LoadObj(path,true);
                isLoaded = false;
            }
            
        }
    }

    public void OpenMenu(InNode node)
    {
        this.node = node;
        ImGui.OpenPopup("Object chooser###"+node.Id);
    }

    public void DrawMenu()
    {
        
        if (node!=null&&ImGui.BeginPopupContextItem("Object chooser###"+node.Id))
        {
            if (ImGui.BeginMenu("Static data"))
            {
                if (ImGui.MenuItem("Vertices"))
                {
                    node.DataBuffer = StaticDataMesh.Vertices;
                    _lenght = node.DataBuffer.Length;
                    node.BufferTitle = "Static vertex";
                }
                if (ImGui.MenuItem("Normals"))
                {
                    node.DataBuffer = StaticDataMesh.Normals;
                    node.BufferTitle = "Static normals";
                }
                if (ImGui.MenuItem("uvs"))
                {
                    node.DataBuffer = StaticDataMesh.Uvs;
                    node.BufferTitle = "Static uvs";
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
                        node.BufferTitle = "Object vertex";
                        
                    }
                    if (ImGui.MenuItem("Normals"))
                    {
                        node.DataBuffer = Mesh.Normals;
                        node.BufferTitle = "Object normals";
                    }
                    if (ImGui.MenuItem("uvs"))
                    {
                        node.DataBuffer = Mesh.Uvs;
                        node.BufferTitle = "Object uvs";
                    }
                    ImGui.EndMenu();
                }
            }

            ImGui.EndPopup();
        }
    }
}