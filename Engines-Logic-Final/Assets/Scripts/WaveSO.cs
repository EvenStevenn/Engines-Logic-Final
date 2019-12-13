using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public enum WaveState
{
    unavailable,
    available,
    attacking,
    finished
}

[CreateAssetMenu(fileName = "Wave", menuName = "Create Wave SO", order = 2)]
public class WaveSO : ScriptableObject
{
    //public List<ScriptableObject> ListOfEnemiesinWave;

    public List<GameObject> ListOfEnemies = new List<GameObject>();
    public int waveID;
    public WaveState waveState;
    public int deadEnemyCount = 0;
}
