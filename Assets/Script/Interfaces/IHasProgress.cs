using System;

public interface IHasProgress
{
    public event Action<float> OnProgressChanged;

}
