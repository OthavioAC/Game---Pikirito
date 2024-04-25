using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static Unity.Collections.AllocatorManager;

public class GiveFood : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
{
    public GameObject game;
    private bool blocked = false;
    public GameObject itemPuxado;
    public GameObject activated;
    public GameObject foodCountText;

    private int idFoodActual = 0;
    public List<GameObject> Foods = new List<GameObject>();

    private void Start()
    {
        Instantiate(Foods[0], activated.transform);
    }


    public void ResetFoodCount()
    {
        if (game.GetComponent<Game>().FoodCount.Count()>0)
        {
            foodCountText.GetComponent<TextMeshProUGUI>().text = game.GetComponent<Game>().FoodCount[idFoodActual].ToString();
        }
    }

    public bool GetBlocked()
    {
        return blocked;
    }
    public void SetBlocked(bool bloc)
    {
        blocked = bloc;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(activated==null)
        {
            blocked = true;
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                itemPuxado = Instantiate(Foods[idFoodActual],transform);
                itemPuxado.SetActive(false);
            }
            if(activated.activeSelf)
            {
                blocked = true;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        blocked = false;
        if(itemPuxado != null)
        {
            Destroy(itemPuxado);
            itemPuxado = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        activated.SetActive(false);
        itemPuxado.SetActive(true);
        itemPuxado.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        activated.SetActive(true);
    }
}