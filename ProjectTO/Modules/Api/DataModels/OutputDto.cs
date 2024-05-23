namespace ProjectTo.Modules.GraphicApi.DataModels;

public class OutputDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DataTypeDto DataTypeDto { get; set; }
    public Guid outputId  { get; set; }

    public OutputDto(int Id, string Name, DataTypeDto DataTypeDto)
    {
        this.Id = Id;
        this.Name = Name;
        this.DataTypeDto = DataTypeDto;
        outputId = Guid.NewGuid();
    }


} 