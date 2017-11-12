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
        public TagCloudVizualizer(Color backgroundColor, Brush brushForRectangles, Color rectangleBordersColor)       
        {
            this.backgroundColor = backgroundColor;
            this.brushForRectangles = brushForRectangles;
            this.rectangleBordersColor = rectangleBordersColor;
        }

        public Bitmap Vizualize(RectanglesCloud components)   //TODO RV(atolstov): почему бы тебе просто не возвращать битмап? Вдруг кто-то захочит отриовать его на WinForm-ах        
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
            return bitmap;
        }
    }
}
