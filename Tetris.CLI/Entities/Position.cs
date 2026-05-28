    public class Position
    {
        public Position() { }
        public Position(int x, int y)
        {
            X = x;
            Y = y;
            Used = false;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Used { get; set; }
    }
