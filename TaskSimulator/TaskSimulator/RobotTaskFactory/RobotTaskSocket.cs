using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSimulatorLib.Entitys;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace TaskSimulator.RobotTaskFactory
{
    /// <summary>
    /// 把任务动作和MQTT相连
    /// </summary>
    public class RobotTaskSocket
    {
        /// <summary>
        /// qos=1
        /// </summary>
        byte[] qosLevels = new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE };

        /// <summary>
        /// 监听主题
        /// </summary>
        public string ListenSubject = "shore2boat";

        /// <summary>
        /// 指令发送主题
        /// </summary>
        public string CommandSendSubject = "boat2shore";

        /// <summary>
        /// 图片发送主题
        /// </summary>
        public string PictureSendSubject = "picture2shore";

        /// <summary>
        /// 机器人用户
        /// </summary>
        public RobotUser RobotUser { get; set; }

        /// <summary>
        /// 远程IP
        /// </summary>
        public string RemoteIP { get; set; }

        /// <summary>
        /// 远程端口
        /// </summary>
        public int RemotePort { get; set; }

        /// <summary>
        /// 远程登录用户
        /// </summary>
        public string RemoteUserName { get; set; }

        /// <summary>
        /// 远程登录密码
        /// </summary>
        public string RemotePassword { get; set; }

        /// <summary>
        /// MQTT客户端
        /// </summary>
        public MqttClient Client { get; set; }

        /// <summary>
        /// 客户端Id
        /// </summary>
        public string ClientId { get; set; }

        public RobotTaskSocket(RobotUser user,string ips,int ports,string username,string password)
        {
            //记录项
            this.RobotUser = user;
            this.RemoteIP = ips;
            this.RemotePort = ports;
            this.RemoteUserName = username;
            this.RemotePassword = password;
            this.ClientId = Guid.NewGuid().ToString();

            //连接服务器
            Client = new MqttClient(RemoteIP,
                                                RemotePort,
                                                true, // 开启TLS
                                                null,
                                                null,
                                                MqttSslProtocols.TLSv1_0 // TLS版本
                                               );
            Client.ProtocolVersion = MqttProtocolVersion.Version_3_1;

            //登录服务器
            byte code = Client.Connect(ClientId,
                                        RemoteUserName,
                                        RemotePassword,
                                        true, // cleanSession
                                        60); // keepAlivePeriod
            
            //添加事件
            Client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            Client.MqttMsgSubscribed += client_MqttMsgSubscribed;
            Client.MqttMsgUnsubscribed += client_MqttMsgUnsubscribed;
            Client.MqttMsgPublished += client_MqttMsgPublished;
            Client.ConnectionClosed += client_ConnectionClosed;

            //打印日志
            TaskSimulatorLib.SimulatorObject.logger.Info(Client.IsConnected ? "机器人(" + RobotUser.UserName + ")已连接到服务器(" + RemoteIP + ")!Code:" + code + ",ClientId:" + ClientId : "机器人(" + RobotUser.UserName + ")未能连接到服务器(" + RemoteIP + ")!");

            //监听主题
            Client.Subscribe(new string[] { ListenSubject }, qosLevels); // sub 的qos=1
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="subjects"></param>
        /// <param name="strValues"></param>
        public void Publish(string subjects,string strValues)
        {
            Client.Publish(subjects, Encoding.UTF8.GetBytes(strValues), qosLevels[0], false);
        }

        /// <summary>
        /// 发布消息到服务器
        /// </summary>
        /// <param name="strValue"></param>
        public void PublishCommand(string strValue)
        {
            Publish(CommandSendSubject, strValue);
        }

        /// <summary>
        /// 发布图片到服务器
        /// </summary>
        /// <param name="strValue"></param>
        public void PublishPicture(string strValue)
        {
            Publish(PictureSendSubject, strValue);
        }

        /// <summary>
        /// Send Picture
        /// </summary>
        /// <param name="bmp"></param>
        public void PublishPicture(Bitmap bmp)
        {
            //PIC,JPEG,IMG_9987,3,5,12776，图片数据
            //传输图片，图片格式JPEG，文件名为IMG_9987,当前为第3包，总共5包，本包图片数据长度12776字节，图片数据

            if (bmp != null)
            {
                //Write BMP To Stream
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Position = 0;

                //Convert To Base64
                byte[] total = new byte[ms.Length];
                ms.Read(total, 0, total.Length);
                ms = new MemoryStream();
                total = Encoding.UTF8.GetBytes(Convert.ToBase64String(total));
                ms.Write(total, 0, total.Length);
                ms.Position = 0;

                //Count PageSize
                int MaxPicUnitSize = 28 * 1000;
                int PicPageSize = 0;
                if (ms.Length > MaxPicUnitSize)
                {
                    //Need Split
                    PicPageSize = (int)ms.Length / MaxPicUnitSize;
                    if (ms.Length % MaxPicUnitSize > 0)
                    {
                        PicPageSize++;
                    }
                }
                else
                {
                    //No Split
                    PicPageSize = 1;
                }

                //Send Pic Page
                string fileName = "BMP_" + Guid.NewGuid().ToString();
                for (int kkk = 0; kkk < PicPageSize; kkk++)
                {
                    //Read Pic Unit
                    byte[] buffer = new byte[MaxPicUnitSize];
                    if (ms.Length - ms.Position >= buffer.Length)
                    {
                        ms.Read(buffer, 0, buffer.Length);
                    }
                    else
                    {
                        ms.Read(buffer, 0, (int)(ms.Length - ms.Position));
                    }

                    //Convert To String
                    string bufferString = Encoding.UTF8.GetString(buffer);

                    //SendTo
                    PublishPicture("PIC,BMP," + fileName + "," + (kkk + 1) + "," + (PicPageSize + 1) + "," + ms.Length + "," + bufferString);
                }

                //End Send
                PublishPicture("PIC,BMP," + fileName + "," + (PicPageSize + 1) + "," + (PicPageSize + 1) + "," + ms.Length + "," + "=======================");
            }
        }

        /// <summary>
        /// Convert GPSPosition To 南纬S，北纬用N，东经用E，西经用W
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        public void PublishGPSPosition(double lat, double lng)
        {
            //BOAT POS=23.227N,37.223E	船的位置为北纬23.227度，东经37.223度
            string latString = Math.Abs(lat).ToString() + (lat > 0 ? "N" : (lat == 0 ? string.Empty : "S"));
            string lngString = Math.Abs(lng).ToString() + (lng > 0 ? "E" : (lng == 0 ? string.Empty : "W"));

            PublishCommand("BOAT POS=" + latString + "," + lngString);
        }

        /// <summary>
        /// 订阅ListenSubject后的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            TaskSimulatorLib.SimulatorObject.logger.Debug("机器人:" + RobotUser.UserName + "(" + RobotUser.UserCode + ")" + "," + "已订阅的主题的Id = " + e.MessageId);
        }

        /// <summary>
        /// 接受消息后的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //接收消息
            string receiveString = string.Empty;
            if (e.Message != null && e.Message.Length > 0)
            {
                receiveString = Encoding.UTF8.GetString(e.Message);
            }

            //打印日志
            TaskSimulatorLib.SimulatorObject.logger.Debug("机器人:" + RobotUser.UserName + "(" + RobotUser.UserCode + ")" + "," + "收到消息 = " + receiveString + " 来自主题 " + e.Topic);
        
            
        }
        
        /// <summary>
        /// 发布消息后的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            TaskSimulatorLib.SimulatorObject.logger.Debug("机器人:" + RobotUser.UserName + "(" + RobotUser.UserCode + ")" + "," + "消息ID = " + e.MessageId + " 是否发布成功 = " + (e.IsPublished ? "成功" : "失败"));
        }
        
        /// <summary>
        /// 关闭连接后的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void client_ConnectionClosed(object sender, EventArgs e)
        {
            TaskSimulatorLib.SimulatorObject.logger.Debug("机器人:" + RobotUser.UserName + "(" + RobotUser.UserCode + ")" + "," + "连接已断开"); ;
        }
        
        /// <summary>
        /// 取消订阅ListenSubject后的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void client_MqttMsgUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
            TaskSimulatorLib.SimulatorObject.logger.Debug("机器人:" + RobotUser.UserName + "(" + RobotUser.UserCode + ")" + "," + "连接已断开");
        }
    }
}