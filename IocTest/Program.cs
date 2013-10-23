using System;
using Interfaces;
using Logic;

namespace IocTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var res = new Resolver();
            res.Bind<IConfig>().To<SingleObjectConfig>();
            res.Bind<IMessageWriter>().To<Writer>();
            res.Bind<SecondObject>().ToSelf().Singleton();
            
            var result = res.Resolve<SecondObject>();
            result.SayHello("steve");

            var secondResult = res.Resolve<SecondObject>();
            secondResult.SayHello("Fred");
            
            Console.ReadLine();
        }
    }
}
