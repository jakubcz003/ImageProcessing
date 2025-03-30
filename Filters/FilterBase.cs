using Avalonia.Controls;
using Avalonia.Media.Imaging;
using System;
using System.IO;
using System.Threading.Tasks;
using GimpClone.Models;

namespace GimpClone.Filters
{
    public abstract class FilterBase
    {

        protected ImageModel _source;
        protected ImageModel _copy;

        public FilterBase(ImageModel image, ImageModel originalImage)
        {
            _source = image;
            _copy = originalImage;
        }

        public abstract void Transform();

        public ImageModel GetCopy()
        {
            return _copy;
        }


        protected void BeforeFilter()
        {
            System.Runtime.InteropServices.Marshal.Copy(_source.Address, _source.Buffer, 0, _source.Buffer.Length);
        }
        protected void AfterFilter()
        {
            System.Runtime.InteropServices.Marshal.Copy(_source.Buffer, 0, _source.Address, _source.Buffer.Length);
        }
    }
}
