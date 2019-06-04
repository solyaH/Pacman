using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using PacManGame.Logic;
using PacManGame.Maze;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace PacManGame.GameVisualization
{
    public class PacmanMovement
    {
        public DispatcherTimer moveTimer;// відповідає за рух пакмена
        private DispatcherTimer chewingTimer;//відповідає за 'клацання'
        public GameScore gs;//екземпляр класу логіки рахунку гри
        public Pacman pacman;//екземпляр класу логіки руху пакмена
        private int thickness;
        private int imageSize;
        private Coins coins;//поле монеток
        private BitmapImage pacmanBitmapUp;//джерела зображень пакмена при різних напрямках
        private BitmapImage pacmanBitmapDown;
        private BitmapImage pacmanBitmapLeft;
        private BitmapImage pacmanBitmapRight;
        private BitmapImage pacmanBitmapUpSecond;
        private BitmapImage pacmanBitmapDownSecond;
        private BitmapImage pacmanBitmapLeftSecond;
        private BitmapImage pacmanBitmapRightSecond;
        public Image pacmanImage;
        private MyPoint currentPacmanPosition;//поточне розташування в canvas
        private CoordinatesConverter converter;
        private double animDuration;

        public PacmanMovement(Cell[,] myMaze, int thickness, int imageSize, GameScore gs, DispatcherTimer moveTimer)
        {
            this.thickness = thickness;
            this.imageSize = imageSize;

            pacman = new Pacman(myMaze);
            coins = new Coins(myMaze, pacman,thickness, imageSize);
            coins.SetCellVisited(pacman.currentPosition);
            converter = new CoordinatesConverter(thickness, imageSize);
            this.gs = gs;

            currentPacmanPosition = converter.ToCanvasCoordinates(pacman.currentPosition);
            pacmanBitmapUp = new BitmapImage(new Uri(@".\Images\upFirst.png", UriKind.Relative));
            pacmanBitmapDown = new BitmapImage(new Uri(@".\Images\downFirst.png", UriKind.Relative));
            pacmanBitmapLeft = new BitmapImage(new Uri(@".\Images\leftFirst.png", UriKind.Relative));
            pacmanBitmapRight = new BitmapImage(new Uri(@".\Images\rigthFirst.png", UriKind.Relative));
            pacmanBitmapUpSecond = new BitmapImage(new Uri(@".\Images\upSecond.png", UriKind.Relative));
            pacmanBitmapDownSecond = new BitmapImage(new Uri(@".\Images\downSecond.png", UriKind.Relative));
            pacmanBitmapLeftSecond = new BitmapImage(new Uri(@".\Images\leftSecond.png", UriKind.Relative));
            pacmanBitmapRightSecond = new BitmapImage(new Uri(@".\Images\rigthSecond.png", UriKind.Relative));
            BitmapImage pacmanBitmap = new BitmapImage(new Uri(@".\Images\basic.png", UriKind.Relative));
            pacmanImage = new Image();
            pacmanImage.Source = pacmanBitmap;
            Canvas.SetLeft(pacmanImage, currentPacmanPosition.X);
            Canvas.SetTop(pacmanImage, currentPacmanPosition.Y);
            ((MainWindow)System.Windows.Application.Current.MainWindow).canvas.Children.Add(pacmanImage);
            (System.Windows.Application.Current.MainWindow).KeyDown += this.HandleKeyDown;

            animDuration = (imageSize + 2 * thickness) / 100.0;

            this.moveTimer = moveTimer;
            moveTimer.Interval = TimeSpan.FromSeconds((imageSize + 2 * thickness) / 110.0);
            moveTimer.Tick += Timer_Tick;
            chewingTimer = new DispatcherTimer();
            chewingTimer.Interval = TimeSpan.FromSeconds(0.1);
            chewingTimer.Tick += Chewing_Tick;
        }

        bool completedAnim = true;
        int counter = 0;

        private void Timer_Tick(object sender, EventArgs e)
        {
            StartMovement(pacman.currentDirection);
        }

        private void Chewing_Tick(object sender, EventArgs e)
        {
            counter++;
            if (pacman.currentDirection == Direction.up)
            {
                if (counter % 2 == 0)
                {
                    pacmanImage.Source = pacmanBitmapUp;
                }
                else
                {
                    pacmanImage.Source = pacmanBitmapUpSecond;
                }
            }
            if (pacman.currentDirection == Direction.down)
            {
                if (counter % 2 == 0)
                {
                    pacmanImage.Source = pacmanBitmapDown;
                }
                else
                {
                    pacmanImage.Source = pacmanBitmapDownSecond;
                }
            }
            if (pacman.currentDirection == Direction.right)
            {
                if (counter % 2 == 0)
                {
                    pacmanImage.Source = pacmanBitmapRight;
                }
                else
                {
                    pacmanImage.Source = pacmanBitmapRightSecond;
                }
            }
            if (pacman.currentDirection == Direction.left)
            {
                if (counter % 2 == 0)
                {
                    pacmanImage.Source = pacmanBitmapLeft;
                }
                else
                {
                    pacmanImage.Source = pacmanBitmapLeftSecond;
                }
            }

        }
      

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            moveTimer.Stop();
            chewingTimer.Stop();
            switch (e.Key)
            {
                case Key.Up:
                    pacman.currentDirection = Direction.up;
                    break;
                case Key.Down:
                    pacman.currentDirection = Direction.down;
                    break;
                case Key.Left:
                    pacman.currentDirection = Direction.left;
                    break;
                case Key.Right:
                    pacman.currentDirection = Direction.right;
                    break;
            }
            chewingTimer.Start();
            moveTimer.Start();
        }

        private void InterruptedEndPosition()//викликається, якщо попередня подія не була успішно завершена
        {//згладжує візуалізацію перерваної анімації
            MyPoint currentTemp = new MyPoint((int)Canvas.GetLeft(pacmanImage), (int)Canvas.GetTop(pacmanImage));
            pacman.currentPosition = converter.ToCellCoordinates(currentTemp);
            if (pacman.myMaze[pacman.currentPosition.X, pacman.currentPosition.Y].visited == false)
            {
                gs.AddScore();
                coins.SetCellVisited(pacman.currentPosition);
            }
            currentPacmanPosition = converter.ToCanvasCoordinates(pacman.currentPosition);
            DoubleAnimation animX = new DoubleAnimation(currentTemp.X, currentPacmanPosition.X, TimeSpan.FromSeconds(0.05));
            DoubleAnimation animY = new DoubleAnimation(currentTemp.Y, currentPacmanPosition.Y, TimeSpan.FromSeconds(0.05));
            Storyboard.SetTarget(animX, pacmanImage);
            Storyboard.SetTargetProperty(animX, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTarget(animY, pacmanImage);
            Storyboard.SetTargetProperty(animY, new PropertyPath("(Canvas.Top)"));
            sb.Children.Add(animX);
            sb.Children.Add(animY);
            sb.Begin();          
        }

        Storyboard sb = new Storyboard();
        MyPoint endAnimPoint = new MyPoint();

        private void StartMovement(Direction dir)
        {
            sb.Pause();
            sb.Children.Clear();
            if (completedAnim == false)
            {
                InterruptedEndPosition();
            }
            endAnimPoint = converter.ToCanvasCoordinates(pacman.currentPosition);
            PerformStep();
        }

        public void PerformStep()//здійснити крок
        {
            pacman.NextStep();  //зробити крок у відповідній матриці лабіринта     
            if (pacman.cantPerformNextStep)//зупинити пакмена, коли зіткнувся зі стіною
            {
                chewingTimer.Stop();
                moveTimer.Stop();
                return;
            }
            endAnimPoint = converter.ToCanvasCoordinates(pacman.currentPosition);//очікувана позиція пакмена при закінченні анімації
            sb.Children.Clear();
            completedAnim = false;

            if (pacman.currentDirection == Direction.up)
            {
                DoubleAnimation anim = new DoubleAnimation(currentPacmanPosition.Y, endAnimPoint.Y, TimeSpan.FromSeconds(animDuration));
                anim.Completed += new EventHandler(myanim_Completed);
                Storyboard.SetTarget(anim, pacmanImage);
                Storyboard.SetTargetProperty(anim, new PropertyPath("(Canvas.Top)"));
                sb.Children.Add(anim);
            }
            else if (pacman.currentDirection == Direction.down)
            {
                DoubleAnimation anim = new DoubleAnimation(currentPacmanPosition.Y, endAnimPoint.Y, TimeSpan.FromSeconds(animDuration));
                anim.Completed += new EventHandler(myanim_Completed);
                Storyboard.SetTarget(anim, pacmanImage);
                Storyboard.SetTargetProperty(anim, new PropertyPath("(Canvas.Top)"));
                sb.Children.Add(anim);
            }
            else if (pacman.currentDirection == Direction.left)
            {         
                DoubleAnimation anim = new DoubleAnimation(currentPacmanPosition.X, endAnimPoint.X, TimeSpan.FromSeconds(animDuration));
                anim.Completed += new EventHandler(myanim_Completed);
                Storyboard.SetTarget(anim, pacmanImage);
                Storyboard.SetTargetProperty(anim, new PropertyPath("(Canvas.Left)"));
                sb.Children.Add(anim);
            }
            else if (pacman.currentDirection == Direction.right)
            {             
                DoubleAnimation anim = new DoubleAnimation(currentPacmanPosition.X, endAnimPoint.X, TimeSpan.FromSeconds(animDuration));
                anim.Completed += new EventHandler(myanim_Completed);
                Storyboard.SetTarget(anim, pacmanImage);
                Storyboard.SetTargetProperty(anim, new PropertyPath("(Canvas.Left)"));
                sb.Children.Add(anim);
            }
            currentPacmanPosition = endAnimPoint;
            sb.Begin();
        }

        private void myanim_Completed(object sender, EventArgs e)//сигналізує про успішне завершення анімації
            //якщо анімація успішно закінчилась, то нараховує можливі бали і вносить зміни в монеткову матрицю класу Сoins
        {
            completedAnim = true;
            if (pacman.myMaze[pacman.currentPosition.X, pacman.currentPosition.Y].visited == false)
            {
                gs.AddScore();
                coins.SetCellVisited(pacman.currentPosition);
            }
        }
    }
}
