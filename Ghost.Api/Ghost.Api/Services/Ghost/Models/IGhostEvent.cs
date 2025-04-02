namespace Ghost.Api.Services.Ghost.Models;

/// <summary>
/// An shared interface for entities related to Ghost
/// </summary>
public interface IGhostObject
{
}

/// <summary>
/// An event that occured within ghost, relating to type T
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGhostEvent<T> where T : IGhostObject
{
    T Current { get; set; }
    T Previous { get; set; }
}