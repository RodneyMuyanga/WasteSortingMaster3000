// using System.Collections;
// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.TestTools;
//
// namespace Tests.PlayMode
// {
//     [TestFixture]
//     public class WasteSpawnerStressTest
//     {
//         private GameObject spawnerObject;
//         private WasteSpawner spawner;
//         private int initialObjectCount;
//
//         [SetUp]
//         public void Setup()
//         {
//             // Create a new GameObject with the WasteSpawner component
//             spawnerObject = new GameObject("TestWasteSpawner");
//             spawner = spawnerObject.AddComponent<WasteSpawner>();
//
//             // Mock some waste prefabs with Rigidbody
//             GameObject wastePrefab = GameObject.CreatePrimitive(PrimitiveType.Cube); 
//             wastePrefab.AddComponent<Rigidbody>(); // Fix: Add Rigidbody
//             spawner.wastePrefabs = new GameObject[] { wastePrefab };
//
//             // Start waste spawning
//             spawner.StartCoroutine(spawner.SpawnWasteWithDelay());
//
//             // Store initial count of spawned objects
//             initialObjectCount = Object.FindObjectsOfType<Rigidbody>().Length;
//         }
//
//         [UnityTest]
//         public IEnumerator StressTestSpawnRate()
//         {
//             float testDuration = 10f; // Run test for 10 seconds
//             float startTime = Time.time;
//
//             while (Time.time - startTime < testDuration)
//             {
//                 yield return new WaitForSeconds(1f); // Wait a second before checking count
//
//                 int currentCount = Object.FindObjectsOfType<Rigidbody>().Length; // Count Rigidbodies
//                 Debug.Log($"Objects Spawned: {currentCount - initialObjectCount}");
//
//                 Assert.LessOrEqual(currentCount - initialObjectCount, 500,
//                     "Too many objects spawned! Performance risk.");
//             }
//         }
//
//         [UnityTest]
//         public IEnumerator TestSpawnDelayDecreasing()
//         {
//             float initialDelay = spawner.maxSpawnDelay;
//             yield return new WaitForSeconds(10f); // Let spawn delay decrease over time
//
//             Assert.Less(spawner.currentMaxDelay, initialDelay, "Spawn delay should have decreased.");
//         }
//
//         [UnityTest]
//         public IEnumerator TestSpeedIncrease()
//         {
//             float initialSpeed = spawner.wasteSpeed;
//             yield return new WaitForSeconds(5f); // Wait 5 seconds
//
//             Assert.Greater(spawner.wasteSpeed, initialSpeed, "Waste speed should increase over time.");
//         }
//
//         [TearDown]
//         public void Cleanup()
//         {
//             Object.Destroy(spawnerObject);
//         }
//     }
// }
