    public interface IFigurePositionUpdater
    {
        Position[] Falling(Position[] currentPosition, int Ylimit);

        Position[] MoveLeft(Position[] currentPosition, int Ylimit, int leftLimit);
        Position[] MoveRight(Position[] currentPosition, int Ylimit, int rightLimit);
    }
