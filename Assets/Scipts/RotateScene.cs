using UnityEngine;
using DG.Tweening;
public class RotateScene : MonoBehaviour
{
   [SerializeField]private Transform sceneObject;

   public void Rotate()
   {
      sceneObject.DOLocalRotate(new Vector3(0f,45f,0f), 0.2f);
   }
}
