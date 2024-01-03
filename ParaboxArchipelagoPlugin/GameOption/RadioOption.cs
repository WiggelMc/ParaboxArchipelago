using System;
using ParaboxArchipelago.State;
using UnityEngine;

namespace ParaboxArchipelago.GameOption
{
    public class RadioOption : IGameOption
    {
        private readonly string _name;
        private readonly string[] _options;
        private readonly Action<int> _optionSetter;
        private readonly Func<int> _optionGetter;

        public RadioOption(string name, string[] options, Action<int> optionSetter, Func<int> optionGetter)
        {
            _name = name;
            _options = options;
            _optionSetter = optionSetter;
            _optionGetter = optionGetter;
        }
        
        public void Draw()
        {
            GUILayout.Label(_name);
            var oldState = _optionGetter.Invoke();
            var newState = GUILayout.SelectionGrid(oldState, _options, _options.Length, new GUIStyle("toggle"));
            _optionSetter.Invoke(newState);
        }
    }
}