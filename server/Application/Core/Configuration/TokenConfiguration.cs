using System;

namespace Com.SomeGameCorp.Bylinjen.Application.Core.Configuration;

public class TokenConfiguration
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string SigningKey { get; init; }
    public required TimeSpan TokenLifetime { get; set; }

}