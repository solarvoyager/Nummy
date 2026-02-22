namespace NummyApi.Exceptions;

public class ApplicationNotFoundException(Guid applicationId)
    : Exception($"Application '{applicationId}' does not exist. " +
                $"Please create a new application in Nummy " +
                $"and copy its Id to the Nummy logger registration in Program.cs.");
