using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabController : MonoBehaviour
{
    public GameObject content1;
    public GameObject content2;
    public GameObject content3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTab1Click()
    {
        ShowContent(content1);
    }

    public void OnTab2Click()
    {
        ShowContent(content2);
    }

    public void OnTab3Click()
    {
        ShowContent(content3);
    }

    private void ShowContent(GameObject content)
    {
        content1.SetActive(content == content1);
        content2.SetActive(content == content2);
        content3.SetActive(content == content3);
    }
}