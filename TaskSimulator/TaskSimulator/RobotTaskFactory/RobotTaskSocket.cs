using System;
using System.Collections.Generic;
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
        /// 订阅ListenSubject后的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            Console.WriteLine("Subscribed for id = " + e.MessageId);
        }

        /// <summary>
        /// 接受消息后的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Console.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic);
        }
        
        /// <summary>
        /// 发布消息后的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Console.WriteLine("MessageId = " + e.MessageId + " Published = " + e.IsPublished);
        }
        
        /// <summary>
        /// 关闭连接后的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void client_ConnectionClosed(object sender, EventArgs e)
        {
            Console.WriteLine("connect closed");
        }
        
        /// <summary>
        /// 取消订阅ListenSubject后的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void client_MqttMsgUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
            Console.WriteLine("connect closed");
        }
    }
}