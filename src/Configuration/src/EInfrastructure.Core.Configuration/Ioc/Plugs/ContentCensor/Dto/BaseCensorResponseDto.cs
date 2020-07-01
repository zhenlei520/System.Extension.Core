// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.ContentCensor.Enumerations;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.ContentCensor.Dto
{
    /// <summary>
    /// 基本审查结果
    /// </summary>
    public class BaseCensorResponseDto
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="state">审查结果</param>
        /// <param name="censorType"></param>
        /// <param name="msg">审查备注</param>
        public BaseCensorResponseDto(ConclusionState state,
            List<CensorTypeResponseDto> censorType, string msg)
        {
            State = state;
            CensorType = censorType;
            Msg = msg;
        }

        /// <summary>
        /// 审查结果
        /// </summary>
        public ConclusionState State { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public List<CensorTypeResponseDto> CensorType { get; private set; }

        /// <summary>
        /// 审查备注
        /// </summary>
        public string Msg { get; private set; }

        /// <summary>
        /// 审查类型
        /// </summary>
        public class CensorTypeResponseDto
        {
            /// <summary>
            ///
            /// </summary>
            public CensorTypeResponseDto()
            {
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="type">审查类型</param>
            /// <param name="levelOfReview">审查级别</param>
            /// <param name="subTypes">审查详细类型</param>
            /// <param name="rate">置信度分数</param>
            public CensorTypeResponseDto(ContentType type, LevelOfReview levelOfReview,
                List<CensorSubTypeResponseDto> subTypes, decimal rate) : this()
            {
                Type = type;
                LevelOfReview = levelOfReview;
                SubTypes = subTypes;
                Rate = rate;
            }

            /// <summary>
            /// 审查类型
            /// </summary>
            public ContentType Type { get; private set; }

            /// <summary>
            /// 审查级别
            /// </summary>
            public LevelOfReview LevelOfReview { get; private set; }

            /// <summary>
            /// 审查详细类型
            /// </summary>
            public List<CensorSubTypeResponseDto> SubTypes { get; private set; }

            /// <summary>
            /// 置信度分数
            /// </summary>
            public decimal Rate { get; private set; }
        }

        /// <summary>
        /// 详细审查类型
        /// </summary>
        public class CensorSubTypeResponseDto
        {
            /// <summary>
            ///
            /// </summary>
            public CensorSubTypeResponseDto()
            {
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="subType">详细类型</param>
            /// <param name="rate">置信度分数</param>
            /// <param name="extend">扩展</param>
            public CensorSubTypeResponseDto(ContentSubType subType, decimal rate, object extend) : this()
            {
                SubType = subType;
                Rate = rate;
                Extend = extend;
            }

            /// <summary>
            /// 详细类型
            /// </summary>
            public ContentSubType SubType { get; private set; }

            /// <summary>
            /// 置信度分数
            /// </summary>
            public decimal Rate { get; private set; }

            /// <summary>
            /// 扩展信息
            /// </summary>
            public object Extend { get; private set; }
        }
    }
}
