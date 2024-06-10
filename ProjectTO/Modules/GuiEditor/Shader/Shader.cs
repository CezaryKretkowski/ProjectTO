using ProjectTo.Modules.GraphicApi;
using ProjectTo.Modules.GraphicApi.DataModels;
using ProjectTo.Modules.GuiEditor;
using ProjectTo.Modules.GuiEditor.InputOutput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectTO.Modules.GuiEditor.Shader
{
    public abstract class Shader
    {
        
        protected Compiler _compiler = new Compiler();
        
        protected  Dictionary<Guid, Node> _nodes = new Dictionary<Guid, Node>();

        public List<Node> GetListNode()
        {
            return _nodes.Values.ToList();
        }

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

        public virtual string GetSource()
        {
            return "";
        }

        public string Serialize()
        {
            var listEntities = _nodes.Select(x => x.Value.Entity);
            string var = JsonSerializer.Serialize(listEntities);
            return var;
        }
        public void Deserialize(string val)
        {
            var nodes=JsonSerializer.Deserialize<List<NodeDto>>(val);
            var factory = NodeFactory.Instance;
            foreach (var variaNode in nodes)
            {

                var node = factory.CreateNode(variaNode, new Vector2(variaNode.X, variaNode.Y), this);
                node.Title = variaNode.Name;
                Console.WriteLine(variaNode.Name);
                this._nodes.Add(variaNode.guid,node);
            }

            var inputs = _nodes.Values;
            var outputs = _nodes.Values.Select(x =>x.Output).Where(x => x != null).ToList();
            var outputs1 = outputs.Select(x => (Output)x!).ToList();
            foreach (var node in inputs)
            {
                var notnullInputs = node.Inputs.Where(x => x.GetInputDto()!.outputID != Guid.Empty);
                foreach (var notnull in notnullInputs)
                {
                    var input = (Input)notnull;
                    var output = outputs1.SingleOrDefault(x => x.GetOutDto().outputId == input.GetInputDto()!.outputID);
                    if(output!=null)
                        input.AsignOutput(output);
                }                
            }
            


        }
    }
}
