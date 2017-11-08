using System;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework.Internal.Execution;

namespace TagsCloudVisualization.Geometry
{
    class RectanglesRender                      
    {                                      
        public static ImageComponents Render(List<Rectangle> rectangles, Point center) =>
            new ImageComponents(CalculateSize(rectangles), rectangles, center);

        internal static Size CalculateSize(List<Rectangle> rectangles)  //TODO RV(atolstov): Зачем internal?
        {
            if(rectangles.Count==0)
                return new Size(0,0);
            var leftBorder = rectangles[0].X;
            var upBorder = rectangles[0].Y;
            var rightBorder = rectangles[0].X + rectangles[0].Width;
            var downBorder = rectangles[0].Bottom;
            foreach (var rect in rectangles)
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
            return new Size(rightBorder-leftBorder, upBorder-downBorder);
        }
    }
}
