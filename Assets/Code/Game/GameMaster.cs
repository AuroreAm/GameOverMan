using UnityEngine;
using Pixify;

public class GameMaster : MonoBehaviour
{
    void Awake()
    {
        ResourcesLoad ();
    }

    void ResourcesLoad ()
    {
        SubResources <VirtusAuthor>.LoadAll ( "Virtus" );
        SubResources <CurveRes>.LoadAll ("Path");
        SubResources <Material>.LoadAll ("Material");
        SubResources <CharacterAuthor>.LoadAll ("Character");
        
        SubResources <AudioClip>.LoadAll ("SE");
    }
}
