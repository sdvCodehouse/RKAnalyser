using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using RunkeeperAnalyser.Domain;
using RunkeeperAnalyser.Domain.Gpx;

namespace RunkeeperAnalyser.Tests.Domain
{
    [TestFixture]
    public class GpxTests
    {
        private GpxTrack _gpxTrack;

        [SetUp]
        public void TestSetup()
        {
            using (Stream gpxFileStream = SetupFileStream())
            {
                _gpxTrack = GpxEngine.GetGpxTrackFromStream(gpxFileStream);
            }
        }

        [Test]
        public void TestSetupProducesGpxTrack()
        {
            Assert.That(_gpxTrack, Is.Not.Null);
        }

        [Test]
        public void ImportGpxFile_CorrectSegmentElevationCalculated()
        {
            ExerciseSession session = ExerciseSession.Create(_gpxTrack);
            var sut = session.TrackSegments as List<TrackSegment>;
            
            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.Count, Is.EqualTo(2));
            Assert.That(sut[0].Elevation, Is.EqualTo(7));
            Assert.That(sut[1].Elevation, Is.EqualTo(10).Within(0.1).Percent);
        }

        [Test]
        public void ImportGpxFile_MoreAccurateSegmentElevationCalculated()
        {
            ExerciseSession session = ExerciseSession.Create(_gpxTrack);
            var sut = session.TrackSegments as List<TrackSegment>;

            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.Count, Is.EqualTo(2));
            Assert.That(sut[1].Elevation, Is.EqualTo(10));
        }

        [Test]
        public void ImportGpxFile_CorrectTrackElevationCalculated()
        {
            ExerciseSession sut = ExerciseSession.Create(_gpxTrack);
            //var sut = session.TrackSegments as List<TrackSegment>;

            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.Elevation, Is.EqualTo(17).Within(0.1).Percent);
        }

        [Test]
        public void ImportGpxFile_MoreAccurateTrackElevationCalculated()
        {
            ExerciseSession sut = ExerciseSession.Create(_gpxTrack);
            //var sut = session.TrackSegments as List<TrackSegment>;

            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.Elevation, Is.EqualTo(17));
        }

        [Test]
        public void ImportGpxFile_CorrectSegmentDurationCalculated()
        {
            ExerciseSession session = ExerciseSession.Create(_gpxTrack);
            var sut = session.TrackSegments as List<TrackSegment>;

            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.Count, Is.EqualTo(2));
            Assert.That(sut[0].Duration, Is.EqualTo(new DateTime(2009, 8, 24, 9, 9, 29).Subtract(new DateTime(2009, 8, 24, 9, 9, 19))));
            Assert.That(sut[1].Duration, Is.EqualTo(new DateTime(2009, 8, 24, 9, 11, 43).Subtract(new DateTime(2009, 8, 24, 9, 11, 30))));
        }

        [Test]
        public void ImportGpxFile_CorrectSessionDurationCalculated()
        {
            ExerciseSession sut = ExerciseSession.Create(_gpxTrack);

            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.Duration, Is.EqualTo(new TimeSpan(0, 0, 23)));
        }

        [Test]
        public void ImportGpxFile_CorrectSegmentDistanceCalculated()
        {
            ExerciseSession session = ExerciseSession.Create(_gpxTrack);
            var sut = session.TrackSegments as List<TrackSegment>;

            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.Count, Is.EqualTo(2));
            Assert.That(sut[0].Distance, Is.EqualTo(0.0499477).Within(0.1).Percent);
            Assert.That(sut[1].Distance, Is.EqualTo(0.0708563).Within(0.1).Percent);
        }

        [Test]
        public void ImportGpxFile_CorrectSessionDistanceCalculated()
        {
            ExerciseSession sut = ExerciseSession.Create(_gpxTrack);

            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.Distance, Is.EqualTo(0.12));
        }

        #region XmlFileStream Setup

        private Stream SetupFileStream()
        {
            string xmlString = BuildXmlString();
            byte[] xmlBytes = Encoding.ASCII.GetBytes(xmlString);
            return new MemoryStream(xmlBytes);
        }

        private string BuildXmlString()
        {
            // Setup an xml set to emulate a file import for testing
            StringBuilder sb = new StringBuilder();

            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.Append("<gpx version=\"1.1\" creator=\"Runkeeper - http://www.runkeeper.com\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ");
            sb.Append("xmlns=\"http://www.topografix.com/GPX/1/1\" ");
            sb.Append("xsi:schemaLocation=\"http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd\" ");
            sb.Append("xmlns:gpxtpx=\"http://www.garmin.com/xmlschemas/TrackPointExtension/v1\">");
            sb.Append("<trk><name><![CDATA[Cycling 24/8/09 9:09 am]]></name><time>2009-08-24T09:09:19Z</time>");
            sb.Append("<trkseg>");
            sb.Append("<trkpt lat=\"51.416420000\" lon=\"-0.298719000\"><ele>10</ele><time>2009-08-24T09:09:19Z</time><extensions><gpxtpx:TrackPointExtension><gpxtpx:hr>86</gpxtpx:hr></gpxtpx:TrackPointExtension></extensions></trkpt>");
            sb.Append("<trkpt lat=\"51.416493000\" lon=\"-0.298448000\"><ele>15</ele><time>2009-08-24T09:09:24Z</time><extensions><gpxtpx:TrackPointExtension><gpxtpx:hr>87</gpxtpx:hr></gpxtpx:TrackPointExtension></extensions></trkpt>");
            sb.Append("<trkpt lat=\"51.416604000\" lon=\"-0.298062000\"><ele>17</ele><time>2009-08-24T09:09:29Z</time><extensions><gpxtpx:TrackPointExtension><gpxtpx:hr>88</gpxtpx:hr></gpxtpx:TrackPointExtension></extensions></trkpt>");
            sb.Append("</trkseg>");
            sb.Append("<trkseg>");
            sb.Append("<trkpt lat=\"51.419097000\" lon=\"-0.293218000\"><ele>15</ele><time>2009-08-24T09:11:30Z</time><extensions><gpxtpx:TrackPointExtension><gpxtpx:hr>92</gpxtpx:hr></gpxtpx:TrackPointExtension></extensions></trkpt>");
            sb.Append("<trkpt lat=\"51.419176000\" lon=\"-0.293103000\"><ele>14</ele><time>2009-08-24T09:11:34Z</time><extensions><gpxtpx:TrackPointExtension><gpxtpx:hr>105</gpxtpx:hr></gpxtpx:TrackPointExtension></extensions></trkpt>");
            sb.Append("<trkpt lat=\"51.419258000\" lon=\"-0.292857000\"><ele>11.1</ele><time>2009-08-24T09:11:38Z</time><extensions><gpxtpx:TrackPointExtension><gpxtpx:hr>105</gpxtpx:hr></gpxtpx:TrackPointExtension></extensions></trkpt>");
            sb.Append("<trkpt lat=\"51.419299000\" lon=\"-0.292289000\"><ele>21.1</ele><time>2009-08-24T09:11:43Z</time><extensions><gpxtpx:TrackPointExtension><gpxtpx:hr>104</gpxtpx:hr></gpxtpx:TrackPointExtension></extensions></trkpt>");
            sb.Append("</trkseg>");
            sb.Append("</trk>");
            sb.Append("</gpx>");

            return sb.ToString();
        }
        

        #endregion
    }
}
