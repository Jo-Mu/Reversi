using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    class InvalidBoardSizeException : SystemException
    {
        public InvalidBoardSizeException()
        { }

        public InvalidBoardSizeException(string message) : base (message)
        { }
    }
}
