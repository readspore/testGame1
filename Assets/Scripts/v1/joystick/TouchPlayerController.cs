using UnityEngine;

public enum ControllType {EditTransfotm, AddForce };
public class TouchPlayerController : MonoBehaviour
{
    public ControllType controllType = ControllType.EditTransfotm;
    // Update is called once per frame
    void Update()
    {
            Debug.Log("y  "+ MobileJoystick_UI.instance.moveDirection.y + " X  " + MobileJoystick_UI.instance.moveDirection.x);
        //Move Front/Back
        if (MobileJoystick_UI.instance.moveDirection.y != 0)
        {
            //transform.Translate(transform.forward * Time.deltaTime * 2.45f * MobileJoystick_UI.instance.moveDirection.y, Space.World);
        }

        //Rotate Left/Right
        if (MobileJoystick_UI.instance.moveDirection.x != 0)
        {
            //transform.Rotate(new Vector3(0, 14, 0) * Time.deltaTime * 4.5f * MobileJoystick_UI.instance.moveDirection.x, Space.Self);
        }
    }
}