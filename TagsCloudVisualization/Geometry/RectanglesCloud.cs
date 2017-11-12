using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public class RectanglesCloud           
    {
        public Size Size { get; private set; }
        public LinkedList<Rectangle> Rectangles { get; private set; }
        private const int StepToCenterCount = 30;
        public Point Center { get; private  set; }
        private int leftBorder;
        private int rightBorder;
        private int downBorder;
        private int upBorder;
        public RectanglesCloud(Point center)
        {
            Rectangles = new LinkedList<Rectangle>();
            Center = center;
            Size = new Size(0,0);
        }

        private void UpdateBordersAndSize(Rectangle newRectangle)
        {
            if (newRectangle.X < leftBorder)
                leftBorder = newRectangle.X;
            if (newRectangle.Y < downBorder)
                downBorder = newRectangle.Y;
            if (newRectangle.X + newRectangle.Width > rightBorder)
                rightBorder = newRectangle.X + newRectangle.Width;
            if (newRectangle.Y + newRectangle.Height > upBorder)
                upBorder = newRectangle.Bottom;
            Size = new Size(rightBorder - leftBorder, upBorder - downBorder);
        }

        public Rectangle PutNextRectangle(Rectangle foundRectangle)
        {
            if (!CheckIntersectionWithExistigRectangles(foundRectangle))
            {
                foundRectangle = MoveRectangleToCenter(foundRectangle);
                Rectangles.AddLast(foundRectangle);
                UpdateBordersAndSize(foundRectangle);
                return foundRectangle;
            }
            return Rectangle.Empty;
        }

        public  bool CheckIntersectionWithExistigRectangles(Rectangle rect)
        {
            for (var currRect = Rectangles.Last; currRect != null; currRect = currRect.Previous)
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
            while (curX != Center.X && curY != Center.Y && stepCount != StepToCenterCount)
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
            cur < center ? cur + 1 : cur - 1;
    }
}
