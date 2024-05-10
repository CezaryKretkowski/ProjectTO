namespace ProjectTo.Modules.Scene;

public class StaticDataMesh
{
    public static float[] Vertices = 
    {
        // -0.5f, -0.5f, 0.0f,
        // 0.5f, -0.5f, 0.0f,
        // 0.0f,  0.5f, 0.0f 
        0.5f,  0.5f, 0.0f,
        0.5f, -0.5f, 0.0f,
        -0.5f,  0.5f, 0.0f,
        0.5f, -0.5f, 0.0f,
        -0.5f, -0.5f, 0.0f,
        -0.5f,  0.5f, 0.0f
    };
    public static float[] Uvs = {
        // 0.0f, 0.0f,  // lower-left corner  
        // 1.0f, 0.0f,  // lower-right corner
        // 0.5f, 1.0f  
        1.0f, 1.0f,
        1.0f, 0.0f,
        0.0f, 1.0f,
        1.0f, 0.0f,
        0.0f, 0.0f,
        0.0f, 1.0f

    };
    public static float[] Normals = 
    {
        0.0f, 1.0f, 0.0f, 
        0.0f, 1.0f, 0.0f, 
        0.0f, 1.0f, 0.0f 
    };
}