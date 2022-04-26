namespace Common.DTOs;

public record DataChangeDto(string EntityName, string EntityId, List<ChangesDto> Changes);

public record ChangesDto(string FieldName, object OldValue, object NewValue);
