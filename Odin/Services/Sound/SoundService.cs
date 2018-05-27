using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Odin.Services.Sound
{
    public class SoundService
    {
        private static SoundService _instance;
        private static readonly Assembly assembly;
        private static readonly string[] resources;
        //private static Plugin.SimpleAudioPlayer.Abstractions.ISimpleAudioPlayer _player;

        private IList< ISimpleAudioPlayer> _listOfActiveAudioPlayers;
       

        static SoundService()
        {
           //// var type = typeof(App);
           // assembly = type.GetTypeInfo().Assembly;
           // resources = assembly.GetManifestResourceNames();
           // Instance._listOfActiveAudioPlayers = new List<ISimpleAudioPlayer>();
        }

        public static SoundService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SoundService();
                }
                return _instance;
            }
        }

        public Stream LoadStream(string path)
        {
            return FileManager.LoadStream(assembly, resources, path);
        }

        public ISimpleAudioPlayer CreatePlayer()
        {
            return CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        }

        public void Play(string path, bool loop=false)
        {
            Guid guid = Guid.NewGuid();
            var audioStream = FileManager.LoadStream(assembly, resources, path);
            var player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();

            if (player.Load(audioStream))
            {
                player.Loop = loop;
                player.Play();
            }
        }

        public void Play(Stream stream,bool loop = false)
        {
            var player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            _listOfActiveAudioPlayers.Add(player);
            if (player.Load(stream))
            {
                player.Loop = loop;
                player.Play();
                player.PlaybackEnded += (s,e) => _listOfActiveAudioPlayers.Remove(player);
            }
        }

        public void StopAll()
        {
            foreach (var player in _listOfActiveAudioPlayers)
            {
                player.Pause();
            }
        }

        public void Pause(ISimpleAudioPlayer player)
        {
            _listOfActiveAudioPlayers.FirstOrDefault(p => p == player).Pause();
        }

        public void ClearPlayers()
        {
            _listOfActiveAudioPlayers = new List<ISimpleAudioPlayer>();
        }
        
    }
}
