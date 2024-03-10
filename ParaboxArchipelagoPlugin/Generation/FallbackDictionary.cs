using System;
using System.Collections.Generic;

namespace ParaboxArchipelago.Generation
{
    public class FallbackDictionary<T> : Dictionary<string, Func<T>>
    {
        
    }
}