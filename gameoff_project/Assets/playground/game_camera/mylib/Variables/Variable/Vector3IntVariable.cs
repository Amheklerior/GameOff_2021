using System;
using UnityEngine;

namespace Amheklerior.Core.Variables {
    
    [Serializable]
    [CreateAssetMenu(menuName = GameVariableUtility.GAME_VARIABLE_MENU_ROOT + "Int 3D vector var", order = 10)]
    public class Vector3DIntVariable : GameVariable<Vector3Int> { }

}