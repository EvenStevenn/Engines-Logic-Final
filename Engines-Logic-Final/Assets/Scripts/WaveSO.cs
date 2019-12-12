using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public enum WaveState
{
    available,
    attacking,
    finished
}

[CreateAssetMenu(fileName = "Wave", menuName = "Create Wave SO", order = 2)]
public class WaveSO : ScriptableObject
{
    //public List<ScriptableObject> ListOfEnemiesinWave;

    public List<EnemySO> ListOfEnemies = new List<EnemySO>();
    public WaveState waveState;
    public int deadEnemyCount = 0;
}
