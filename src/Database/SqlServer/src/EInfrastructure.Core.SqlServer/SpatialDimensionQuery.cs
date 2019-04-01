// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Text;
using EInfrastructure.Core.Ddd;
using EInfrastructure.Core.HelpCommon;
using Microsoft.EntityFrameworkCore;

namespace EInfrastructure.Core.SqlServer
{
    /// <summary>
    /// 
    /// </summary>
    public class SpatialDimensionQuery : ISpatialDimensionQuery
    {
        /// <summary>
        /// 
        /// </summary>
        protected DbContext Dbcontext;

        private readonly IExecute _execute;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork">unitwork</param>
        public SpatialDimensionQuery(IUnitOfWork unitOfWork, IExecute execute)
        {
            this.Dbcontext = unitOfWork as DbContext;
            _execute = execute;
        }

        #region get list

        /// <summary>
        /// get list
        /// </summary>
        /// <param name="param"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetList<T>(SpatialDimensionParam param)
        {
            throw new System.Exception("Unimplemented interface");
        }

        #endregion
    }
}