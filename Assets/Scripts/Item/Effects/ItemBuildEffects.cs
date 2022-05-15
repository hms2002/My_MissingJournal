using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "ItemEft/Costomable/Build")]
public class ItemBuildEffects : ItemEffects
{
    public GameObject prefab;
    public float upValue;
    PlayerMovment player;

    private Vector2 FootPosition;

    int buildLayer;

    public override bool ExecuteRole()
    {
        buildLayer = (1 << LayerMask.NameToLayer("Building"));

        player = PlayerMovment.instance;

        if(player.isCaveGround == true)
        {
            Bounds bounds = player.GetComponent<CapsuleCollider2D>().bounds;

            FootPosition = new Vector2(bounds.center.x, bounds.min.y);

            if (Physics2D.OverlapCircle(FootPosition + new Vector2(0, 1), 3f, buildLayer)) { return false; }
            else Instantiate(prefab, FootPosition + new Vector2(0, upValue), Quaternion.identity);
            return true;
        }

        return false;
    }
}
