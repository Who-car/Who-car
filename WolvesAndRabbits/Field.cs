namespace WolvesAndRabbits;

using System.Threading.Tasks;
public class Field
{
    protected int[,] _field; 
    
    public Field(int n, int m)
    {
        _field = new int[n+2, m+2];
        var rnd = new Random();
        for (var i = 1; i < n+1; i++)
        for (var j = 1; j < m + 1; j++)
            _field[i, j] = rnd.Next(3);
    }

    public void ChangeFieldState()
    {
        var field = _field;
        var tasks = new Task[field.GetLength(1)];
        for (var i = 1; i < field.GetLength(1)-1; i++)
        {
            var i1 = i;
            tasks[i1] = Task.Run(() => GoThroughColumn(i1, field));
            tasks[i1].Wait();
        }
    }
    
    private static void GoThroughColumn(int col, int[,] field)
    {
        for (var i = 1; i < field.GetLength(0)-1; i++)
        {
            var neighbours = CheckAliveNeighboursCount(i, col, field);
            if (field[i, col] == 0)
            {
                field[i, col] = neighbours == 3 ? 1 : 0;
            }
            else if (field[i, col] == 1)
            {
                field[i, col] = neighbours < 2 ? 0
                    : neighbours < 5 ? 1
                    : 0;
            }
            else
            {
                field[i, col] = neighbours < 2 ? 0
                    : neighbours < 5 ? 1
                    : 0;
                TryEatRabbit(i, col, field);
            }
        }
    }
    
    private static int CheckAliveNeighboursCount(int x, int y, int[,] field)
    {
        var count = 0;
        for (var i = x-1; i <= x+1; i++)
        for (var j = y - 1; j <= y + 1; j++)
            count += i == x && j == y ? 0
                : field[i, j] == 0 ? 0
                : 1;
        return count;
    }

    private static void TryEatRabbit(int x, int y, int[,] field)
    {
        for (var i = x-1; i <= x+1; i++)
        for (var j = y - 1; j <= y + 1; j++)
            if (field[i, j] == 1)
            {
                field[i, j] = 0;
                return;
            }
    }

    public void PrintField()
    {
        var field = _field;
        for (int i = 1; i < field.GetLength(0)-1; i++)
        {
            var str = String.Empty;
            for (int j = 1; j < field.GetLength(1)-1; j++)
            {
                str += field[i, j].ToString() + " ";
            }
            Console.WriteLine(str);
        }
    }
}