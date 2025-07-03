namespace therabia.Helper
{
    public static class ProfessionalHelper
    {
        public static double CalculateAverageRate(List<int> rateValues)
        {
            if (rateValues == null || rateValues.Count == 0)
                return 0;

            return rateValues.Average();
        }
    }

}
