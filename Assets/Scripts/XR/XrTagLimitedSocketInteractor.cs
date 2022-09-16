using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XrTagLimitedSocketInteractor : XRSocketInteractor
{
    public string interactableTag;
    
    public override bool CanSelect(XRBaseInteractable interactable)
    {
        return base.CanSelect(interactable) && interactable.CompareTag(interactableTag);
    }
}