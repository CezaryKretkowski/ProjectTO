using ProjectTo.Modules.GraphicApi.DataModels;
using System.Data.SQLite;

namespace ProjectTo.Modules.GraphicApi;

public class DataBaseInterface
{
    private const string ConnectionString = "Data Source=Static/NodeEditorDb.db;";
    private static DataBaseInterface? _instance;
    public static DataBaseInterface Instance => _instance ??= new DataBaseInterface();

    public List<NodeDto> Nodes { get; } = new List<NodeDto>();
    public List<InputDto> Inputs { get; } = new List<InputDto>();
    public List<OutputDto> Outputs { get; } = new List<OutputDto>();

    private DataBaseInterface()
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            try
            {
                connection.Open();
                UpData();
                connection.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to load data: " + e.Message);
            }
        }
    }

    private void UpData()
    {
    }



}