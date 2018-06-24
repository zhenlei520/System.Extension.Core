using System;
using EInfrastructure.Core.Redis.Common.Internal.IO;

namespace EInfrastructure.Core.Redis.Common.Internal.Commands
{
    class RedisScanCommand<T>: RedisCommand<RedisScan<T>>
    {
        RedisCommand<T[]> _command;

        public RedisScanCommand(RedisCommand<T[]> command)
            : base(command.Command, command.Arguments)
        {
            _command = command;
        }

        public override RedisScan<T> Parse(RedisReader reader)
        {
            reader.ExpectType(RedisMessage.MultiBulk);
            if (reader.ReadInt(false) != 2)
                throw new RedisProtocolException("Expected 2 items");

            long cursor = Int64.Parse(reader.ReadBulkString());
            T[] items = _command.Parse(reader);

            return new RedisScan<T>(cursor, items);
        }
    }
}
