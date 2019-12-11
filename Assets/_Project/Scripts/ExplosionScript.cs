using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    void Start()
    {
        DOTween.Sequence()
            .SetEase(Ease.OutCubic)
            .Append(transform.DOScale(0, .5f).From())
            .Append(GetComponent<MeshRenderer>().material.DOColor(new Color(1,1,1,0), "_ExplosionColor", .3f))
            .AppendCallback(() => Destroy(gameObject));
    }
}
