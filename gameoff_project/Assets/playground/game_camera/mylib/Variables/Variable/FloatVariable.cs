using System;
using UnityEngine;

namespace Amheklerior.Core.Variables {
    
    [Serializable]
    [CreateAssetMenu(menuName = GameVariableUtility.GAME_VARIABLE_MENU_ROOT + "Float var", order = 4)]
    public class FloatVariable : GameVariable<float> { }

}