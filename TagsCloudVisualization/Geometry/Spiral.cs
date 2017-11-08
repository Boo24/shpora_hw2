using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{

    public class Spiral
    {
        private const double GrowthRate = 1;
        private const double DistBetweenCoils = 1;
        private const double StepTheta = 0.1;
        private Point center;

        public Spiral(Point center) =>                  //TODO RV(atolstov): Почему бы не дать возможность растягивать спираль по горизонтали и вертикали
            this.center = center;                       //TODO RV(atolstov): Либо пиши однострочную функцию в одну строку, либо пиши обычную
        public IEnumerator<PointF> GetSriral()          //TODO RV(atolstov): Spiral.GetSpiral? Название вводит в заблуждение
        {
            yield return center;
            var theta = 0.0;
            while (true)
            {
                theta += StepTheta;
                var angle = 0.1 * theta;
                var x = (float)((DistBetweenCoils + GrowthRate * angle) * Math.Cos(angle) + center.X);
                var y = (float)((DistBetweenCoils + GrowthRate * angle) * Math.Sin(angle) + center.Y);
                yield return new PointF(x, y);
            }
        }

    }
}
