namespace Toodledo.Model
{
    /// <summary>
    /// Represents the user's preferred date format
    /// </summary>
    public enum DateFormat
    {
        /// <summary>
        /// M D, Y
        /// </summary>
        MDY = 0,
        /// <summary>
        /// M/D/Y
        /// </summary>
        MMDDYYY = 1,
        /// <summary>
        /// D/M/Y
        /// </summary>
        DDMMYYY = 2,
        /// <summary>
        /// Y-M-D
        /// </summary>
        YYYYMMDD = 3
    }

    /// <summary>
    /// Represents the priority for a task
    /// </summary>
    public enum Priority
    {
        /// <summary>
        /// No priority at all
        /// </summary>
        Negative = -1,
        /// <summary>
        /// Low priority
        /// </summary>
        Low = 0,
        /// <summary>
        /// Medium priority
        /// </summary>
        Medium = 1,
        /// <summary>
        /// High priority
        /// </summary>
        High = 2,
        /// <summary>
        /// Top priority
        /// </summary>
        Top = 3,
        /// <summary>
        /// (undocumented) Even higher than Top Priority!
        /// </summary>
        Ultimate = 4
    }

    /// <summary>
    /// Represents the level of a goal
    /// </summary>
    public enum Level
    {
        /// <summary>
        /// Lifetime goal
        /// </summary>
        Lifetime = 0,
        /// <summary>
        /// Long-term goal
        /// </summary>
        LongTerm = 1,
        /// <summary>
        /// Short-term goal
        /// </summary>
        ShortTerm = 2
    }

    /// <summary>
    /// Represents the frequency a task should repeat
    /// </summary>
    public enum Frequency
    {
        /// <summary>
        /// No repetition
        /// </summary>
        Once = 0,
        /// <summary>
        /// Once a week
        /// </summary>
        Weekly = 1,
        /// <summary>
        /// Once a month
        /// </summary>
        Monthly = 2,
        /// <summary>
        /// Once a year
        /// </summary>
        Yearly = 3,
        /// <summary>
        /// Each day
        /// </summary>
        Daily = 4,
        /// <summary>
        /// Every other week
        /// </summary>
        Biweekly = 5,
        /// <summary>
        /// Twice a month
        /// </summary>
        Bimonthly = 6,
        /// <summary>
        /// Twice a year
        /// </summary>
        Semiannually = 7,
        /// <summary>
        /// Every three months
        /// </summary>
        Quarterly = 8,
        /// <summary>
        /// Whenever the parent task occurs
        /// </summary>
        WithParent = 9,
        /// <summary>
        /// Use the special instructions in AdvancedRepeat
        /// </summary>
        Advanced = 50
    }

    /// <summary>
    /// Represents the status of a task
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Status is undefined
        /// </summary>
        None = 0,
        /// <summary>
        /// This task is a Next Action
        /// </summary>
        NextAction = 1,
        /// <summary>
        /// This task is active
        /// </summary>
        Active = 2,
        /// <summary>
        /// This task is still in the planning stage
        /// </summary>
        Planning = 3,
        /// <summary>
        /// This task has been delegated to someone else
        /// </summary>
        Delegated = 4,
        /// <summary>
        /// This task is waiting for something else to occur
        /// </summary>
        Waiting = 5,
        /// <summary>
        /// This task is on hold
        /// </summary>
        Hold = 6,
        /// <summary>
        /// This task has been postponed
        /// </summary>
        Postponed = 7,
        /// <summary>
        /// This task has been postponed indefinitely
        /// </summary>
        Someday = 8,
        /// <summary>
        /// This task has been canceled
        /// </summary>
        Canceled = 9,
        /// <summary>
        /// This task is just for reference
        /// </summary>
        Reference = 10
    }
}