using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame.GameVisualization
{
    public class CoordinatesConverter//конвертер координат із матриці у canvas і навпаки
    {
        int imageSize;
        int thickness;

        public CoordinatesConverter(int thickness,int imageSize)
        {
            this.thickness = thickness;
            this.imageSize = imageSize;
        }

        public MyPoint ToCanvasCoordinates(MyPoint pos)
        {
            int x = (pos.X) * (thickness + imageSize) + (pos.X + 1) * thickness;
            int y = (pos.Y) * (thickness + imageSize) + (pos.Y + 1) * thickness;

            return new MyPoint(x, y);
        }

        public MyPoint ToCellCoordinates(MyPoint pos)
        {
            int x = (int)Math.Round((double)(pos.X - thickness) / (2 * thickness + imageSize));
            int y = (int)Math.Round((double)(pos.Y - thickness) / (2 * thickness + imageSize));
            return new MyPoint(x, y);
        }
    }
}
