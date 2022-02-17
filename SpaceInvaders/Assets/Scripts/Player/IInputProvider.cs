using System;

public interface IInputProvider
{
  float Horizontal { get; }

  event Action FireEvent;
}