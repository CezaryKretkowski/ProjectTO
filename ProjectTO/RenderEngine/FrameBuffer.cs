using OpenTK.Graphics.OpenGL4;

namespace ProjectTo.RenderEngine;

public class FrameBuffer
{
    private readonly int _fbo;
    private readonly int _rbo;
    private readonly int _tex;
    public int Texture => _tex;

    public FrameBuffer(int width,int height)
    {
        _fbo = GL.GenFramebuffer();    
        GL.BindFramebuffer(FramebufferTarget.Framebuffer,_fbo);

        _tex = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D,Texture);
        GL.TexImage2D(TextureTarget.Texture2D,0,PixelInternalFormat.Rgb,width,height,0,PixelFormat.Rgb,PixelType.Byte,IntPtr.Zero);
        GL.TexParameter(TextureTarget.Texture2D,TextureParameterName.TextureMinFilter,(int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D,TextureParameterName.TextureMagFilter,(int)TextureMinFilter.Linear);
        GL.FramebufferTexture2D(FramebufferTarget.Framebuffer,FramebufferAttachment.ColorAttachment0,TextureTarget.Texture2D,Texture,0);

        _rbo = GL.GenRenderbuffer();
        GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer,_rbo);
        GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer,RenderbufferStorage.DepthComponent32f,width,height);
        GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer,FramebufferAttachment.DepthAttachment,RenderbufferTarget.Renderbuffer,_rbo);

        if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
            Console.WriteLine("Error");
        GL.BindFramebuffer(FramebufferTarget.Framebuffer,0);
        GL.BindTexture(TextureTarget.Texture2D,0);
        GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer,0);
    }

    public void OnUnload()
    {
        GL.DeleteFramebuffer(_fbo);
        GL.DeleteRenderbuffer(_rbo);
        GL.DeleteTexture(_tex);
    }

    public void OnResize(int width,int height)
    {
        GL.BindTexture(TextureTarget.Texture2D,_tex);
        GL.TexImage2D(TextureTarget.Texture2D,0,PixelInternalFormat.Rgb,width,height,0,PixelFormat.Rgb,PixelType.UnsignedByte,0);
        GL.TexParameter(TextureTarget.Texture2D,TextureParameterName.TextureMinFilter,(int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D,TextureParameterName.TextureMagFilter,(int)TextureMinFilter.Linear);
        GL.FramebufferTexture2D(FramebufferTarget.Framebuffer,FramebufferAttachment.ColorAttachment0,TextureTarget.Texture2D,_tex,0);
        
        GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer,_rbo);
        GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer,RenderbufferStorage.DepthComponent32f,width,height);
        GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer,FramebufferAttachment.DepthAttachment,RenderbufferTarget.Renderbuffer,_rbo);
    }

    public void Bind()
    {
        GL.BindFramebuffer(FramebufferTarget.Framebuffer,_fbo);

    }

    public void Unbind()
    {
        GL.BindFramebuffer(FramebufferTarget.Framebuffer,0);
    }
}