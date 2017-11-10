using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    class TagCloudVizualizer
    {
        private const int FrameSize = 30;
        private string filename = "TagCloud.jpg";
        private readonly Color backgroundColor;
        private readonly Brush brushForRectangles;
        private readonly Color rectangleBordersColor;
        public TagCloudVizualizer(Color backgroundColor, Brush brushForRectangles, Color rectangleBordersColor)       //TODO RV(atolstov): здесь ведь можно принимать параметры для отрисовки!
        {
            this.backgroundColor = backgroundColor;
            this.brushForRectangles = brushForRectangles;
            this.rectangleBordersColor = rectangleBordersColor;
        }

        public void Vizualize(ImageComponents components)                        //TODO RV(atolstov): зачем принимать ImageComponents в конструкторе, а не в методе Visualize
        {
            var bitmap = new Bitmap(components.Size.Width+FrameSize, components.Size.Height+FrameSize);
            var gr = Graphics.FromImage(bitmap);
            gr.TranslateTransform(components.Size.Width/2-components.Center.X, components.Size.Height/2-components.Center.Y);
            gr.Clear(backgroundColor);
            foreach (var rect in components.Rectangles)
            {
                gr.FillRectangle(brushForRectangles, rect);
                gr.DrawRectangle(new Pen(rectangleBordersColor), rect);
            }
            bitmap.Save(filename);
        }
    }
}
