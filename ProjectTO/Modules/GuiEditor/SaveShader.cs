using System.ComponentModel.Design;
using System.Numerics;
using ImGuiNET;
using System.IO;
using ProjectTO.Modules.GuiEditor.Shader;

namespace ProjectTo.Modules.GuiEditor;
using Gtk;

public class SaveShader
{
    private bool _showSaveDialog;
    private bool _showErrorDialog;
    private static SaveShader? _instance;
    private string _directory="";
    private readonly List<string> _prevDirectory;
    private string _sufix = "";
    private List<string> _fileList;
    private List<string> _directoryList;
    private string _fileName ="";
    private bool _saveFileDialog = true;
    private string _fileSource;
    private string outPath = "";
    public bool IsPathReady
    {
        get { return string.IsNullOrEmpty(outPath); }
    }

    private void GetFileList()
    {
        var directoryList = Directory.EnumerateDirectories(_directory);
        foreach (var dir in directoryList)
        {
            try
            {
                Directory.GetFiles(dir);
                _directoryList.Add(Path.GetFileName(dir));
            }
            catch (UnauthorizedAccessException) { }
        }
        _fileList= Directory.GetFiles(_directory,"*"+_sufix).Select(x=>Path.GetFileName(x)).ToList();
        
    }

    public void UpdateDirector()
    {   
        _prevDirectory.Clear();
        _fileList.Clear();
        _directoryList.Clear();
        var list = _directory.Split("\\").ToList();
        for(int i=1;i<=list.Count;i++)
        {
            var path = "";
            for (int j = 0; j < i; j++)
            {
                path += list[j] + "\\";
            }
            _prevDirectory.Add(path);
        }
        GetFileList();
    }

    private SaveShader()
    {
        _directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        _prevDirectory = new List<string>();
        _fileList = new List<string>();
        _directoryList = new List<string>();
        _fileName += _sufix;
        _showErrorDialog = false;
    }

    public static SaveShader Instance
    {
        get { return _instance ??= new SaveShader(); }
    }

    public bool ShowSaveDialog
    {
        get =>_showSaveDialog;
        set => _showSaveDialog = value;
    }

    public void DrawCombo()
    {
        if (ImGui.BeginCombo("Chose Path", _directory))
        {
            foreach (var directory in _prevDirectory)
            {
                bool isSelected = _directory == directory;
                if (ImGui.Selectable(directory, isSelected))
                {
                    _directory = directory;
                }
                if (isSelected)
                {
                    ImGui.SetItemDefaultFocus();
                }
            }
            ImGui.EndCombo();
        }
    }

    private void ShowDirectoryAndFiles()
    {
        ImGui.BeginChild(_directory,new Vector2(500,200),ImGuiChildFlags.AutoResizeX);
        ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(0.0f, 1.0f, 0.0f, 1.0f));
        foreach (var directory in _directoryList)
        {
            if (ImGui.MenuItem(directory))
            {
                _directory +="\\"+ directory;
                    
            }
        }
        ImGui.PopStyleColor();

        foreach (var files in _fileList)
        {
            if (ImGui.MenuItem(files))
            {
                    _fileName =files;
                    
            }
        }
        
        
        UpdateDirector();
        ImGui.EndChild();
    }
    

    public void SaveFile(string fileName, string source, string sufix)
    {
        _fileName = fileName+sufix;
        _sufix = sufix;
        UpdateDirector();
        _showSaveDialog = true;
        _saveFileDialog = true;
        _fileSource = source;
    }
    public void GetFilePath(string sufix)
    {
        UpdateDirector();
        _sufix = sufix;
        _showSaveDialog = true;
        _saveFileDialog = false;

    }

    public string GetPath()
    {
        var path = outPath;
        outPath = "";
        return path;
    }

    public void ImGuiDialog()
    {
        
        if (_showSaveDialog)
        {
            ImGui.OpenPopup("ChoseFile");
        }
        ImGui.SetNextWindowSize(new Vector2(520,305));
        if (ImGui.BeginPopupModal("ChoseFile",ref _showSaveDialog))
        {

            DrawCombo();
            ShowDirectoryAndFiles();
            
            ImGui.InputText("File Name", ref _fileName, 1024);
            
            
            if (_saveFileDialog)
            {
                if (ImGui.Button("Save"))
                {
                    
                    Save();
                    _showSaveDialog = false;
                }
            }
            else
            {
                if (ImGui.Button("ChoseFile"))
                {
                    var filePath = _directory + "\\" + _fileName;
                    Console.WriteLine(filePath);
                    outPath = filePath;
                    _showSaveDialog = false;
                }
            }
            ImGui.SameLine();
            if (ImGui.Button("Cancel"))
            {
                _showSaveDialog = false;
            }
            ImGui.EndPopup();
        }
        ErrorModal();
    }

    private void ErrorModal()
    {
        if(_showErrorDialog)
            ImGui.OpenPopup("File already exist");
        if (ImGui.BeginPopupModal("File already exist", ref _showErrorDialog))
        {
            ImGui.Text("File "+_fileName+"  already exist in this directory do you wont to override this?");
            ImGui.Text("");
            if (ImGui.Button("Yes"))
            {
                var filePath = _directory + "\\" + _fileName;
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(_fileSource);
                }

                _showErrorDialog = false;
            }
            ImGui.SameLine();
            if (ImGui.Button("Cancel"))
            {
                _showErrorDialog = false;
            }

            ImGui.EndPopup();
        }
    }

    private void Save()
    {
        foreach (var file in _fileList)
        {
            if (_fileName == file)
            {
                
                _showErrorDialog = true;
                Console.WriteLine("error");
                return;
            }
        }
        var filePath = _directory + "\\" + _fileName;
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.Write(_fileSource);
        }
    }
}