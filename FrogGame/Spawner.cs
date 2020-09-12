using System;
using System.Collections.Generic;
using System.Text;

namespace FrogGame
{
    public static class Spawner
    {

        static int enemySpawnCooldown = 500;
        static int maxEnemySpawnCooldown = 500;

        static int pickupSpawnCooldown = 100;
        static int maxPickupSpawnCooldown = 300;

        static Random rng = new Random();

        public static void Update()
        {
            //enemy spawner
            if (enemySpawnCooldown > 0)
                enemySpawnCooldown--;
            else
            {
                enemySpawnCooldown = maxEnemySpawnCooldown;

                if (maxEnemySpawnCooldown < 5000)
                    maxEnemySpawnCooldown += 100;

                SpawnEnemy();
            }

            //pickup spawner
            if (pickupSpawnCooldown > 0)
                pickupSpawnCooldown--;
            else
            {
                pickupSpawnCooldown = maxPickupSpawnCooldown;
                SpawnPickup();
            }
        }

        static void SpawnEnemy()
        {
            int spawnX = rng.Next(0, Renderer.cam.width / Renderer.cam.scale);
            int spawnY = rng.Next(0, Renderer.cam.height / Renderer.cam.scale);

            BadFrog badFrog = new BadFrog(spawnX, spawnY);
            EntityManager.AddEntity(badFrog);
        }

        static void SpawnPickup()
        {
            int spawnX = rng.Next(0, Renderer.cam.width / Renderer.cam.scale);
            int spawnY = rng.Next(0, Renderer.cam.height / Renderer.cam.scale);

            Pickup pickup = new Pickup((Pickup.PickupType)2, spawnX, spawnY);
            EntityManager.AddEntity(pickup);
        }

    }
}
