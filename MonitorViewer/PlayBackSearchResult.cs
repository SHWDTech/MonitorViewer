namespace MonitorViewer
{
    public class PlayBackSearchResult
    {
        public PlaybackFileInfo[] FileList { get; set; }

        public int FileSize { get; set; }
    }

    public class PlaybackFileInfo
    {
        public string StartTime { get; set; }

        public string EndTime { get; set; }
    }
}
