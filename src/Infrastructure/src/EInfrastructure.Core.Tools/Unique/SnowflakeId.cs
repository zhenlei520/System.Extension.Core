// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace EInfrastructure.Core.Tools.Unique
{
    /// <summary>
    /// 雪花id
    /// </summary>
    public class SnowflakeId
    {
        /// <summary>
        /// 开始时间截
        /// </summary>
        public const long Twepoch = 1288834974657L;

        /// <summary>
        /// 机器id所占的位数
        /// </summary>
        private const int WorkerIdBits = 5;

        /// <summary>
        /// 数据标识id所占的位数
        /// </summary>
        private const int DatacenterIdBits = 5;

        /// <summary>
        /// 序列在id中占的位数
        /// </summary>
        private const int SequenceBits = 12;

        /// <summary>
        /// 支持的最大机器id，结果是31 (这个移位算法可以很快的计算出几位二进制数所能表示的最大十进制数)
        /// </summary>
        private const long MaxWorkerId = -1L ^ (-1L << WorkerIdBits);

        /// <summary>
        /// 支持的最大数据标识id，结果是31
        /// </summary>
        private const long MaxDatacenterId = -1L ^ (-1L << DatacenterIdBits);

        /// <summary>
        /// 机器ID向左移12位
        /// </summary>
        private const int WorkerIdShift = SequenceBits;

        /// <summary>
        ///
        /// </summary>
        private const int DatacenterIdShift = SequenceBits + WorkerIdBits;
        public const int TimestampLeftShift = SequenceBits + WorkerIdBits + DatacenterIdBits;

        /// <summary>
        /// 生成序列的掩码，这里为4095 (0b111111111111=0xfff=4095)
        /// </summary>
        private const long SequenceMask = -1L ^ (-1L << SequenceBits);

        private static SnowflakeId _snowflakeId;

        private readonly object _lock = new object();
        private static readonly object SLock = new object();

        /// <summary>
        /// 上次生成ID的时间截
        /// </summary>
        private long _lastTimestamp = -1L;

        /// <summary>
        ///
        /// </summary>
        /// <param name="workerId">工作机器ID</param>
        /// <param name="datacenterId">数据中心ID</param>
        /// <exception cref="ArgumentException"></exception>
        public SnowflakeId(long workerId, long datacenterId)
        {
            WorkerId = workerId;
            DatacenterId = datacenterId;
            Sequence = 0L;

            // sanity check for workerId
            if (workerId > MaxWorkerId || workerId < 0)
                throw new ArgumentException($"worker Id can't be greater than {MaxWorkerId} or less than 0");

            if (datacenterId > MaxDatacenterId || datacenterId < 0)
                throw new ArgumentException($"datacenter Id can't be greater than {MaxDatacenterId} or less than 0");
        }

        /// <summary>
        /// 工作机器ID(0~31)
        /// </summary>
        public long WorkerId { get; protected set; }

        /// <summary>
        /// 数据中心ID(0~31)
        /// </summary>
        public long DatacenterId { get; protected set; }

        /// <summary>
        /// 毫秒内序列(0~4095)
        /// </summary>
        public long Sequence { get; internal set; }

        /// <summary>
        /// 获取默认的雪花id对象
        /// </summary>
        /// <returns></returns>
        public static SnowflakeId Default()
        {
            lock (SLock)
            {
                if (_snowflakeId != null)
                {
                    return _snowflakeId;
                }

                var random = new Random();

                if (!int.TryParse(
                    Environment.GetEnvironmentVariable("Request_WORKERID", EnvironmentVariableTarget.Machine),
                    out var workerId))
                {
                    workerId = random.Next((int) MaxWorkerId);
                }

                if (!int.TryParse(
                    Environment.GetEnvironmentVariable("Request_DATACENTERID", EnvironmentVariableTarget.Machine),
                    out var datacenterId))
                {
                    datacenterId = random.Next((int) MaxDatacenterId);
                }

                return _snowflakeId = new SnowflakeId(workerId, datacenterId);
            }
        }

        /// <summary>
        /// 获得下一个ID
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual long NextId()
        {
            lock (_lock)
            {
                var timestamp = TimeGen();

                if (timestamp < _lastTimestamp)
                    throw new Exception(
                        $"InvalidSystemClock: Clock moved backwards, Refusing to generate id for {_lastTimestamp - timestamp} milliseconds");

                if (_lastTimestamp == timestamp)
                {
                    Sequence = (Sequence + 1) & SequenceMask;
                    if (Sequence == 0) timestamp = TilNextMillis(_lastTimestamp);
                }
                else
                {
                    Sequence = 0;
                }

                _lastTimestamp = timestamp;
                var id = ((timestamp - Twepoch) << TimestampLeftShift) |
                         (DatacenterId << DatacenterIdShift) |
                         (WorkerId << WorkerIdShift) | Sequence;

                return id;
            }
        }

        protected virtual long TilNextMillis(long lastTimestamp)
        {
            var timestamp = TimeGen();
            while (timestamp <= lastTimestamp) timestamp = TimeGen();
            return timestamp;
        }

        protected virtual long TimeGen()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
    }
}
