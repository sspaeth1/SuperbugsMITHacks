using UnityEngine;
using UnityEngine.SceneManagement;

namespace CompleteProject
{
    public class EnemyManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;       // Reference to the player's heatlh.
        public GameObject enemy;                // The enemy prefab to be spawned.
        public float spawnInterval = 2f;            // How long between each spawn.
        private int count = 12;
        private float previousSpawnTime = 0;
        public float spawnRadius = 15.0f;
        public float startSpawn;
        private float deathDelay = 18;
        private float allInstantDone = 30;
        private float startShrink = 25;
        private float deathDelayStar = 40;


        void Update ()
        {

            if (Time.time > startSpawn && count > 0 && Time.time - previousSpawnTime > spawnInterval) Spawn();
            deathDelay -= Time.deltaTime;
            deathDelayStar -= Time.deltaTime;
            allInstantDone -= Time.deltaTime;
            startShrink -= Time.deltaTime;
            if (deathDelay <= 0) DestroyAll("Van");
            if (deathDelayStar <= 0) DestroyAll("Star");

            float f =  ( (Time.time - 35) / 10);
            f = Mathf.Max(0, f);
            f = Mathf.Min(1, f);
            f = 1 - f;
            Transform target = GameObject.FindGameObjectWithTag("Player").transform;
            Transform target2 = GameObject.FindGameObjectWithTag("Capsule").transform;
            target.localScale = target.localScale * f;
            target2.localScale = target2.localScale * f;

            if (allInstantDone <= 0) SceneManager.LoadScene("EndingVideo");
        }


        void Spawn ()
        {

            previousSpawnTime = Time.time;
            count--;

            // If the player has no health left...
            if(playerHealth.currentHealth <= 0f)
            {
                // ... exit the function.
                return;
            }


            if (deathDelay <= 0) {
                if (enemy.tag == "Van")
                    return;
            }
            if (allInstantDone <= 0) return;
           
            Vector3 spawnPosition = new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1);
            Instantiate(enemy, spawnPosition.normalized*spawnRadius, Quaternion.identity);
      
    }

        private void DestroyAll(string tag)
        {
            GameObject[] objsToDestroy = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject obj in objsToDestroy)
            {
                Destroy(obj);
            }

        }



    }
}