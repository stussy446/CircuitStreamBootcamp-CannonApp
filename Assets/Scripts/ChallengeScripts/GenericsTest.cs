using UnityEngine;
using TMPro;

public class GenericsTest : MonoBehaviour, IGenericsTest
{
    private void CustomAddComponent<T>(GameObject gObject) where T : Component
    {
        gObject.AddComponent<T>();
    }

    private void Awake()
    {
        CustomAddComponent<BoxCollider>(this.gameObject);

        CustomAddComponent<MeshRenderer>(this.gameObject);

        CustomAddComponent<Animator>(this.gameObject);

        CustomAddComponent<TextMeshPro>(this.gameObject);
    }



    public void Log<T>(T score)
    {
        Debug.Log($"GenericsTest {score}");
    }

    public void ParentStuff<T>(Transform obj)
    {
        gameObject.transform.SetParent(obj);
    }
}
