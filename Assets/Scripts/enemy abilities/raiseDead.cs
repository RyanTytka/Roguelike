using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raiseDead : AbilityInterface
{
    public GameObject skeletonPrefab;

    public override void Use()
    {
        Debug.Log("raise dead used");

        //summon a skeleton
        BattleManager bm = GameObject.Find("GameManager").GetComponent<BattleManager>();
        Encounter encounter = bm.gameObject.GetComponentInChildren<Encounter>();
        GameObject newSkeleton = Instantiate(skeletonPrefab, new Vector3(bm.battlingUnits.Count * 2, -2, 10), Quaternion.identity, encounter.transform);
        bm.battlingUnits.Add(newSkeleton);
    }
}
