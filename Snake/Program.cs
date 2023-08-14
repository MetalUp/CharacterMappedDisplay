using static EntryPoints;

class Program
{
    const Colour bodyColour = Colour.Green;
    const Colour appleColour = Colour.Red;

    public static void Main()
    {
        bool newGame = true;
        Display.printLine(welcome);
        Keyboard.readKey();
        while (newGame)
        {
            Display.setCursor(0, 0);
            Display.paintBackground();
            var currentDirection = Direction.Right;
            var game = new Game(new Random(), Display.width, Display.height, currentDirection);
            bool gameOn = true;

            while (gameOn)
            {
                Thread.Sleep(200); //Slow down game by pausing for 150 milliseconds. Can make this adjustable - for different levels of skill

                var head = game.head;
                Display.putBlock(head.x, head.y, bodyColour);
                Display.putBlock(head.x + 1, head.y, bodyColour);

                var apple = game.apple;
                Display.putBlock(apple.x, apple.y, appleColour);
                Display.putBlock(apple.x + 1, apple.y, appleColour);

                var priorTail = game.tail;

                if (Keyboard.keyHasBeenPressed())
                {
                    var k = Keyboard.readKeyWithoutPrinting();
                    currentDirection = (k == 'w') ? Direction.Up :
                                       (k == 's') ? Direction.Down :
                                       (k == 'a') ? Direction.Left :
                                       (k == 'd') ? Direction.Right :
                                       currentDirection; // if no key pressed keep same direction
                }

                gameOn = game.clockTick(currentDirection);
                if (!game.tail.isSameSquareAs(priorTail))
                {
                    Display.clear(priorTail.x, priorTail.y);
                    Display.clear(priorTail.x + 1, priorTail.y); //Need to clear two blocks cover one square
                }
            }
            Keyboard.clearBuffer();
            Display.print($"Game Over! Score: {game.GetScore()}. Do you want to play again (y/n)?");
            char ans;
            do
            {
                ans = Keyboard.readKeyWithoutPrinting();
            } while (ans != 'y' && ans != 'n' );          
            if (ans != 'y') newGame = false;
        }
    }

    const string welcome = $@"Welcome to the Snake game. 

Use the w,a,s, and d keys to control the direction of the snake. Letting the snake get to any edge will lose you the game, as will letting the snake's head pass over its body. Eating an apple will
cause the snake to grow by 

If you want to re-size the window, please do so now, before starting the game.

Click on this window to get 'focus' (and see a flashing cursor). Then press any key to start..";
}

