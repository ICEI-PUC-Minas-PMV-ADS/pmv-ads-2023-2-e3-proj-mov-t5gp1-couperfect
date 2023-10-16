namespace CouperfectServer.Domain.Extensions.Serialization;

/// <summary>
/// This makes sure the contracts API 
/// </summary>
/// <typeparam name="TBase"></typeparam>
public interface IJsonDerivedTypeBase { }

/// <summary>
/// This makes sure the contracts API 
/// </summary>
/// <typeparam name="TBase"></typeparam>
public interface IJsonDerivedType<TBase> : IDiscriminator, IJsonDerivedTypeBase where TBase : IJsonDerivedTypeBase { }