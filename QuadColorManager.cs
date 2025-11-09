using UdonSharp;
using UnityEngine;
using VRC.SDK3.Persistence;


public class QuadColorManager : UdonSharpBehaviour
{
    public LaserPointerRayCaster myRayCaster;
    public Material[] materialToChange;
    public string shaderPropertyToChange = "_Color";
    public LineRenderer palleteLine;
    public Transform orginalPos;
    public Color quadColor = new Color(0.557f, 0.976f, 1.000f);
    public Rigidbody penRidgidbody;

    private void OnEnable()
    {
        foreach (Material material in materialToChange)
        {
            material.SetColor(shaderPropertyToChange, quadColor);
            PlayerData.SetColor("settings_color", quadColor);
        }
    }
    public override void OnPickup() //this can be desync with current setup
    {
        myRayCaster.enabled = true;
    }
    public override void OnDrop()
    {
        myRayCaster.enabled = false;
        SendCustomEventDelayedFrames("DisableLine", 1);
        penRidgidbody.velocity = new Vector3(0, 0, 0);
        penRidgidbody.angularVelocity = new Vector3(0, 0, 0);
    }

    public void UpdateColor(Color newColor)
    {
        quadColor = newColor;
        foreach (Material material in materialToChange)
        {
            material.SetColor(shaderPropertyToChange, quadColor);
            if (material.name != "Nya")
            {
                material.SetColor("_EmissionColor", quadColor);
                material.SetColor("_EmissionColor1", quadColor);
                material.SetColor("_EmissionColor2", quadColor);
            }
            PlayerData.SetColor("settings_color", quadColor);
        }
    }

    public void RespawnPen()
    {
        gameObject.transform.position = orginalPos.position;
    }

    public void DisableLine()
    {
        palleteLine.enabled = false;
    }
}

