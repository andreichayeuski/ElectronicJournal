using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Common.Utils.Syslog
{
    public enum Severity
    {
        Emergency = 0,
        Alert = 1,
        Critical = 2,
        Error = 3,
        Warning = 4,
        Notice = 5,
        Information = 6,
        Debug = 7
    }

    public enum Facility
    {
        Kernel = 0,
        User = 1,
        Mail = 2,
        Daemon = 3,
        Auth = 4,
        Syslog = 5,
        Lpr = 6,
        News = 7,
        UUCP = 8,
        Cron = 9,
        Local0 = 10,
        Local1 = 11,
        Local2 = 12,
        Local3 = 13,
        Local4 = 14,
        Local5 = 15,
        Local6 = 16,
        Local7 = 17
    }

    public class Message
    {
        public Message()
        {
        }

        public Message(int facility, int severity, string text)
        {
            Facility = facility;
            Severity = severity;
            Text = text;
        }

        public int Facility { get; set; }
        public int Severity { get; set; }
        public string Text { get; set; }
    }

    /// need this helper class to expose the Active propery of UdpClient /// (why is it protected, anyway?)
    public class SyslogClient : UdpClient
    {
        public SyslogClient()
        {
        }

        public SyslogClient(IPEndPoint ipe)
            : base(ipe)
        {
        }

        public bool IsActive
        {
            get { return Active; }
        }

        ~SyslogClient()
        {
            if (Active) Close();
        }
    }

    public class SyslogHelper
    {
        private readonly SyslogClient client;
        private readonly IPAddress ipAddress;
        private readonly IPHostEntry ipHostInfo;
        private readonly IPEndPoint ipLocalEndPoint;
        private string _hostIp ;//= ConfigurationManager.AppSettings["SyslogServerIp"];
        private int _port = 514;

        public SyslogHelper()
        {
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            ipLocalEndPoint = new IPEndPoint(ipAddress, 0);
            client = new SyslogClient(ipLocalEndPoint);
        }

        public bool IsActive
        {
            get { return client.IsActive; }
        }

        public int Port
        {
            set { _port = value; }
            get { return _port; }
        }

        public string HostIp
        {
            get { return _hostIp; }
            set
            {
                if ((_hostIp == null) && (!IsActive))
                {
                    _hostIp = value;

                    client.Connect(_hostIp, _port);
                }
                else if (_hostIp != null && (!IsActive))
                {
                    client.Connect(_hostIp, _port);
                }
            }
        }

        public void Close()
        {
            if (client.IsActive) client.Close();
        }

        /// <summary>
        /// Посылает сообщение в Syslog, по умолчанию кодовая страница 1251
        /// </summary>
        public void Send(Message message)
        {
            SendSyslogMessage(message, Encoding.GetEncoding(1251));
        }

        /// <summary>
        /// Посылает сообщение в Syslog, возможно указать кодовую страницу
        /// </summary>
        public void Send(Message message,Encoding encoding)
        {
            SendSyslogMessage(message, encoding);
        }

        private void SendSyslogMessage(Message message, Encoding encoding)
        {
            if (string.IsNullOrEmpty(_hostIp))
                return;

            if (!client.IsActive)
                client.Connect(_hostIp, _port);
            if (client.IsActive)
            {
                var msg = string.Format("<{0}>{1}", message.Facility * 8 + message.Severity, message.Text);

                var bytes =encoding.GetBytes(msg);
                client.Send(bytes, bytes.Length);

            }
            else throw new Exception("Syslog client Socket is not connected. Please set the host IP in config");
        }

    }
}
