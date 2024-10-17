using System;

namespace DeveloperSample.ClassRefactoring
{
    public enum SwallowType
    {
        African, European
    }

    public enum SwallowLoad
    {
        None, Coconut
    }

    public class SwallowFactory
    {
        public Swallow GetSwallow(
            SwallowType type,
            SwallowLoad load = SwallowLoad.None
        )
        {
            if (type == SwallowType.African)
                return new AfricanSwallow(load);
            else if (type == SwallowType.European)
                return new EuropeanSwallow(load);
            else
                throw new ArgumentException("Invalid swallow type", nameof(type));  //fail loud/early
        }
    }

    public abstract class Swallow
    {
        public SwallowType Type { get; }
        public SwallowLoad Load { get; private set; }

        public Swallow(SwallowType swallowType, SwallowLoad load = SwallowLoad.None)
        {
            Type = swallowType;
            Load = load;
        }

        protected virtual double UnladenVelocity { get; }
        protected virtual double LadenVelocity { get; }

        public virtual void ApplyLoad(SwallowLoad load)
        {
            Load = load;
        }
        public virtual void RemoveLoad()
        {
            ApplyLoad(SwallowLoad.None);
        }

        public double GetAirspeedVelocity()
        {
            return (Load == SwallowLoad.None)
                   ? UnladenVelocity
                   : LadenVelocity;
        }
    }

    public class AfricanSwallow : Swallow
    {
        public AfricanSwallow(SwallowLoad load = SwallowLoad.None)
            : base (SwallowType.African, load)
        {
        }

        protected override double UnladenVelocity => 22;
        protected override double LadenVelocity => 18;
    }

    public class EuropeanSwallow : Swallow
    {
        public EuropeanSwallow(SwallowLoad load = SwallowLoad.None)
            : base(SwallowType.European, load)
        {
        }

        protected override double UnladenVelocity => 20;
        protected override double LadenVelocity => 16;  // I thought European swallows were incapable of carrying coconuts?!?
    }
}