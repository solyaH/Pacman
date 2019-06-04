using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacManGame.Logic;
using PacManGame.Maze;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Threading;

namespace PacManGame.GameVisualization
{
    public class GhostMovement
    {
        public DispatcherTimer timer;//таймер руху привида
        public Ghost ghost;//екземпляр класу логіки руху привида
        int thickness;
        int imageSize;
        public Image ghostImage;
        MyPoint currentGhostPosition;//поточне розташування в canvas
        CoordinatesConverter converter;
        Collapse collapse;//екземпляр класу зіткнення пакмена та привида
        public delegate void LoseGameEventHandler(DispatcherTimer pacmanTimer, DispatcherTimer ghostTimer, string message);
        public event LoseGameEventHandler LoseGame;//подія програшу гри
        DispatcherTimer pacmanTimer;

        public GhostMovement(Cell[,] myMaze,Pacman pacman, int thickness, int imageSize,string Path, Random rand, DispatcherTimer timer, DispatcherTimer pacmanTimer)
        {
            this.pacmanTimer = pacmanTimer;
            this.thickness = thickness;
            this.imageSize = imageSize;
            converter = new CoordinatesConverter(thickness, imageSize);
            ghost = new Ghost(myMaze, pacman.currentPosition, rand);
            collapse = new Collapse(pacman, ghost);//екземпляр класу зіткнення, який приймає вхідними параметрами привида та пакмена
            currentGhostPosition = converter.ToCanvasCoordinates(ghost.currentPosition);//поточне розташування
            BitmapImage pacmanBitmap = new System.Windows.Media.Imaging.BitmapImage(new Uri(Path, UriKind.Relative));
            ghostImage = new Image();
            ghostImage.Source = pacmanBitmap;
            Canvas.SetLeft(ghostImage, currentGhostPosition.X);
            Canvas.SetTop(ghostImage, currentGhostPosition.Y);
            ((MainWindow)System.Windows.Application.Current.MainWindow).canvas.Children.Add(ghostImage);
            this.timer = timer;
            timer.Interval = TimeSpan.FromSeconds((imageSize+2*thickness)/100.0);
            timer.Tick += Timer_Tick; ;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {            
            if (collapse.CheckForCollapse())
            {
                    LoseGame(pacmanTimer, timer, "YOU LOSE!");//якщо відбулося зіткнення-завершити гру
            }
            StartMovement();
        }

        private void StartMovement()
        {
            if (!collapse.PredictCollapse())//якщо не очікується зіткнення-зробити крок
            {
                ghost.MoveGhost();
            }
            else
            {
                return;
            }
            Direction dir = ghost.currentDirection;
            MyPoint endAnimPoint = converter.ToCanvasCoordinates(ghost.currentPosition);
            if (dir == Direction.up)
            {
                double duration = Math.Abs(endAnimPoint.Y - currentGhostPosition.Y) / 100.0;
                Storyboard sb = new Storyboard();
                DoubleAnimation anim = new DoubleAnimation(currentGhostPosition.Y, endAnimPoint.Y, TimeSpan.FromSeconds(duration));
                Storyboard.SetTarget(anim, ghostImage);
                Storyboard.SetTargetProperty(anim, new PropertyPath("(Canvas.Top)"));
                sb.Children.Add(anim);
                sb.Begin();
                currentGhostPosition = endAnimPoint;
            }
            else if (dir == Direction.down)
            {
                double duration = Math.Abs(endAnimPoint.Y - currentGhostPosition.Y) / 100.0;
                Storyboard sb = new Storyboard();
                DoubleAnimation anim = new DoubleAnimation(currentGhostPosition.Y, endAnimPoint.Y, TimeSpan.FromSeconds(duration));
                Storyboard.SetTarget(anim, ghostImage);
                Storyboard.SetTargetProperty(anim, new PropertyPath("(Canvas.Top)"));
                sb.Children.Add(anim);
                sb.Begin();
                currentGhostPosition = endAnimPoint;
            }
            else if (dir == Direction.left)
            {
                double duration = Math.Abs(endAnimPoint.X - currentGhostPosition.X) / 100.0;
                Storyboard sb = new Storyboard();
                DoubleAnimation anim = new DoubleAnimation(currentGhostPosition.X, endAnimPoint.X, TimeSpan.FromSeconds(duration));
                Storyboard.SetTarget(anim, ghostImage);
                Storyboard.SetTargetProperty(anim, new PropertyPath("(Canvas.Left)"));
                sb.Children.Add(anim);
                sb.Begin();
                currentGhostPosition = endAnimPoint;
            }
            else if (dir == Direction.right)
            {
                double duration = Math.Abs(endAnimPoint.X - currentGhostPosition.X) / 100.0;
                Storyboard sb = new Storyboard();
                DoubleAnimation anim = new DoubleAnimation(currentGhostPosition.X, endAnimPoint.X, TimeSpan.FromSeconds(duration));
                Storyboard.SetTarget(anim, ghostImage);
                Storyboard.SetTargetProperty(anim, new PropertyPath("(Canvas.Left)"));
                sb.Children.Add(anim);
                sb.Begin();
                currentGhostPosition = endAnimPoint;
            }
        }
    }
}
