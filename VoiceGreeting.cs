
using System;
using System.IO;
using System.Media;

namespace CybersecurityAwarenessBot.GUI
{
    public class VoiceGreeting
    {
        public void PlayGreeting()
        {
            string audioPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Audio",
                "greeting.wav"
            );

            if (File.Exists(audioPath))
            {
                try
                {
                    using (SoundPlayer player = new SoundPlayer(audioPath))
                    {
                        player.Play();
                    }
                }
                catch (Exception)
                {
                    // Keep the application running if audio fails.
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(
                    "The voice greeting file could not be found."
                );
            }
        }
    }
}
