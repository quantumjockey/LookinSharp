///////////////////////////////////////
#region Namespace Directives

using LookinSharp.WPF.Model;
using System.Windows.Shapes;

#endregion
///////////////////////////////////////

namespace LookinSharp.WPF.ViewModel
{
    public class PixelViewModel
    {
        ////////////////////////////////////////
        #region Properties

        public Rectangle Graphic
        {
            get;
            private set;
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public PixelViewModel(Ipixel _data, double _xScale, double _yScale)
        {
            Graphic = InitializeRectangle(_yScale, _xScale);
            Graphic.Tag = _data.Tag;
            Graphic.ToolTip = _data.ToolTip;
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods

        private Rectangle InitializeRectangle(double _height, double _width)
        {
            Rectangle rec = new Rectangle();
            rec.Height = _height;
            rec.Width = _width;
            return rec;
        }

        #endregion
    }
}
