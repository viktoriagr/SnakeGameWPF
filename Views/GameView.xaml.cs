using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfSnakeGameVGr.Models;
using WpfSnakeGameVGr.Views;

namespace WpfSnakeGameVGr
{
    public partial class GameView : UserControl
    {
        // This list describes the food obj pieces on the Canvas
        private List<Point> foodObjsPoints = new List<Point>();

        // This list describes the body of the snake on the Canvas
        private List<Point> snakeBodyPoints = new List<Point>();

        private Brush snakeBodyColor = Brushes.Beige;

        private Point startPoint = new Point(100, 100);
        private Point currPosition = new Point();

        private int direction = 0;

        private int previousDirection = 0;

        private int headSize = (int)Models.Size.THICK;

        private int length = 100;
        private int score = 0;
        private Random rnd = new Random();

        private MainWindow parentWindow;
        private DispatcherTimer timer;

        public GameView(TimeSpan snakeSpeed)
        {
            InitializeComponent();

            this.timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);

            /* Here user can change the speed of the snake.
             * Possible speeds are FAST, MODERATE, SLOW */
            timer.Interval = snakeSpeed;
            timer.Start();

            this.parentWindow = Application.Current.MainWindow as MainWindow;
            if (parentWindow != null)
            {
                parentWindow.KeyDown += new KeyEventHandler(OnButtonKeyDown);
            }
            
            drawSnakeBody(startPoint);
            currPosition = startPoint;

            // Instantiate Food Objects
            for (int n = 0; n < 10; n++)
            {
                drawFoodObjs(n);
            }
        }


        private void drawSnakeBody(Point currPosition)
        {
            Rectangle snakeBodyPart = new Rectangle();
            snakeBodyPart.Fill = snakeBodyColor;
            snakeBodyPart.Width = headSize;
            snakeBodyPart.Height = headSize;

            Canvas.SetTop(snakeBodyPart, currPosition.Y);
            Canvas.SetLeft(snakeBodyPart, currPosition.X);

            int count = drawingCanvas.Children.Count;
            drawingCanvas.Children.Add(snakeBodyPart);
            snakeBodyPoints.Add(currPosition);

            // Restrict the tail of the snake
            if (count > length)
            {
                drawingCanvas.Children.RemoveAt(count - length + 9);
                snakeBodyPoints.RemoveAt(count - length);
            }
        }

        private void drawFoodObjs(int index)
        {
            Point bonusPoint = new Point(rnd.Next(5, 620), rnd.Next(5, 380));

            Rectangle foodObj = new Rectangle();
            foodObj.Fill = Brushes.RoyalBlue;
            foodObj.Width = headSize;
            foodObj.Height = headSize;

            Canvas.SetTop(foodObj, bonusPoint.Y);
            Canvas.SetLeft(foodObj, bonusPoint.X);
            drawingCanvas.Children.Insert(index, foodObj);
            foodObjsPoints.Insert(index, bonusPoint);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Expand the body of the snake to the direction of movement
            switch (direction)
            {
                case (int)MovingDirection.DOWN:
                    currPosition.Y += 1;
                    drawSnakeBody(currPosition);
                    break;
                case (int)MovingDirection.UP:
                    currPosition.Y -= 1;
                    drawSnakeBody(currPosition);
                    break;
                case (int)MovingDirection.LEFT:
                    currPosition.X -= 1;
                    drawSnakeBody(currPosition);
                    break;
                case (int)MovingDirection.RIGHT:
                    currPosition.X += 1;
                    drawSnakeBody(currPosition);
                    break;
            }

            // Restrict to boundaries of the Canvas
            if ((currPosition.X < 5) || (currPosition.X > 620) ||
                (currPosition.Y < 5) || (currPosition.Y > 380))
                GameOver();

            // Eating a food object point causes the snake body to increase its length
            int n = 0;
            foreach (Point point in foodObjsPoints)
            {
                if ((Math.Abs(point.X - currPosition.X) < headSize) &&
                    (Math.Abs(point.Y - currPosition.Y) < headSize))
                {
                    length += 10;
                    score += 10;

                    // In case of food object consumption, erase the food object from the list of food objs and the canvas
                    foodObjsPoints.RemoveAt(n);
                    drawingCanvas.Children.RemoveAt(n);
                    drawFoodObjs(n);
                    break;
                }
                n++;
            }

            // Restrict hits to body of Snake
            for (int q = 0; q < (snakeBodyPoints.Count - headSize * 2); q++)
            {
                Point point = new Point(snakeBodyPoints[q].X, snakeBodyPoints[q].Y);
                if ((Math.Abs(point.X - currPosition.X) < (headSize)) &&
                     (Math.Abs(point.Y - currPosition.Y) < (headSize)))
                {
                    GameOver();
                    break;
                }
            }
        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    if (previousDirection != (int)MovingDirection.UP)
                        direction = (int)MovingDirection.DOWN;
                    break;
                case Key.Up:
                    if (previousDirection != (int)MovingDirection.DOWN)
                        direction = (int)MovingDirection.UP;
                    break;
                case Key.Left:
                    if (previousDirection != (int)MovingDirection.RIGHT)
                        direction = (int)MovingDirection.LEFT;
                    break;
                case Key.Right:
                    if (previousDirection != (int)MovingDirection.LEFT)
                        direction = (int)MovingDirection.RIGHT;
                    break;
            }
            previousDirection = direction;

        }

        private void GameOver()
        {
            // Detach from event handlers in order to prevent memory leak
            parentWindow.KeyDown -= new KeyEventHandler(OnButtonKeyDown);
            timer.Tick -= new EventHandler(timer_Tick);

            var result = MessageBox.Show("Game over! Your score is " + score.ToString() + ". Would you like to start a new game?",
                "Game Over", MessageBoxButton.YesNo, MessageBoxImage.Stop);
            if (result == MessageBoxResult.Yes)
            {
                this.parentWindow.viewPresenter.Content = new SnakeSpeedView();
            }
            else
            {
                this.parentWindow.Close();
            }
        }
    }
}
