using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFTMarketPlace.MetaMask.Exceptions
{
    public class NoMetaMaskException : ApplicationException
    {
        public NoMetaMaskException() : base("MetaMask is not installed.")
        {

        }
    }
}
