using System;
using System.Collections.Generic;
using EInfrastructure.Core.HelpCommon;
using Xunit;

namespace EInfrastructure.Core.Test
{
    public class ListCommonUnitTest
    {
        [Fact]
        public void Add()
        {
            List<Users> users = new List<Users>()
            {
                new Users()
                {
                    Age = 18,
                    Name = "张三"
                }
            };
            List<Users> user2 = new List<Users>()
            {
                new Users()
                {
                    Age = 20,
                    Name = "里斯"
                },
                new Users()
                {
                    Age = 18,
                    Name = "张三"
                }
            };
            users.Add(user2, true);
            
            Console.WriteLine(users);
        }

        
    }
    [Serializable]
    public class Users
    {
        public int Age { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}