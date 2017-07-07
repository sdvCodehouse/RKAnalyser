using System;
using System.Xml.Serialization;

namespace RunkeeperAnalyser.Domain.XmlSerializers
{
    [Serializable()]
    [XmlRoot("gpx")]
    public class RunkeeperGpxXml
    {
        [XmlAttribute("xmlns")]
        public string xmlns { get; set; }

        [XmlArray("trk")]
        [XmlArrayItem("trkseg", typeof(TrkSeg))]
        public TrkSeg[] TrkSegs { get; set; }

        //todo a proof of concept Use simple deserialize method to import data
        //public void Serialize()
        //{
        //    //StreamReader reader = new StreamReader(stream);

        //    //XmlSerializer serializer = new XmlSerializer(typeof(RunkeeperGpxXml));

        //    //RunkeeperGpxXml exerciseSession = (RunkeeperGpxXml)serializer.Deserialize(reader); 
        //}
    }

    [Serializable()]
    public class TrkSeg
    {
        [XmlArray("trkseg")]
        [XmlArrayItem("trkpt", typeof(TrkPt))]
        public TrkPt[] TrkPts { get; set; }
    }

    [Serializable()]
    public class TrkPt
    {
        [XmlAttribute("lat")]
        public string Latitude { get; set; }

        [XmlAttribute("lon")]
        public string Longitude { get; set; }

        [XmlElement("ele")]
        public string Elevation { get; set; }

        [XmlElement("time")]
        public string Time { get; set; }
    }
}
