///////////////////////////////////////
#region Namespace Directives

using LookinSharp.WPF.Model;
using LookinSharp.WPF.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

#endregion
///////////////////////////////////////

namespace LookinSharp.Test.WPF.ViewModel
{
    /// <summary>
    /// Summary description for DataRenderingWorkspaceViewModel_spec
    /// </summary>
    [TestClass]
    public class ImageGraphViewModel_spec
    {
        ////////////////////////////////////////
        #region Fields

        private pixel[][] _imageData;
        private ImageGraphViewModel _masterPiece;

        #endregion

        ////////////////////////////////////////
        #region Constructor (Auto-generated)

        public ImageGraphViewModel_spec()
        {
            int size = 300;
            Action<Rectangle> mouseLeft = (x) => DoStuff();
            Action<Rectangle> mouseRight = (x) => DoStuff();
            Func<double, int, int, int, Color> mouseWrong = (a, b, c, d) => DoColorfulStuff(a, b, c, d);

            _masterPiece = new ImageGraphViewModel(size, mouseLeft, mouseRight, mouseWrong);
        }

        #endregion

        ////////////////////////////////////////
        #region TestContext Components (Auto-Generated)

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #endregion

        ////////////////////////////////////////
        #region Unit Tests (Methods)

        // Clear

        [TestMethod]
        public void Clear_ImageHasContent_ImageHasNoContent()
        {
            _masterPiece.RenderedImage = new Canvas();
            _masterPiece.RenderedImage.Children.Add(new Rectangle());
            _masterPiece.Clear();

            int count = _masterPiece.RenderedImage.Children.Count;

            Assert.AreEqual(0, count);
        }

        // RefreshImage

        [TestMethod]
        public void RefreshImage_InputSameImageMoreThanOnce_GetSameParameters()
        {
            _imageData = GenerateImageData();

            int expectedPixels = GetNumberOfPixels(_imageData);

            _masterPiece.RefreshImage(_imageData);
            _masterPiece.RefreshImage(_imageData);

            int actualPixels = _masterPiece.RenderedImage.Children.Count;

            Assert.AreEqual(expectedPixels, actualPixels);
        }

        // RefreshLayeredImage

        [TestMethod]
        public void RefreshLayeredImage_InputMoreThanOneLayer_NumberOfChildrenEqualsTotalNumberOfPixels()
        {
            _imageData = GenerateImageData();

            int numPixels = GetNumberOfPixels(_imageData);
            int numLayers = 3;
            List<Ipixel[][]> layers = new List<Ipixel[][]>();

            for (int i = 0; i < numLayers; i++)
            {
                layers.Add(_imageData);
            }

            _masterPiece.RefreshLayeredImage(layers);

            int actualPixels = _masterPiece.RenderedImage.Children.Count;

            Assert.AreEqual(numPixels * numLayers, actualPixels);
        }

        #endregion

        ////////////////////////////////////////
        #region Supporting Methods

        private pixel[][] GenerateImageData()
        {
            int rows = 4;
            int columns = 4;
            pixel[][] data = new pixel[rows][];

            for (int i = 0; i < rows; i++)
            {
                data[i] = new pixel[columns];
                for (int j = 0; j < columns; j++)
                {
                    double x = 0.5;
                    double y = 0.5;
                    double temp = 0.5;
                    data[i][j] = new pixel(x, y, temp);
                }
            }

            return data;
        }

        private int GetNumberOfPixels(pixel[][] array)
        {
            return array.Length * array[0].Length;
        }

        #endregion

        ////////////////////////////////////////
        #region Dummy Methods (for filling events)

        void DoStuff()
        {
            // stuff done right here
        }

        Color DoColorfulStuff(double a, int b, int c, int d)
        {
            return new Color();
        }

        #endregion
    }
}
