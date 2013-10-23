using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Logic
{
    public class SecondConfig : IConfig
    {
        public string Message { get; set; }

        public SecondConfig()
        {
            Message = "I'm the second message.";
        }
    }

    public class SecondObject
    {
        private int RunCounter { get; set; }

        private IConfig SecondConfig { get; set; }

        private SingleObject FirstObject { get; set; }

        private IMessageWriter Writer { get; set; }

        public SecondObject(SingleObject obj, SecondConfig config, IMessageWriter writer)
        {
            Writer = writer;
            RunCounter = 0;
            FirstObject = obj;
            SecondConfig = config;
        }

        public void SayHello(string name)
        {
            RunCounter++;
            FirstObject.SayHello(name);
            Writer.Write(SecondConfig.Message);
            Writer.Write(string.Format("I've been run {0} times.",RunCounter));
        }
    }
}
