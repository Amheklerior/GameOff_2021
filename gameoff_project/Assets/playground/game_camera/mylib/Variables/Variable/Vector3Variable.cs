using System;
using UnityEngine;

namespace Amheklerior.Core.Variables {
    
    [Serializable]
    [CreateAssetMenu(menuName = GameVariableUtility.GAME_VARIABLE_MENU_ROOT + "3D vector var", order = 9)]
    public class Vector3DVariable : GameVariable<Vector3> { }

}