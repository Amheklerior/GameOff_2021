using System;
using UnityEngine;

namespace Amheklerior.Core.Variables {
    
    [Serializable]
    [CreateAssetMenu(menuName = GameVariableUtility.GAME_VARIABLE_MENU_ROOT + "Int 2D vector var", order = 8)]
    public class Vector2DIntVariable : GameVariable<Vector2Int> { }

}