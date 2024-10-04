using System.Collections.Generic;
using UnityEngine;

public class FAnimate : MonoBehaviour
{
    List<BlendShape> bShapes = new List<BlendShape>();    //list of blendshapes
    List<FacialExpression> fExpr = new List<FacialExpression>();  //list of expressions
    List<FacialExpression> normal = new List<FacialExpression>();
    List<FacialExpression> exaggerated = new List<FacialExpression>();

    string currentExpr = "None";
    string nextExpr = "None";

    SkinnedMeshRenderer skinnedMeshRenderer;
    float blendSpeed = 1f;

    GameObject leftEye = null;
    GameObject rightEye = null;
    SkinnedMeshRenderer upTeethRenderer = null;
    SkinnedMeshRenderer downTeethRenderer = null;

    private int game = 0;
    private int playerID = 0;

    void Awake()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        ImportData();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameState();

        if (game == 2 && (playerID == 2 || playerID == 4))
        {
            fExpr = normal;
        }
        else if (game == 3 && (playerID == 1 || playerID == 3))
        {
            fExpr = exaggerated;
        }
        else
        {
            fExpr.Clear();
        }

        if (currentExpr != "None")
        {
            if (nextExpr != "None")
            {
                FacialExpression expr = GetExpression(currentExpr);
                if (expr.GetDuration() > expr.GetTime())
                {
                    expr.PassTime(expr.GetDuration());
                    if (nextExpr == "Cancel")
                    {
                        nextExpr = "None";
                    }
                }
            }
            ExecuteExpression(currentExpr);
        }
    }

    void ExecuteExpression(string name)
    {
        FacialExpression expr = GetExpression(name);
        if (expr == null) {
            return;
        }

        if (!expr.isPeaked() && expr.GetTime() == 0)  //if expr is not active yet it will increase
        {
            expr.changeExpression(skinnedMeshRenderer, upTeethRenderer, downTeethRenderer, leftEye, rightEye, blendSpeed, true);
        }
        else if (expr.isActive() && expr.isOver())    //if expr has reached its full duration it will decrease
        {
            expr.changeExpression(skinnedMeshRenderer, upTeethRenderer, downTeethRenderer, leftEye, rightEye, blendSpeed, false);
        }
        else if (!expr.isActive() && expr.isOver())   //if expr reached 0 it stops the animation
        {
            expr.resetTime(upTeethRenderer, downTeethRenderer);
            currentExpr = nextExpr; //if there is another expression in queue, it will be given the signal to start
            nextExpr = "None";
        }
        else {
            expr.PassTime(blendSpeed);
        }
    }

    public void QueueExpression(string expr) {
        if (currentExpr == "None")
        {
            currentExpr = expr;
        }

        else if (nextExpr == "None")
        {
            nextExpr = expr;
        }
    }

    void ImportData()
    {
        //BlendShape creation
        BlendShape jawDown = new BlendShape("JawDown", 0);
        BlendShape jawForward = new BlendShape("JawForward", 1);
        BlendShape jawLeft = new BlendShape("JawLeft", 2);
        BlendShape jawRight = new BlendShape("JawRight", 3);
        BlendShape smile = new BlendShape("Smile", 4);
        BlendShape frown = new BlendShape("Frown", 5);
        BlendShape mouthOut = new BlendShape("MouthOut", 6);
        BlendShape pucker = new BlendShape("Pucker", 7);
        BlendShape mouthUp = new BlendShape("MouthUp", 8);

        BlendShape cheekPuff = new BlendShape("CheekPuff", 14);
        BlendShape squint = new BlendShape("Squint", 15);
        BlendShape browUp = new BlendShape("BrowUp", 16);
        BlendShape innerBrowUp = new BlendShape("InnerBrowUp", 17);
        BlendShape outerBrowUp = new BlendShape("OuterBrowUp", 18);
        BlendShape innerBrowDown = new BlendShape("InnerBrowDown", 19);
        BlendShape outerBrowDown = new BlendShape("OuterBrowDown", 20);
        BlendShape browMad = new BlendShape("BrowMad", 21);
        BlendShape browSad = new BlendShape("BrowSad", 22);

        BlendShape upperLipUp = new BlendShape("UpperLipUp", 23);
        BlendShape lowerLipDown = new BlendShape("LowerLipDown", 24);

        BlendShape noseWrinkle = new BlendShape("NoseWrinkle", 33);
        BlendShape upperLidDown = new BlendShape("UpperLidDown", 34);
        BlendShape lowerLidUp = new BlendShape("LowerLidUp", 35);
        BlendShape closeEyes = new BlendShape("CloseEyes", 36);

        //Adding BlendShapes to database
        bShapes.Add(jawDown);
        bShapes.Add(jawForward);
        bShapes.Add(jawLeft);
        bShapes.Add(jawRight);
        bShapes.Add(smile);
        bShapes.Add(frown);
        bShapes.Add(mouthOut);
        bShapes.Add(pucker);
        bShapes.Add(mouthUp);

        bShapes.Add(cheekPuff);
        bShapes.Add(squint);
        bShapes.Add(browUp);
        bShapes.Add(innerBrowUp);
        bShapes.Add(outerBrowUp);
        bShapes.Add(innerBrowDown);
        bShapes.Add(outerBrowDown);
        bShapes.Add(browMad);
        bShapes.Add(browSad);

        bShapes.Add(upperLipUp);
        bShapes.Add(lowerLipDown);

        bShapes.Add(noseWrinkle);
        bShapes.Add(lowerLidUp);
        bShapes.Add(upperLidDown);
        bShapes.Add(closeEyes);

        //Facial expressions - regular, X(distortion level 2), Y(distortion level 3) and Z(distortion level 1)

        //Happy
        BlendShape[] b = { smile, lowerLidUp };
        float[] w = { 0.5f, 0.5f };
        float[] l = { 0.0f, 0.0f, 0.0f };
        float[] r = { 0.0f, 0.0f, 0.0f };
        FacialExpression happy = new FacialExpression("Happy", b, w, l, r, false);

        BlendShape[] bX = { smile, lowerLidUp, upperLidDown };
        float[] wX = { 1f, 1f, 0.25f };
        float[] lX = { 0.0f, 0.0f, 0.0f };
        float[] rX = { 0.0f, 0.0f, 0.0f };
        FacialExpression happyX = new FacialExpression("Happy", bX, wX, lX, rX, false);

        normal.Add(happy);
        exaggerated.Add(happyX);


        //Sad
        BlendShape[] b2 = { frown, upperLidDown, browSad };
        float[] w2 = { 0.5f, 0.5f, 0.33f };
        float[] l2 = { 0.0f, 0.0f, 0.0f };
        float[] r2 = { 0.0f, 0.0f, 0.0f };
        FacialExpression sad = new FacialExpression("Sad", b2, w2, l2, r2, false);

        BlendShape[] b2Y = { frown, upperLidDown, lowerLidUp, browSad };
        float[] w2Y = { 1.5f, 1.5f, 0.5f, 1f };
        float[] l2Y = { 0.0f, 0.0f, 0.0f };
        float[] r2Y = { 0.0f, 0.0f, 0.0f };
        FacialExpression sadY = new FacialExpression("Sad", b2Y, w2Y, l2Y, r2Y, false);

        normal.Add(sad);
        exaggerated.Add(sadY);


        //Angry
        BlendShape[] b3 = { frown, outerBrowUp, browMad };
        float[] w3 = { 1f, 0.5f, 0.5f };
        float[] l3 = { 0.0f, 0.0f, 0.0f };
        float[] r3 = { 0.0f, 0.0f, 0.0f };
        FacialExpression angry = new FacialExpression("Angry", b3, w3, l3, r3, false);

        BlendShape[] b3Y = { frown, outerBrowUp, browMad, lowerLipDown, upperLipUp };
        float[] w3Y = { 2f, 1.25f, 1.5f, 0.5f, 0.75f };
        float[] l3Y = { 0.0f, 0.0f, 0.0f };
        float[] r3Y = { 0.0f, 0.0f, 0.0f };
        FacialExpression angryY = new FacialExpression("Angry", b3Y, w3Y, l3Y, r3Y, false);

        normal.Add(angry);
        exaggerated.Add(angryY);


        //Disgusted
        BlendShape[] b4 = { noseWrinkle, closeEyes, mouthUp, pucker, mouthOut, squint, frown, upperLipUp, browMad };
        float[] w4 = { 1f, 0.5f, 0.25f, 0.2f, 0.5f, 0.5f, 0.2f, 0.2f, 0.5f };
        float[] l4 = { 0.0f, 0.0f, 0.0f };
        float[] r4 = { 0.0f, 0.0f, 0.0f };
        FacialExpression disgusted = new FacialExpression("Disgusted", b4, w4, l4, r4, false);

        BlendShape[] b4Y = { noseWrinkle, closeEyes, mouthUp, pucker, mouthOut, squint, frown, upperLipUp, browMad };
        float[] w4Y = { 2.5f, 0.85f, 1f, 0.6f, 1.5f, 1.5f, 0.6f, 0.6f, 1.25f };
        float[] l4Y = { 0.0f, 0.0f, 0.0f };
        float[] r4Y = { 0.0f, 0.0f, 0.0f };
        FacialExpression disgustedY = new FacialExpression("Disgusted", b4Y, w4Y, l4Y, r4Y, false);

        normal.Add(disgusted);
        exaggerated.Add(disgustedY);


        //Scared
        BlendShape[] b5 = { lowerLidUp, frown, jawDown, innerBrowUp, browSad, browUp };
        float[] w5 = { 0.25f, 0.5f, 0.25f, 0.2f, 0.5f, 0.25f };
        float[] l5 = { 0.0f, 0.0f, 0.0f };
        float[] r5 = { 0.0f, 0.0f, 0.0f };
        FacialExpression scared = new FacialExpression("Scared", b5, w5, l5, r5, false);

        BlendShape[] b5X = { lowerLidUp, frown, jawDown, innerBrowUp, browSad, browUp };
        float[] w5X = { 0.75f, 1f, 0.75f, 0.4f, 1f, 0.75f };
        float[] l5X = { 0.0f, 0.0f, 0.0f };
        float[] r5X = { 0.0f, 0.0f, 0.0f };
        FacialExpression scaredX = new FacialExpression("Scared", b5X, w5X, l5X, r5X, false);

        normal.Add(scared);
        exaggerated.Add(scaredX);


        //Surprised
        BlendShape[] b6 = { jawDown, browUp, mouthOut, pucker };
        float[] w6 = { 0.25f, 0.5f, 0.5f, 0.5f };
        float[] l6 = { 0.0f, 0.0f, 0.0f };
        float[] r6 = { 0.0f, 0.0f, 0.0f };
        FacialExpression surprised = new FacialExpression("Surprised", b6, w6, l6, r6, false);

        BlendShape[] b6X = { jawDown, browUp, mouthOut, pucker };
        float[] w6X = { 0.75f, 1f, 1f, 1f };
        float[] l6X = { 0.0f, 0.0f, 0.0f };
        float[] r6X = { 0.0f, 0.0f, 0.0f };
        FacialExpression surprisedX = new FacialExpression("Surprised", b6X, w6X, l6X, r6X, false);

        normal.Add(surprised);
        exaggerated.Add(surprisedX);
    }

    FacialExpression GetExpression(string name) {
        foreach (FacialExpression f in fExpr) {
            if (name == f.Name()) {
                return f;
            }
        }
        return null;
    }

    public bool QueueIsEmpty() { 
        if(currentExpr == "None") { return true; }
        else { return false; }
    }

    public void CheckGameState()
    {
        ListControl lc = transform.parent.parent.gameObject.GetComponent<ListControl>();
        if (lc.GetPlayerID() != playerID)
        {
            playerID = lc.GetPlayerID();
        }

        if (lc.GetGame() != game)
        {
            game = lc.GetGame();
        }
    }
}
