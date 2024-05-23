using System.Numerics;
using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using ProjectTo.Modules.GraphicApi.DataModels;
using ProjectTo.Modules.GuiEditor;
using ProjectTO.Modules.GuiEditor.Shader;

namespace ProjectTo.Modules.Scene;

public class SceneInspector
{
    private ShaderHelper _helper;
    private List<Node> _uniformNodes;
    private ObjectHelper _objectHelper = new ObjectHelper();

    public bool SceneIsReady { get; set; }
    public bool InitScene { get; set; } = false;
    public bool DrawScene { get; set; } = false;
    

    private int _vao;
    private int _vbo;

    
    public void SetShaders()
    {
        //SceneIsReady = false;
        var list1 = ShaderEditorGui.Instance.fragmentShader.GetListNode().Where(x=>x.Entity.ShaderType != Types.In);
        var list2 = ShaderEditorGui.Instance.vertexShader.GetListNode();
        var list = list1.Concat(list2);
        _uniformNodes = list.Where(x=>x.Entity.ShaderType == Types.Uniform || x.Entity.ShaderType == Types.In).ToList();
    }

    public void Init(CompileResult result)
    {
        Console.WriteLine("Init");
        _vao = GL.GenVertexArray();
        GL.BindVertexArray(_vao);
        var inList = _uniformNodes.Where(x => x.Entity.ShaderType == Types.In);
        foreach (var inNode in inList)
        {
            LocationHelper hellper = new LocationHelper();
            if (inNode.Output != null)
            {
                var dataBuffer = ((InNode)inNode).DataBuffer;
                if (dataBuffer != null)
                    hellper.Init(dataBuffer, inNode.Output.GetLocationSize(), inNode.LocationId,
                        BufferUsageHint.StaticDraw);
            }
        }
        

        _helper = new ShaderHelper(result.VertSource, result.FragSource);
        _helper.Use();        
    }

    private void SetUniforms()
    {
        var uniformList = _uniformNodes.Where(x => x.Entity.ShaderType == Types.Uniform);
        foreach (var uniform in uniformList)
        {
            ((UniformNode)uniform).Output?.SetUniforms(_helper);
        }
    }

    private void DrawUniforms()
    {
        var uniformList = _uniformNodes.Where(x => x.Entity.ShaderType == Types.Uniform);
        foreach (var uniform in uniformList)
        {
            ((UniformNode)uniform).Output?.DrawOutput();
        }
    }

    private void DrawIns()
    {
        var inList = _uniformNodes.Where(x => x.Entity.ShaderType == Types.In).ToList();
        var isRedy =true;
        if (inList.Any())
        {
            foreach (var inNode in inList)
            {
                if (((InNode)inNode).DataBuffer == null)
                {
                    isRedy = false;
                }
            }

            if (isRedy)
                SceneIsReady = true;
        }

        foreach (var inNode in inList)
        {
            ImGui.Text(inNode.Title);
            ImGui.SameLine();
            if (ImGui.Button("Set data##"+inNode.Id))
            {
                _objectHelper.OpenMenu(((InNode)inNode));
            }
            ImGui.SameLine();
            var bufferTitle = ((InNode)inNode).BufferTitle;
            if (bufferTitle.Equals("Empty"))
            {
                ImGui.PushStyleColor(ImGuiCol.Text,new Vector4(1.0f,0.0f,0.0f,1.0f));
            }
            else
            {
                ImGui.PushStyleColor(ImGuiCol.Text,new Vector4(0.0f,1.0f,0.0f,1.0f));
            }
            ImGui.Text(bufferTitle);
            ImGui.PopStyleColor();
        }
        _objectHelper.DrawMenu();
    }

    public void Draw()
    {
        _helper.Use();
        SetUniforms();
        GL.BindVertexArray(_vao); 
        GL.DrawArrays(PrimitiveType.Triangles,0,_objectHelper.GetVericesLenght());
    }

    public void OnRender(bool compileSuccess)
    {
        SetShaders();
        ImGui.Begin("Scene Inspector");
        if (SceneIsReady&&compileSuccess)
        {
            if (!DrawScene)
            {
                if (ImGui.Button("Play Scene"))
                {
                    InitScene = true;
                    DrawScene = true;
                }
            }
            else
            {
                if (ImGui.Button("Stop Scene"))
                {
                    InitScene = false;
                    DrawScene = false;
                }
            }
        }

        ImGui.Separator();
        ImGui.Text("Object Settings");
        ImGui.Text("");
        _objectHelper.Draw();
        ImGui.Separator();
        ImGui.Text("In nodes settings");
        ImGui.Text("");
        DrawIns();
        ImGui.Separator();
        ImGui.Text("Uniform nodes settings");
        ImGui.Text("");
        DrawUniforms();
        ImGui.End();
    }
}