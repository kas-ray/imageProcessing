using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime;
using System.Numerics;


namespace lab1sem2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {

        public System.Collections.Generic.List<System.Windows.Point> listPoint = new System.Collections.Generic.List<System.Windows.Point>();
        //public double[,] MatrixLab4 = new double[,] { { }};
        public List<List<double>> MatrixLab4 = new List<List<double>>();

        public MainWindow()
        {
            InitializeComponent();
            SelectImage1.SelectedIndex = 5;
            SelectImage2.SelectedIndex = 1;
            System.Windows.Point point = new System.Windows.Point(0, 255);
            listPoint.Add(point);

        }
        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
        public BitmapImage ConvertImg(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    
        public void SaveImage(Bitmap img_out, string name, bool needGist, bool furi = false)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = name;
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "(*.jpg)|*.jpg";

            if (dlg.ShowDialog() == true)
            {
                img_out.Save(dlg.FileName);

            }
            if (needGist == true)
            {
                MakeGisto(img_out);
                //Image_Out.Source = this.ConvertImg(img_out);
            }
            if(!furi)
                Image_Out.Source = this.ConvertImg(img_out);
            else
                ImageGisto.Source = this.ConvertImg(img_out);
        }

        

        public void MakeGisto(Bitmap img1)
        {
            int[] arrImg = new int[256];
            
            int img_W = img1.Width;
            int img_H = img1.Height;
            var rect = new Bitmap(256, 256);
            var rectGr = Graphics.FromImage(rect);
            rectGr.Clear(System.Drawing.Color.FromArgb(0x38,0x3B,0x40));
            int avver;
            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Plum, 2);

            for (int i = 0; i < img_H - 1; ++i)
                for (int j = 0; j < img_W - 1; ++j)
                {
                    avver = (int)((img1.GetPixel(j, i).R + img1.GetPixel(j, i).G + img1.GetPixel(j, i).B) / 3);
                    arrImg[avver]++;
                }
            int max = arrImg.Max();
            double k = (double)rect.Height / (double)max;
            for (int i = 0; i < 255; ++i)
            {
                System.Drawing.Point beg = new System.Drawing.Point(i, rect.Height - 1);
                System.Drawing.Point end = new System.Drawing.Point(i, rect.Height - 1 - (int)(arrImg[i] * k));
                rectGr.DrawLine(myPen, beg, end);
            }

            ImageGisto.Source = this.ConvertImg(rect);

            rect.Dispose();
            rectGr.Dispose();
                 
        }
        public void ImageChoice(ComboBox select, System.Windows.Controls.Image image)
        {
            string uriString = "pack://application:,,,/images/img" + (select.SelectedIndex + 1).ToString() + ".jpg";
            image.Source = new BitmapImage(new Uri(uriString));
        }

        private void ComboBox_SelectImage1(object sender, SelectionChangedEventArgs e)
        {
            ImageChoice(SelectImage1, Image_1);
        }

        private void ComboBox_SelectImage2(object sender, SelectionChangedEventArgs e)
        {
            ImageChoice(SelectImage2, Image_2);
        }

        public System.Drawing.Color RGB(System.Drawing.Color pix, int r, int g, int b)
        {

            pix = (SelectRGB.SelectedIndex == 0) ? System.Drawing.Color.FromArgb(r, g, b) :
                  (SelectRGB.SelectedIndex == 1) ? System.Drawing.Color.FromArgb(r, 0, 0) :
                  (SelectRGB.SelectedIndex == 2) ? System.Drawing.Color.FromArgb(0, g, 0) :
                  (SelectRGB.SelectedIndex == 3) ? System.Drawing.Color.FromArgb(0, 0, b) :
                  (SelectRGB.SelectedIndex == 4) ? System.Drawing.Color.FromArgb(r, g, 0) :
                  (SelectRGB.SelectedIndex == 5) ? System.Drawing.Color.FromArgb(r, 0, b) :
                  System.Drawing.Color.FromArgb(0, g, b);
            return pix;
        }
        public void Do_PixMagic(Bitmap img1, Bitmap img2, int img_W, int img_H, int ch)
        {
            var rect1 = new Bitmap(img_W, img_H);
            var rect2 = new Bitmap(img_W, img_H);
            using (var img_out1 = Graphics.FromImage(rect1))
            {
                var img_out2 = Graphics.FromImage(rect2);
                img_out1.DrawImage(img1, 0, 0, img_W, img_H);
                img_out2.DrawImage(img2, 0, 0, img_W, img_H);

                for (int i = 0; i < img_H; ++i)
                    for (int j = 0; j < img_W; ++j)
                    {
                        var pix1 = rect1.GetPixel(j, i);
                        var pix2 = rect2.GetPixel(j, i);

                        int r = pix1.R;
                        int g = pix1.G;
                        int b = pix1.B;

                        switch (ch)
                        {
                            case 0:
                                r = (int)Clamp((r + pix2.R), 0, 255);
                                g = (int)Clamp((g + pix2.G), 0, 255);
                                b = (int)Clamp((b + pix2.B), 0, 255);
                                break;
                            case 1:
                                r = (int)Clamp((r + pix2.R) / 2, 0, 255);
                                g = (int)Clamp((g + pix2.G) / 2, 0, 255);
                                b = (int)Clamp((b + pix2.B) / 2, 0, 255);
                                break;
                            case 2:
                                r = Math.Min(r, pix2.R);
                                g = Math.Min(g, pix2.G);
                                b = Math.Min(b, pix2.B);
                                break;
                            case 3:
                                r = Math.Max(r, pix2.R);
                                g = Math.Max(g, pix2.G);
                                b = Math.Max(b, pix2.B);

                                break;
                        }


                        pix1 = RGB(pix1, r, g, b);
                        rect1.SetPixel(j, i, pix1);
                    }

                SaveImage(rect1, "img_out", true);
                img_out2.Dispose();
                rect2.Dispose();
                rect1.Dispose();

            }
        }
        private void Pix_Sum(object sender, RoutedEventArgs e)
        {
            string path = "..\\..\\images\\" + "img" + (SelectImage1.SelectedIndex + 1).ToString() + ".jpg";
            using (var img1 = new Bitmap(path))
            {
                path = "..\\..\\images\\" + "img" + (SelectImage2.SelectedIndex + 1).ToString() + ".jpg";
                using (var img2 = new Bitmap(path))
                {
                    int img_H = Math.Max(img1.Height, img2.Height);
                    int img_W = Math.Max(img1.Width, img2.Width);
                    Do_PixMagic(img1, img2, img_W, img_H, 0);
                }
            }
        }

        private void Pix_Average(object sender, RoutedEventArgs e)
        {
            string path = "..\\..\\images\\" + "img" + (SelectImage1.SelectedIndex + 1).ToString() + ".jpg";
            using (var img1 = new Bitmap(path))
            {
                path = "..\\..\\images\\" + "img" + (SelectImage2.SelectedIndex + 1).ToString() + ".jpg";
                using (var img2 = new Bitmap(path))
                {
                    int img_H = Math.Max(img1.Height, img2.Height);
                    int img_W = Math.Max(img1.Width, img2.Width);
                    Do_PixMagic(img1, img2, img_W, img_H, 1);
                }
            }
        }


        private void Pix_Min(object sender, RoutedEventArgs e)
        {
            string path = "..\\..\\images\\" + "img" + (SelectImage1.SelectedIndex + 1).ToString() + ".jpg";
            using (var img1 = new Bitmap(path))
            {
                path = "..\\..\\images\\" + "img" + (SelectImage2.SelectedIndex + 1).ToString() + ".jpg";
                using (var img2 = new Bitmap(path))
                {
                    int img_H = Math.Max(img1.Height, img2.Height);
                    int img_W = Math.Max(img1.Width, img2.Width);
                    Do_PixMagic(img1, img2, img_W, img_H, 2);
                }
            }
        }
        private void Pix_Max(object sender, RoutedEventArgs e)
        {
            string path = "..\\..\\images\\" + "img" + (SelectImage1.SelectedIndex + 1).ToString() + ".jpg";
            using (var img1 = new Bitmap(path))
            {
                path = "..\\..\\images\\" + "img" + (SelectImage2.SelectedIndex + 1).ToString() + ".jpg";
                using (var img2 = new Bitmap(path))
                {
                    int img_H = Math.Max(img1.Height, img2.Height);
                    int img_W = Math.Max(img1.Width, img2.Width);
                    Do_PixMagic(img1, img2, img_W, img_H, 3);
                }
            }
        }
        private void Pix_Mask(object sender, RoutedEventArgs e)
        {
            string path = "..\\..\\images\\" + "img" + (SelectImage1.SelectedIndex + 1).ToString() + ".jpg";
            using (var img1 = new Bitmap(path))
            {
                int img_W = img1.Width;
                int img_H = img1.Height;
                int mini = Math.Min(img_H, img_W);
                var rect = new Bitmap(img_W, img_H);
                using (var gr = Graphics.FromImage(rect))
                {
                    RectangleF size = new RectangleF(img_W / 2 - mini / 4, img_H / 2 - mini / 4, mini / 2, mini / 2);
                    gr.Clear(System.Drawing.Color.White);
                    switch (SelectMask.SelectedIndex)
                    {
                        case 0:
                            gr.FillEllipse(System.Drawing.Brushes.Black, size);
                            break;
                        case 1:
                            gr.FillRectangle(System.Drawing.Brushes.Black, size);
                            break;
                        case 2:
                            gr.FillRectangle(System.Drawing.Brushes.Black, img_W / 2 - img_W / 6, img_H / 4, img_W / 3, img_H / 2);
                            break;
                    }


                    for (int i = 0; i < img_H; ++i)
                        for (int j = 0; j < img_W; ++j)
                        {
                            var pix1 = img1.GetPixel(j, i);
                            var pix2 = rect.GetPixel(j, i);

                            int r = pix1.R;
                            int g = pix1.G;
                            int b = pix1.B;

                            r = (int)Clamp(r * (pix2.R + 1), 0, 255);
                            g = (int)Clamp(g * (pix2.G + 1), 0, 255);
                            b = (int)Clamp(b * (pix2.B + 1), 0, 255);

                            pix1 = RGB(pix1, r, g, b);
                            rect.SetPixel(j, i, pix1);

                        }
                    SaveImage(rect, "img_out", true);
                    rect.Dispose();
                }
            }
        }
        public void Make_Line(double x1, double y1, double x2, double y2)
        {
            var line = new System.Windows.Shapes.Line()
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = System.Windows.Media.Brushes.Plum,
                StrokeThickness = 1
            };
            
            Graphic.Children.Add(line);
        }
        private void ImageGisto_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var l in Graphic.Children.OfType<System.Windows.Shapes.Line > ().ToList())
            {
                Graphic.Children.Remove(l);
            }

            System.Windows.Point point = e.GetPosition(Graphic);
            listPoint.Add(point);
            listPoint = listPoint.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
            double x = point.X - 4, y = point.Y - 4;

            var pointG = new System.Windows.Shapes.Rectangle()
            {
                Fill = System.Windows.Media.Brushes.Plum,
                Height = 8, Width = 8                           
            };
            pointG.AllowDrop = true;
            Graphic.Children.Add(pointG);
            
            Canvas.SetLeft(pointG, x);
            Canvas.SetTop(pointG, y);

            System.Windows.Point pointEnd = new System.Windows.Point(255,0);
            listPoint.Add(pointEnd);

            for (int i = 0; i < listPoint.Count - 1; i++)
                Make_Line(listPoint[i].X, listPoint[i].Y, listPoint[i + 1].X, listPoint[i + 1].Y);

            string path = "..\\..\\images\\" + "img" + (SelectImage1.SelectedIndex + 1).ToString() + ".jpg";
            using (var img1 = new Bitmap(path))
            {
                int img_W = img1.Width;
                int img_H = img1.Height;
                var rect = new Bitmap(img_W, img_H);

                for (int i = 0; i < img_H - 1; ++i)
                    for (int j = 0; j < img_W - 1; ++j)
                    {
                        var pix1 = img1.GetPixel(j, i);
                        
                        int r = pix1.R;
                        int g = pix1.G;
                        int b = pix1.B;

                        for (int t = 0; t < listPoint.Count - 1; t++)
                        {
                            if ((r <= listPoint[t + 1].X) && (r >= listPoint[t].X))
                            {
                                r = (int)Clamp((((r - listPoint[t].X) * ((255 - listPoint[t + 1].Y) - (255 - listPoint[t].Y))) / (listPoint[t + 1].X - listPoint[t].X) + (255 - listPoint[t].Y)), 0, 255); 
                            }
                            if ((g <= listPoint[t + 1].X) && (g >= listPoint[t].X))
                            {
                                g = (int)Clamp((((g - listPoint[t].X) * ((255 - listPoint[t + 1].Y) - (255 - listPoint[t].Y))) / (listPoint[t + 1].X - listPoint[t].X) + (255 - listPoint[t].Y)), 0, 255);
                            }
                            if ((b <= listPoint[t + 1].X) && (b >= listPoint[t].X))
                            {
                                b = (int)Clamp((((b - listPoint[t].X) * ((255 - listPoint[t + 1].Y) - (255 - listPoint[t].Y))) / (listPoint[t + 1].X - listPoint[t].X) + (255 - listPoint[t].Y)), 0, 255);
                            }
                        }

                        pix1 = System.Drawing.Color.FromArgb(r, g, b);
                        rect.SetPixel(j, i, pix1);

                    }
                MakeGisto(rect);
                Image_Out.Source = this.ConvertImg(rect);
                rect.Dispose();
                MessageBox.Show("READYYYY");
            }

            listPoint.Remove(listPoint.Last());
        }

        private void Graph_Clear(object sender, RoutedEventArgs e)
        {
            foreach (var l in Graphic.Children.OfType<System.Windows.Shapes.Line>().ToList())
            {
                Graphic.Children.Remove(l);
            }
            foreach (var p in Graphic.Children.OfType<System.Windows.Shapes.Rectangle>().ToList())
            {
                Graphic.Children.Remove(p);
            }
            listPoint.Clear();
            System.Windows.Point point = new System.Windows.Point(0, 255);
            listPoint.Add(point);
            var myBord= new System.Windows.Shapes.Rectangle()
            {
                Height = 255,
                Width = 255,
                Stroke = System.Windows.Media.Brushes.Gray,
                StrokeThickness = 1
            };
            
            Graphic.Children.Add(myBord);
        }

        /**/
        public Bitmap GrayGrad(Bitmap img, int W, int H)
        {
            for (int i = 0; i < H - 1; ++i)
                for (int j = 0; j < W - 1; ++j)
                {
                    var pix = img.GetPixel(j, i);
                    int r = pix.R;
                    int g = pix.G;
                    int b = pix.B;
                    r = (int)Clamp(0.2125 * r, 0, 255);
                    g = (int)Clamp(0.7154 * g, 0, 255);
                    b = (int)Clamp(0.0721 * b, 0, 255);

                    pix = RGB(pix, r, g, b);
                    img.SetPixel(j, i, pix);
                }
            return img;
        }
        public double MathExpect(int w, int h, int win, int i, int j, int[,] integ)
        {
            int side1 = (i >= 0 && i <= win / 2) ? win / 2 + 1 + i : (i >= h - (win / 2) && i <= h) ? win / 2 + (h - i) + 1 : win,
                side2 = (j >= 0 && j <= win / 2) ? win / 2 + j + 1 : (j >= w - (win / 2) && j <= w) ? win / 2 + (w - j) + 1 : win,
                S = side2 * side1,
                x1 = (j - win / 2) < 0 ? 0 : j - win / 2,
                y1 = (i - win / 2) < 0 ? 0 : i - win / 2,
                x2 = (j + win / 2) >= w ? w - 1 : j + win / 2,
                y2 = (i + win / 2) >= h ? h - 1 : i + win / 2,
                sum = (x1 == 0) ? (y1 == 0 ? integ[x2, y2] : integ[x2, y2] - integ[x2, y1 - 1]) : 
                      (y1 == 0 ? integ[x2, y2] - integ[x1 - 1, y2] : 
                      integ[x2, y2] + integ[x1 - 1, y1 - 1] - integ[x1 - 1, y2] - integ[x2, y1 - 1]);

            sum = (sum < 0) ? sum * -1 : sum;

            return sum / S;
        }

        public int [,] Integ (int h, int w, int power, Bitmap img)
        {
            int[,] integ = new int[w, h];
            for (int i = 0; i < h; ++i)
                for (int j = 0; j < w; ++j)
                {
                    int g = img.GetPixel(j, i).G;
                    integ[j, i] = (i == 0 && j == 0) ? (int)Math.Pow(g, power) : j == 0 ? 
                        (int)Math.Pow(g, power) + integ[j, i - 1] :
                        i == 0 ? (int)Math.Pow(g, power) + integ[j - 1, i] :
                        (int)Math.Pow(g, power) + integ[j - 1, i] + integ[j, i - 1] - integ[j - 1, i - 1];
                }           
                    return integ;
        }

        private void Gavrilov(object sender, RoutedEventArgs e)
        {
            string path = "..\\..\\images\\" + "img" + (SelectImage1.SelectedIndex + 1).ToString() + ".jpg";
            using (var img1 = new Bitmap(path))
            {
                int img_W = img1.Width,
                    img_H = img1.Height;
                var rect = new Bitmap(img_W, img_H);
                int sum = 0;
                

                for (int i = 0; i < img_H - 1; ++i)
                    for (int j = 0; j < img_W - 1; ++j)
                        sum += img1.GetPixel(j, i).G;

                double t = sum / (img_H * img_W);

                for (int i = 0; i < img_H - 1; ++i)
                    for (int j = 0; j < img_W - 1; ++j)
                    {                     
                        int r = img1.GetPixel(j, i).R,
                            g = img1.GetPixel(j, i).G,
                            b = img1.GetPixel(j, i).B;
                        if (g <= t)
                        {
                            r = 0; g = 0; b = 0;
                        }
                        else
                        {
                            r = 255; g = 255; b = 255;
                        }

                        rect.SetPixel(j, i, RGB(img1.GetPixel(j,i), r, g, b));
                    }
                SaveImage(rect, "img_out", NeedGist.IsChecked.Value);
                rect.Dispose();
            }

        }

        private void Otsu(object sender, RoutedEventArgs e)
        {
            string path = "..\\..\\images\\" + "img" + (SelectImage1.SelectedIndex + 1).ToString() + ".jpg";
            using (var img1 = new Bitmap(path))
            {
                int img_W = img1.Width,
                    img_H = img1.Height;
                var rect = new Bitmap(img_W, img_H);

                int t = 0, max = 0, avver;
                double miut = 0, Sigmab = 0;

                double[] arrImg = new double[256];          

                for (int i = 0; i < img_H - 1; ++i)
                    for (int j = 0; j < img_W - 1; ++j)
                    {
                        avver = (int)((img1.GetPixel(j, i).R + img1.GetPixel(j, i).G + img1.GetPixel(j, i).B) / 3);
                        arrImg[avver] += 1;
                    }
               

                double c = arrImg[0] / (img_H * img_W);

                for (int i = 0; i < 255; ++i)
                {
                    arrImg[i] /= img_H * img_W;
                    if (arrImg[i] > c)
                    {
                        c = arrImg[i];
                        max = i;
                    }
                    
                }

                for (int i = 0; i <= max; ++i)
                    miut += arrImg[i] * i;

                for (int i = 0; i < max; ++i)
                {
                    double  miu1 = 0, omega1 = 0;
                    for (int j = 0; j < i - 1; ++j)
                    {
                        omega1 += arrImg[j];
                        miu1 += j * arrImg[j];
                    }                  

                    double omega2 = 1 - omega1,
                           miu2 = (miut - omega1 * miu1) / omega2,
                           Sigma = omega1 * omega2 * (miu1 - miu2) * (miu1 - miu2);
                    if (Sigma > Sigmab)
                    {
                        t = i;
                        Sigmab = Sigma;
                    }
                }

                for (int i = 0; i < img_H - 1; ++i)
                    for (int j = 0; j < img_W - 1; ++j)
                    {
                        int r = img1.GetPixel(j, i).R,
                            g = img1.GetPixel(j, i).G,
                            b = img1.GetPixel(j, i).B;
                        if (g <= t)
                        {
                            r = 0; g = 0; b = 0;
                        }
                        else
                        {
                            r = 255; g = 255; b = 255;
                        }

                        rect.SetPixel(j, i, RGB(img1.GetPixel(j, i), r, g, b));
                    }
                SaveImage(rect, "img_out", NeedGist.IsChecked.Value);
                rect.Dispose();
            }

        }

        private void Niblek(object sender, RoutedEventArgs e)
        {
            string path = "..\\..\\images\\" + "img" + (SelectImage1.SelectedIndex + 1).ToString() + ".jpg";
            using (var img1 = new Bitmap(path))
            {
                int img_W = img1.Width,
                    img_H = img1.Height;
                var rect = new Bitmap(img_W, img_H);
              
                int[,] integ1 = Integ(img_H, img_W, 1, img1),
                       integ2 = Integ(img_H, img_W, 2, img1);

                double sence = Convert.ToDouble(Sence.Text);
                int win = Convert.ToInt32(WinSize.Text);

                for (int i = 0; i < img_H - 1; ++i)
                    for (int j = 0; j < img_W - 1; ++j)
                    {
                        double Mx1 = MathExpect(img_W, img_H, win, i, j, integ1),
                               Mx2 = MathExpect(img_W, img_H, win, i, j, integ2),
                               Dx = Mx2 - Mx1 * Mx1, Sigma = Math.Sqrt(Dx);
                        int r = img1.GetPixel(j, i).R,
                            g = img1.GetPixel(j, i).G,
                            b = img1.GetPixel(j, i).B;
                        if (g <= Mx1 + Sigma * sence)
                        {
                            r = 0; g = 0; b = 0;
                        }
                        else
                        {
                            r = 255; g = 255; b = 255;
                        }
                        rect.SetPixel(j, i, RGB(img1.GetPixel(j, i), r, g, b));
                    }
                SaveImage(rect, "img_out", NeedGist.IsChecked.Value);
                rect.Dispose();
            }

        }

        private void Sauvol(object sender, RoutedEventArgs e)
        {
            string path = "..\\..\\images\\" + "img" + (SelectImage1.SelectedIndex + 1).ToString() + ".jpg";
            using (var img1 = new Bitmap(path))
            {
                int img_W = img1.Width,
                    img_H = img1.Height;
                var rect = new Bitmap(img_W, img_H);
               
                int[,] integ1 = Integ(img_H, img_W, 1, img1),
                       integ2 = Integ(img_H, img_W, 2, img1);

                double sence = Convert.ToDouble(Sence.Text);
                int win = Convert.ToInt32(WinSize.Text);

                for (int i = 0; i < img_H - 1; ++i)
                    for (int j = 0; j < img_W - 1; ++j)
                    {
                        double Mx1 = MathExpect(img_W, img_H, win, i, j, integ1),
                               Mx2 = MathExpect(img_W, img_H, win, i, j, integ2),
                               Dx = Mx2 - Mx1 * Mx1, Sigma = Math.Sqrt(Dx);
                        int r = img1.GetPixel(j, i).R,
                            g = img1.GetPixel(j, i).G,
                            b = img1.GetPixel(j, i).B;
                        if (g <= Mx1 * (1 + sence * (Sigma / 128 - 1)))
                        {
                            r = 0; g = 0; b = 0;
                        }
                        else
                        {
                            r = 255; g = 255; b = 255;
                        }
                        rect.SetPixel(j, i, RGB(img1.GetPixel(j, i), r, g, b));
                    }
                SaveImage(rect, "img_out", NeedGist.IsChecked.Value);
                rect.Dispose();
            }
        }

        private void Wolf(object sender, RoutedEventArgs e)
        {
            string path = "..\\..\\images\\" + "img" + (SelectImage1.SelectedIndex + 1).ToString() + ".jpg";
            using (var img1 = new Bitmap(path))
            {
                int img_W = img1.Width,
                    img_H = img1.Height;
                var rect = new Bitmap(img_W, img_H);

                int[,] integ1 = Integ(img_H, img_W, 1, img1),
                       integ2 = Integ(img_H, img_W, 2, img1);

                double sence = Convert.ToDouble(Sence.Text);
                int win = Convert.ToInt32(WinSize.Text);

                double[,] Mx1 = new double[img_W, img_H], 
                          Sigma = new double[img_W, img_H];
                
                int min = 256;
                double max = 0;

                for (int i = 0; i < img_H - 1; ++i)
                    for (int j = 0; j < img_W - 1; ++j)
                    {
                        min = min > img1.GetPixel(j, i).G ? img1.GetPixel(j, i).G : min;
                        Mx1[j, i] = MathExpect(img_W, img_H, win, i, j, integ1);
                        double Mx2 = MathExpect(img_W, img_H, win, i, j, integ2),
                               Dx = Mx2 - Mx1[j, i] * Mx1[j, i];
                        Sigma[j, i] = Math.Sqrt(Dx);
                        max = max < Sigma[j, i] ? Sigma[j, i] : max;
                    }

                for (int i = 0; i < img_H - 1; ++i)
                    for (int j = 0; j < img_W - 1; ++j)
                    {
                        int r = img1.GetPixel(j, i).R,
                            g = img1.GetPixel(j, i).G,
                            b = img1.GetPixel(j, i).B;
                        if (g <= (1 - sence) * Mx1[j, i] + sence * min + sence * Sigma[j, i] * (Mx1[j, i] - min) / max)
                        {
                            r = 0; g = 0; b = 0;
                        }
                        else
                        {
                            r = 255; g = 255; b = 255;
                        }
                        rect.SetPixel(j, i, RGB(img1.GetPixel(j, i), r, g, b));
                    }
                SaveImage(rect, "img_out", NeedGist.IsChecked.Value);
                rect.Dispose();
            }
        }

        private void Bredly(object sender, RoutedEventArgs e)
        {
            string path = "..\\..\\images\\" + "img" + (SelectImage1.SelectedIndex + 1).ToString() + ".jpg";
            using (var img1 = new Bitmap(path))
            {
                int img_W = img1.Width,
                    img_H = img1.Height;
                var rect = new Bitmap(img_W, img_H);
                double sence = Convert.ToDouble(Sence.Text);
                int win = Convert.ToInt32(WinSize.Text);

                int[,] integ = Integ(img_H, img_W, 1, img1);

                for (int i = 0; i < img_H - 1; ++i)
                    for (int j = 0; j < img_W - 1; ++j)
                    {
                        int r = img1.GetPixel(j, i).R,
                            g = img1.GetPixel(j, i).G,
                            b = img1.GetPixel(j, i).B;
                      //  MessageBox.Show(img1.GetPixel(j, i).R.ToString());
                        if (g <= (1 - sence) * MathExpect(img_W, img_H, win, i, j, integ))
                        {
                            r = 0; g = 0; b = 0;
                        }
                        else
                        {
                            r = 255; g = 255; b = 255;
                        }
                        rect.SetPixel(j, i, RGB(img1.GetPixel(j, i), r, g, b));
                    }
                SaveImage(rect, "img_out", NeedGist.IsChecked.Value);
                rect.Dispose();
            }
        }

        private void WinSize_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = e.Text == "," || e.Text == "-" ? false: !(Char.IsNumber(e.Text, 0));
        }
        private void MatrixText(object sender, TextCompositionEventArgs e)
        {
            e.Handled = e.Text == "," || e.Text == "-" || e.Text == " " || e.Text == "\n" ? false : !(Char.IsNumber(e.Text, 0));
        }

        private void Make_Matrix(object sender, RoutedEventArgs e)
        {
            string[] mat = MyMatrix.Text.Split('\n');
            for (int i = 0; i < mat.Length; i++)
            {
                MatrixLab4.Add(new List<double>());
                string[] mat2 = mat[i].Split(' ');
                for (int j = 0; j < mat2.Length; j++)
                    MatrixLab4[i].Add(Convert.ToDouble(mat2[j]));                    
            }

            string path = "..\\..\\images\\" + "img" + (SelectImage1.SelectedIndex + 1).ToString() + ".jpg";
            using (var img1 = new Bitmap(path))
            {
                int img_W = img1.Width,
                    img_H = img1.Height,
                    Mat_H = MatrixLab4.Count,
                    Mat_W = MatrixLab4[0].Count,
                    R_H = MatrixLab4.Count / 2,
                    R_W = MatrixLab4[0].Count / 2;
                var rect = new Bitmap(img_W, img_H);
                for (int i = 0; i < img_H - 1; ++i)
                    for (int j = 0; j < img_W - 1; ++j)
                    {
                        int r = 0,
                            g = 0,
                            b = 0;
                        for (int Mi = 0; Mi < Mat_H; Mi++)
                            for(int Mj = 0; Mj < Mat_W; Mj++)
                            {
                                int //соответствующий пиксель картинки
                                    Cor_i = R_H > Mi ? i - R_H + Mi : (R_H < Mi ? i - R_H + Mi -1 : i),
                                    Cor_j = R_W > Mj ? j - R_W + Mj : (R_W < Mj ? j - R_W + Mj -1 : j),
                                    //пиксель с заменой/без
                                    trueImgi = (i < R_H) && (Cor_i < 0) || (img_H - R_H - 1 < i) && (Cor_i > img_H) ? Cor_i * (-1) : Cor_i,
                                    trueImgj = (j < R_W) && (Cor_j < 0) || (img_W - R_W - 1 < j) && (Cor_j > img_W) ? Cor_j * (-1) : Cor_j;

                                r += (int)(img1.GetPixel(trueImgj, trueImgi).R * MatrixLab4[Mi][Mj]);
                                g += (int)(img1.GetPixel(trueImgj, trueImgi).G * MatrixLab4[Mi][Mj]);
                                b += (int)(img1.GetPixel(trueImgj, trueImgi).B * MatrixLab4[Mi][Mj]);            
                            }
                      
                        rect.SetPixel(j, i, RGB(img1.GetPixel(j, i),Clamp(r, 0, 255), Clamp(g, 0, 255), Clamp(b, 0, 255)));
                    }
                SaveImage(rect, "img_out", NeedGist.IsChecked.Value);
                rect.Dispose();
            }
            MatrixLab4.Clear();
        }
        static List<int> Swap(List<int> arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
            return arr;
        }
        static int Partition(List<int> arr, int l, int r)
        {
            int lst = arr[r], i = l, j = l;
            while (j < r)
            {
                if (arr[j] < lst)
                {
                    arr = Swap(arr, i, j);
                    i++;
                }
                j++;
            }
            arr = Swap(arr, i, r);
            return i;
        }

        static int randomPartition(List<int> arr, int l, int r)
        {
            int n = r - l + 1;
            int pivot = (int)(new Random().Next() % n);
            arr = Swap(arr, l + pivot, r);
            return Partition(arr, l, r);
        }

        static int MedianUtil(List<int> arr, int l, int r, int k, int a = -1, int b = -1)
        {
            if (l <= r)
            {
                int partitionIndex = randomPartition(arr, l, r);
                b = partitionIndex == k ? arr[partitionIndex] : partitionIndex >= k ?
                       MedianUtil(arr, l, partitionIndex - 1, k, a, b) : 
                       MedianUtil(arr, partitionIndex + 1, r, k, a, b);
            }
            return b;
        }

        private void Median(object sender, RoutedEventArgs e)
        {
            int M_R = Convert.ToInt32(WinSize.Text);
            int SizeMat = M_R * 2 + 1;
            string path = "..\\..\\images\\" + "img" + (SelectImage1.SelectedIndex + 1).ToString() + ".jpg";
            using (var img1 = new Bitmap(path))
            {
                int img_W = img1.Width,
                    img_H = img1.Height;
                var rect = new Bitmap(img_W, img_H);
                for (int i = 0; i < img_H; ++i)
                    for (int j = 0; j < img_W; ++j)
                    {                       
                        int[,] trueArr = new int[SizeMat, SizeMat];
                        int Cor_i, Cor_j, trueImgi, trueImgj;
                        for (int Mi = 0; Mi < SizeMat; Mi++)
                            for (int Mj = 0; Mj < SizeMat; Mj++)
                            {
                                //соответствующий пиксель картинки
                                Cor_i = M_R > Mi ? i - M_R + Mi : ((M_R <= Mi) && (i + M_R < img_H) ? i - M_R + Mi : img_H - 1);
                                Cor_j = M_R > Mj ? j - M_R + Mj : ((M_R <= Mj) && (j + M_R < img_W) ? j - M_R + Mj : img_W - 1);
                                //пиксель с заменой/без
                                trueImgi = (i < M_R) && (Cor_i < 0) || (img_H - M_R - 1 < i) && (Cor_i > img_H) ? Cor_i * (-1) : Cor_i;
                                trueImgj = (j < M_R) && (Cor_j < 0) || (img_W - M_R - 1 < j) && (Cor_j > img_W) ? Cor_j * (-1) : Cor_j;
                         
                                 trueArr[Mi, Mj] = (int)((img1.GetPixel(trueImgj, trueImgi).R + img1.GetPixel(trueImgj, trueImgi).G
                                                  + img1.GetPixel(trueImgj, trueImgi).B) / 3);
                            }
                        var arr = trueArr.Cast<int>().ToList();
                        int Medi = MedianUtil(arr, 0, arr.Count() - 1, arr.Count() / 2);                 
                        Medi = trueArr.Cast<int>().ToList().IndexOf(Medi);
                        int res_I = (int)(Medi / SizeMat),
                            res_J = Medi - ((int)(Medi / SizeMat)) * SizeMat;
                        Cor_i = M_R > res_I ? i - M_R + res_I : ((M_R <= res_I) && (i + M_R < img_H) ? i - M_R + res_I : img_H - 1);
                        Cor_j = M_R > res_J ? j - M_R + res_J : ((M_R <= res_J) && (j + M_R < img_W) ? j - M_R + res_J : img_W - 1);
                        trueImgi = (i < M_R) && (Cor_i < 0) || (img_H - M_R - 1 < i) && (Cor_i > img_H) ? Cor_i * (-1) : Cor_i;
                        trueImgj = (j < M_R) && (Cor_j < 0) || (img_W - M_R - 1 < j) && (Cor_j > img_W) ? Cor_j * (-1) : Cor_j;                     
                        int r = img1.GetPixel(trueImgj, trueImgi).R,
                            g = img1.GetPixel(trueImgj, trueImgi).G,
                            b = img1.GetPixel(trueImgj, trueImgi).B;
                        rect.SetPixel(j, i, RGB(img1.GetPixel(j, i), r, g, b));
                    }               
                SaveImage(rect, "img_out", NeedGist.IsChecked.Value);
                rect.Dispose();
            }

        }

        private void SigmaGause(object sender, RoutedEventArgs e)
        {
            int rad = Convert.ToInt32(WinSize.Text);
            double sig = Convert.ToInt32(Sence.Text);
            double s = 0;
            string ready = "";
            for (int i = -rad; i <= rad; i++)
            {
                for (int j = -rad; j <= rad; j++)
                {
                    double g = Math.Round(1 / (2 * 3.1415 * sig * sig) * Math.Exp(-1 * (i * i + j * j) / (2 * sig * sig)), 5);
                    s += g;
                    ready += g.ToString() + " ";
                }
                ready = ready.Trim();
                ready += "\n";
            }
            ready = ready.Trim();
          
            MessageBox.Show(ready);

            string[] mat = ready.Split('\n');
            for (int i = 0; i < mat.Length; i++)
            {
                MatrixLab4.Add(new List<double>());
                string[] mat2 = mat[i].Split(' ');
                for (int j = 0; j < mat2.Length; j++)
                    MatrixLab4[i].Add(Convert.ToDouble(mat2[j]));
            }

            string path = "..\\..\\images\\" + "img" + (SelectImage1.SelectedIndex + 1).ToString() + ".jpg";
            using (var img1 = new Bitmap(path))
            {
                int img_W = img1.Width,
                    img_H = img1.Height,
                    Mat_H = MatrixLab4.Count,
                    Mat_W = MatrixLab4[0].Count,
                    R_H = MatrixLab4.Count / 2,
                    R_W = MatrixLab4[0].Count / 2;
                var rect = new Bitmap(img_W, img_H);
                for (int i = 0; i < img_H - 1; ++i)
                    for (int j = 0; j < img_W - 1; ++j)
                    {
                        int r = 0,
                            g = 0,
                            b = 0;
                        for (int Mi = 0; Mi < Mat_H; Mi++)
                            for (int Mj = 0; Mj < Mat_W; Mj++)
                            {
                                int //соответствующий пиксель картинки
                                     Cor_i = R_H > Mi ? i - R_H + Mi : ((R_H <= Mi) && (i + R_H < img_H) ? i - R_H + Mi : img_H - 1),
                                     Cor_j = R_W > Mj ? j - R_W + Mj : ((R_W <= Mj) && (j + R_W < img_W) ? j - R_W + Mj : img_W - 1),
                                //пиксель с заменой/без
                                     trueImgi = (i < R_H) && (Cor_i < 0) || (img_H - R_H - 1 < i) && (Cor_i > img_H) ? Cor_i * (-1) : Cor_i,
                                     trueImgj = (j < R_W) && (Cor_j < 0) || (img_W - R_W - 1 < j) && (Cor_j > img_W) ? Cor_j * (-1) : Cor_j;
                               

                                r += (int)(img1.GetPixel(trueImgj, trueImgi).R * MatrixLab4[Mi][Mj]);
                                g += (int)(img1.GetPixel(trueImgj, trueImgi).G * MatrixLab4[Mi][Mj]);
                                b += (int)(img1.GetPixel(trueImgj, trueImgi).B * MatrixLab4[Mi][Mj]);
                            }

                        rect.SetPixel(j, i, RGB(img1.GetPixel(j, i), Clamp(r, 0, 255), Clamp(g, 0, 255), Clamp(b, 0, 255)));
                    }
                SaveImage(rect, "img_out", NeedGist.IsChecked.Value);
                rect.Dispose();
            }

                MatrixLab4.Clear();
        }
       
        private double maxR = 0, maxG = 0, maxB = 0;
        private Complex[,] R2 = new Complex[128, 128], 
                           G2 = new Complex[128, 128], 
                           B2 = new Complex[128, 128];

      

        private bool sv = true; 
        private void MaxiFurry()
        {
            for (int i = 0; i < 128; i++)
                for (int j = 0; j < 128; j++)
                {
                    maxR = Math.Log(R2[i, j].Magnitude + 1) > maxR ? Math.Log(R2[i, j].Magnitude + 1) : maxR;
                    maxG = Math.Log(G2[i, j].Magnitude + 1) > maxG ? Math.Log(G2[i, j].Magnitude + 1) : maxG;
                    maxB = Math.Log(B2[i, j].Magnitude + 1) > maxB ? Math.Log(B2[i, j].Magnitude + 1) : maxB;
                }
        }
        private void FuriForm(object sender, RoutedEventArgs e)
        {
            string path = "..\\..\\images\\" + "img" + (SelectImage1.SelectedIndex + 1).ToString() + ".jpg";
            using (var img1 = new Bitmap(new Bitmap(path), new System.Drawing.Size(128, 128)))
            {
                int img_W = img1.Width,
                    img_H = img1.Height;
                Complex[,] R1 = new Complex[128, 128],
                           G1 = new Complex[128, 128],
                           B1 = new Complex[128, 128];
                          
                var rect = new Bitmap(128, 128);

                for (int i = 0; i < img_H; ++i)
                    for (int j = 0; j < img_W; ++j)
                    {
                        for (int m = 0; m < img_W; m++)
                        {
                            var mnim = new Complex(Math.Pow(-1, m + i) * img1.GetPixel(m, i).R, 0);
                            double fi = -2.0 * Math.PI * m * j / 128;
                            R1[i, j] += new Complex(Math.Cos(fi), Math.Sin(fi)) * mnim;
                            mnim = new Complex(Math.Pow(-1, m + i) * img1.GetPixel(m, i).G, 0);
                            G1[i, j] += new Complex(Math.Cos(fi), Math.Sin(fi)) * mnim;
                            mnim = new Complex(Math.Pow(-1, m + i) * img1.GetPixel(m, i).B, 0);
                            B1[i, j] += new Complex(Math.Cos(fi), Math.Sin(fi)) * mnim;
                        }
                        R1[i, j] /= 128;
                        G1[i, j] /= 128;
                        B1[i, j] /= 128;
                    }

                for (int j = 0; j < img_W; j++)
                    for (int u = 0; u < img_H; u++)
                    {
                        for (int n = 0; n < img_H; n++)
                        {
                            double fi = -2 * Math.PI * n * u / 128;
                            R2[u, j] += new Complex(Math.Cos(fi), Math.Sin(fi)) * R1[n, j];
                            G2[u, j] += new Complex(Math.Cos(fi), Math.Sin(fi)) * G1[n, j];
                            B2[u, j] += new Complex(Math.Cos(fi), Math.Sin(fi)) * B1[n, j];
                        }
                        R2[u, j] /= 128;
                        G2[u, j] /= 128;
                        B2[u, j] /= 128;

                    }               
                maxR = 0; maxG = 0; maxB = 0;
                MaxiFurry();                
                for (int i = 0; i < img_H - 1; ++i)
                    for (int j = 0; j < img_W - 1; ++j)
                    {
                        int r = Clamp((int)(Math.Log(R2[i, j].Magnitude + 1) * 255 / maxR), 0, 255),
                            g = Clamp((int)(Math.Log(G2[i, j].Magnitude + 1) * 255 / maxG), 0, 255),
                            b = Clamp((int)(Math.Log(B2[i, j].Magnitude + 1) * 255 / maxB), 0, 255);
                        rect.SetPixel(j, i, RGB(img1.GetPixel(j, i), r, g, b));
                    }
                if (sv) SaveImage(rect, "furi", false, true);
                rect.Dispose();
            }
        }

        private void LowFuri(object sender, RoutedEventArgs e)
        {
            sv = false;
            FuriForm(null, null);
            int param = Convert.ToInt32(WinSize.Text);
            string path = "..\\..\\images\\" + "img" + (SelectImage1.SelectedIndex + 1).ToString() + ".jpg";
            using (var img1 = new Bitmap(new Bitmap(path), new System.Drawing.Size(128, 128)))
            {
                int img_W = img1.Width,
                    img_H = img1.Height;
                Complex[,] R1 = new Complex[128, 128],
                           G1 = new Complex[128, 128],
                           B1 = new Complex[128, 128],
                           dpR = new Complex[128, 128],
                           dpG = new Complex[128, 128],
                           dpB = new Complex[128, 128];
                
                var rectF = new Bitmap(128, 128);
                var rectR = new Bitmap(128, 128);

                for (int i = 0; i < img_H; ++i)
                    for (int j = 0; j < img_W; ++j)                                          
                        for (int m = 0; m < img_W; m++)
                        {
                            if (j == 0)
                            {
                                int r, g, b;
                                (r, g, b) = (i - 64) * (i - 64) + (m - 64) * (m - 64)
                                    <= param * param && (i - 64) * (i - 64) + (m - 64) * (m - 64)
                                    >= (param - 1) * (param - 1) ? (221, 160, 221)
                                    : (i - 64) * (i - 64) + (m - 64) * (m - 64) <= param * param ?
                                    ((int)(Math.Log(R2[i, m].Magnitude + 1) * 255 / maxR),
                                    (int)(Math.Log(G2[i, m].Magnitude + 1) * 255 / maxG),
                                    (int)(Math.Log(B2[i, m].Magnitude + 1) * 255 / maxB)) : (0, 0, 0);
                                (R2[i, m], G2[i, m], B2[i, m]) = (i - 64) * (i - 64) + (m - 64) * (m - 64) >= param * param ?
                                    (0, 0, 0) : (R2[i, m], G2[i, m], B2[i, m]);
                                rectF.SetPixel(m, i, RGB(img1.GetPixel(m, i), r, g, b));
                            }
                            double fi = 2 * Math.PI * m * j / 128;
                            R1[i, j] += new Complex(Math.Cos(fi), Math.Sin(fi)) * R2[i, m];
                            G1[i, j] += new Complex(Math.Cos(fi), Math.Sin(fi)) * G2[i, m];
                            B1[i, j] += new Complex(Math.Cos(fi), Math.Sin(fi)) * B2[i, m];
                        }
                    
                for (int j = 0; j < img_W; j++)
                    for (int u = 0; u < img_H; u++)
                        for (int n = 0; n < img_H; n++)
                        {
                            double fi = 2 * Math.PI * n * u / 128;
                            dpR[u, j] += new Complex(Math.Cos(fi), Math.Sin(fi)) * R1[n, j];
                            dpG[u, j] += new Complex(Math.Cos(fi), Math.Sin(fi)) * G1[n, j];
                            dpB[u, j] += new Complex(Math.Cos(fi), Math.Sin(fi)) * B1[n, j];
                        }

                for (int i = 0; i < img_H; ++i)
                    for (int j = 0; j < img_W; ++j)
                    {
                        int r = Clamp((int)dpR[i, j].Magnitude, 0, 255),
                            g = Clamp((int)dpG[i, j].Magnitude, 0, 255),
                            b = Clamp((int)dpB[i, j].Magnitude, 0, 255);
                        rectR.SetPixel(j, i, RGB(img1.GetPixel(j, i), r, g, b));
                    }

                SaveImage(rectF, "furi", false, true);
                SaveImage(rectR, "img_out", false);
                rectF.Dispose();
                rectR.Dispose();
            }
            sv = true; 
        }
        private void HighFuri(object sender, RoutedEventArgs e)
        {
            sv = false;
            FuriForm(null, null);
            int param = Convert.ToInt32(WinSize.Text);
            string path = "..\\..\\images\\" + "img" + (SelectImage1.SelectedIndex + 1).ToString() + ".jpg";
            using (var img1 = new Bitmap(new Bitmap(path), new System.Drawing.Size(128, 128)))
            {
                int img_W = img1.Width,
                    img_H = img1.Height;
                Complex[,] R1 = new Complex[128, 128],
                           G1 = new Complex[128, 128],
                           B1 = new Complex[128, 128],
                           dpR = new Complex[128, 128],
                           dpG = new Complex[128, 128],
                           dpB = new Complex[128, 128];

                var rectF = new Bitmap(128, 128);
                var rectR = new Bitmap(128, 128);

                for (int i = 0; i < img_H; ++i)
                    for (int j = 0; j < img_W; ++j)
                        for (int m = 0; m < img_W; m++)
                        {
                            if (j == 0)
                            {
                                int r, g, b;
                                (r, g, b) = (i - 64) * (i - 64) + (m - 64) * (m - 64)
                                    <= param * param && (i - 64) * (i - 64) + (m - 64) * (m - 64)
                                    >= (param - 1) * (param - 1) ? (221, 160, 221)
                                    : (i - 64) * (i - 64) + (m - 64) * (m - 64) >= param * param ?
                                    ((int)(Math.Log(R2[i, m].Magnitude + 1) * 255 / maxR),
                                    (int)(Math.Log(G2[i, m].Magnitude + 1) * 255 / maxG),
                                    (int)(Math.Log(B2[i, m].Magnitude + 1) * 255 / maxB)) : (0, 0, 0);
                                (R2[i, m], G2[i, m], B2[i, m]) = (i - 64) * (i - 64) + (m - 64) * (m - 64) <= param * param ?
                                    (0, 0, 0) : (R2[i, m], G2[i, m], B2[i, m]);
                                rectF.SetPixel(m, i, RGB(img1.GetPixel(m, i), r, g, b));
                            }
                            double fi = 2 * Math.PI * m * j / 128;
                            R1[i, j] += new Complex(Math.Cos(fi), Math.Sin(fi)) * R2[i, m];
                            G1[i, j] += new Complex(Math.Cos(fi), Math.Sin(fi)) * G2[i, m];
                            B1[i, j] += new Complex(Math.Cos(fi), Math.Sin(fi)) * B2[i, m];
                        }

                for (int j = 0; j < img_W; j++)
                    for (int u = 0; u < img_H; u++)
                        for (int n = 0; n < img_H; n++)
                        {
                            double fi = 2 * Math.PI * n * u / 128;
                            dpR[u, j] += new Complex(Math.Cos(fi), Math.Sin(fi)) * R1[n, j];
                            dpG[u, j] += new Complex(Math.Cos(fi), Math.Sin(fi)) * G1[n, j];
                            dpB[u, j] += new Complex(Math.Cos(fi), Math.Sin(fi)) * B1[n, j];
                        }

                for (int i = 0; i < img_H; ++i)
                    for (int j = 0; j < img_W; ++j)
                    {
                        int r = Clamp((int)dpR[i, j].Magnitude, 0, 255),
                            g = Clamp((int)dpG[i, j].Magnitude, 0, 255),
                            b = Clamp((int)dpB[i, j].Magnitude, 0, 255);
                        rectR.SetPixel(j, i, RGB(img1.GetPixel(j, i), r, g, b));
                    }

                SaveImage(rectF, "furi", false, true);
                SaveImage(rectR, "img_out", false);
                rectF.Dispose();
                rectR.Dispose();
            }
            sv = true;
        }
    }
}