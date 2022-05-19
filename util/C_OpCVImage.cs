using Microsoft.Win32;
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace util
{
    public class C_OpCVImage
    {
        public enum Arrow
        {
            Left,
            Right,
            Top,
            Bottom,
            Up_Left,//↖
            Up_Right,//↗
            Down_Right,//↘
            Down_Left,//↙
            Expansion,//확대
            Reduction//축소
        }

        private Mat _Src = new Mat();
        private int _Width = 0;
        private int _Height = 0;
        private double _fx = 1;
        private List<Point2f> srcpoint2 = new List<Point2f>();
        int LR_move = 0;
        int TB_move = 0;

        public BitmapSource OpenImage()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a picture";
            openFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png;";

            if (openFileDialog.ShowDialog() == true)
            {
                var src = Cv2.ImRead(openFileDialog.FileName, ImreadModes.Unchanged);
                _Width = src.Width;
                _Height = src.Height;
                if(srcpoint2.Count != 0)
                {
                    srcpoint2.Clear();
                }
                srcpoint2.Add(new Point2f(0, 0));
                srcpoint2.Add(new Point2f(0, _Width));
                srcpoint2.Add(new Point2f(_Height, 0));
                srcpoint2.Add(new Point2f(_Width, _Height));
                LR_move = 0;
                TB_move = 0;
                _fx = 1;
                src.CopyTo(_Src);
                return BitmapSourceConverter.ToBitmapSource(_Src);
            }
            return null;
        }

        public BitmapSource MoveImage(BitmapSource image, Arrow arrow)
        {
            var dst = new Mat();

            switch (arrow)
            {
                case Arrow.Left:
                    {
                        LR_move -= 5;
                        Move(dst);
                    }
                    break;
                case Arrow.Right:
                    {
                        LR_move += 5;
                        Move(dst);
                    }
                    break;
                case Arrow.Top:
                    {
                        TB_move -= 5;
                        Move(dst);
                    }
                    break;
                case Arrow.Bottom:
                    {
                        TB_move += 5;
                        Move(dst);
                    }
                    break;
                case Arrow.Up_Left:
                    {
                        LR_move -= 5;
                        TB_move -= 5;
                        Move(dst);
                    }
                    break;
                case Arrow.Up_Right:
                    {
                        LR_move += 5;
                        TB_move -= 5;
                        Move(dst);
                    }
                    break;
                case Arrow.Down_Right:
                    {
                        LR_move += 5;
                        TB_move += 5;
                        Move(dst);
                    }
                    break;
                case Arrow.Down_Left:
                    {
                        LR_move -= 5;
                        TB_move += 5;
                        Move(dst);
                    }
                    break;
                case Arrow.Expansion:
                    {
                        _Width = _Width / 2;
                        _Height = _Height / 2;
                        _fx = _fx / 2;

                        if (_Width == 0 || _Height == 0) return null;

                        Resize(dst);
                        Move(dst);
                    }
                    break;
                case Arrow.Reduction:
                    {
                        _Width = _Width * 2;
                        _Height = _Height * 2;
                        _fx = _fx * 2;

                        if (_fx > 5) return null;

                        Resize(dst);
                        Move(dst);
                    }
                    break;
            }
            return BitmapSourceConverter.ToBitmapSource(dst);
        }

        void Resize(Mat dst)
        {
            Cv2.Resize(_Src, dst, new Size(_Width, _Height), _fx, _fx);
        }

        void Move(Mat dst)
        {
            List<Point2f> dstpoint2 = new List<Point2f>();
            dstpoint2.Add(new Point2f(srcpoint2[0].X + LR_move, srcpoint2[0].Y + TB_move));
            dstpoint2.Add(new Point2f(srcpoint2[1].X + LR_move, srcpoint2[1].Y + TB_move));
            dstpoint2.Add(new Point2f(srcpoint2[2].X + LR_move, srcpoint2[2].Y + TB_move));
            dstpoint2.Add(new Point2f(srcpoint2[3].X + LR_move, srcpoint2[3].Y + TB_move));
            Mat perspectivematrix = Cv2.GetPerspectiveTransform(srcpoint2, dstpoint2);
            Cv2.WarpPerspective(_Src, dst, perspectivematrix, new Size(_Width, _Height));
        }
    }
}
