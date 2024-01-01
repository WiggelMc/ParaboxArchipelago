using System.Collections.Generic;
using HarmonyLib;

namespace ParaboxArchipelago.State
{
    public class MethodState
    {
        private readonly Dictionary<string, int> _runningMethods = new Dictionary<string, int>();

        public bool IsMethodRunning(string method) => _runningMethods.GetValueSafe(method) > 0;
        public void StartMethod(string method) => _runningMethods[method] = _runningMethods.GetValueSafe(method) + 1;
        public void EndMethod(string method) => _runningMethods[method] = _runningMethods.GetValueSafe(method) - 1;
    }
}