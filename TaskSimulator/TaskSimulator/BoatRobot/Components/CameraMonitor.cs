using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskSimulatorLib;
using TaskSimulatorLib.Entitys;
using TaskSimulatorLib.Extends.Base;

namespace TaskSimulator.BoatRobot.Components
{
    /// <summary>
    /// 摄像头监视器
    /// </summary>
    public class CameraMonitor : BaseMonitor
    {
        /// <summary>
        /// 获得摄像头指令
        /// </summary>
        public const string Command_GetCameraImage = "GetCameraImage";

        Random random = new Random((int)DateTime.Now.Ticks);

        public override CommandResult Process(Command commandObj)
        {
            List<KeyValuePair<string, object>> resultParams = new List<KeyValuePair<string, object>>();

            Bitmap bmp = null;
            if (Command_GetCameraImage.Equals(commandObj.CommandText))
            {
                //绘制虚拟摄像头图像
                bmp = new Bitmap(VirtualCameraImageWidth, VirtualCameraImageHeight);
                Graphics g = Graphics.FromImage(bmp);
                try
                {
                    //填充背景
                    if (VirtualCameraBackgroundImages != null && VirtualCameraBackgroundImages.Length >= 1)
                    {
                        //有配置背景文件，直接使用
                        int randomPictureIndex = random.Next(0, VirtualCameraBackgroundImages.Length);
                        if (string.IsNullOrEmpty(VirtualCameraBackgroundImages[randomPictureIndex]))
                        {
                            //没有配置背景文件，使用随机颜色
                            List<Color> backgroundColors = new List<Color>();
                            backgroundColors.AddRange(VirtualCameraImageBackgroundColors);
                            //填充一个随机背景色
                            g.FillRectangle(new SolidBrush(backgroundColors[random.Next(0, backgroundColors.Count)]), new Rectangle(0, 0, VirtualCameraImageWidth, VirtualCameraImageHeight));
                        }
                        else
                        {
                            string destFile = VirtualCameraBackgroundImages[randomPictureIndex].Contains(@":\") ? VirtualCameraBackgroundImages[randomPictureIndex] : Path.Combine(Application.StartupPath, VirtualCameraBackgroundImages[randomPictureIndex]);
                            if (File.Exists(destFile))
                            {
                                //图片存在
                                Image destImage = null;
                                try
                                {
                                    destImage = Image.FromFile(destFile);
                                    g.DrawImage(destImage, new Rectangle(0, 0, VirtualCameraImageWidth, VirtualCameraImageHeight));
                                }
                                catch (Exception ex)
                                {
                                    SimulatorObject.logger.Error(ex.ToString());

                                    //没有配置背景文件，使用随机颜色
                                    List<Color> backgroundColors = new List<Color>();
                                    backgroundColors.AddRange(VirtualCameraImageBackgroundColors);
                                    //填充一个随机背景色
                                    g.FillRectangle(new SolidBrush(backgroundColors[random.Next(0, backgroundColors.Count)]), new Rectangle(0, 0, VirtualCameraImageWidth, VirtualCameraImageHeight));
                                }
                                finally
                                {
                                    if (destImage != null)
                                    {
                                        destImage.Dispose();
                                    }
                                }
                            }
                            else
                            {
                                //没有配置背景文件，使用随机颜色
                                List<Color> backgroundColors = new List<Color>();
                                backgroundColors.AddRange(VirtualCameraImageBackgroundColors);
                                //填充一个随机背景色
                                g.FillRectangle(new SolidBrush(backgroundColors[random.Next(0, backgroundColors.Count)]), new Rectangle(0, 0, VirtualCameraImageWidth, VirtualCameraImageHeight));
                            }
                        }
                    }
                    else
                    {
                        //没有配置背景文件，使用随机颜色
                        List<Color> backgroundColors = new List<Color>();
                        backgroundColors.AddRange(VirtualCameraImageBackgroundColors);
                        //填充一个随机背景色
                        g.FillRectangle(new SolidBrush(backgroundColors[random.Next(0, backgroundColors.Count)]), new Rectangle(0, 0, VirtualCameraImageWidth, VirtualCameraImageHeight));
                    }

                    //写RobotUser名称
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.FormatFlags = StringFormatFlags.DisplayFormatControl;
                    g.DrawString(User.UserName + (Name != null ? "-" + Name : string.Empty), VirtualCameraImageFont, new SolidBrush(VirtualCameraImageFontColor), new RectangleF(0, 0, VirtualCameraImageWidth, (int)(VirtualCameraImageHeight * 0.8)), sf);
                    //写时间
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Near;
                    sf.FormatFlags = StringFormatFlags.DisplayFormatControl;
                    g.DrawString(DateTime.Now.ToLongTimeString(), VirtualCameraImageFont, new SolidBrush(VirtualCameraImageFontColor), new RectangleF(0, (int)(VirtualCameraImageHeight * 0.8), VirtualCameraImageWidth, VirtualCameraImageHeight), sf);
                }
                finally
                {
                    g.Dispose();
                }
            }

            return new CommandResult(commandObj.CommandText, true, string.Empty, bmp, resultParams.ToArray());
        }

        /// <summary>
        /// 虚拟摄像头图片宽度
        /// </summary>
        public int VirtualCameraImageWidth = 200;

        /// <summary>
        /// 虚拟摄像头图片高度
        /// </summary>
        public int VirtualCameraImageHeight = 180;

        /// <summary>
        /// 虚拟摄像头图片背景色
        /// </summary>
        public Color[] VirtualCameraImageBackgroundColors = new Color[] { Color.White, Color.Yellow, Color.Red, Color.CadetBlue, Color.Green, Color.Orange };

        /// <summary>
        /// 虚拟摄像头图片默认字体颜色
        /// </summary>
        public Color VirtualCameraImageFontColor = Color.Black;

        /// <summary>
        /// 虚拟摄像头图片中默认字体
        /// </summary>
        public Font VirtualCameraImageFont = new Font("宋体", 16);

        /// <summary>
        /// 虚拟摄像头背景
        /// </summary>
        public string[] VirtualCameraBackgroundImages { get; set; }
    }
}