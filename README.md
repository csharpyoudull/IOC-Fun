IOC-Fun
=======

Really simple ICO container built in C#

Recently the topic of IOC has been buzzing around at work so I decided to see what I could do with C# on my own. I attempted to create a simple (ninject) like syntax and I believe that has been achieved. This project was just for fun so it's far from feature complete, if you have any questions, comments or idea's i'd be happy to hear them. 


###How it works
The Resolver class is backbone of the project it allows you to resolve concrete types as well as adding custom type maps to be resolved. 

In this sample the resolver is created and three bindings are added, two bindings used to map interfaces to their concrete types and one to bind the "SecondObject" to iteself and manage it as a singleton.

    var res = new Resolver();
    res.Bind<IConfig>().To<SingleObjectConfig>();
    res.Bind<IMessageWriter>().To<Writer>();
    res.Bind<SecondObject>().ToSelf().Singleton();


Getting composed objects is simple just call Resolve<T>().

    var result = res.Resolve<SecondObject>();
    result.SayHello("steve");
    var secondResult = res.Resolve<SecondObject>();
    secondResult.SayHello("Fred");


When running this sample code you'll see the singleton, mapping and type creation in action. 


Hope you find this sample helpful.
