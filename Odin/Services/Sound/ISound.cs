using System.Threading.Tasks;

namespace Odin.Services.Sound
{
    public interface ISound
    {
        Task Play(bool loop = false);
        Task Pause();
        Task FadeOut(int fadingDurationMs);
        Task Stop();
        float Volume { get; set; }
    }
}
