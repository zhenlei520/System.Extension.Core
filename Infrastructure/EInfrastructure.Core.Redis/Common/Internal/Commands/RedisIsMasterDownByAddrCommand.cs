using EInfrastructure.Core.Redis.Common.Internal.IO;

namespace EInfrastructure.Core.Redis.Common.Internal.Commands
{
    class RedisIsMasterDownByAddrCommand : RedisCommand<RedisMasterState>
    {
        public RedisIsMasterDownByAddrCommand(string command, params object[] args)
            : base(command, args)
        { }

        public override RedisMasterState Parse(RedisReader reader)
        {
            reader.ExpectType(RedisMessage.MultiBulk);
            reader.ExpectSize(3);
            long down_state = reader.ReadInt();
            string leader = reader.ReadBulkString();
            long vote_epoch = reader.ReadInt();
            return new RedisMasterState(down_state, leader, vote_epoch);
        }
    }
}
