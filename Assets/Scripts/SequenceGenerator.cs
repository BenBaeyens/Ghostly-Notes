using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceGenerator : MonoBehaviour
{
    [Header("DEBUG")]
    public bool debugMode = false;

    public PianoKey[] keys;
    public AudioClip[] detunedNotes;
    public AudioClip[] mediumNotes;
    public AudioClip[] goodNotes;

    public AudioClip successSound;
    public AudioClip failureSound;

    public int midDetuneLevel = 4;
    public int goodDetuneLevel = 6;

    public int currentDetuneLevel = 0; // 0 = detuned, 1 = medium, 2 = good



    private Dictionary<PianoKey, List<AudioClip>> keyToNote = new Dictionary<PianoKey, List<AudioClip>>();
    public static SequenceGenerator instance;

    public int[] currentSequence;
    public List<int> playerSequence = new List<int>();

    private bool acceptPlayerInput;


    private void Awake()
    {
        if (keys.Length != detunedNotes.Length || keys.Length == 0 || mediumNotes.Length == 0)
        {
            Debug.LogError("Keys and notes arrays must be the same length");
        }

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        for (int i = 0; i < keys.Length; i++)
        {
            List<AudioClip> notes = new List<AudioClip>
            {
                detunedNotes[i],
                mediumNotes[i],
                goodNotes[i]
            };
            keyToNote.Add(keys[i], notes);
        }

        if (!debugMode)
        {
            GenerateSequence(3);
            StartCoroutine(IPlaySequence());
        }
    }

    private void OnDestroy()
    {
        instance = null;
        StopAllCoroutines();
    }


    public void GenerateSequence(int length)
    {
        currentSequence = new int[length];
        for (int i = 0; i < length; i++)
        {
            currentSequence[i] = Random.Range(0, keys.Length);
        }
    }

    public IEnumerator IPlaySequence()
    {
        foreach (int keyIndex in currentSequence)
        {
            PlayNote(keyIndex, false);
            yield return new WaitForSeconds(1.0f);
        }

        // AFTER THAT, IT WILL BE THE PLAYER'S TURN
        acceptPlayerInput = true;
    }


    public void PlayNote(int keyIndex, bool isPlayer)
    {
        PianoKey key = keys[keyIndex];
        PlayNote(key, isPlayer);
    }

    public void PlayNote(PianoKey key, bool isPlayer)
    {
        if (!debugMode)
        {
            if (key == null || key.isPressed || (!acceptPlayerInput && isPlayer))
            {
                return;
            }
        }
        else if (debugMode && (key == null || key.isPressed))
        {
            return;
        }


        AudioClip note = keyToNote[key][currentDetuneLevel];
        key.Press();
        AudioSource.PlayClipAtPoint(note, Camera.main.transform.position);

        if (isPlayer && !debugMode)
        {
            playerSequence.Add(System.Array.IndexOf(keys, key));

            // Check if current key was correct
            if (playerSequence[playerSequence.Count - 1] != currentSequence[playerSequence.Count - 1])
            {
                acceptPlayerInput = false;
                StartCoroutine(IPlayFailure());
            }
            else if (playerSequence.Count == currentSequence.Length)
            {
                acceptPlayerInput = false;
                StartCoroutine(IPlaySuccess());
            }
        }
    }

    public IEnumerator IPlayFailure()
    {
        // Check if length is shorter than 4
        if (currentSequence.Length < midDetuneLevel)
        {
            currentDetuneLevel = 0;
        }
        else if (currentSequence.Length < goodDetuneLevel)
        {
            currentDetuneLevel = 1;
        }

        FindObjectOfType<GhostDialogue>().ShowCommentAndSpawnGhost(false);

        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayClipAtPoint(failureSound, Camera.main.transform.position);
        yield return new WaitForSeconds(3.0f);
        playerSequence.Clear();
        GenerateSequence(Mathf.Max(3, currentSequence.Length - 1));
        StartCoroutine(IPlaySequence());

        
        
    }

    public IEnumerator IPlaySuccess()
    {
        // Check if length is longer than 4
        if (currentSequence.Length >= goodDetuneLevel)
        {
            currentDetuneLevel = 2;
        }
        else if (currentSequence.Length >= midDetuneLevel)
        {
            currentDetuneLevel = 1;
        }

        FindObjectOfType<GhostDialogue>().ShowCommentAndSpawnGhost(true);

        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayClipAtPoint(successSound, Camera.main.transform.position);
        yield return new WaitForSeconds(3.0f);
        playerSequence.Clear();
        GenerateSequence(currentSequence.Length + 1);
        StartCoroutine(IPlaySequence());

        

    }


}