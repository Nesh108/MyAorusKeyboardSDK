using System;
using System.Drawing;
using System.Net.Sockets;
using System.Threading.Tasks;
using OBSWebsocketDotNet;

namespace MyAorus
{
    class OBSHandler
    {
        protected OBSWebsocket _obs;
        readonly String CountdownSceneName = "Intro Scene - Countdown";
        public bool IsConnected = false;
        public bool IsInCountdownScene = false;
        public bool IsStreaming = false;
        public bool IsRecording = false;
        public Color StreamingColor = Color.DeepPink;

        public OBSHandler()
        {
            _obs = new OBSWebsocket { WSTimeout = new TimeSpan(0, 0, 0, 30) };
            _obs.Connected += onConnect;
            _obs.Disconnected += onDisconnect;

            _obs.SceneChanged += onSceneChange;

            _obs.StreamingStateChanged += onStreamingStateChange;
            _obs.RecordingStateChanged += onRecordingStateChange;
        }

        public void Init()
        {
            TcpClient tc = new TcpClient();
            try
            {
                tc.Connect("127.0.0.1", 4444);
                _obs.Connect("ws://127.0.0.1:4444", "");
            }
            catch (Exception)
            {
            }
            tc.Close();
        }

        private void onConnect(object sender, EventArgs e)
        {
            Console.WriteLine("OBS: Connected to OBS Studio!");
            IsConnected = true;
        }

        private void onDisconnect(object sender, EventArgs e)
        {
            Console.WriteLine("OBS: Disconnected from OBS Studio!");
            IsConnected = false;
        }

        private async void onSceneChange(OBSWebsocket sender, string newSceneName)
        {
            if (newSceneName.Equals(CountdownSceneName))
            {
                IsInCountdownScene = true;
            }
            else
            {
                IsInCountdownScene = false;
                if (IsStreaming && !IsRecording)
                {
                    Console.WriteLine("OBS: Started Recording");
                    setRecordingStatus(true);
                    await StartRecording();
                }
            }
        }

        private async Task StartRecording()
        {
            await Task.Run(() => _obs.StartRecording());
            return;
        }

        private async Task StopRecording()
        {
            await Task.Run(() => _obs.StopRecording());
            return;
        }

        private void onStreamingStateChange(OBSWebsocket sender, OutputState newState)
        {
            switch (newState)
            {
                case OutputState.Started:
                    setStreamingStatus(true);
                    break;
                case OutputState.Stopped:
                    setStreamingStatus(false);
                    break;
            }
        }

        private void onRecordingStateChange(OBSWebsocket sender, OutputState newState)
        {
            switch (newState)
            {
                case OutputState.Started:
                    setRecordingStatus(true);
                    break;
                case OutputState.Stopped:
                    setRecordingStatus(false);
                    break;
            }
        }

        private void setRecordingStatus(bool b)
        {
            IsRecording = b;
            Console.WriteLine("OBS: Recording " + (b ? "started" : "stopped"));
            MyAorus.MainLoop(true);
        }

        private async void setStreamingStatus(bool b)
        {
            IsStreaming = b;
            Console.WriteLine("OBS: Streaming " + (b ? "started" : "stopped"));
            if (!IsStreaming && IsRecording)
            {
                await StopRecording();
                setRecordingStatus(false);
            }
            else
            {
                MyAorus.MainLoop(true);
            }
        }

        public void CheckStatus()
        {
            if (_obs.GetStreamingStatus().IsStreaming != IsStreaming)
            {
                setStreamingStatus(_obs.GetStreamingStatus().IsStreaming);
            }
            if (_obs.GetStreamingStatus().IsRecording != IsRecording)
            {
                setRecordingStatus(_obs.GetStreamingStatus().IsRecording);
            }
        }
    }
}
