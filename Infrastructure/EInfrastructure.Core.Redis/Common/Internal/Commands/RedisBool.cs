using EInfrastructure.Core.Redis.Common.Internal.IO;

namespace EInfrastructure.Core.Redis.Common.Internal.Commands
{
    class RedisBool : RedisCommand<bool>
    {
        public RedisBool(string command, params object[] args)
            : base(command, args)
        { }

        public override bool Parse(RedisReader reader)
        {
            return reader.ReadInt() == 1;
        }
    }
}
