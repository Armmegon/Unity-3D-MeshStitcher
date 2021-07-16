# Unity-3D-MeshStitcher
This is basic script that allows you to attached separated body meshes into one whole body with all meshes using same rig.


```c#
/**
Place the main prefab that will be firstly instantiated here. 
On that object all others will be attached.
**/
public GameObject objPlayer;

//Place all meshes that you want to stitch here. Make sure list is not empty. You can use prefabs.
public List<GameObject> limbs;

//On start all meshes will be stitched together when this code is executed:

void Start()
{
    foreach (GameObject limb in limbs)
    {
        Debug.Log("Trying to add" + limb.name);
        AddLimb(limb, objPlayer);
    }
}

```