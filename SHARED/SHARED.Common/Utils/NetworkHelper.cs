using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace SHARED.Common.Utils
{
    public class NetworkHelper
    {
        public static int GetFreePortInRange(int portStartIndex, int portEndIndex)
        {
            try
            {
                var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

                var tcpEndPoints = ipGlobalProperties.GetActiveTcpListeners();
                var usedServerTCpPorts = tcpEndPoints.Select(p => p.Port).ToList();

                var udpEndPoints = ipGlobalProperties.GetActiveUdpListeners();
                var usedServerUdpPorts = udpEndPoints.Select(p => p.Port).ToList();

                var tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
                var usedPorts =
                    tcpConnInfoArray.Where(p => p.State != TcpState.Closed).Select(p => p.LocalEndPoint.Port).ToList();

                usedPorts.AddRange(usedServerTCpPorts.ToArray());
                usedPorts.AddRange(usedServerUdpPorts.ToArray());

                var unusedPort = 0;

                for (var port = portStartIndex; port < portEndIndex; port++)
                    if (!usedPorts.Contains(port))
                    {
                        unusedPort = port;
                        break;
                    }

                if (unusedPort == 0)
                    throw new ApplicationException("GetFreePortInRange, Out of ports");

                return unusedPort;
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                throw;
            }
        }
    }
}