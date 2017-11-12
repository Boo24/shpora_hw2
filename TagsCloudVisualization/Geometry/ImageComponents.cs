using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    class ImageComponents           
    {
        public Size Size{get; set; }	//TODO RV(atolstov): Почему сеттеры публичные?
        public List<Rectangle> Rectangles { get; set; }
        public Point Center { get; set; }
        public ImageComponents(List<Rectangle> rects, Point center)
        {
            Rectangles = rects;
            Center = center;
            CalculateSize();
        }
        private void CalculateSize()  //TODO RV(atolstov): попробуй сделать Rectangles приватным, и тогда тв сможешь контролировать добавление нового прямоуголника => сможешь вычислять Size "на лету"
        {
            if (Rectangles.Count == 0)
            {
                Size = new Size(0, 0);
                return;
            }
            var leftBorder = Rectangles[0].X;
            var upBorder = Rectangles[0].Y;
            var rightBorder = Rectangles[0].X + Rectangles[0].Width;
            var downBorder = Rectangles[0].Bottom;
            foreach (var rect in Rectangles)
            {
                if (rect.X < leftBorder)
                    leftBorder = rect.X;
                if (rect.Y < downBorder)
                    downBorder = rect.Y;
                if (rect.X + rect.Width > rightBorder)
                    rightBorder = rect.X + rect.Width;
                if (rect.Y + rect.Height > upBorder)
                    upBorder = rect.Bottom;
            }
            Size= new Size(rightBorder - leftBorder, upBorder - downBorder);
        }
    }
}
