using System;
using Interfaces;

namespace Logic
{
    public class SingleObjectConfig:IConfig
    {
        public string Message { get; set; }

        public SingleObjectConfig()
        {
            Message = "Hello {0} how are you today?";
        }
    }

    public class SingleObject:ISingleObject
    {
        private IConfig Config { get; set; }

        private IMessageWriter Writer { get; set; }

        public SingleObject(IConfig config, IMessageWriter writer)
        {
            Config = config;
            Writer = writer;
        }

        public void SayHello(string name)
        {
            Writer.Write(string.Format(Config.Message,name));
        }
    }
}
