﻿class Game
{
    private Random rng;
    private int width;
    private int height;
    private Snake snake;
    public Square head { get; private set; }
    public Square tail { get; private set; }
    public Square apple { get; private set; }

    public Game(Random rng, int width, int height, Direction startingDirection)
    {
        this.rng = rng;
        this.width = width;
        this.height = height;
        var halfW = width / 2;
        var halfH = height / 2;
        var centreW = halfW % 2 == 0 ? halfW : halfW + 1; //Ensure even number
        var centreH = halfH % 2 == 0 ? halfH : halfH + 1; 
        snake = new Snake(centreW, centreH, startingDirection); 
        head = snake.head;
        tail = snake.tail;
        updateApple();
    }

    public bool clockTick(Direction d)
    {
        snake.advanceHeadOneSquare(d);
        head = snake.head;
        if (head.isSameSquareAs(apple))
        {
            updateApple();
        }
        else
        {
            snake.advanceTailOneSquare();
        }
        tail = snake.tail;
        return !HasHitEdge() && !snake.hasHitItself();
    }

    public bool HasHitEdge()
    {
        var x = snake.head.x;
        var y = snake.head.y;
        return x < 0 || y < 0 || x == width || y == height; 
    }

    private void updateApple()
    {
        Square sq;
        do
        {
            sq = new Square(rng.Next(width / 2) * 2, rng.Next(height / 2) * 2);

        } while (snake.bodyCovers(sq));
        apple = sq;
    }

    internal int GetScore() => snake.Length() - 2;
}
