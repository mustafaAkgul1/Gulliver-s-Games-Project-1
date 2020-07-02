using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockSkin : MonoBehaviour
{
    public List<GameObject> skinList = new List<GameObject>();
    public GameObject greenFrame;
    public Text allSkinsUnlockedText;
    public Button unlockSkinButton;

    [SerializeField] private float frameShuffleDuration = 3f;
    [SerializeField] private float frameShuffleTickRate = 0.2f;

    int unlockedIndex = 0;
    private float currentFrameShuffleDuration;

    void Start()
    {
        currentFrameShuffleDuration = frameShuffleDuration;

    } // Start()

    public void UnlockSkinButton()
    {
        StartCoroutine("MoveGreenFrameOnSkins");
        
    } // UnlockSkinButton()

    IEnumerator MoveGreenFrameOnSkins()
    {
        while (currentFrameShuffleDuration > 0f)
        {
            if (skinList.Count == 1)
            {
                currentFrameShuffleDuration = 0f;
                skinList[0].GetComponent<Image>().enabled = true;
                greenFrame.transform.position = skinList[0].transform.position;
                skinList.RemoveAt(0);
                allSkinsUnlockedText.gameObject.SetActive(true);
                unlockSkinButton.GetComponent<Button>().interactable = false;
                StopCoroutine("MoveGreenFrameOnSkins");
            }
            else
            {
                int tmpRnd = Random.Range(0, skinList.Count);
                greenFrame.transform.position = skinList[tmpRnd].transform.position;
                unlockedIndex = tmpRnd;
                currentFrameShuffleDuration -= frameShuffleTickRate;

                yield return new WaitForSeconds(frameShuffleTickRate);
            }

        } // while

        if (currentFrameShuffleDuration <= 0f && skinList.Count > 1)
        {
            currentFrameShuffleDuration = frameShuffleDuration;
            skinList[unlockedIndex].GetComponent<Image>().enabled = true;
            skinList.RemoveAt(unlockedIndex);

            StopCoroutine("MoveGreenFrameOnSkins");
        }

    } // MoveGreenFrameOnSkins()

} // sınıf
