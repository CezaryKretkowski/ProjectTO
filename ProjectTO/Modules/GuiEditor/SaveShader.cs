using System.ComponentModel.Design;
using ProjectTO.Modules.GuiEditor.Shader;

namespace ProjectTo.Modules.GuiEditor;
using Gtk;

public class SaveShader
{
    public static void SaveDialog(string name,string source)
    {
        Application.Init();
        FileChooserDialog fileChooser = new FileChooserDialog(
            "Save fill",
            null,
            FileChooserAction.Save,
            "Cancel", ResponseType.Cancel,
            "Save", ResponseType.Accept);

        fileChooser.CurrentName = "my"+name+".glsl";
        


        ResponseType response = (ResponseType)fileChooser.Run();


        if (response == ResponseType.Accept)
        {

            string filePath = fileChooser.Filename;
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(source);
            }

        }


        fileChooser.Destroy();
        //Application.Run();
        
    }

}