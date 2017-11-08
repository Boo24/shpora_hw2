using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    class ImageComponents           //TODO RV(atolstov): возможно стоит вычислять размер прямо здесь? И RectanglesRender вообще не нужен. И назвать как-нибудь вроже `RectanglesCloud`
    {
        public Size Size{get; set; }
        public List<Rectangle> Items { get; set; }
        public Point Center { get; set; }
        public ImageComponents(Size size, List<Rectangle> rects, Point center)
        {
            Size = size;
            Items = rects;
            Center = center;
        }
    }
}
