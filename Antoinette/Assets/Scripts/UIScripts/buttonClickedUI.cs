using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
// using TMPro;

public class buttonClickedUI : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerEnterHandler, IPointerExitHandler
{
    // changes the button sprites + add Click sounds
/*
 to enable for a button object: 
 1. attach this script to the button 
 2. add the button related sprites + audio scorces
*/
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default ,_hover, _pressed;
    [SerializeField] private AudioClip _compressClip;
    [SerializeField] private AudioSource _source;
    

    
    public void OnPointerDown(PointerEventData eventData){
        if (_img != null && _source != null){
            _img.sprite = _pressed;
            _source.PlayOneShot(_compressClip);
        }
    }
    public void OnPointerUp(PointerEventData eventData){
        if (_img != null && _source != null){
            _img.sprite = _default;
            _source.PlayOneShot(_compressClip);
        }
    }



    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (_img != null){
            _img.sprite = _hover;
        }
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (_img != null){
            _img.sprite = _default;
        }
    }

}
