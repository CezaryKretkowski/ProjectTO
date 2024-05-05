using OpenTK.Graphics.OpenGL4;
using ProjectTo.Modules.GuiEditor;

namespace ProjectTo.Modules.Scene;

public class LocationHelper
{
    private IEnumerable<float> _bufferList;
    private int _vbo;
    private int _size;

    public LocationHelper()
    {
        _bufferList = new List<float>();
    }

    public void Init(IEnumerable<float>list, int size,int locationId,BufferUsageHint hint)
    {
        _bufferList = list;
        _vbo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, _bufferList.ToArray().Length *sizeof(float), _bufferList.ToArray(), hint);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, size * sizeof(float), 0);
        GL.EnableVertexAttribArray(locationId);
    }

}