using NUnit.Framework;
using UnityEngine;

namespace src.tests
{
    [TestFixture]
    public class EnemyTests
    {
        private Enemy enemy;
            
            [SetUp]
        public void Init()
        {
           enemy = new Enemy();
//           enemy.tankSprites[0] = new Mock<Sprite>();
        }
        [Test]
        public void Test()
        {
            
        }

    }
}