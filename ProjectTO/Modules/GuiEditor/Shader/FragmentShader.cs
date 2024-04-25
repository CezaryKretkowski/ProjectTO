using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTO.Modules.GuiEditor.Shader
{
    public class FragmentShader:Shader
    {
        public FragmentShader()
        {
            
        }

        public override string GetSource()
        {
            var list = _nodes.Values.ToList();
            _compiler.Init(_nodes);
            return _compiler.CompileWrite(ShaderProfile.Fragment);
        }
    }
}
