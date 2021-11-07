using System;
using UnityEngine;

namespace Amheklerior.Core.Variables {
    
    [Serializable]
    [CreateAssetMenu(menuName = GameVariableUtility.GAME_VARIABLE_MENU_ROOT + "Double var", order = 5)]
    public class DoubleVariable : GameVariable<double> { }

}