using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization.Geometry;

namespace TagsCloudVisualization
{
    [TestFixture]
    internal class CircularCloudLayouter_Should
    {
        private CircularCloudLayouter layouter;


        [SetUp]
        public void SetUp()
        {
            layouter = new CircularCloudLayouter(new Point(300, 300));
            layouter.PutNextRectangle(new Size(20, 10));
            layouter.PutNextRectangle(new Size(20, 10));
        }

        [TestCase(0, 0, 10, 20, ExpectedResult = false, TestName = "Add in left-up")]
        [TestCase(330, 14, 10, 20, ExpectedResult = false, TestName = "Add in down")]
        [TestCase(370, 300, 10, 20, ExpectedResult = false, TestName = "Add in rigt")]
        public bool NotIntersection_WhenAddRectangeInFreeSpace(int x, int y, int width, int height)
        {
            return layouter.CheckIntersectionWithExistigRectangles(new Rectangle(x, y, width, height));
        }

        [TestCase(300, 300, 10, 20, ExpectedResult = true, TestName = "Add in center")]
        [TestCase(310, 300, 10, 20, ExpectedResult = true, TestName = "Add in another rectangle Space")]
        public bool ExistIntersection_WhenAddRectangeInOccupiedSpace(int x, int y, int width, int height)
        {
            return layouter.CheckIntersectionWithExistigRectangles(new Rectangle(x, y, width, height));
        }
        [Test]
        public void СheckThatStylingIsInTheFormOfCircle()
        {
            layouter.PutNextRectangle(new Size(30, 30));
            layouter.PutNextRectangle(new Size(20, 40));
            var expectedRadius = 30;
            foreach (var rect in layouter.Items)
            {
                var actualRadius =
                    Math.Sqrt(Math.Pow(layouter.Center.X - rect.X, 2) + Math.Pow(layouter.Center.Y - rect.Y, 2));
                actualRadius.Should().BeLessOrEqualTo(expectedRadius);
            }
        }

        [Test]
        public void FilledSpaceIsMoreThan70Percent()
        {
            var rnd = new Random();
            for (var i = 0; i <= 5000; i++)
                layouter.PutNextRectangle(new Size(rnd.Next(70), rnd.Next(35)));
            var imageComp = RectanglesRender.Render(layouter.Items.ToList(), layouter.Center);
            var totalArea = Math.Pow(imageComp.Size.Height, 2) * Math.PI/4;
            var filledArea = imageComp.Items.Select(x => x.Size.Height * x.Size.Width).Sum();
            var percentageOfFill = filledArea / totalArea*100;
            (percentageOfFill).Should().BeGreaterOrEqualTo(70);
        }
        [TearDown]
        public void TearDown()
        {
            if (Equals(TestContext.CurrentContext.Result.Outcome, ResultState.Failure))
            {
                var imageComp = RectanglesRender.Render(layouter.Items.ToList(), layouter.Center);
                var d = new TagCloudVizualizer(imageComp);
                d.Vizualize();
            }
        }

    }
}
