using System.ComponentModel;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace Portfolio
{
    public class ImageModel
    {
        private BitmapSource souce;

        public BitmapSource Souce
        {
            get
            {
                return souce;
            }
            set
            {
                souce = value;
            }
        }
    }
}