using UnityEngine;

public class Platform : MonoBehaviour
{
    private float initialDelta;

    private float GetWorldMouseX()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        return worldMousePos.x;
    }

    private void OnMouseDown()
    {
        //Calculate difference in initial distance btw mouse and platform X
        initialDelta = transform.position.x - GetWorldMouseX();

        Cursor.visible = false;
    }

    private void OnMouseUp()
    {
        Cursor.visible = true;
    }

    private void OnMouseDrag()
    {
        //Set X platform to mouse position with initial indent; Y, Z stay same
        transform.position = new Vector3(GetWorldMouseX() + initialDelta, transform.position.y, transform.position.z);
    }
}
