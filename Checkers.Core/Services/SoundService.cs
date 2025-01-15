using System.Media;
using Checkers.Core.Services;
using System.Threading.Tasks;

namespace Checkers.Core.Services
{
    public class SoundService
    {
        private readonly SoundPlayer moveSound;
        private readonly SoundPlayer captureSound;
        private readonly SoundPlayer kingSound;
        private readonly SoundPlayer selectSound;
        private readonly SoundPlayer gameOverSound;

        private static SoundService instance;
        public static SoundService Instance => instance ??= new SoundService();

        private SoundService()
        {
            moveSound = new SoundPlayer(new MemoryStream(Resource.move));
            captureSound = new SoundPlayer(new MemoryStream(Resource.capture));
            kingSound = new SoundPlayer(new MemoryStream(Resource.king));
            selectSound = new SoundPlayer(new MemoryStream(Resource.select));
            gameOverSound = new SoundPlayer(new MemoryStream(Resource.gameover));

            moveSound.LoadAsync();
            captureSound.LoadAsync();
            kingSound.LoadAsync();
            selectSound.LoadAsync();
            gameOverSound.LoadAsync();
        }

        private async Task PlaySoundWithDelay(SoundPlayer sound, int delayMs = 0)
        {
            if (delayMs > 0)
                await Task.Delay(delayMs);
            sound?.Play();
        }

        public async Task PlayMove() => await PlaySoundWithDelay(moveSound);
        public async Task PlayCapture() => await PlaySoundWithDelay(captureSound);
        public async Task PlayKing() => await PlaySoundWithDelay(kingSound, 200);
        public async Task PlaySelect() => await PlaySoundWithDelay(selectSound);
        public async Task PlayGameOver() => await PlaySoundWithDelay(gameOverSound, 1000);

        public void Dispose()
        {
            moveSound?.Dispose();
            captureSound?.Dispose();
            kingSound?.Dispose();
            selectSound?.Dispose();
            gameOverSound?.Dispose();
        }
    }
} 