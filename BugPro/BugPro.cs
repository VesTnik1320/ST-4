using Stateless;
using System;

namespace BugPro
{
    public enum BugState
    {
        NewDefect,
        Analysis,
        Fixed,
        NotDefect,
        WonTFix,
        Duplicate,
        NotReproducible,
        NeedMoreInfo,
        SeparateSolution,
        NoTimeNow,
        OtherProduct
    }

    public enum BugTrigger
    {
        StartAnalysis,
        MarkAsFixed,
        MarkAsNotDefect,
        MarkAsWonTFix,
        MarkAsDuplicate,
        MarkAsNotReproducible,
        NeedMoreInfo,
        MarkAsSeparateSolution,
        MarkAsNoTimeNow,
        MarkAsOtherProduct,
        ProvideInfo
    }

    public class Bug
    {
        private readonly StateMachine<BugState, BugTrigger> _machine;
        private BugState _currentState;

        public BugState CurrentState => _machine.State;

        public Bug()
        {
            _machine = new StateMachine<BugState, BugTrigger>(() => _currentState, state => _currentState = state);

            _machine.Configure(BugState.NewDefect)
                .Permit(BugTrigger.StartAnalysis, BugState.Analysis);

            _machine.Configure(BugState.Analysis)
                .Permit(BugTrigger.MarkAsFixed, BugState.Fixed)
                .Permit(BugTrigger.MarkAsNotDefect, BugState.NotDefect)
                .Permit(BugTrigger.MarkAsWonTFix, BugState.WonTFix)
                .Permit(BugTrigger.MarkAsDuplicate, BugState.Duplicate)
                .Permit(BugTrigger.MarkAsNotReproducible, BugState.NotReproducible)
                .Permit(BugTrigger.NeedMoreInfo, BugState.NeedMoreInfo)
                .Permit(BugTrigger.MarkAsSeparateSolution, BugState.SeparateSolution)
                .Permit(BugTrigger.MarkAsNoTimeNow, BugState.NoTimeNow)
                .Permit(BugTrigger.MarkAsOtherProduct, BugState.OtherProduct);

            _machine.Configure(BugState.NeedMoreInfo)
                .Permit(BugTrigger.ProvideInfo, BugState.Analysis);
        }

        public void StartAnalysis() => _machine.Fire(BugTrigger.StartAnalysis);
        public void MarkAsFixed() => _machine.Fire(BugTrigger.MarkAsFixed);
        public void MarkAsNotDefect() => _machine.Fire(BugTrigger.MarkAsNotDefect);
        public void MarkAsWonTFix() => _machine.Fire(BugTrigger.MarkAsWonTFix);
        public void MarkAsDuplicate() => _machine.Fire(BugTrigger.MarkAsDuplicate);
        public void MarkAsNotReproducible() => _machine.Fire(BugTrigger.MarkAsNotReproducible);
        public void NeedMoreInfo() => _machine.Fire(BugTrigger.NeedMoreInfo);
        public void MarkAsSeparateSolution() => _machine.Fire(BugTrigger.MarkAsSeparateSolution);
        public void MarkAsNoTimeNow() => _machine.Fire(BugTrigger.MarkAsNoTimeNow);
        public void MarkAsOtherProduct() => _machine.Fire(BugTrigger.MarkAsOtherProduct);
        public void ProvideInfo() => _machine.Fire(BugTrigger.ProvideInfo);
    }

    public class Program
    {
        public static void Main()
        {
            var bug = new Bug();
            Console.WriteLine($"Initial state: {bug.CurrentState}");
            bug.StartAnalysis();
            Console.WriteLine($"After analysis: {bug.CurrentState}");
            bug.MarkAsFixed();
            Console.WriteLine($"After fixed: {bug.CurrentState}");
        }
    }
}