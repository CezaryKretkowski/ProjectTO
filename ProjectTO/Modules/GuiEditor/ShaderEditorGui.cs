using System.Numerics;
using ImGuiNET;
using ProjectTo.Gui.Interfaces;
using ProjectTo.Modules.GraphicApi;
using ProjectTo.Modules.GraphicApi.DataModels;
using ProjectTo.Modules.GuiEditor.InputOutput;
using ProjectTO.Modules.GuiEditor.Shader;

namespace ProjectTo.Modules.GuiEditor;

public class ShaderEditorGui : IGui
{
    private Vector2 _nodePose = new Vector2(0,0);
    private readonly Dictionary<Guid, Node> _nodes = new Dictionary<Guid, Node>();
    public static ShaderEditorGui _instance;
    private bool _showCompileResult = false;
    public CompileResult CompileResult;
    public static ShaderEditorGui Instance
    {
        get { return _instance ??= new ShaderEditorGui(); }
    }
    private string[] Items { get; set; }
    private int _displayIndex = 0;
    private int _menuShowIndeks = 0;
    public readonly VertexShader vertexShader;
    public readonly FragmentShader fragmentShader;

    private ShaderEditorGui()
    {
        vertexShader = new VertexShader();  
        fragmentShader = new FragmentShader();
        NodeDto node = new NodeDto() { 
        Name = "type",
        ShaderType = Types.In
        };
        NodeDto node1 = new NodeDto()
        {
            Name = "type1",
            ShaderType = Types.Function
        };
        CompileResult = new CompileResult(false, "Not compiled", "", "");
        Items = new[] { "Vertex Shader", "Fragment Shader" };
    }


    private void DisplayGrid(int gridSize)
    {
        var size =Window.Window.Instance().Size;
        var gap = size.X / gridSize;
        var pos = ImGui.GetWindowPos();
        uint color = ImGui.GetColorU32(new Vector4(0.58f,0.58f,0.58f,1));
        float x = pos.X;
        float y = pos.Y;
        for (var i = 0; i < gridSize; i++)
        {
            
            ImGui.GetWindowDrawList().AddLine(new Vector2(x+gap,0),new Vector2(x+gap,size.Y),color);
            ImGui.GetWindowDrawList().AddLine(new Vector2(0,y+gap),new Vector2(size.X,y+gap),color);
            x += gap;
            y += gap;
        }
        
    }


    private void OnWindowContextMenu()
    {
        if (ImGui.BeginPopupContextWindow())
        {
            
            if (ImGui.BeginMenu("New function node"))
            {
                foreach (var nodes in DataBaseInterface.Instance.Nodes)
                {
                    if (ImGui.MenuItem(nodes.Name))
                    {
                        if (_displayIndex == 0)
                        {
                            vertexShader.AndNode(nodes,new Vector2(50,50));
                        }
                        else
                        {
                            fragmentShader.AndNode(nodes,new Vector2(50,50));
                        }
                    }
                }
                ImGui.EndMenu();
            }
            
            if (ImGui.BeginMenu("New in node"))
            {
                foreach (var data in DataBaseInterface.Instance.Dictionary.Values)
                {
                    if (ImGui.MenuItem(data.GlslType))
                    {
                        var nodeDto = new NodeDto()
                        {
                            ShaderType = Types.In,
                            Name = "New_In "+data.GlslType,
                            Outputs = new List<OutputDto>()
                             {
                                 new OutputDto(1,data.GlslType,data)
                             }
                        };
                        if (_displayIndex == 0)
                        {
                            vertexShader.AndNode(nodeDto,new Vector2(50,50));
                        }
                        else
                        {
                            fragmentShader.AndNode(nodeDto,new Vector2(50,50));
                        }

                    }
                }
                ImGui.EndMenu();
            }
        
            if (ImGui.BeginMenu("New out node"))
            {
                foreach (var data in DataBaseInterface.Instance.Dictionary.Values)
                {
                    if (ImGui.MenuItem(data.GlslType))
                    {
                        var nodeDto = new NodeDto()
                        {
                            ShaderType = Types.Out,
                            Name = "New_out",
                            Inputs = new List<InputDto>()
                            {
                                new InputDto(1,data.GlslType,"",data)
                            }
                        };
                        if (_displayIndex == 0)
                        {
                            vertexShader.AndNode(nodeDto,new Vector2(50,50));
                        }
                        else
                        {
                            fragmentShader.AndNode(nodeDto,new Vector2(50,50));
                        }
                    }
                }
                ImGui.EndMenu();
            }
          
            if (ImGui.BeginMenu("New uniform node"))
            {
                foreach (var data in DataBaseInterface.Instance.Dictionary.Values)
                {
                    if (ImGui.MenuItem(data.GlslType))
                    {
                        var nodeDto = new NodeDto()
                        {
                            ShaderType = Types.Uniform,
                            Name = "New_uniform " + data.GlslType,
                            Outputs = new List<OutputDto>()
                            {
                                new OutputDto(1,data.GlslType,data)
                            }
                        };
                        if (_displayIndex == 0)
                        {
                            vertexShader.AndNode(nodeDto,new Vector2(50,50));
                        }
                        else
                        {
                            fragmentShader.AndNode(nodeDto,new Vector2(50,50));
                        }
                    }
                }
                ImGui.EndMenu();
            }
            
            
        }

    }

    public void DrawCompileResult()
    {
        if (ImGui.BeginPopupModal("Compile result",ref _showCompileResult))
        {
            ImGui.Text(CompileResult.Message);
            if (ImGui.Button("close"))
            {
                ImGui.CloseCurrentPopup();
                _showCompileResult = false;
            }
            ImGui.EndPopup();
        }
    }

    public void OnRender()
    {
        
        
        ImGui.Begin("Inspector");

            for(var i = 0; i<Items.Length;i++)
            {
                ImGui.Selectable(Items[i],(i==_displayIndex));
                if (ImGui.IsItemClicked())
                {
                    _displayIndex = i;
                }
            }

            ImGui.BeginChild("Compiler");
                ImGui.Text("");
                ImGui.Text("Compiler");
 
                if (ImGui.Button("Compile shader"))
                {
                        var vertSource =vertexShader.GetSource();
                        var fragSource=fragmentShader.GetSource();
                        CompileResult = Compiler.TryCreatProgram(vertSource,fragSource);
                        _showCompileResult = true;
                        ImGui.OpenPopup("Compile result");
                }

                DrawCompileResult();

            ImGui.EndChild();
        ImGui.End();  
      
 
        ImGui.Begin("Node");
        
            ImGui.PushStyleColor(ImGuiCol.ChildBg,new Vector4(0.24f, 0.24f, 0.27f, 0.68f));
            ImGui.BeginChild("menu");
                DisplayGrid(32);
                if(_displayIndex==0) 
                    vertexShader.DrawNodes();
                else
                    fragmentShader.DrawNodes();                
                
                OnWindowContextMenu();
             
     
            ImGui.EndChild();
            ImGui.PopStyleColor();
        
        ImGui.End();
    }
}