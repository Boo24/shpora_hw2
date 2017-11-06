using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    [TestFixture]
    class Spiral_Should
    {

        [TestCase(0, 0, TestName = "Center is point (0, 0)")]
        [TestCase(-20, -20, TestName = "Center is negative coordinates")]
        [TestCase(20, 20, TestName = "Center is positive coordinates")]
        public void ReturnPointEqualsCenter_WhenGetFirstPoint(int x, int y)
        {
            var spiral = new Spiral(new Point(x, y)).GetSriral();
            spiral.MoveNext();
            spiral.Current.Should().Be(new PointF(x, y));
        }
    }
}
