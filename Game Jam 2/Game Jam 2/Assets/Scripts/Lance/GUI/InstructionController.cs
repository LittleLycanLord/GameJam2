using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InstructionController : MonoBehaviour
{
    [SerializeField] public int currentPage;
    [SerializeField] private GameObject pageImage1;
    [SerializeField] private GameObject pageImage2;
    [SerializeField] private GameObject pageImage3;
    [SerializeField] private GameObject pageImage4;
    [SerializeField] private GameObject OkButton;

    [SerializeField] private bool enablePageUpdate;

    public int maxPage = 4;
   
    // Start is called before the first frame update
    void Start()
    {
        this.currentPage = 1;
        this.pageImage1.SetActive(false);
        this.pageImage2.SetActive(false);
        this.pageImage3.SetActive(false);
        this.pageImage4.SetActive(false);
        this.OkButton.SetActive(false);
    }

    private void updatePageSprite()
    {
        switch (this.currentPage)
        {
            case 1:
                pageImage1.SetActive(true);
                pageImage2.SetActive(false);
                pageImage3.SetActive(false);
                pageImage4.SetActive(false);
                OkButton.SetActive(true);
                break;
            case 2:
                pageImage1.SetActive(false);
                pageImage2.SetActive(true);
                pageImage3.SetActive(false);
                pageImage4.SetActive(false);
                OkButton.SetActive(true);
                break;
            case 3:
                pageImage1.SetActive(false);
                pageImage2.SetActive(false);
                pageImage3.SetActive(true);
                pageImage4.SetActive(false);
                OkButton.SetActive(true);
                break;
            case 4:
                pageImage1.SetActive(false);
                pageImage2.SetActive(false);
                pageImage3.SetActive(false);
                pageImage4.SetActive(true);
                OkButton.SetActive(true);
                break;
        }
    }

    public void enablePageUpdateTrigger()
    {
        if (!this.enablePageUpdate)
        {
            this.enablePageUpdate = true;
        }
    }

    public void updatePage()
    {
        if (this.currentPage != this.maxPage)
        {
            this.currentPage++;
        }     
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("enablePageUpdate value: " + this.enablePageUpdate);
        if (this.enablePageUpdate == true)
        {
            this.updatePageSprite();
        }
        
    }
}
