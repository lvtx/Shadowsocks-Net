﻿/*
 * Shadowsocks-Net https://github.com/shadowsocks/Shadowsocks-Net
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Shadowsocks.Infrastructure.Pipe
{
    using Sockets;
    public interface IClientWriterAccessor
    {
        IWriter this[IClient client] { get; }
    }
}
