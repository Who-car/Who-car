namespace Treaty;

public interface IMapManager
{
    public int[,] GetMap();
    public delegate void UpdateHandler();
    public UpdateHandler SourceUpdated { get; set; }
    public Task UpdateMap();
    public void Clear();
}