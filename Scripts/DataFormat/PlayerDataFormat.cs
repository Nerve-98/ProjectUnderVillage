using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region SwordCharacter
[System.Serializable]
public class CharacterStat
{
    public string id;
    public int hp;
    public int attackdmg;
    public int eskillattackdmg;

}

[System.Serializable]
public class CharacterStatData : ILoader<string, CharacterStat>
{
    public List<CharacterStat> stats = new List<CharacterStat>();

    public Dictionary<string, CharacterStat> MakeDict()
    {
        Dictionary<string, CharacterStat> dict = new Dictionary<string, CharacterStat>();

        foreach (CharacterStat stat in stats)
            dict.Add(stat.id, stat);

        return dict;
    }
}
#endregion