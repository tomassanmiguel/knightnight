using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerChoiceData", menuName = "Player Choice Data")]
public class PlayerChoiceData : ScriptableObject {

    [SerializeField] public KnightData player1;
    [SerializeField] public KnightData player2;
    
}
