using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        public readonly Point Center;
        private const int StepToCenterCount = 30;
        public LinkedList<Rectangle> Rectangles { get; }     //TODO RV(atolstov): Наверное все же Rectangles
        private readonly Spiral spiral;
        public CircularCloudLayouter(Point center)
        {
            Center = center;
            Rectangles = new LinkedList<Rectangle>();
            spiral = new Spiral(center);
        }
        public Rectangle PutNextRectangle(Size rectangleSize) => FindFreeRectangle(rectangleSize);

        internal Rectangle FindFreeRectangle(Size size)
        {
            while (true)             //TODO RV(atolstov): а это нельзя переписать с циклом foreach и методами LINQ?
            {
                var point = spiral.GetNextPoint();
                var foundRectangle = new Rectangle((int)point.X, (int)point.Y, size.Width, size.Height);
                if (!CheckIntersectionWithExistigRectangles(foundRectangle))
                {
                    foundRectangle = MoveRectangleToCenter(foundRectangle); //если закоментить эту строчку, то время раобты увеличится в разы
                    Rectangles.AddLast(foundRectangle);
                    return foundRectangle;
                }
            }
        }

        internal bool CheckIntersectionWithExistigRectangles(Rectangle rect)
        {
            for (var currRect = Rectangles.Last; currRect != null; currRect = currRect.Previous)//TODO RV(atolstov): А это нельзя переписать на цикл for / foreach?
                if (currRect.Value.IntersectsWith(rect))
                    return true;
            return false;
        }

        internal Rectangle MoveRectangleToCenter(Rectangle rect)
        {
            int lastGoodX;
            int lastGoodY;
            var curX = lastGoodX = rect.X;
            var curY = lastGoodY = rect.Y;
            var stepCount = 0;
            while (curX != Center.X && curY != Center.Y && stepCount != StepToCenterCount)  //TODO RV(atolstov): Возможно должно быть `&& stepCount!=StepToCenterCount`. Или я неправильно понимаю твою логику?
            {
                curX = MoveCoordinateToCenter(curX, Center.X);
                curY = MoveCoordinateToCenter(curY, Center.Y);
                var tempRect = new Rectangle(new Point(curX, curY), rect.Size);
                if (!CheckIntersectionWithExistigRectangles(tempRect))
                {
                    lastGoodX = curX;
                    lastGoodY = curY;
                }
                else
                    stepCount += 1;
            }
            return new Rectangle(new Point(lastGoodX, lastGoodY), rect.Size);
        }

        internal int MoveCoordinateToCenter(int cur, int center) =>
            cur < center ? cur+1 : cur-1;
    }
}
