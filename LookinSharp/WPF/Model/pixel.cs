﻿///////////////////////////////////////
#region Namespace Directives

using System;
using System.Windows;

#endregion
///////////////////////////////////////

namespace LookinSharp.WPF.Model
{
    public class pixel : Ipixel
    {
        ////////////////////////////////////////
        #region Properties

        public Point Coordinate
        {
            get;
            private set;
        }

        public string Description
        {
            get;
            protected set;
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

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public pixel(double _x, double _y, double _temp) : this(_x, _y, _temp, String.Empty) { }

        public pixel(double _x, double _y, double _temperature, string _toolTip)
        {
            this.Coordinate = new Point(_x, _y);
            this.Description = _toolTip;
            this.Tag = Guid.NewGuid().ToString();
            this.Temperature = _temperature;
        }

        #endregion
    }
}
