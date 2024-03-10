using System;
using System.Collections.Generic;

namespace ParaboxArchipelago.Generation
{
    public class Registry
    {
        private Dictionary<Type, object> items = new();
        
        public T Get<T>() where T : IRegistered, new() => (T) items[typeof(T)];
    }
}