using System;

public interface IDetectorFinishAnimation : IUpdateble
{
    public event Action OnFinish;
}