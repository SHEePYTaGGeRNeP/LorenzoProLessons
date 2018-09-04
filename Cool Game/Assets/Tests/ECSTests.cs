//using UnityEngine.TestTools;
//using NUnit.Framework;
//using Unity.Mathematics;
//using UnityEngine;
//using Unity.Entities;
//using UnityEngine.Assertions;
//
//namespace Tests
//{
//    [Category("Unity ECS Tests")]
//    public class ECSTests
//    {
//        [Test]
//        public void When_MoveSpeedEqualsZero_Then_EntityDoesntMove()
//        {
//            // arrange
//            var entity = m_Manager.CreateEntity(
//                typeof(MoveForward),
//                typeof(MoveSpeed),
//                typeof(Position),
//                typeof(Heading));
//            m_Manager.SetComponentData(entity, new MoveSpeed {speed = 0});
//            m_Manager.SetComponentData(entity, new Position {Value = new float3(0, 0, 0)});
//            m_Manager.SetComponentData(entity, new Heading {Value = new float3(0, 0, 0)});
//
//            // act
//            World.CreateManager<MoveForwardSystem>().Update();
//
//            // assert
//            Assert.AreEqual(new float3(0, 0, 0), m_Manager.GetComponentData<Position>(entity).Value);
//        }
//
//        [Test]
//        public void When_MoveSpeedEqualsOne_Then_EntityMovesForward()
//        {
//            // arrange
//            var entity = m_Manager.CreateEntity(
//                typeof(MoveForward),
//                typeof(MoveSpeed),
//                typeof(Position),
//                typeof(Heading));
//            m_Manager.SetComponentData(entity, new MoveSpeed {speed = 1});
//            m_Manager.SetComponentData(entity, new Position {Value = new float3(0, 0, 0)});
//            m_Manager.SetComponentData(entity, new Heading {Value = new float3(1, 0, 0)});
//
//            // act
//            World.CreateManager<MoveForwardSystem>().Update();
//
//            // assert
//            Assert.AreEqual(new float3(Time.deltaTime, 0, 0), m_Manager.GetComponentData<Position>(entity).Value);
//        }
//    }
//}