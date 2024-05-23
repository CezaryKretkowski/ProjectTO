using ProjectTo.Modules.Scene;

namespace ProjectTo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Window.Window.Instance(1600, 900, "Shader editor").Run();
            //var mesh = MeshLoader.LoadObj("C:\\Users\\cezar\\Documents\\cube.obj",false);
        }
    }
}

