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

    float xMin, xMax, yMin, yMax;

    void Start()
    {
        weaponSpawner.total = 1.0f;
        float size = Camera.main.orthographicSize;
        float aspect = Camera.main.aspect;
        xMin = -size * aspect; xMax = size * aspect;
        yMin = -size; yMax = size;
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
                weaponSpawner.Reset();

                int value = Random.Range(1, 3);
                if (value == (int)WeaponType.SHOTGUN)
                {
                    Debug.Log("Spawning Shotgun");
                }
                else if (value == (int)WeaponType.SNIPER)
                {
                    Debug.Log("Spawning Sniper");
                }

                Vector3 position = WeaponSpawnPosition();
                GameObject weapon = Instantiate(shotgunPrefab);
                weapon.transform.position = position;

                // Spawner test:
                //else
                //{
                //    Debug.Log("Connor can't write if-statements ;)");
                //}
                //Debug.Log(value);
            }
        }
    }

    Vector3 WeaponSpawnPosition()
    {
        float x = Random.Range(xMin, xMax);
        float y = Random.Range(yMin, yMax);
        return new Vector3(x, y);
    }
}
