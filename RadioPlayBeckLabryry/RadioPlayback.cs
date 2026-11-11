using NAudio.Wave;
using RadioPlaybackLabrary;
using System.Diagnostics;
using PlaybackState = NAudio.Wave.PlaybackState;

namespace RadioPlayBeckLabrary;

public interface IRadioPlayback
{
    event EventHandler<PlaybackEventArgs> PlaybackStateChanged;

    void Play(string url);
    void Stop();
    void SetVolumeLevel(double level);
    double GetVolumeLevel();
}
public class PlaybackEventArgs : EventArgs
{
    public bool IsPlaying { get; set; } = false;
    public string Url { get; set; } = string.Empty;
}
internal class RadioPlayback : IRadioPlayback
{
    public event EventHandler<PlaybackEventArgs> PlaybackStateChanged;
    private IWavePlayer? waveOutDevice = null;
    private MediaFoundationReader? audioFileReader = null;
    private double volumeLevel = 0.1;
    private WaveFileWriter writer;
    private bool isRecording = false;

    public void Play(string url)
    {
        try
        {
            Stop();
            waveOutDevice = new WaveOutEvent();
            audioFileReader = new MediaFoundationReader(url);
            SetVolumeLevel(volumeLevel);
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
            // Fire event
            PlaybackStateChanged?.Invoke(null, new PlaybackEventArgs() { IsPlaying = true, Url = url });
            PlaybackCenter.Instance.NotifyStateChanged(PlayState.Play, url);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error playing audio: {ex.Message}");
        }
    }

    public void Stop()
    {
        try
        {
            if (waveOutDevice != null)
            {
                if (waveOutDevice.PlaybackState == PlaybackState.Playing)
                {
                    waveOutDevice.Stop();
                }
                audioFileReader?.Dispose();
                waveOutDevice.Dispose();
                // Fire event
                PlaybackStateChanged?.Invoke(null, new PlaybackEventArgs() { IsPlaying = false, Url = string.Empty });
                PlaybackCenter.Instance.NotifyStateChanged(PlayState.Pause, string.Empty);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error stopping audio: {ex.Message}");
        }
    }

    public void SetVolumeLevel(double level)
    {
        this.volumeLevel = level;
        if (waveOutDevice != null)
        {
            waveOutDevice.Volume = (float)volumeLevel;
        }
    }

    public double GetVolumeLevel() => waveOutDevice?.Volume ?? volumeLevel;

}

