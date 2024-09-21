using UnityEngine;

public interface IGameFactory
{
   
    public void SpawnHero(Vector3 at);

    public void SpawnMap(Vector3 at);

    public void SpawnUI();
    void Cleanup();
}