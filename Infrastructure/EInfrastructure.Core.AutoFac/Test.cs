using System;
using System.Collections.Generic;
using System.Text;

namespace EInfrastructure.Core.AutoFac
{
  class Test<T, U>
    where U : struct
    where T :  new()
  { }
}
