using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stitcher : MonoBehaviour
{
    //Place here all body meshes as prefabs
    public List<GameObject> limbs;
    //Place here the main mesh aka Head GameObject. Make sure that the rig on that GameObject is set to Humanoid.
    public GameObject objPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject limb in limbs)
        {
            Debug.Log("Trying to add" + limb.name);
            AddLimb(limb, objPlayer);
        }
    }
    /**
     * Creates new instance of the prefab and attaches it to body
     */
    void AddLimb(GameObject BonedObj, GameObject RootObj)
    {
        GameObject tempLimb = Instantiate(BonedObj);

        SkinnedMeshRenderer[] BonedObjects = tempLimb.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer SkinnedRenderer in BonedObjects)
        {
            ProcessBonedObject(SkinnedRenderer, RootObj);
        }
        Destroy(tempLimb);
    }
    
    /**
     * 
     */
    private void ProcessBonedObject(SkinnedMeshRenderer ThisRenderer, GameObject RootObj)
    {
        /*      Create the SubObject        */
        GameObject NewObj = new GameObject(ThisRenderer.gameObject.name);
        NewObj.transform.parent = RootObj.transform;
        /*      Add the renderer        */
        NewObj.AddComponent<SkinnedMeshRenderer>();
        SkinnedMeshRenderer NewRenderer = NewObj.GetComponent<SkinnedMeshRenderer>();
        /*      Assemble Bone Structure     */
        Transform[] MyBones = new Transform[ThisRenderer.bones.Length];
        for (var i = 0; i < ThisRenderer.bones.Length; i++)
            MyBones[i] = FindChildByName(ThisRenderer.bones[i].name, RootObj.transform);
        /*      Assemble Renderer       */
        NewRenderer.bones = MyBones;
        NewRenderer.sharedMesh = ThisRenderer.sharedMesh;
        NewRenderer.materials = ThisRenderer.materials;
    }
    
    private Transform FindChildByName(string ThisName, Transform ThisGObj)
    {
        Transform ReturnObj;
        if (ThisGObj.name == ThisName)
            return ThisGObj.transform;
        foreach (Transform child in ThisGObj)
        {
            ReturnObj = FindChildByName(ThisName, child);
            if (ReturnObj)
                return ReturnObj;
        }

        return null;
    }
}
