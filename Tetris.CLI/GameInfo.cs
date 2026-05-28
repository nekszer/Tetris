public class GameInfo
{
    public int VerticalSize { get; }
    public int HorizontalSize { get; }
    public long Points { get; set; }
    public long Lines { get; set; }

    public GameInfo(int verticalSize, int horizontalSize)
    {
        VerticalSize = verticalSize;
        HorizontalSize = horizontalSize;
    }
        
}