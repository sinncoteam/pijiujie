using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Text;
using System.Text.RegularExpressions;

namespace JFB.Utils
{
    public enum ThumbnailType
    {
        /// <summary>
        /// 头像
        /// </summary>
        Logo,
        /// <summary>
        /// 照片
        /// </summary>
        Photo
    }

    public class ThumbnailHelper
    {
        /// <summary>
        /// 得到图片格式
        /// </summary>
        /// <param name="name">文件名称</param>
        /// <returns></returns>
        public static ImageFormat GetFormat(string name)
        {
            string ext = name.Substring(name.LastIndexOf(".") + 1);
            switch (ext.ToLower())
            {
                case "jpg":
                case "jpeg":
                    return ImageFormat.Jpeg;
                case "bmp":
                    return ImageFormat.Bmp;
                case "png":
                    return ImageFormat.Png;
                case "gif":
                    return ImageFormat.Gif;
                default:
                    return ImageFormat.Jpeg;
            }
        }

        /// <summary>
        /// 按比例缩放图片
        /// </summary>
        /// <param name="fileName">源图片</param>
        /// <param name="newFileName">缩略图保存路径</param>
        /// <param name="width">缩略图宽度</param>
        public static void MakeThumbnailImage(string fileName, string newFileName, int width)
        {
            Bitmap bmp = new Bitmap(fileName);

            double scale = (double)bmp.Width / width;
            double height = (double)bmp.Height / scale;

            //Image.ThumbMode mode = mode = Image.ThumbMode.Max;
            ContentAlignment contentAlignment = ContentAlignment.TopLeft;
            Bitmap image = Image.Thumbnail(bmp, new Size(width, (int)height), contentAlignment, ThumbnailType.Photo);
            Image.SaveIamge(image, 100L, newFileName);
        }

        /// <summary>
        /// 按用户指定长宽缩放图片
        /// </summary>
        /// <param name="fileName">源图片</param>
        /// <param name="newFileName">缩略图保存路径</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        public static void MakeThumbnailImage(string fileName, string newFileName, int width, int height, ThumbnailType type = ThumbnailType.Photo)
        {
            Bitmap bmp = new Bitmap(fileName);
            Image.ThumbMode mode = mode = Image.ThumbMode.Max;
            ContentAlignment contentAlignment = ContentAlignment.TopLeft;
            Bitmap image = Image.Thumbnail(bmp, new Size(width, height), contentAlignment, type);
            Image.SaveIamge(image, 100L, newFileName);
        }

        public class Image
        {
            /// <summary>
            /// 缩略模式。
            /// </summary>
            public enum ThumbMode : byte
            {
                /// <summary>
                /// 完整模式
                /// </summary>
                Full = 1,
                /// <summary>
                /// 最大尺寸
                /// </summary>
                Max
            }

            /// <summary>
            /// 缩略图。
            /// </summary>
            /// <param name="image">要缩略的图片</param>
            /// <param name="size">要缩放的尺寸</param>
            /// <param name="mode">缩略模式</param>
            /// <param name="contentAlignment">对齐方式</param>
            /// <returns>返回已经缩放的图片。</returns>
            public static Bitmap Thumbnail(Bitmap image, Size size, ContentAlignment contentAlignment, ThumbnailType type)
            {

                if (image.Width <= size.Width && image.Height <= size.Height)
                {
                    if (type == ThumbnailType.Photo)
                        return image;
                }

                if (!size.IsEmpty && !image.Size.IsEmpty && !size.Equals(image.Size))
                {
                    //先取一个宽比例。
                    double scale = (double)image.Width / (double)size.Width;

                    if (image.Height > image.Width)
                        scale = (double)image.Height / (double)size.Height;
                    //缩略模式
                    //switch (mode)
                    //{
                    //    case ThumbMode.Full:
                    //        if (image.Height > image.Width)
                    //            scale = (double)image.Height / (double)size.Height;
                    //        break;
                    //    case ThumbMode.Max:
                    //        if (image.Height / scale < size.Height)
                    //            scale = (double)image.Height / (double)size.Height;
                    //        break;
                    //}

                    Size newSzie = new Size((int)(image.Width / scale), (int)(image.Height / scale));
                    Bitmap result = new Bitmap(newSzie.Width, newSzie.Height);

                    if (type == ThumbnailType.Logo)
                    {
                        newSzie = new Size(size.Width, size.Height);
                        result = new Bitmap(newSzie.Width, newSzie.Height);
                    }

                    using (Graphics g = Graphics.FromImage(result))
                    {
                        //背景颜色
                        g.FillRectangle(Brushes.Transparent, new Rectangle(new Point(0, 0), size));
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        g.CompositingMode = CompositingMode.SourceOver;
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                        //对齐方式
                        Rectangle destRect;
                        switch (contentAlignment)
                        {
                            case ContentAlignment.TopCenter:
                                destRect = new Rectangle(new Point(-((newSzie.Width - size.Width) / 2), 0), newSzie);
                                break;
                            case ContentAlignment.TopRight:
                                destRect = new Rectangle(new Point(-(newSzie.Width - size.Width), 0), newSzie);
                                break;
                            case ContentAlignment.MiddleLeft:
                                destRect = new Rectangle(new Point(0, -((newSzie.Height - size.Height) / 2)), newSzie);
                                break;
                            case ContentAlignment.MiddleCenter:
                                destRect = new Rectangle(new Point(-((newSzie.Width - size.Width) / 2), -((newSzie.Height - size.Height) / 2)), newSzie);
                                break;
                            case ContentAlignment.MiddleRight:
                                destRect = new Rectangle(new Point(-(newSzie.Width - size.Width), -((newSzie.Height - size.Height) / 2)), newSzie);
                                break;
                            case ContentAlignment.BottomLeft:
                                destRect = new Rectangle(new Point(0, -(newSzie.Height - size.Height)), newSzie);
                                break;
                            case ContentAlignment.BottomCenter:
                                destRect = new Rectangle(new Point(-((newSzie.Width - size.Width) / 2), -(newSzie.Height - size.Height)), newSzie);
                                break;
                            case ContentAlignment.BottomRight:
                                destRect = new Rectangle(new Point(-(newSzie.Width - size.Width), -(newSzie.Height - size.Height)), newSzie);
                                break;
                            default:
                                destRect = new Rectangle(new Point(0, 0), newSzie);
                                break;
                        }
                        g.DrawImage(image, destRect, new Rectangle(new Point(0, 0), image.Size), GraphicsUnit.Pixel);
                        image.Dispose();
                    }
                    return result;
                }
                else
                    return image;
            }

            /// <summary>
            /// 保存图片。
            /// </summary>
            /// <param name="image">要保存的图片</param>
            /// <param name="quality">品质（1L~100L之间，数值越大品质越好）</param>
            /// <param name="filename">保存路径</param>
            public static void SaveIamge(Bitmap image, long quality, string filename)
            {
                using (EncoderParameters encoderParams = new EncoderParameters(1))
                {
                    using (EncoderParameter parameter = (encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality)))
                    {
                        ImageCodecInfo encoder = null;
                        //取得扩展名
                        string ext = Path.GetExtension(filename);
                        if (string.IsNullOrEmpty(ext))
                            ext = ".jpg";
                        //根据扩展名得到解码、编码器
                        foreach (ImageCodecInfo codecInfo in ImageCodecInfo.GetImageEncoders())
                        {
                            if (Regex.IsMatch(codecInfo.FilenameExtension, string.Format(@"(;|^)\*\{0}(;|$)", ext), RegexOptions.IgnoreCase))
                            {
                                encoder = codecInfo;
                                break;
                            }
                        }
                        Directory.CreateDirectory(Path.GetDirectoryName(filename));
                        image.Save(filename, encoder, encoderParams);
                    }
                }
            }

            /// <summary>
            /// 保存图片。
            /// </summary>
            /// <param name="stream">要保存的流</param>
            /// <param name="quality">品质（1L~100L之间，数值越大品质越好）</param>
            /// <param name="filename">保存路径</param>
            public static void SaveIamge(Stream stream, long quality, string filename)
            {
                using (Bitmap bmpTemp = new Bitmap(stream))
                {
                    SaveIamge(bmpTemp, quality, filename);
                }
            }
        }
    }//end class
}
