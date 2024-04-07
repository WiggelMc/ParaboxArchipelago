using System;
using System.Collections.Generic;
using System.Linq;

namespace ParaboxArchipelago.Gen
{
    public class Registry
    {
        private Registry()
        {
        }

        public static Registry Load()
        {
            var r = new Registry();
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.GetCustomAttributes(typeof(RegisterAttribute), false).Length > 0);
            
            foreach (var type in types)
                r.items[type] = Activator.CreateInstance(type);
            
            return r;
        }

        private readonly Dictionary<Type, object> items = new();
        
        public T Get<T>() where T : IRegistered, new() => (T) items[typeof(T)];
    }
}