﻿///////////////////////////////////////
#region Namespace Directives

using System;
using System.Windows;

#endregion
///////////////////////////////////////

namespace LookinSharp.WPF.Model
{
    public class pixelBase : Ipixel
    {
        ////////////////////////////////////////
        #region Properties

        public Point Coordinate
        {
            get;
            private set;
        }

        public string Tag
        {
            get;
            private set;
        }

        public double Temperature
        {
            get;
            private set;
        }

        public string ToolTip
        {
            get;
            private set;
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor


        public pixelBase(double _x, double _y, double _temperature, string _toolTip)
        {
            this.Coordinate = new Point(_x, _y);
            this.Tag = Guid.NewGuid().ToString();
            this.Temperature = _temperature;
            this.ToolTip = _toolTip;
        }

        #endregion
    }
}