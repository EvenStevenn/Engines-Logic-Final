using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Create Wave SO", order = 2)]
public class WaveSO : ScriptableObject
{
    //public List<ScriptableObject> ListOfEnemiesinWave;

    public List<EnemySO> ListOfEnemies = new List<EnemySO>();
}
