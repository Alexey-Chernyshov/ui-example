using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public RectTransform content;
    public RectTransform prefab;

    Sprite[] img_btnTransparent;
    public static Sprite[] img_element;
    public static Sprite[] img_mark;
    public static Sprite[] img_eye;

    GameObject go_window; 
    Button btn_hide;
    CanvasGroup cg_window;
    Button[] btn_transparent;
    Button btn_showMarked;
    Button btn_markAll;

    int numberElements;
    Element[] elements;

    bool hide = false;
    int transparentState = 0;
    bool showMarked = false;
    bool markAll = false;
    int currentElement = -1;

    void Awake()
    {
        img_btnTransparent = new Sprite[2];
        img_btnTransparent[0] = Resources.Load<Sprite>("Sprites/btn_transparent0_64_64");
        img_btnTransparent[1] = Resources.Load<Sprite>("Sprites/btn_transparent1_64_64");
        img_element = new Sprite[2];
        img_element[0] = Resources.Load<Sprite>("Sprites/element0_128_32");
        img_element[1] = Resources.Load<Sprite>("Sprites/element1_128_32");
        img_mark = new Sprite[2];
        img_mark[0] = Resources.Load<Sprite>("Sprites/mark0_64_64");
        img_mark[1] = Resources.Load<Sprite>("Sprites/mark1_64_64");
        img_eye = new Sprite[2];
        img_eye[0] = Resources.Load<Sprite>("Sprites/eye0_64_64");
        img_eye[1] = Resources.Load<Sprite>("Sprites/eye1_64_64");

        go_window = GameObject.Find("UI/Window/img_bg_window").GetComponent<Image>().gameObject;
        btn_hide = GameObject.Find("UI/Window/btn_hide").GetComponent<Button>();

        cg_window = GameObject.Find("UI/Window").GetComponent<CanvasGroup>();

        btn_transparent = new Button[5];
        for (int i = 0; i < 5; i++)
        {
            btn_transparent[i] = GameObject.Find("UI/Window/img_bg_window/img_bg_transparency/btn_transparent_" + (i + 1)).GetComponent<Button>();
            if (i == 0)
                btn_transparent[i].image.sprite = img_btnTransparent[1];
            else
                btn_transparent[i].image.sprite = img_btnTransparent[0];
        }

        btn_showMarked = GameObject.Find("UI/Window/img_bg_window/btn_show_marked").GetComponent<Button>();
        btn_showMarked.image.sprite = img_eye[0];
        btn_markAll = GameObject.Find("UI/Window/img_bg_window/btn_mark_all").GetComponent<Button>();
        btn_markAll.image.sprite = img_mark[0];

        InitializeElements(7);
    }

    void InitializeElements(int n)
    {
        numberElements = n;
        elements = new Element[numberElements];

        for (int i = 0; i < numberElements; i++)
        {
            var element = GameObject.Instantiate(prefab.gameObject) as GameObject;
            elements[i] = new Element(element, i, "element " + (i+1));
            elements[i].go_element.transform.SetParent(content, false);

            Element elem = elements[i];
            elem.btn_element.onClick.AddListener(
                () =>
                {
                    if (currentElement != -1)
                        elements[currentElement].RemoveSelection();
                    elem.ChangeMark_1();
                    currentElement = elem.id;
                    
                }
            );
        }
    }

    void ContentClear()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }

    void ShowAllElements()
    {
        for (int i = 0; i < numberElements; i++)
        {
            elements[i].Show();
        }
    }

    void ShowMarkedElements()
    {
        for (int i = 0; i < numberElements; i++)
        {
            elements[i].ShowMarked();
        }
    }

    public void btn_ShowMarked()
    {
        markAll = false;
        btn_markAll.image.sprite = img_mark[0];

        if (currentElement != -1)
        {
            elements[currentElement].RemoveSelection();
            currentElement = -1;
        }

        if (showMarked == false)
        {
            showMarked = true;
            btn_showMarked.image.sprite = img_eye[1];
            ShowMarkedElements();
        }
        else
        {
            showMarked = false;
            btn_showMarked.image.sprite = img_eye[0];
            ShowAllElements();
        }
    }

    public void btn_MarkAll()
    {
        showMarked = false;
        btn_showMarked.image.sprite = img_eye[0];

        if (currentElement != -1)
        {
            elements[currentElement].RemoveSelection();
            currentElement = -1;
        }

        if (markAll == false)
        {
            markAll = true;
            btn_markAll.image.sprite = img_mark[1];
            for (int i = 0; i < numberElements; i++)
            {
                elements[i].ChangeMark_2(false);
            }
        }
        else
        {
            markAll = false;
            btn_markAll.image.sprite = img_mark[0];
            for (int i = 0; i < numberElements; i++)
            {
                elements[i].ChangeMark_2(true);
            }
        }

        ShowAllElements();
    }

    public void btn_HideClick()
    {
        if (hide == false)
        {
            hide = true;
            go_window.SetActive(false);
        }
        else
        {
            hide = false;
            go_window.SetActive(true);
        }
    }

    public void btn_TransparentClick(int newState)
    {
        btn_transparent[transparentState].image.sprite = img_btnTransparent[0];
        transparentState = newState;
        btn_transparent[transparentState].image.sprite = img_btnTransparent[1];

        switch (transparentState)
        {
            case 0:
                cg_window.alpha = 1f;
                break;

            case 1:
                cg_window.alpha = 0.8f;
                break;

            case 2:
                cg_window.alpha = 0.6f;
                break;

            case 3:
                cg_window.alpha = 0.4f;
                break;

            case 4:
                cg_window.alpha = 0.2f;
                break;
        }
    }
}
