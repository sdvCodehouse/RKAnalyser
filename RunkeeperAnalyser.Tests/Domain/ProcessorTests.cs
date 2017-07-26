using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using RunkeeperAnalyser.Domain;
using RunkeeperAnalyser.Domain.Processors;

namespace RunkeeperAnalyser.Tests.Domain
{
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ProcessorTests
    {
        [Test]
        public void ActivityProcessor_Can_Set_RunningActivityType()
        {
            var exerciseSession = new ExerciseSession {Name = "[Running 20/7/17 12:25 pm]"};
            ActivityType result = ActivityProcessor.GetActivityType(exerciseSession);

            Assert.That(result, Is.EqualTo(ActivityType.Run));
        }
        [Test]
        public void ActivityProcessor_Can_Set_CyclingActivityType()
        {
            var exerciseSession = new ExerciseSession {Name = "[Cycling 9/7/17 7:53 am]]"};
            ActivityType result = ActivityProcessor.GetActivityType(exerciseSession);

            Assert.That(result, Is.EqualTo(ActivityType.Cycle));
        }
        [Test]
        public void ActivityProcessor_Can_Set_UnknownType_For_EmptyName()
        {
            var exerciseSession = new ExerciseSession {Name = string.Empty};
            ActivityType result = ActivityProcessor.GetActivityType(exerciseSession);

            Assert.That(result, Is.EqualTo(ActivityType.Unknown));
        }
        [Test]
        public void ActivityProcessor_Can_Set_UnknownType_For_Null_Argument()
        {
            ExerciseSession exerciseSession = null;
            ActivityType result = ActivityProcessor.GetActivityType(exerciseSession);

            Assert.That(result, Is.EqualTo(ActivityType.Unknown));
        }
        [Test]
        public void ActivityProcessor_Can_Set_UnknownType_For_RandonName()
        {
            var exerciseSession = new ExerciseSession { Name = "svuprustgisdh;a" };
            ActivityType result = ActivityProcessor.GetActivityType(exerciseSession);

            Assert.That(result, Is.EqualTo(ActivityType.Unknown));
        }
    }
}
