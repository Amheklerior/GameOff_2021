using System;

namespace Amheklerior.Core.Variables {

    // TODO: Think about removing the IEquatable constraint and create the GameObj var
    
    public interface IVariable<T> {
        T Value { get; set; }
        T PreviousValue { get; }
        T InitialValue { get; }

        void Reset(bool triggerOnChangeEvent = false);
        
    }

}
