using System;
using UnityEngine;
using Amheklerior.Core.Common;

namespace Amheklerior.Core.Variables {

    // TODO: instantiate the debuggable version when in debugMode

    public abstract class GameVariable<T> : RichScriptableObject, IVariable<T> {
        
        #region Inspector interface

        [Space, Header("Settings:")]

        [Tooltip(tooltip:"The initial value for this variable.")]
        [SerializeField] private T _initialValue = default(T);

        [Tooltip(tooltip:"If set, an onChange event is fired everytime the value changes.")]
        [SerializeField] private bool _triggerEvents = true;

        [Tooltip(tooltip:"If set, it reset the variable to its initial value when enabled.")]
        [SerializeField] private bool _resetOnEnabled = true;

        #endregion

        private IVariable<T> _var;


        #region Unity lifecycle

        private void OnEnable() {
            Debug.Log($"AWAKE {_var}");
#if UNITY_EDITOR
            _var = new Variable<T>(_initialValue, _triggerEvents); // _debugMode ? new DebuggableVariable<T>() : new Variable<T>();
            _description = _description ?? $"{name} Variable";
#else
            _var = new Variable<T>(_initialValue, _triggerEvents);
#endif
        }

        #endregion


        #region Interface implementation

        public T Value {
            get => _var.Value;
            set => _var.Value = value;
        }

        public T PreviousValue { 
            get => _var.PreviousValue;
        }

        public T InitialValue { 
            get => _var.InitialValue;
        }

        public void Reset(bool triggerOnChangeEvent = false) => _var.Reset(triggerOnChangeEvent); 

        public bool Equals(IVariable<T> other) => _var.Equals(other);

        #endregion
        
    }
}