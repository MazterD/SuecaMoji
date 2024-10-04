using Photon.Pun;
using UnityEngine;

public class InvokeAnimation : MonoBehaviourPun
{
    public GameObject avatar;

    [PunRPC]
    public void SetExpression(int playerID, string expr)
    {
        Debug.Log("Player " + playerID.ToString() + " selected " + expr);

        if (playerID == 1 || playerID == 2)
        {
            FAnimate fAnimate = avatar.GetComponent<FAnimate>();
            if (expr == "Cancel" && fAnimate.QueueIsEmpty()) {
                return;
            }
            fAnimate.QueueExpression(expr);
        }
        else { 
            Animate animate = avatar.GetComponent<Animate>();
            if (expr == "Cancel" && animate.QueueIsEmpty())
            {
                return;
            }
            animate.QueueExpression(expr);
        }
    }
}
