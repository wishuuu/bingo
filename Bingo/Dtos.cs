namespace Bingo;

public class CreateBoardDto
{
    public string Name { get; set; }
    public List<string> values { get; set; } = new();
}