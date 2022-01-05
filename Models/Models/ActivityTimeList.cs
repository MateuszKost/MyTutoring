namespace Models.Models
{
    public static class ActivityTimeList
    {
        private static IEnumerable<float> activityTime = new List<float>()
        {
            0f, 0.15f, 0.30f, 0.45f,
            1f, 1.15f, 1.30f, 1.45f,
            2f, 2.15f, 2.30f, 2.45f,
            3f, 3.15f, 3.30f, 3.45f,
            4f, 4.15f, 4.30f, 4.45f,
            5f, 5.15f, 5.30f, 5.45f,
            6f, 6.15f, 6.30f, 6.45f,
            7f, 7.15f, 7.30f, 7.45f,
            8f, 8.15f, 8.30f, 8.45f,
            9f, 9.15f, 9.30f, 9.45f,
            10f, 10.15f, 10.30f, 10.45f,
            11f, 11.15f, 11.30f, 11.45f,
            12f, 12.15f, 12.30f, 12.45f,
            13f, 13.15f, 13.30f, 13.45f,
            14f, 14.15f, 14.30f, 14.45f,
            15f, 15.15f, 15.30f, 15.45f,
            16f, 16.15f, 16.30f, 16.45f,
            17f, 17.15f, 17.30f, 17.45f,
            18f, 18.15f, 18.30f, 18.45f,
            19f, 19.15f, 19.30f, 19.45f,
            20f, 20.15f, 20.30f, 20.45f,
            21f, 21.15f, 21.30f, 21.45f,
            22f, 22.15f, 22.30f, 22.45f,
            23f, 23.15f, 23.30f, 23.45f,
        };

        private static Dictionary<int, string> dayOfWeek = new Dictionary<int, string>()
        {
            {1, "Poniedziałek" },
            {2, "Wtorek" },
            {3, "Środa" },
            {4, "Czwartek" },
            {5, "Piątek" },
            {6, "Sobota" },
            {7, "Niedziela" }
        };

        public static IEnumerable<float> CreateActivityTimeList()
        {
            return activityTime;
        }

        public static Dictionary<int, string> CreateDayOfWeek()
        {
            return dayOfWeek;
        }
    }
}
