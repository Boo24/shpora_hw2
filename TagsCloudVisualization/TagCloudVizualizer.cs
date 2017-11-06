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
        private ImageComponents components;
        private string filename = "TagCloud.jpg";
        public TagCloudVizualizer(ImageComponents components)
        {
            this.components = components;
        }

        public void Vizualize()
        {
            var bitmap = new Bitmap(components.Size.Width+FrameSize, components.Size.Height+FrameSize);
            var gr = Graphics.FromImage(bitmap);
            gr.TranslateTransform(components.Size.Width/2-components.Center.X, components.Size.Height/2-components.Center.Y);
            gr.Clear(Color.RosyBrown);
            foreach (var rect in components.Items)
            {
                gr.FillRectangle(Brushes.Aqua, rect);
                gr.DrawRectangle(new Pen(Color.Black), rect);
            }
            bitmap.Save(filename);
        }
    }
}
