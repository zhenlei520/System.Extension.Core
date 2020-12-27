// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Tools.Enumerations
{
    /// <summary>
    ///
    /// </summary>
    public class SequentialGuidType : Enumeration
    {
        /// <summary>
        /// The GUID should be sequential when formatted using the <see cref="Guid.ToString()" /> method.
        /// Used by MySql and PostgreSql.
        /// </summary>
        public static SequentialGuidType SequentialAsString = new SequentialGuidType(1, "SequentialAsString");

        /// <summary>
        /// The GUID should be sequential when formatted using the <see cref="Guid.ToByteArray" /> method.
        /// Used by Oracle.
        /// </summary>
        public static SequentialGuidType SequentialAsBinary = new SequentialGuidType(2, "SequentialAsBinary");

        /// <summary>
        /// The sequential portion of the GUID should be located at the end of the Data4 block.
        /// Used by SqlServer.
        /// </summary>
        public static SequentialGuidType SequentialAtEnd = new SequentialGuidType(3, "SequentialAtEnd");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public SequentialGuidType(int id, string name) : base(id, name)
        {
        }
    }
}
