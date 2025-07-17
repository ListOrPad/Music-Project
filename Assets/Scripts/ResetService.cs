using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResetService : MonoBehaviour
{
    private List<IResettable> resettables = new List<IResettable>();

    private void Awake()
    {
        // Find all Monobehaviours that implement IResettable
        var resetObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IResettable>();
        resettables.AddRange(resetObjects);
    }

    public void Register(IResettable resettable)
    {
        resettables.Add(resettable);
    }

    public void ResetAll()
    {
        foreach (var resettable in resettables)
        {
            resettable.Reset();
        }
    }
}