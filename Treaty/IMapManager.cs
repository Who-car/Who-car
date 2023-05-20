namespace Treaty;

public interface IMapManager
{
    public int[,] GetMap();
    public void UpdateMap();
    public void Clear();
}