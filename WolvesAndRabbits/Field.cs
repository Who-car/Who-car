using Treaty;

namespace WolvesAndRabbits;

using System.Threading.Tasks;
public class Field : IMapManager
{
    private ReaderWriterLockSlim _lockSlim;
    private int[,] _field;
    private bool _isEmpty;
    private int N;
    private int M;
    
    public Field(int n, int m)
    {
        _lockSlim = new ReaderWriterLockSlim();
        _field = new int[n, m];
        _isEmpty = true;
        N = n;
        M = m;
    }

    public void Clear()
    {
        for (var i = 0; i < N; i++)
        for (var j = 0; j < M; j++)
            _field[i, j] = 0;
        _isEmpty = true;
    }

    public void UpdateMap()
    {
        if (_isEmpty)
            GenerateMap();
        var tasks = new Task[M];
        for (var i = 0; i < M; i++)
        {
            var i1 = i;
            tasks[i1] = Task.Run(() => GoThroughColumn(i1, _field));
        }
        Task.WaitAll(tasks);
    }
    
    private void GoThroughColumn(int col, int[,] field)
    {
        for (var i = 0; i < N; i++)
        {
            var neighbours = CheckAliveNeighboursCount(i, col, field);
            switch (field[i, col])
            {
                case 2:
                    field[i, col] = neighbours < 2 ? 0
                        : neighbours < 4 ? 2
                        : 0;
                    if (neighbours is >= 2 and < 4)
                        TryEatRabbit(i, col, field);
                    break;
                case 1:
                    field[i, col] = neighbours < 2 ? 0
                        : neighbours < 5 ? 1
                        : 0;
                    break;
                default:
                    field[i, col] = neighbours < 2 ? 0
                        : neighbours < 4 ? new Random().Next(1, 3)
                        : 0;
                    break;
            }
        }
    }
    
    private int CheckAliveNeighboursCount(int x, int y, int[,] field)
    {
        var count = 0;
        for (var i = x-1; i <= x+1; i++)
        for (var j = y - 1; j <= y + 1; j++)
        {
            if (i < 0 || i >= N || j < 0 || j >= M) continue;
            _lockSlim.EnterReadLock();
            try
            {
                count += field[i, j] == 0 ? 0 : 1;
            }
            finally
            {
                _lockSlim.ExitReadLock();
            }
        }
        return count;
    }

    private void TryEatRabbit(int x, int y, int[,] field)
    {
        for (var i = x-1; i <= x+1; i++)
        for (var j = y - 1; j <= y + 1; j++)
            if (i < 0 || i >= N || j < 0 || j >= M || i == x && j == y) continue;
            else if (field[i, j] == 1)
            {
                var locker = new object();
                lock (locker)
                {
                    field[i, j] = 2;
                    field[x, y] = 0;
                    return;
                }
            }
    }

    public int[,] GetMap()
    {
        if (_isEmpty)
            GenerateMap();
        return _field;
    }

    private void GenerateMap()
    {
        var rnd = new Random();
        for (var i = 0; i < N; i++)
        for (var j = 0; j < M; j++)
            _field[i, j] = rnd.Next(3);
        _isEmpty = false;
    }
}