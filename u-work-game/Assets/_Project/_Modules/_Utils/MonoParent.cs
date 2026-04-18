using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class MonoParent : MonoBehaviour
{
    #region Properties

    public bool IsActiveInHeirarchy
    {
        get
        {
            return this.gameObject.activeInHierarchy;
        }
    }
    public bool IsActive
    {
        get
        {
            return this.gameObject.activeSelf;
        }
    }

    public string ClassName
    {
        get => GetType().Name;
    }
    public static string MethodName
    {

        get => new StackTrace().GetFrame(1).GetMethod().Name;
    }

    public string ClassMethodName
    {

        get => GetType().Name + " " + new StackTrace().GetFrame(1).GetMethod().Name + " ";
    }

    #endregion Properties

    #region Non Public Methods
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetScale(Vector3 mScale)
    {
        transform.localScale = mScale;
    }

    public void SetLocalPosition(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    public void SetActive(bool val)
    {
        this.gameObject.SetActive(val);
    }



    #endregion Non Public Methods

    public virtual void PlayButtonClickedSound()
    {
        SoundManager.Instance.Play(SoundManager.Instance.Clips.buttonClicked);
    }
}
