using System;
using System.Collections.Generic;
using System.Text;

namespace FrogGame
{
    public static class Spawner
    {

        static int enemySpawnCooldown = 0;
        static int maxEnemySpawnCooldown = 260;
        static int lowestMaxSpawnCooldown = 180;

        static int pickupSpawnCooldown = 100;
        static int maxPickupSpawnCooldown = 200;

        static Random rng = new Random();

        public static void Update()
        {
            //enemy spawner
            if (enemySpawnCooldown > 0)
                enemySpawnCooldown--;
            else
            {
                enemySpawnCooldown = maxEnemySpawnCooldown;

                if (maxEnemySpawnCooldown > lowestMaxSpawnCooldown)
                    maxEnemySpawnCooldown -= 10;

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
            int spawnX = rng.Next(8, (Renderer.cam.width / Renderer.cam.scale) - 16);
            int spawnY = rng.Next(8, (Renderer.cam.height / Renderer.cam.scale) - 16);

            BadFrog badFrog = new BadFrog(spawnX, spawnY);
            EntityManager.AddEntity(badFrog);
        }

        static void SpawnPickup()
        {
            int spawnX = rng.Next(8, (Renderer.cam.width / Renderer.cam.scale) - 16);
            int spawnY = rng.Next(8, (Renderer.cam.height / Renderer.cam.scale) - 16);

            Pickup pickup = new Pickup((Pickup.PickupType)rng.Next(2), spawnX, spawnY);
            EntityManager.AddEntity(pickup);
        }

    }
}
