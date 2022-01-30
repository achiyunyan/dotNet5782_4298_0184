using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Factory creational design implementetion 
/// </summary>
namespace BlApi
{
    public class BlFactory
    {
        public static IBL GetBl()
        {
            return BL.BL.Instance;
        }
    }
}
