using RadioPlayBeckLabrary;
using RadioPlayBeckLabryry;
using System;
using System.Collections.Generic;
using System.Text;

namespace RadioPlaybackLabrary;

public enum PlayState
{
    Play,
    Pause
}

public delegate void PlaybackStateChanged(PlayState state, string playbackUrl);

public sealed class PlaybackCenter
{
    #region Singleton
    public static PlaybackCenter Instance { get; } = new();

    private PlaybackCenter()
    {

    }
    static PlaybackCenter()
    {

    }
    #endregion Singleton

    public event PlaybackStateChanged NotifyPlaybackStateChanged;
    private IRadioPlayback radioPlayback;
    public void NotifyStateChanged(PlayState state, string playbackUrl)
    {
        NotifyPlaybackStateChanged?.Invoke(state, playbackUrl);
    }
    public IRadioPlayback GetRadioPlayback()
    {
        if (radioPlayback == null)
        {
            radioPlayback = new RadioPlaybackFactory().GetInstance();
        }

        return radioPlayback;
    }

}

