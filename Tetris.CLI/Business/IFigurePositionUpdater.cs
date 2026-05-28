public interface IFigurePositionUpdater
{
    Position[] Falling(Position[] currentPosition, Position[] usedPositions, int Ylimit);
    Position[] MoveLeft(Position[] currentPosition, Position[] usedPositions, int Ylimit, int leftLimit);
    Position[] MoveRight(Position[] currentPosition, Position[] usedPositions, int Ylimit, int rightLimit);
    Position[] MoveDown(Position[] currentPosition, Position[] usedPositions, int Ylimit, int downLimit);
    Position[] Rotate(IFigure figure);
}