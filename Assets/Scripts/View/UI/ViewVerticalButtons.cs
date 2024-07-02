using UnityEngine;
using UnityEngine.UI;

public class ViewVerticalButtons : MonoBehaviour
{
    [SerializeField] private DestructionBlocks _destructionBlocks;  
    [SerializeField] private FaceController _faceController;  
    [SerializeField] private GameObject _move;  
    [SerializeField] private GameObject _activities;
    [SerializeField] private Button _switchingMove;
    [SerializeField] private Button _switchingActivities;

    private void OnEnable()
    {
        UpdateView();
    }

    public void UpdateView()
    {
        if(_faceController.ShapeType == ShapeType.Classic)
        {
            _move.SetActive(false);
            _activities.SetActive(true);
            _switchingMove.interactable = false;
            _switchingActivities.interactable = false;
        }
        else
        {
            _move.SetActive(!_destructionBlocks.IsWork);
            _activities.SetActive(_destructionBlocks.IsWork);
            _switchingMove.interactable = true;
            _switchingActivities.interactable = true;
        }
    }
}
