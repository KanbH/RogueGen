using UnityEngine;
using NUnit.Framework;

public class SpriteDepthSorter : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        Assert.NotNull(_spriteRenderer);
    }

    void LateUpdate()
    {
        _spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }
}
