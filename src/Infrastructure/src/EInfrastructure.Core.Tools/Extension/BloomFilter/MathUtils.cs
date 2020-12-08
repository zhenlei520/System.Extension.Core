// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// 参考来源：https://sourceforge.net/projects/bloomfilternet/

namespace EInfrastructure.Core.Tools.Extension.BloomFilter
{
	internal static class MathUtils
	{
		/// <summary>
		/// Compute the absolute value of an integer
		/// </summary>
		/// <param name="n"></param>
		/// <returns></returns>
		public static int Abs( int n )
		{
			if (n >= 0) return n;
			else return -n;
		}
	}
}
