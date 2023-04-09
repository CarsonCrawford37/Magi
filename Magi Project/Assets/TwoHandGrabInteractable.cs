using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/*Instruction source:
 * https://circuitstream.com/blog/two-handed-interactions-with-unity-xr-interaction-toolkit
 */

public class TwoHandGrabInteractable : XRGrabInteractable
{
    [SerializeField] private Transform _secondAttachTransform;

    //Sets selection mode to multiple
    protected override void Awake()
    {
        base.Awake();
        selectMode = InteractableSelectMode.Multiple;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        //if one hand is grabbing then do default logic
        if(interactorsSelecting.Count == 1)
        {
            base.ProcessInteractable(updatePhase);
        }
        //if two hands are grabbing then do different logic
        else if(interactorsSelecting.Count == 2 && updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            ProcessDoubleGrip();
        }
    }

    //What happens when we have to hands gripping the object
    private void ProcessDoubleGrip()
    {
        //Get required Transforms
        Transform firstAttach = GetAttachTransform(null);
        Transform firstHand = interactorsSelecting[0].transform;
        Transform secondAttach = _secondAttachTransform;
        Transform secondHand = interactorsSelecting[1].transform;

        //Gets a vecotor from both the hand positions
        Vector3 directionBetweenHands = secondHand.position - firstHand.position;
        //Quaternion rotationFromAttachToForward = Quaternion.FromToRotation(directionBetweenHands, transform.forward);

        Quaternion targetRoatation = Quaternion.LookRotation(directionBetweenHands, firstHand.up);

        //takes world position and converts it to a local position
        Vector3 worldDirection = transform.position - firstAttach.position;
        Vector3 localDirection = transform.InverseTransformDirection(worldDirection);

        Vector3 targetPosition = firstHand.position + targetRoatation * localDirection;

        transform.SetPositionAndRotation(targetPosition, targetRoatation);
    }
}
