using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PacManGame.Maze
{
    public enum Direction { up = 0, down = 1, left = 2, right = 3};
    public class Cell
    {
        public bool visited;
        public bool start;
        public bool end;

        public bool leftWall;
        public bool rightWall;
        public bool upperWall;
        public bool lowerWall;

        public bool leftDirChecked;
        public bool rightDirChecked;
        public bool upperDirChecked;
        public bool lowerDirChecked;

        public Cell()
        {
            visited = start = false;
            leftWall = rightWall = upperWall = lowerWall = true;
            end = leftDirChecked = rightDirChecked = upperDirChecked = lowerDirChecked = false;
        }
    }
}
