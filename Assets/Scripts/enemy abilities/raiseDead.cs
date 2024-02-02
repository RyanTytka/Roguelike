using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raiseDead : AbilityInterface
{
    public GameObject skeletonPrefab;

    public override void Use()
    {
        Debug.Log("raise dead used");

        //turn all bone piles into skeletons
        BattleManager bm = GameObject.Find("GameManager").GetComponent<BattleManager>();
        Encounter encounter = bm.gameObject.GetComponentInChildren<Encounter>();
        for(int i = 0; i < bm.battlingUnits.Count; i++)
        {
            if(bm.battlingUnits[i].tag == "Enemy" && bm.battlingUnits[i].GetComponent<UnitStats>().unitName == "Bone Pile")
            {
                Destroy(bm.battlingUnits[i]);
                GameObject newSkeleton = Instantiate(skeletonPrefab, new Vector3(bm.battlingUnits.Count * 2, -2, 10), Quaternion.identity, encounter.transform);
                bm.battlingUnits[i] = newSkeleton;
            }
        }
        //restructure unit order
        GameObject.Find("GameManager").GetComponent<BattleManager>().UpdateUnitPositions();
    }
    public override string GetDescription()
    {
        return "Summon a skeleton.";
    }

}
