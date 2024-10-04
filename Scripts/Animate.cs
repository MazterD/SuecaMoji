using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    List<BlendShape> bShapes = new List<BlendShape>();    //list of blendshapes
    List<FacialExpression> fExpr = new List<FacialExpression>();  //list of expressions
    List<FacialExpression> normal = new List<FacialExpression>();
    List<FacialExpression> exaggerated = new List<FacialExpression>();

    string currentExpr = "None";
    string nextExpr = "None";

    SkinnedMeshRenderer skinnedMeshRenderer;
    float blendSpeed = 1f;

    public GameObject upTeeth;
    public GameObject downTeeth;
    public GameObject leftEye;
    public GameObject rightEye;

    SkinnedMeshRenderer upTeethRenderer;
    SkinnedMeshRenderer downTeethRenderer;

    private int game = 0;
    private int playerID = 0;

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
                    if (nextExpr == "Cancel") {
                        nextExpr = "None";
                    }
                }
            }
            ExecuteExpression(currentExpr);
        }
    }

    void Awake()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        upTeethRenderer = upTeeth.GetComponent<SkinnedMeshRenderer>();
        downTeethRenderer = downTeeth.GetComponent<SkinnedMeshRenderer>();

        ImportData();
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
        else
        {
            expr.PassTime(blendSpeed);
        }
    }

    public void QueueExpression(string expr) {
        if (currentExpr == "None") {
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
        BlendShape lipIn = new BlendShape("LipIn", 0);
        BlendShape lipOut = new BlendShape("LipOut", 1);
        BlendShape cheekPuff = new BlendShape("CheekPuff", 10);
        BlendShape pucker = new BlendShape("Pucker", 12);
        BlendShape browUp = new BlendShape("BrowUp", 13);
        BlendShape browDown = new BlendShape("BrowDown", 14);
        BlendShape browIn = new BlendShape("BrowIn", 15);
        BlendShape smile = new BlendShape("Smile", 30);
        BlendShape lowerLidUp = new BlendShape("LowerLidUp", 31);
        BlendShape outerBrowUp = new BlendShape("OuterBrowUp", 32);
        BlendShape innerBrowUp = new BlendShape("InnerBrowUp", 33);
        BlendShape upperLidDown = new BlendShape("UpperLidDown", 34);
        BlendShape frown = new BlendShape("Frown", 35);
        BlendShape mouthOpenV = new BlendShape("MouthOpenV", 36);
        BlendShape noseWrinkle = new BlendShape("NoseWrinkle", 37);
        BlendShape mouthOpenH = new BlendShape("MouthOpenH", 38);
        BlendShape lowerLidDown = new BlendShape("LowerLidDown", 39);
        BlendShape upperLidUp = new BlendShape("UpperLidUp", 40);

        //Adding BlendShapes to database
        bShapes.Add(lipIn);
        bShapes.Add(lipOut);
        bShapes.Add(cheekPuff);
        bShapes.Add(pucker);
        bShapes.Add(browUp);
        bShapes.Add(browDown);
        bShapes.Add(browIn);
        bShapes.Add(smile);
        bShapes.Add(lowerLidUp);
        bShapes.Add(outerBrowUp);
        bShapes.Add(innerBrowUp);
        bShapes.Add(upperLidDown);
        bShapes.Add(frown);
        bShapes.Add(mouthOpenV);
        bShapes.Add(noseWrinkle);
        bShapes.Add(mouthOpenH);
        bShapes.Add(lowerLidDown);
        bShapes.Add(upperLidUp);

        //Happy
        BlendShape[] b = { smile, lowerLidUp };
        float[] w = { 1f, 1f };
        float[] l = { 0.0f, 0.0f, 0.0f };
        float[] r = { 0.0f, 0.0f, 0.0f };
        FacialExpression happy = new FacialExpression("Happy", b, w, l, r, false);

        BlendShape[] bX = { smile, lowerLidUp };
        float[] wX = { 2f, 2f };
        float[] lX = { 0.0f, 0.0f, 0.0f };
        float[] rX = { 0.0f, 0.0f, 0.0f };
        FacialExpression happyX = new FacialExpression("Happy", bX, wX, lX, rX, false);

        normal.Add(happy);
        exaggerated.Add(happyX);


        //Sad
        BlendShape[] b2 = { frown, upperLidDown, innerBrowUp, browDown };
        float[] w2 = { 1f, 0.5f, 1f, 0.33f };
        float[] l2 = { 0.04f, 0.0f, 0.0f };
        float[] r2 = { 0.04f, 0.0f, 0.0f };
        FacialExpression sad = new FacialExpression("Sad", b2, w2, l2, r2, false);

        BlendShape[] b2Y = { frown, upperLidDown, innerBrowUp, browDown, lowerLidUp };
        float[] w2Y = { 2f, 1.5f, 2.5f, 1f, 1f };
        float[] l2Y = { 0.04f, 0.0f, 0.0f };
        float[] r2Y = { 0.04f, 0.0f, 0.0f };
        FacialExpression sadY = new FacialExpression("Sad", b2Y, w2Y, l2Y, r2Y, false);

        normal.Add(sad);
        exaggerated.Add(sadY);


        //Angry
        BlendShape[] b3 = { frown, outerBrowUp, browDown, browIn, mouthOpenH };
        float[] w3 = { 1f, 1f, 0.5f, 0.5f, 0.5f };
        float[] l3 = { 0.0f, 0.0f, 0.0f };
        float[] r3 = { 0.0f, 0.0f, 0.0f };
        FacialExpression angry = new FacialExpression("Angry", b3, w3, l3, r3, false);

        BlendShape[] b3X = { frown, outerBrowUp, browDown, browIn, mouthOpenH };
        float[] w3X = { 1.5f, 2f, 1f, 1f, 1f };
        float[] l3X = { 0.0f, 0.0f, 0.0f };
        float[] r3X = { 0.0f, 0.0f, 0.0f };
        FacialExpression angryX = new FacialExpression("Angry", b3X, w3X, l3X, r3X, false);

        normal.Add(angry);
        exaggerated.Add(angryX);


        //Disgusted
        BlendShape[] b4 = { noseWrinkle, upperLidDown, lowerLidUp, mouthOpenH, browDown, frown, browIn };
        float[] w4 = { 1f, 0.5f, 1f, 0.5f, 0.5f, 0.5f, 0.5f };
        float[] l4 = { 0.0f, 0.0f, 0.0f };
        float[] r4 = { 0.0f, 0.0f, 0.0f };
        FacialExpression disgusted = new FacialExpression("Disgusted", b4, w4, l4, r4, false);

        BlendShape[] b4X = { noseWrinkle, upperLidDown, lowerLidUp, mouthOpenH, browDown, frown, browIn };
        float[] w4X = { 2f, 1f, 2f, 1f, 1f, 1f, 1f };
        float[] l4X = { 0.0f, 0.0f, 0.0f };
        float[] r4X = { 0.0f, 0.0f, 0.0f };
        FacialExpression disgustedX = new FacialExpression("Disgusted", b4X, w4X, l4X, r4X, false);

        normal.Add(disgusted);
        exaggerated.Add(disgustedX);


        //Scared
        BlendShape[] b5 = { lowerLidUp, frown, mouthOpenH, mouthOpenV, innerBrowUp, browIn, browUp, browDown };
        float[] w5 = { 1f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.2f };
        float[] l5 = { 0.0f, 0.0f, 0.0f };
        float[] r5 = { 0.0f, 0.0f, 0.0f };
        FacialExpression scared = new FacialExpression("Scared", b5, w5, l5, r5, true);

        BlendShape[] b5Y = { lowerLidUp, frown, mouthOpenH, mouthOpenV, innerBrowUp, browIn, browUp, browDown };
        float[] w5Y = { 2.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 0.6f };
        float[] l5Y = { 0.0f, 0.0f, 0.0f };
        float[] r5Y = { 0.0f, 0.0f, 0.0f };
        FacialExpression scaredY = new FacialExpression("Scared", b5Y, w5Y, l5Y, r5Y, true);

        normal.Add(scared);
        exaggerated.Add(scaredY);


        //Surprised
        BlendShape[] b6 = { mouthOpenV, browUp, lipOut, lowerLidDown, upperLidUp };
        float[] w6 = { 0.5f, 0.5f, 0.5f, 1f, 1f };
        float[] l6 = { 0.0f, 0.0f, 0.0f };
        float[] r6 = { 0.0f, 0.0f, 0.0f };
        FacialExpression surprised = new FacialExpression("Surprised", b6, w6, l6, r6, true);

        BlendShape[] b6X = { mouthOpenV, browUp, lipOut, lowerLidDown, upperLidUp };
        float[] w6X = { 1f, 1f, 1f, 2f, 2f };
        float[] l6X = { 0.0f, 0.0f, 0.0f };
        float[] r6X = { 0.0f, 0.0f, 0.0f };
        FacialExpression surprisedX = new FacialExpression("Surprised", b6X, w6X, l6X, r6X, true);

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

    public bool QueueIsEmpty()
    {
        if (currentExpr == "None") { return true; }
        else { return false; }
    }

    public void CheckGameState() {
        ListControl lc = transform.parent.parent.parent.gameObject.GetComponent<ListControl>();
        if (lc.GetPlayerID() != playerID) {
            playerID = lc.GetPlayerID();
        }

        if (lc.GetGame() != game) {
            game = lc.GetGame();
        }
    }
}
