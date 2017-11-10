using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    class ImageComponents           //TODO RV(atolstov): возможно стоит вычислять размер прямо здесь? И RectanglesRender вообще не нужен. И назвать как-нибудь вроже `RectanglesCloud`
    {
        public Size Size{get; set; }
        public List<Rectangle> Rectangles { get; set; }
        public Point Center { get; set; }
        public ImageComponents(List<Rectangle> rects, Point center)
        {
            Rectangles = rects;
            Center = center;
            CalculateSize();
        }
        private void CalculateSize()  //TODO RV(atolstov): Зачем internal?
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
