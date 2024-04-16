using ProjectTo.Modules.GraphicApi;
using ProjectTo.Modules.GraphicApi.DataModels;
using ProjectTo.Modules.GuiEditor;
using ProjectTo.Modules.GuiEditor.InputOutput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectTO.Modules.GuiEditor.Shader
{
    public abstract class Shader
    {
        protected readonly Dictionary<Guid, Node> _nodes = new Dictionary<Guid, Node>();

        public void AndNode(NodeDto nodeDto ,Vector2 pos)
        {
            var node = NodeFactory.Instance.CreateNode(nodeDto, pos, this);
            _nodes.Add(node.Id, node);
        }
      
        public void TryAttach(IForm output)
        {
            foreach (var input in _nodes.Values.Where(node => !node.Id.Equals(output.GetParentId())).SelectMany(node => node.Inputs))
            {
                input.AttachOutput(output);
            }
        }

        public void Remove(Guid id)
        {
            _nodes.Remove(id);
        }

        public void DrawNodes() {
            foreach (var node in _nodes.Values) { 
                node.DrawNode();
            }
        }
    }
}
