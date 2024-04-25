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
    public bool InitScene { get; set; } = false;
    public bool DrawScene { get; set; } = false;

    private float[] _vertices = 
    {
        -0.5f, -0.5f, 0.0f, 
        0.5f, -0.5f, 0.0f, 
        0.0f,  0.5f, 0.0f 
    };
    private int _vao;
    private int _vbo;

    public void SetShaders()
    {
        var list1 = ShaderEditorGui.Instance.fragmentShader.GetListNode();
        var list2 = ShaderEditorGui.Instance.vertexShader.GetListNode();
        var list = list1.Concat(list2);
        _uniformNodes = list.Where(x=>x.Entity.ShaderType == Types.Uniform || x.Entity.ShaderType == Types.In).ToList();
    }

    public void Init(CompileResult result)
    {
        Console.WriteLine("Init");
        _vao = GL.GenVertexArray();
        GL.BindVertexArray(_vao); 
        
        
        _vbo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.ToArray().Length *sizeof(float), _vertices.ToArray(), BufferUsageHint.StaticDraw);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        _helper = new ShaderHelper(result.VertSource, result.FragSource);
        _helper.Use();        
    }

    private void SetUniforms()
    {
        var uniformList = _uniformNodes.Where(x => x.Entity.ShaderType == Types.Uniform);
        foreach (var uniform in uniformList)
        {
            ((UniformNode)uniform).Output.SetUnforms(_helper);
        }
    }

    private void DrawUniforms()
    {
        var uniformList = _uniformNodes.Where(x => x.Entity.ShaderType == Types.Uniform);
        foreach (var uniform in uniformList)
        {
            ((UniformNode)uniform).Output.DrawOutpute();
        }
    }

    public void Draw()
    {
        Console.WriteLine("draw Scene");
        _helper.Use();
        SetUniforms();
        GL.BindVertexArray(_vao); 
        GL.DrawArrays(PrimitiveType.Triangles,0,3);
    }

    public void OnRender()
    {
        SetShaders();
        ImGui.Begin("Scene Inspector");
        if (ImGui.Button("Play Scene"))
        {
            InitScene = true;
            DrawScene = true;
        }

        DrawUniforms();
        ImGui.End();
    }
}