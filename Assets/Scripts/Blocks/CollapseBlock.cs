using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseBlock : MonoBehaviour, ICollapseable
{
    [SerializeField] private Sprite breakSprite;
    [SerializeField] private GameObject breakingParticles;
    private bool isCollapsing = false, isShaking = false;
    private float collapseTime=.7f, collapseTimer;
    private int collapseCount=0, collapseCountMax = 1;

    public void Collapse()
    {
        if(!isCollapsing)
        {
            SwapSprite();
            isCollapsing = true;
            collapseTimer = collapseTime;
            collapseCount = 0;
        }
    }

    private void Update() 
    {
        if(isCollapsing)
        {
            collapseTimer -= Time.deltaTime;
            if(collapseTimer < 0)
            {
                collapseCount++;
                if(collapseCount > collapseCountMax)
                {
                    Break();
                }
            }
        }   
    }

    private void Break()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, 1f);
    }

    private void SwapSprite()
    {
        GetComponent<SpriteRenderer>().sprite = breakSprite;
        breakingParticles.SetActive(true);
    }
}
