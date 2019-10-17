using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NineumManager : MonoBehaviour
{
    public static NineumManager manager;
    private List<string> Nineum = new List<string>();

    private void Awake()
    {
        if(manager == null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        } else if(this != manager)
        {
            Destroy(gameObject);
        }
    }

    public void AddNineum(string ninea)
    {
        Nineum.Add(ninea);
        CharacterStats.characterStats.Nineum = Nineum;
    }

    public string GetNineumForEquipLocationAtIndex(EquipLocations location, int index)
    {
        return Nineum[0];
    }
}
