using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Geometry;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    class RectanglesRender_Should
    {
        [Test]
        public void EmptyRectanglesAndZeroSize_WhenNoRectangles()  
        {
            var rectangles = new List<Rectangle>();
            var expectedImageComp = new ImageComponents(new Size(0, 0), rectangles, new Point(0,0));
            var imageComp =RectanglesRender.Render(rectangles, new Point(0,0));
            imageComp.ShouldBeEquivalentTo(expectedImageComp);
        }

        [Test]
        public void CalculateSize_WhenRectanglesWithNegativeCoordinates()
        {
            var rectangles = new List<Rectangle>()
            {
                new Rectangle(-10, -10, 5, 5),
                new Rectangle(-20, -15, 5, 5),
                new Rectangle(-20, -40, 10, 10)
            };
            var expectedImageComp = new ImageComponents(new Size(15, 35), rectangles, rectangles[0].Location);
            var imageComp = RectanglesRender.Render(rectangles, rectangles[0].Location);
            imageComp.ShouldBeEquivalentTo(expectedImageComp);
        }
        [Test]
        public void CalculateSize_WhenRectanglesWithPositiveCoordinates()
        {
            var rectangles = new List<Rectangle>()
            {
                new Rectangle(10, 10, 5, 5),
                new Rectangle(20, 15, 5, 5),
                new Rectangle(20, 40, 10, 10)
            };
            var expectedImageComp = new ImageComponents(new Size(20, 40), rectangles, rectangles[0].Location);
            var imageComp = RectanglesRender.Render(rectangles, rectangles[0].Location);
            imageComp.ShouldBeEquivalentTo(expectedImageComp);
        }

        [Test]
        public void CalculateSize_WhenRectanglesWithPositiveAndNeganiveCoordinates()
        {
            var rectangles = new List<Rectangle>()
            {
                new Rectangle(10, 10, 5, 5),
                new Rectangle(-20, -15, 5, 5),
                new Rectangle(20, -40, 10, 10)
            };
            var expectedImageComp = new ImageComponents(new Size(50, 55), rectangles, rectangles[0].Location);
            var imageComp = RectanglesRender.Render(rectangles, rectangles[0].Location);
            imageComp.ShouldBeEquivalentTo(expectedImageComp);
        }
    }
}
