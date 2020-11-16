namespace GPS.Data
{
    /// <summary>
    /// Enum for the return values of ValidatePredicted. Couldn't put this in the function for some reason.
    /// </summary>
    public enum RetVal
    {
        validWithEndTime = 1,
        validNoEndTime = 2,
        invalid = 3
    }
}