using UnityEngine;
using Amheklerior.Core.Variables;

public enum Direction { RIGHT, LEFT, NONE }

[CreateAssetMenu(menuName = "tmp/cam/Lookup/Movement Direction")]
public class MovementDirection : GameVariable<Direction> { }
