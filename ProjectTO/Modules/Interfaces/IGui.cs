using OpenTK.Windowing.Common;

namespace ProjectTo.Gui.Interfaces;

public interface IGui
{
    void OnRender(FrameEventArgs e);
}