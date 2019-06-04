using PacManGame.GameVisualization;
using PacManGame.Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PacManGame.Logic
{
    public class Collapse
    {
        Pacman pacman;
        Ghost ghost;

        public Collapse(Pacman pacman, Ghost ghost)
        {
            this.pacman = pacman;
            this.ghost = ghost;
        }

        public bool CheckForCollapse()// перевірка, чи співпадають позиції привида та пакмена
        {
            if (ghost.currentPosition.X == pacman.currentPosition.X && ghost.currentPosition.Y == pacman.currentPosition.Y)
            {
                return true;
            }
            return false;
        }

        public bool PredictCollapse()//передбачення зіткнення
        {
            if (ghost.currentDirection == Direction.up && pacman.currentDirection == Direction.down)
            {
                if (pacman.currentPosition.Y == ghost.currentPosition.Y - 1 && pacman.currentPosition.X == ghost.currentPosition.X)
                {
                    return true;
                }
            }
            else if (ghost.currentDirection == Direction.down && pacman.currentDirection == Direction.up)
            {
                if (pacman.currentPosition.Y == ghost.currentPosition.Y + 1 && pacman.currentPosition.X == ghost.currentPosition.X)
                {
                    return true;
                }
            }
            else if (ghost.currentDirection == Direction.right && pacman.currentDirection == Direction.left)
            {
                if (pacman.currentPosition.X == ghost.currentPosition.X + 1 && pacman.currentPosition.Y == ghost.currentPosition.Y)
                {
                    return true;
                }
            }
            else if (ghost.currentDirection == Direction.left && pacman.currentDirection == Direction.right)
            {
                if (pacman.currentPosition.X == ghost.currentPosition.X - 1 && pacman.currentPosition.Y == ghost.currentPosition.Y)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
