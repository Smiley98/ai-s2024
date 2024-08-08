using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    Enemy enemy;

    [SerializeField]
    GameObject shotgunPrefab;

    [SerializeField]
    GameObject sniperPrefab;

    Timer weaponSpawner = new Timer();

    void Start()
    {
        weaponSpawner.total = 1.0f;
    }

    void Update()
    {
        bool canSpawn = !(
            enemy.hasShotgun && enemy.hasSniper &&
            player.hasShotgun && player.hasSniper);

        if (canSpawn)
        {
            weaponSpawner.Tick(Time.deltaTime);
            if (weaponSpawner.Expired())
            {
                int value = Random.Range(1, 3);
                if (value == (int)WeaponType.SHOTGUN)
                {
                    Debug.Log("Spawning Shotgun");
                }
                else if (value == (int)WeaponType.SNIPER)
                {
                    Debug.Log("Spawning Sniper");
                }

                // Spawner test:
                //else
                //{
                //    Debug.Log("Connor can't write if-statements ;)");
                //}
                //Debug.Log(value);
            }
        }
    }
}
