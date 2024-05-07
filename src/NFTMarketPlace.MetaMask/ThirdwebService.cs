using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thirdweb;

namespace NFTMarketPlace.MetaMask
{
    internal class ThirdwebService
    {
        public void TW()
        {
            var obj = ThirdwebClient.Create(
                "c33aa60768c05c73f565e04dd7a9792d", 
                "JZZBFUaT0boLviNk8R6cU5NV0eVTbtMmlGNDAsfrWQHjVtedAfpPnVRwgSVAFORQCP2Ljj3OPi10V3AfWdQUjg",
                null);
        }
    }
}
