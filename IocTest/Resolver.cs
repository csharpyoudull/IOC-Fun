using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IocTest
{
    public enum Lifetime
    {
        Singleton
    }

    public class BindingConfig
    {
        public Type Type { get; set; }

        public object Instance { get; set; }

        public Lifetime Lifetime { get; set; }
    }

    public class LifetimeManager
    {
        private BindingConfig Config { get; set; }

        public LifetimeManager(BindingConfig config)
        {
            Config = config;
        }

        public void Singleton()
        {
            Config.Lifetime = Lifetime.Singleton;
        }
    }

    public class Resolver
    {
        private List<Assembly> Assemblies { get; set; }

        private Dictionary<Type, BindingConfig> Bindings { get; set; } 

        public Resolver()
        {
            Assemblies = new List<Assembly> {Assembly.GetExecutingAssembly()};
            Assemblies.AddRange(Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load));
            Bindings = new Dictionary<Type, BindingConfig>();
        }

        public MapTarget Bind<T>()
        {
            var target = new MapTarget(typeof(T), AddBinding);
            return target;  
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof (T));
        }

        public object Resolve(Type type)
        {
            foreach (var asm in Assemblies)
            {
                var result = asm.GetTypes().FirstOrDefault(t => t == type);
                var bindingConfig = GetBindingConfig(type); 
                if (result == null)
                {
                    if (bindingConfig == null)
                        continue;

                    if (bindingConfig.Lifetime == Lifetime.Singleton && bindingConfig.Instance != null)
                    {
                        return bindingConfig.Instance;
                    }

                    result = bindingConfig.Type;
                }

                var constructors = result.GetConstructors();
                foreach (var con in constructors)
                {
                    object output;
                    var pars = con.GetParameters();
                    if (pars.Count().Equals(0))
                    {
                        output = Activator.CreateInstance(result);
                        if (bindingConfig != null && bindingConfig.Lifetime == Lifetime.Singleton)
                        {
                            bindingConfig.Instance = output;
                            Bindings[type] = bindingConfig;
                        }
                        return output;
                    }

                    var injected = pars.Select(par => Resolve(par.ParameterType)).ToArray();
                    output = Activator.CreateInstance(result, injected);
                    if (bindingConfig != null && bindingConfig.Lifetime == Lifetime.Singleton)
                    {
                        bindingConfig.Instance = output;
                        Bindings[type] = bindingConfig;
                    }
                    return output;
                }
            }

            return null;
        }

        private BindingConfig GetBindingConfig(Type type)
        {
            var result = Bindings.ContainsKey(type) ? Bindings[type] : null;
            if (result != null)
                return result;

            result = Bindings.Values.FirstOrDefault(t => t.Type == type);
            return result;
        }

        private BindingConfig AddBinding(Type source, Type target)
        {
            var config = new BindingConfig {Type = target};
            Bindings.Add(source, config);
            return config;  
        }


    }
}
