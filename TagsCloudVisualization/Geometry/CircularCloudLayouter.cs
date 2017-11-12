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
        public RectanglesCloud RectCloud  { get; }     
        private readonly Spiral spiral;
        public CircularCloudLayouter(Point center, double strainOnX = 1, double strainOnY = 1)
        {
            RectCloud = new RectanglesCloud(center);
            spiral = new Spiral(center, strainOnX, strainOnY);
        }
        public Rectangle PutNextRectangle(Size rectangleSize) => FindFreeRectangle(rectangleSize);

        internal Rectangle FindFreeRectangle(Size size)
        {
            while (true)             
            {
                var point = spiral.GetNextPoint();
                var foundRectangle = new Rectangle((int)point.X, (int)point.Y, size.Width, size.Height);
                foundRectangle = RectCloud.PutNextRectangle(foundRectangle);
                if(foundRectangle!=Rectangle.Empty)
                return foundRectangle;
            }
        }
    }
}
