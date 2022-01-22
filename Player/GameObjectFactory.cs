using UnityEngine;

public class GameObjectFactory : MonoBehaviour
{
	public T CreateGameObjectInstance<T>(T prefab) where T : MonoBehaviour
	{
		T instance = Instantiate(prefab);

		return instance;
	}
}
