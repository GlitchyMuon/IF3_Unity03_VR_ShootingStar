using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SlingshotStringController : MonoBehaviour
{
    [SerializeField] private SlingshotString slingshotStringRenderer;

    private XRGrabInteractable interactable;

    [SerializeField] private Transform midPointGrabObject, midPointVisualObject, midPointParent;

    [SerializeField] private float slingshotStringStretchLimit = 0.5f;

    private Transform interactor;

    private void Awake()
    {
        interactable = midPointGrabObject.GetComponent<XRGrabInteractable>();
    }
    private void Start()
    {
        interactable.selectEntered.AddListener(PrepareSlingshotString);
        interactable.selectExited.AddListener(ResetSlingshotString);
    }

    private void ResetSlingshotString(SelectExitEventArgs args)
    {
        interactor = null;
        midPointGrabObject.localPosition = Vector3.zero;
        midPointVisualObject.localPosition = Vector3.zero;
        slingshotStringRenderer.CreateString(null);
        
    }

    private void PrepareSlingshotString(SelectEnterEventArgs args)
    {
        interactor = args.interactorObject.transform;
    }

    private void Update()
    {
        if(interactor!= null)
        {
            //convert slingshot string midpoint to the local space of the MidPoint
            Vector3 midPointLocalSpace = midPointParent.InverseTransformPoint(midPointGrabObject.position);

            //get the offset
            float midPointLocalZAbs = Mathf.Abs(midPointLocalSpace.z);

            HandleStringPushedBackToStart(midPointLocalSpace);

            HandleStringPulledBackToLimit(midPointLocalZAbs, midPointLocalSpace);

            HandlePullingString(midPointLocalZAbs, midPointLocalSpace);

            slingshotStringRenderer.CreateString(midPointVisualObject.transform.position);
        }
    }

    private void HandlePullingString(float midPointLocalZAbs, Vector3 midPointLocalSpace)
    {
        //quand on est entre point 0 et la limite de stretching
        if(midPointLocalSpace.z < 0 && midPointLocalZAbs > slingshotStringStretchLimit)
        {
            midPointVisualObject.localPosition = new Vector3(0,0, midPointLocalSpace.z);
        }
    }
    private void HandleStringPulledBackToLimit(float midPointLocalZAbs, Vector3 midPointLocalSpace)
    {
        if(midPointLocalSpace.z > 0 && midPointLocalZAbs >= slingshotStringStretchLimit)
        {
            midPointVisualObject.localPosition = new Vector3(0, 0, -slingshotStringStretchLimit);
        }
    }

    private void HandleStringPushedBackToStart(Vector3 midPointLocalSpace)
    {
        if(midPointLocalSpace.z >= 0)
        {
            midPointVisualObject.localPosition = Vector3.zero;
        }
    }
}
