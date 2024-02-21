using System;
using JetBrains.Annotations;

namespace ParaboxArchipelago.LevelGenerator
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GenerateAttribute : Attribute
    {
        public string Since { get; private set; }
        [CanBeNull] public Type ReplacedBy { get; private set; }
        [CanBeNull] public string RemovedSince { get; private set; }
        
        public GenerateAttribute(string since)
        {
            Since = since;
        }
        
        public GenerateAttribute(string since, Type replacedBy) : this(since)
        {
            ReplacedBy = replacedBy;
        }
        
        public GenerateAttribute(string since, string removedSince) : this(since)
        {
            RemovedSince = removedSince;
        }
        
        public abstract class Attribute: System.Attribute
        {
            public abstract void Generate(Type type);
        }
    }
}