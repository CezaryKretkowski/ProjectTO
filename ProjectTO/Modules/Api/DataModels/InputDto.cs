namespace ProjectTo.Modules.GraphicApi.DataModels;

public class InputDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Dv { get; set; }
    public DataTypeDto DataTypeDto{ get; set; }
    public Guid outputID { get; set; }

    public InputDto(int Id, string Name, string Dv, DataTypeDto DataTypeDto)
    {
        this.Id = Id;
        this.Name = Name;
        this.Dv = Dv;
        this.DataTypeDto = DataTypeDto;
    }
}