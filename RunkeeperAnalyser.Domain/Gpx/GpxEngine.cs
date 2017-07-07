using System.IO;
using System.Web;

namespace RunkeeperAnalyser.Domain.Gpx
{
    public static class GpxEngine
    {
        public static GpxTrack GetGpxTrackFromStream(Stream fileStream)
        {
            using (Stream input = fileStream)
            {
                using (GpxReader reader = new GpxReader(input))
                {
                    reader.Read();
                    return reader.Track;
                }
            }
        }

        public static GpxTrack GetGpxTrackFromFile(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                return GetGpxTrackFromStream(file.InputStream);
            }
            return null;
        }
    }
}
