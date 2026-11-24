using System.Text.Json.Serialization;
using HiveMind.Server.Entities;
using HiveMind.Server.QueryEngine;
using HiveMind.Server.Services;

namespace HiveMind.Server.Domain.Scheduler.Sources;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(QueryIdDataSource), "id")]
[JsonDerivedType(typeof(QueryNameDataSource), "name")]


public interface IDataSource
{
    public Query? GetQuery(IQueryService queryService);
}