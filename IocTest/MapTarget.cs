using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocTest
{
    public class MapTarget
    {
        private Func<Type, Type,BindingConfig> AddFunction { get; set; }
        private Type SourceType { get; set; }

        public MapTarget(Type sourceType, Func<Type, Type, BindingConfig> addFunction)
        {
            SourceType = sourceType;
            AddFunction = addFunction;
        }

        public LifetimeManager To<T>()
        {
            var config = AddFunction.Invoke(SourceType,typeof(T));
            return new LifetimeManager(config);
        }

        public LifetimeManager ToSelf()
        {
            var config = AddFunction.Invoke(SourceType,SourceType);
            return new LifetimeManager(config);
        }
    }
}
