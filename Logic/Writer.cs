using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Logic
{
    public class Writer:IMessageWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
