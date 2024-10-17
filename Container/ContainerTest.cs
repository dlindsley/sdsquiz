using System;
using Xunit;

namespace DeveloperSample.Container
{
    internal interface IContainerTestInterface
    {
    }

    internal class ContainerTestClass : IContainerTestInterface
    {
    }

    public class ContainerTest
    {
        [Fact]
        public void CanBindAndGetService()
        {
            var container = new Container();
            container.Bind(typeof(IContainerTestInterface), typeof(ContainerTestClass));
            var testInstance = container.Get<IContainerTestInterface>();
            Assert.IsType<ContainerTestClass>(testInstance);
        }

        [Fact]
        public void CanNotGetUnboundService()
        {
            var container = new Container();
            //container.Bind(typeof(IContainerTestInterface), typeof(ContainerTestClass));
            Assert.Throws<ArgumentException>(() => container.Get<IContainerTestInterface>());
        }
    }
}