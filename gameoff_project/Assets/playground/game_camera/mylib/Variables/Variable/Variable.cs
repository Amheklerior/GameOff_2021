using System;

namespace Amheklerior.Core.Variables {

    // TODO: Implement a Debuggable version of the variable class

    // TODO: Think about removing the IEquatable constraint and create the GameObj var

    public class Variable<T> : IVariable<T> {

        public Variable(T initialVal = default(T), bool triggerEvents = false) {
            _initialValue = initialVal;
            _triggerEvents = triggerEvents;
        }

        public T Value {
            get => _currentValue;
            set {
                if (_currentValue.Equals(value)) return;
                _previousValue = _currentValue;
                _currentValue = value;
            }
        }

        public T PreviousValue { 
            get => _previousValue;
        }

        public T InitialValue { 
            get => _initialValue;
        }

        public void Reset(bool triggerOnChangeEvent = false) {
            _currentValue = _initialValue;
            _previousValue = _initialValue;
        }

        public bool Equals(IVariable<T> other) => Value.Equals(other.Value);
        

        #region Internals

        private T _initialValue;
        private bool _triggerEvents;
        private T _currentValue; 
        private T _previousValue;

        
        #endregion

        public static implicit operator T(Variable<T> variable) => variable.Value;
        
    }

}
