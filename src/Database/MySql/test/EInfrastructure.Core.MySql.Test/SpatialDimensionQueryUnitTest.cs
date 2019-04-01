using System.Collections.Generic;
using EInfrastructure.Core.Ddd;
using Xunit;

namespace EInfrastructure.Core.MySql.Test
{
    /// <summary>
    /// 
    /// </summary>
    public class SpatialDimensionQueryUnitTest
    {
        private readonly ISpatialDimensionQuery<User, int> _spatialDimensionQuery;

        public SpatialDimensionQueryUnitTest()
        {
        }

        [Fact]
        public void Test1()
        {
            _spatialDimensionQuery.GetPageData<User>(new SpatialDimensionPagingParam()
            {
                TableName = "point2",
                Distance = 1000,
                FileKeys = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("id", "Id"),
                    new KeyValuePair<string, string>("lng", "Lng"),
                    new KeyValuePair<string, string>("lat", "Lat"),
                    new KeyValuePair<string, string>("title", "Title")
                },
                DistanceAlias = "Distance",
                Location = new KeyValuePair<decimal, decimal>(113.734792m, 34.772058m),
                Point = new KeyValuePair<string, string>("Lng", "Lat"),
                Sorts = new List<KeyValuePair<string, bool>>()
                {
                    new KeyValuePair<string, bool>("Distance", true),
                    new KeyValuePair<string, bool>("Id", false)
                },
                PageSize = 20,
                PageIndex = 1
            });
        }

        public class User : Entity<int>
        {
            public string Name { get; set; }
        }
    }
}