using UnityEngine;
using UnityEngine.UI;

public class Element
{
    public GameObject go_element;

    public Button btn_element;
    public Image img_mark;
    public Text txt_title;
    public Image img_eye;

    public int id;
    public bool marked;

    public Element(GameObject element, int n, string name)
    {
        go_element = element;

        btn_element = go_element.GetComponent<Button>();
        img_mark = go_element.transform.Find("mark").GetComponent<Image>();
        txt_title = go_element.transform.Find("title").GetComponent<Text>();
        img_eye = go_element.transform.Find("eye").GetComponent<Image>();

        btn_element.image.sprite = UIHandler.img_element[0];
        img_mark.sprite = UIHandler.img_mark[0];
        txt_title.text = name;
        img_eye.sprite = UIHandler.img_eye[0];

        id = n;
        marked = false;
    }

    public void ChangeMark_1()
    {
        ChangeMark_2(marked);
        btn_element.image.sprite = UIHandler.img_element[1];
    }

    public void ChangeMark_2(bool oldState)
    {
        if (oldState == false)
        {
            img_mark.sprite = UIHandler.img_mark[1];
            img_eye.sprite = UIHandler.img_eye[1];
            marked = true;
        }
        else
        {
            img_mark.sprite = UIHandler.img_mark[0];
            img_eye.sprite = UIHandler.img_eye[0];
            marked = false;
        }
    }

    public void RemoveSelection()
    {
        btn_element.image.sprite = UIHandler.img_element[0];
    }

    public void Show()
    {
        go_element.SetActive(true);
    }

    public void ShowMarked()
    {
        if (marked == false)
            go_element.SetActive(false);
        else
            go_element.SetActive(true);
    }
}
