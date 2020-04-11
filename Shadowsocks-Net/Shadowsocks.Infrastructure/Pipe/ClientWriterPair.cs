﻿/*
 * Shadowsocks-Net https://github.com/shadowsocks/Shadowsocks-Net
 */

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Buffers;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Argument.Check;


namespace Shadowsocks.Infrastructure.Pipe
{
    using Sockets;
    public class ClientWriterPair : IClientReaderAccessor
    {
        public IWriter this[IClient client]
        {
            get
            {
                Throw.IfNull(() => client);
                if (null != _writerA && object.ReferenceEquals(_writerA.Client, client)) { return _writerA; }
                else if (null != _writerB && object.ReferenceEquals(_writerB.Client, client)) { return _writerB; }
                else { throw new ArgumentOutOfRangeException("invalid client"); }
            }
        }

        private IWriter _writerA = null;
        private IWriter _writerB = null;


        public IWriter WriterA
        {
            get => _writerA;
            set
            {
                Throw.IfNull(() => value);
                Throw.IfEqualsTo(() => value, _writerB);
                _writerA = value;
            }
        }
        public IWriter WriterB
        {
            get => _writerB;
            set
            {
                Throw.IfNull(() => value);
                Throw.IfEqualsTo(() => value, _writerA);
                _writerB = value;
            }
        }
        public ClientWriterPair(IWriter writerA, IWriter writerB)
        {
            Throw.IfNull(() => writerA);
            Throw.IfNull(() => writerB);

            Throw.IfEqualsTo(() => writerA, writerB);
            Throw.IfEqualsTo(() => writerA.Client, writerB.Client);

            _writerA = writerA;
            _writerB = writerB;
        }

        public ClientWriterPair() { }


    }
}
