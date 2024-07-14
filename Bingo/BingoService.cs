namespace Bingo;

public static class BingoService
{
    public static Dictionary<int, BingoBoard> Boards { get; } = new()
    {
        {
            1, new BingoBoard(new List<string> {"1", "2", ""})
        }
    };

    public static int AddBoard(BingoBoard board)
    {
        var random = new Random();
        var key = random.Next(1, 100000);
        while (Boards.ContainsKey(key))
            key = random.Next(1, 100000);
        Boards.Add(key, board);
        return key;
    }

    public static BingoBoard? MarkField(int board, int field)
    {
        if (Boards.TryGetValue(board, out var boardDto))
        {
            boardDto.MarkField(field);
            return boardDto;
        }

        return null;
    }
}

public class BingoBoard
{
    public int Size => (int)Math.Ceiling(Math.Sqrt(Fields.Count));
    public List<BingoField> Fields { get; set; }

    public List<List<BingoField>> FieldsInRows => Enumerable.Range(0, Size).Select(x => Fields
        .Skip(x * Size)
        .Take(Size)
        .ToList()
    ).ToList();
    
    public BingoBoard(List<string> values)
    {
        Fields = values.Select((x, i) => new BingoField(i, x)).ToList();
    }
    
    public void MarkField(int field)
    {
        Fields[field].IsMarked = true;
    }
}

public class BingoField
{
    public int Id { get; set; }
    public string Value { get; set; }
    public bool IsMarked { get; set; }

    public BingoField()
    {
    }

    public BingoField(int id, string value)
    {
        Id = id;
        Value = value;
        IsMarked = String.IsNullOrEmpty(value);
    }
}