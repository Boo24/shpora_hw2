using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    class ImageComponents_Should
    {
        [Test]
        public void EmptyRectanglesAndZeroSize_WhenNoRectangles()  
        {
            var rectangles = new List<Rectangle>();
            var expectedSize = new Size(0, 0);
            var imageComp =new ImageComponents(rectangles, new Point(0,0));
            imageComp.Size.ShouldBeEquivalentTo(expectedSize);
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
            var expectedSize = new Size(15, 35);
            var imageComp = new ImageComponents(rectangles, rectangles[0].Location);
            imageComp.Size.ShouldBeEquivalentTo(expectedSize);
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
            var expectedSize = new Size(20, 40);
            var imageComp = new ImageComponents(rectangles, rectangles[0].Location);
            imageComp.Size.ShouldBeEquivalentTo(expectedSize);
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
            var expectedSize = new Size(50, 55);
            var imageComp = new ImageComponents(rectangles, rectangles[0].Location);
            imageComp.Size.ShouldBeEquivalentTo(expectedSize);
        }
    }
}
