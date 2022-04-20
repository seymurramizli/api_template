using System;

namespace Contract;

/// <summary>
/// Base class used by API request and responses
/// </summary>
public abstract class BaseMessage
{
    /// <summary>
    /// Unique Identifier used by logging
    /// </summary>
    protected Guid _correlationId = Guid.NewGuid();
    public Guid CorrelationId() => _correlationId;
}
