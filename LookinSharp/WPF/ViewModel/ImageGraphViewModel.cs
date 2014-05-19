﻿///////////////////////////////////////
#region Namespace Directives

using LookinSharp.WPF.Model;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WpfHelper.ViewModel.Workspaces;

#endregion
///////////////////////////////////////

namespace LookinSharp.WPF.ViewModel
{
    public class ImageGraphViewModel : WorkspaceViewModel
    {
        ////////////////////////////////////////
        #region Constants

        const int _maxChannelValue = 255;

        #endregion

        ////////////////////////////////////////
        #region Generic Fields

        // Workspace-Specific
        private Ipixel[][] _imageBuffer;
        private int _imageSize;
        private Canvas _renderedImage;
        private bool _renderToScale;

        // Pixel selection
        private string _selectedPixelTag;

        // Mouse-related
        private Action<Rectangle> _mouseLeftAction;
        private Action<Rectangle> _mouseRightAction;

        // Color-related
        private Func<double, int, int, int, Color> _generateColorAction;

        #endregion

        ////////////////////////////////////////
        #region Properties

        public Canvas RenderedImage
        {
            get
            {
                return _renderedImage;
            }
            set
            {
                _renderedImage = value;
                OnPropertyChanged("RenderedImage");
            }
        }

        public bool RenderToScale
        {
            get
            {
                return _renderToScale;
            }
            set
            {
                _renderToScale = value;
                RefreshImage(_imageBuffer);
                OnPropertyChanged("RenderToScale");
            }
        }

        public string SelectedPixelTag
        {
            get
            {
                return _selectedPixelTag;
            }
            set
            {
                _selectedPixelTag = value;
                OnPropertyChanged("SelectedPixelTag");
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Constructor

        public ImageGraphViewModel(int _size, Action<Rectangle> mouseLeftAction, Action<Rectangle> mouseRightAction,
            Func<double, int, int, int, Color> generateColorAction)
        {
            _generateColorAction = generateColorAction;
            _mouseLeftAction = mouseLeftAction;
            _mouseRightAction = mouseRightAction;
            _imageSize = _size;
            RenderToScale = true;
        }

        #endregion

        ////////////////////////////////////////
        #region Public Methods


        public void Clear()
        {
            RenderedImage.Children.Clear();
        }


        public void RefreshImage(Ipixel[][] imageGrid)
        {
            if (RenderedImage != null)
            {
                Clear();
            }

            if (imageGrid != null)
            {
                RenderedImage = RenderImage(imageGrid, _imageSize, _maxChannelValue, _maxChannelValue, _maxChannelValue);
                _imageBuffer = imageGrid;
            }
        }

        public void RefreshImage(List<Ipixel[][]> imageCollection)
        {
            if (RenderedImage != null)
            {
                Clear();
            }

            foreach (Ipixel[][] imageGrid in imageCollection)
            {
                if (imageGrid != null)
                {
                    RenderedImage = RenderImage(imageGrid, _imageSize, _maxChannelValue, _maxChannelValue, _maxChannelValue);
                    _imageBuffer = imageGrid;
                }
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods


        private void AddPixel(double x, double y, double xScale, double yScale, ref Canvas _renderedImage, Ipixel _data, int _maxR, int _maxG, int _maxB)
        {
            PixelWrapper xrfPix = new PixelWrapper(_data, xScale, yScale);
            Canvas.SetTop(xrfPix.Graphic, y);
            Canvas.SetLeft(xrfPix.Graphic, x);
            xrfPix.Graphic.Fill = new SolidColorBrush(_generateColorAction(_data.Temperature, _maxR, _maxG, _maxB));
            xrfPix.Graphic.MouseEnter += Graphic_MouseEnter;
            xrfPix.Graphic.MouseLeftButtonDown += Graphic_MouseLeftButtonDown;
            xrfPix.Graphic.MouseRightButtonDown += Graphic_MouseRightButtonDown;
            _renderedImage.Children.Add(xrfPix.Graphic);
        }


        private Canvas RenderImage(Ipixel[][] imageGrid, double imageSize, int maxR, int maxG, int maxB)
        {
            Canvas _effectiveImage = new Canvas();

            int imageRows = imageGrid.Length;
            int imageColumns = imageGrid[0].Length;

            double pixelScaleX, pixelScaleY;

            if (RenderToScale)
            {
                int scaleDivisor = (imageRows > imageColumns) ? imageRows : imageColumns;
                pixelScaleY = pixelScaleX = imageSize / scaleDivisor;

                _effectiveImage.Height = (imageRows < imageColumns) ? (imageSize * imageRows / imageColumns) : imageSize;
                _effectiveImage.Width = (imageRows < imageColumns) ? imageSize : (imageSize * imageColumns / imageRows);
            }
            else
            {
                pixelScaleY = imageSize / imageRows;
                pixelScaleX = imageSize / imageColumns;

                _effectiveImage.Height = _effectiveImage.Width = imageSize;
            }

            AddPixel(0, 0, pixelScaleX, pixelScaleY, ref _effectiveImage, imageGrid[0][0], maxR, maxG, maxB);

            for (int i = 0; i < imageRows; i++)
            {
                for (int j = 0; j < imageColumns; j++)
                {
                    AddPixel(j * pixelScaleX, i * pixelScaleY, pixelScaleX, pixelScaleY, ref _effectiveImage, imageGrid[i][j], maxR, maxG, maxB);
                }
            }

            return _effectiveImage;
        }

        #endregion

        ////////////////////////////////////////
        #region Event Handling

        void Graphic_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Rectangle pix = sender as Rectangle;
            SelectedPixelTag = pix.Tag.ToString();
        }


        void Graphic_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Rectangle pix = sender as Rectangle;
            _mouseLeftAction(pix);
        }


        void Graphic_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Rectangle pix = sender as Rectangle;
            _mouseRightAction(pix);
        }

        #endregion

        ////////////////////////////////////////
        #region Child Classes

        class PixelWrapper
        {
            #region Properties

            public Rectangle Graphic
            {
                get;
                private set;
            }

            #endregion

            #region Constructor

            public PixelWrapper(Ipixel _data, double _xScale, double _yScale)
            {
                Graphic = InitializeRectangle(_yScale, _xScale);
                Graphic.Tag = _data.Tag;
                Graphic.ToolTip = _data.Description;
            }

            #endregion

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

        #endregion
    }
}