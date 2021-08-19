// NOTE: this file was taken from a gorup project. It will be changed somewhat, but be aware that it was based on something else.
using UnityEngine;

// audio looping class
public class AudioLooper : MonoBehaviour
{
    // audio source
    public AudioSource audioSource = null;

    // the start of the clip. If this value is negative, then the looper will not work.
    public float clipStart = 0.0F;


    // the end of the clip
    /// <summary>
    /// If clipEnd is set to the end of the audio file, it will ignore clipStart and go back to the start of the audio file.
    /// To avoid this, make sure clipEnd is not set to the end of the audio file. 
    /// The best ways to handle this is to have some silence at the end of the file, or have the song loop through.
    /// </summary>
    public float clipEnd = 0.0F;

    // if 'true', a audio file will play at the start of the clip range upon first being played.
    // if 'false', the audio file will play from the start of the audio file.
    //  * in this case, it will loop back to clip start once it reaches clip end.
    public bool playAtClipStart = false;

    // Start is called before the first frame update
    void Start()
    {
        // gets the start and end of the clip if not set.
        if (audioSource != null)
        {
            // if the clip start and end are both set to zero (i.e. they weren't set)
            // clip start
            if (clipStart <= 0.0F)
                clipStart = 0.0F;

            // clip end
            if (audioSource.clip != null && clipEnd <= 0.0F)
                clipEnd = audioSource.clip.length;

            // start at clip start instead of start of song.
            if(playAtClipStart)
                audioSource.time = clipStart;
        }
    }

    // plays the audio.
    // if limited to the clip start, it starts from the loop point.
    public void PlayAudio()
    {
        // audio source or audio clip doesn't exist.
        if (audioSource == null || audioSource.clip == null)
            return;

        // stops audio if it's currently playing.
        audioSource.Stop();

        // if the audio should start at the clip start when first played.
        if (playAtClipStart && clipStart >= 0.0F && clipStart < audioSource.clip.length)
            audioSource.time = clipStart;
        else // start source at the start of the audio.
            audioSource.time = 0.0F;

        // plays the audio
        audioSource.Play();
    }

    // stops te audio
    public void StopAudio()
    {
        // audio source or audio clip doesn't exist.
        if (audioSource == null || audioSource.clip == null)
            return;

        audioSource.Stop();

        // bring audio to clip start
        if (playAtClipStart && clipStart >= 0.0F && clipStart < audioSource.clip.length)
            audioSource.time = clipStart;
        else // bring audio to start of the song
            audioSource.time = 0.0F;
    }

    // if the audio is set to loop
    public bool GetLooping()
    {
        if (audioSource != null)
            return audioSource.loop;
        else
            return false;
    }

    // sets the audio to loop
    public void SetLooping(bool looping)
    {
        if (audioSource != null)
            audioSource.loop = looping;
    }

    // returns the length of the audio clip
    public float GetClipLength()
    {
        return clipEnd - clipStart;
    }

    // returns the value of clip start
    public float GetClipStart()
    {
        return clipStart;
    }

    // sets the value of clip start in seconds
    public void SetClipStartInSeconds(float seconds)
    {
        // if the audio source is null.
        if (audioSource == null)
            return;

        // if the audio clip is null.
        if (audioSource.clip == null)
            return;


        // setting value to clip start.
        if(seconds >= 0.0F && seconds <= audioSource.clip.length)
            clipStart = seconds;
    }
    
    // sets the clip start time as a percentage, with 0 being 0% and 1 being 100%.
    public void SetClipStartAsPercentage(float p)
    {
        SetClipAsPercentage(p, true);
    }

    // returns the value of clip end
    public float GetClipEnd()
    {
        return clipEnd;
    }

    // sets the value of clip end in seconds
    // this only works if the audioClip has been set.
    public void SetClipEndInSeconds(float seconds)
    {
        // if the audio source is null.
        if (audioSource == null)
            return;

        // if the audio clip is null.
        if (audioSource.clip == null)
            return;


        // setting value to clip start.
        clipEnd = (seconds >= 0.0F && seconds <= audioSource.clip.length) ?
            seconds : clipEnd;
    }

    // sets the clip end time as a percentage, with 0 being 0% and 1 being 100%.
    public void SetClipEndAsPercentage(float p)
    {
        SetClipAsPercentage(p, false);
    }

    // sets the clip as a percentage. If setStart is true, clipStart is changed. If false, clipEnd is changed.
    // since there's only one line of code that's different, this is privately used by the unique functions.
    private void SetClipAsPercentage(float p, bool setStart)
    {
        // if the audio source is null.
        if (audioSource == null)
            return;

        // if the audio clip is null.
        if (audioSource.clip == null)
            return;

        // clamp percentage.
        p = Mathf.Clamp(p, 0.0F, 1.0F);

        // setting values.
        if (setStart) // set the start.
            clipStart = audioSource.clip.length * p;
        else // set the end
            clipEnd = audioSource.clip.length * p;
    }

    // gets the variable that says whether or not to start the song at the start of the clip.
    public bool GetPlayAtClipStart()
    {
        return playAtClipStart;
    }

    // sets the play at clip start
    public void SetPlayAtClipStart(bool pacs)
    {
        playAtClipStart = pacs;

        // if the audio source is null.
        if (audioSource == null)
            return;

        // if the audio clip is null.
        if (audioSource.clip == null)
            return;

        // if the audio should play at the start of the clip.
        if(playAtClipStart)
        {
            // if the start of the clip is greater than the current time of the clip...
            // the audioSouce is set to the start of the clip.
            if (clipStart > audioSource.time)
                audioSource.time = clipStart;
        }
    }


    // Update is called once per frame
    void Update()
    {
        // check if audio source has been set.
        if (audioSource == null)
        {
            Debug.LogError("No audio source supplied.");
            return;
        }
            
        // check for if audio clip has been set.
        if (audioSource.clip == null)
        {
            Debug.LogError("No audio clip provided.");
            return;
        }
            

        // clamp clipStart and clipEnd
        clipStart = Mathf.Clamp(clipStart, 0.0F, audioSource.clip.length);
        clipEnd = Mathf.Clamp(clipEnd, 0.0F, audioSource.clip.length);

        // if the clips are the same, no audio can play.
        if (clipStart == clipEnd)
        {
            Debug.LogAssertion("The clip start and clip end are the same, so no audio can play.");
            return;
        }
        // if the clip end is greater than the clip start, then the values are swapped.
        else if (clipStart > clipEnd)
        {
            // message
            Debug.LogAssertion("Clip end is less than clip start. Swapping values.");

            // swap values
            float temp = clipStart;
            clipStart = clipEnd;
            clipEnd = temp;
        }

        // if the audio source is playing
        if(audioSource.isPlaying)
        {
            // the audioSource has reached the end of the clip.
            if (audioSource.time >= clipEnd)
            {
                // checks to see if the audio is looping
                if(audioSource.loop) // audio is looping
                {
                    audioSource.time = clipStart;
                }
                else // audio is not looping.
                {
                    // audio is stopped, and returns to clip start.
                    audioSource.Stop();
                    audioSource.time = clipStart;
                }
            }
        }
    }
}
