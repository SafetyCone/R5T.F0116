using System;


namespace R5T.F0116
{
    public static class Instances
    {
        public static F0000.IDateTimeOperator DateTimeOperator => F0000.DateTimeOperator.Instance;
        public static F0002.IPathOperator PathOperator => F0002.PathOperator.Instance;
    }
}