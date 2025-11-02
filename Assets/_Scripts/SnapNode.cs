using UnityEngine;

public class SnapNode : MonoBehaviour
{
    public bool IsSnapped { get; set; }
    
    private float _degreeDelta = 360f / SnapConfigs.MaxSnapPoints;

    private void OnCollisionEnter2D(Collision2D other)
    {
        // check if the other object is player, if so then skip. 
        if (other.gameObject.CompareTag("Player"))
        {
            return;
        }
        
        // get the others' SnapNode component
        SnapNode otherSnapNode = other.gameObject.GetComponent<SnapNode>();
        // null check
        if (otherSnapNode == null)
        {
            return;
        }
        
        // check IsSnapped
        if (otherSnapNode.IsSnapped)
        {
            return;
        }

        
        // calculate the angle between the incoming sticky ball direction
        // and the SnapNode's right vector.
        Vector3 dir = other.transform.position - transform.position;
        
        // TODO: optimize the calculate to use dot product instead of angle calculation
        
        // note: safe to do the implicit conversion to drop the z component.
        float angle = Vector2.SignedAngle(transform.right, dir);
        // Debug.Log(angle);
        
        // correct the angle to > 180 if it is negative
        if (angle < 0)
        {
            angle = angle + 360;
        }
        
        // calculate the index of the snap point that the ball should snap to
        int snapPointIndex = (int)(angle / _degreeDelta);
        // Debug.Log(snapPointIndex);
        
        // calculate the snap pos based on the index
        // rotate the right vector
        Vector3 snapDir = Quaternion.Euler(0, 0, snapPointIndex * _degreeDelta) * transform.right;
        Vector3 snapPos = transform.position + snapDir * (SnapConfigs.StickyBallRadius + SnapConfigs.PlayerRadius);
        other.transform.position = snapPos;
        
        // set the parent of the sticky ball to the SnapNode,
        // so it starts moving along with the node
        other.transform.SetParent(transform);
        
        
        // set otherSnapNode's IsSnapped
        otherSnapNode.IsSnapped = true;
    }
    
}
