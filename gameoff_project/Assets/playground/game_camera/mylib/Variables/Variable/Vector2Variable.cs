using System;
using UnityEngine;

namespace Amheklerior.Core.Variables {
    
    [Serializable]
    [CreateAssetMenu(menuName = GameVariableUtility.GAME_VARIABLE_MENU_ROOT + "2D vector var", order = 7)]
    public class Vector2DVariable : GameVariable<Vector2> { }

}