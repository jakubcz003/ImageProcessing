using Avalonia.Controls;
using Avalonia.Media.Imaging;
using System;
using System.IO;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;

namespace GimpClone.Filters
{
    public class Filter
    {

        protected WriteableBitmap _source;
        protected WriteableBitmap _copy;

        protected FilterBase _filter;
        protected int _width;
        protected int _height;
        protected int _rowBytes;
        protected int _pixelOffset;
        protected byte[] _buffer = [];
        protected IntPtr _address;

        public Filter(WriteableBitmap sourceImage, FilterBase Filter)
        {
            _source = sourceImage;
            _copy = sourceImage;
            _filter = Filter;
            InitializeProcessing();
        }

        public void InitializeProcessing()
        {
            if (_source == null)
                return;

            // Now lock the bitmap for pixel operations
            using (var lockedBitmap = _source.Lock())
            {
                // Get the pixel size to iterate through all pixels
                _width = _source.PixelSize.Width;
                _height = _source.PixelSize.Height;

                // Use IntPtr instead of unsafe code
                _address = lockedBitmap.Address;
                _rowBytes = lockedBitmap.RowBytes;
                _buffer = new byte[_rowBytes * _height];
            }
        }

        public void ApplyFilter()
        {
            BeforeFilter();

            // _filter.ApplyFilter();

            AfterFilter();
        }

        public WriteableBitmap GetCopy()
        {
            return _copy;
        }

        public WriteableBitmap GetBitmap()
        {
            // Return the processed bitmap
            return _source;
        }

        protected void BeforeFilter()
        {
            System.Runtime.InteropServices.Marshal.Copy(_address, _buffer, 0, _buffer.Length);
        }
        protected void AfterFilter()
        {
            System.Runtime.InteropServices.Marshal.Copy(_buffer, 0, _address, _buffer.Length);
        }
    }
}
