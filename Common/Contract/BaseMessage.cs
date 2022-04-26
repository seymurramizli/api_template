using System;

namespace Contract;

/// <summary>
/// Base class used by API request and responses
/// </summary>
public abstract class BaseMessage
{
    public string CorrelationId { get; set; }
    public string ApplicationName { get; set; }
}
