using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using util;
using System.Collections.Specialized;
using System.Windows.Media.Imaging;
using System.Threading;

namespace Portfolio
{
    public class ImageViewModel : INotifyPropertyChanged
    {
        private C_OpCVImage COpCVImage;

        private ImageModel _imageModel;
        public DelegateCommand _OpenfileCommand { get; private set; }
        public DelegateCommand _MoveCommand { get; private set; }
        public ImageViewModel()
        {
            COpCVImage = new C_OpCVImage();
            _imageModel = new ImageModel();
            this._OpenfileCommand = new DelegateCommand(OpenFile);
            this._MoveCommand = new DelegateCommand(Move);
        }

        public BitmapSource Souce
        {
            get { return _imageModel.Souce; }
            set 
            { 
                _imageModel.Souce = value;
                OnPropertyChanged("souce");
            }

        }

        void Move(object Arrow)
        {
            switch(Arrow.ToString())
            {
                case "Left":
                    Souce = COpCVImage.MoveImage(Souce, C_OpCVImage.Arrow.Left);
                    break;
                case "Right":
                    Souce = COpCVImage.MoveImage(Souce, C_OpCVImage.Arrow.Right);
                    break;
                case "Top":
                    Souce = COpCVImage.MoveImage(Souce, C_OpCVImage.Arrow.Top);
                    break;
                case "Bottom":
                    Souce = COpCVImage.MoveImage(Souce, C_OpCVImage.Arrow.Bottom);
                    break;
                case "Up_Left":
                    Souce = COpCVImage.MoveImage(Souce, C_OpCVImage.Arrow.Up_Left);
                    break;
                case "Up_Right":
                    Souce = COpCVImage.MoveImage(Souce, C_OpCVImage.Arrow.Up_Right);
                    break;
                case "Down_Left":
                    Souce = COpCVImage.MoveImage(Souce, C_OpCVImage.Arrow.Down_Left);
                    break;
                case "Down_Right":
                    Souce = COpCVImage.MoveImage(Souce, C_OpCVImage.Arrow.Down_Right);
                    break;
                case "Expansion":
                    Souce = COpCVImage.MoveImage(Souce, C_OpCVImage.Arrow.Expansion);
                    break;
                case "Reduction":
                    Souce = COpCVImage.MoveImage(Souce, C_OpCVImage.Arrow.Reduction);
                    break;
            }
            GC.Collect();
        }

        void OpenFile(object x)
        {
            Souce = COpCVImage.OpenImage();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) 
        { 
            var handler = PropertyChanged;

            if (handler != null)
            { 
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
