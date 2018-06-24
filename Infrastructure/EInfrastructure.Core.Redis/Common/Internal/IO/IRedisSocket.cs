using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace EInfrastructure.Core.Redis.Common.Internal.IO
{
    interface IRedisSocket : IDisposable
    {
        bool Connected { get; }
        int ReceiveTimeout { get; set; }
        int SendTimeout { get; set; }
        void Connect(EndPoint endpoint);

        /// <summary>
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="timeout">
        /// The number of milliseconds to wait connect. The default value is 0, 
        /// which indicates an infinite time-out period. Specifying 
        /// Timeout.Infinite (-1) also indicates an infinite time-out period.
        /// </param>
        void Connect(EndPoint endpoint, int timeout);
        bool ConnectAsync(SocketAsyncEventArgs args);
        bool SendAsync(SocketAsyncEventArgs args);
        Stream GetStream();
    }
}
