using System.Text.Json.Serialization;

namespace SocioWeb.Domain.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Sex
{
    MALE,
    FEMALE,
    UNKNOWN
}