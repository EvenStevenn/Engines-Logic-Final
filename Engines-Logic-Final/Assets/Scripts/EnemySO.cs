using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Enemy", menuName ="Create Enemy SO", order = 1)]
public class EnemySO : ScriptableObject
{
    public int health;
    public int numOfCoins;
    public bool isDead;
    public int EnemySpeed;
    public GameObject enemyPrefab;

}
