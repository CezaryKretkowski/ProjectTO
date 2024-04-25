using ProjectTo.Modules.GraphicApi.DataModels;
using System.Data.SQLite;
using ProjectTo.Modules.GuiEditor.InputOutput;

namespace ProjectTo.Modules.GraphicApi;

public class DataBaseInterface
{
    private const string ConnectionString = "Data Source=Static/NodeEditorDb.db;";
    private static DataBaseInterface? _instance;
    public static DataBaseInterface Instance => _instance ??= new DataBaseInterface();

    public List<NodeDto> Nodes { get; } = new List<NodeDto>();
    public Dictionary<int, DataTypeDto> Dictionary;

    private SQLiteConnection _connection;
    private DataBaseInterface()
    {

        Dictionary = new Dictionary<int, DataTypeDto>();
        using (_connection = new SQLiteConnection(ConnectionString))
        {
            try
            {
                _connection.Open();
   
                UpData();
                _connection.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to load data: " + e.Message + e.StackTrace);
            }
        }
    }

    private List<InputDto> LoadInputs(int id)
    {
        var command = _connection.CreateCommand();
        command.CommandText =  $"Select * from Input where Nodeid = {id}";
        var readr = command.ExecuteReader();
        var list = new List<InputDto>();
        while (readr.Read())
        {
            var input = new InputDto(
                readr.GetInt32(0),
                readr.GetString(1),
                readr.GetString(2),
                Dictionary[readr.GetInt32(3)]);
            
            list.Add(input);

        }

        return list;
    }

    private List<OutputDto> LoadOutputs(int id)
    {
        var command = _connection.CreateCommand();
        command.CommandText = $"Select * from Output where Nodeid= {id}";
        var readr = command.ExecuteReader();
        var list = new List<OutputDto>();
        while (readr.Read())
        {
            var output = new OutputDto(
                readr.GetInt32(0),
                readr.GetString(1),
                Dictionary[readr.GetInt32(2)]);
            
            list.Add(output);

        }

        return list;
    }

    private Types GetType(int id)
    {
        switch (id)
        {
            case 1 : return Types.Function;
            case 2: return Types.In;
            case 3 : return Types.Out;
            default: return Types.Uniform;
        }
    }

    private void LoadNode()
    {
        var command = _connection.CreateCommand();
        command.CommandText = "Select * from Node";
        var readr = command.ExecuteReader();
        while (readr.Read())
        {
            var node = new NodeDto()
            {
                Id = readr.GetInt32(0),
                Name = readr.GetString(1),
                ShaderType = GetType(readr.GetInt32(2)),
                ToStringFormat = readr.GetString(3),
                Inputs = LoadInputs(readr.GetInt32(0)),
                Outputs = LoadOutputs(readr.GetInt32(0))
            };

            Nodes.Add(node);
        }
    }

    private void LoadDataTypes()
    {
        var command = _connection.CreateCommand();
        command.CommandText = "Select * from DataTypes";
        var readr = command.ExecuteReader();
        while (readr.Read())
        {
            var type = new DataTypeDto
            (
                readr.GetInt32(0),
                readr.GetString(1),
                readr.GetString(2),
                readr.GetString(3)
            );
            Dictionary.Add(type.Id,type);
        }

    }

    private void UpData()
    {
        LoadDataTypes();
        LoadNode();
    }



}