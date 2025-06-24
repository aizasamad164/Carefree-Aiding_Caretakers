using Microsoft.ML.Data;

namespace Caretaker_System_3
{
    public class InsuranceInfo
    {
        public InsuranceInfo() { }

        // Load from Excel columns
        [LoadColumn(0)]
        public float Age { get; set; }

        [LoadColumn(1)]
        public string Sex { get; set; } = "";

        [LoadColumn(2)]
        public float BMI { get; set; }

        [LoadColumn(3)]
        public float Children { get; set; }

        [LoadColumn(4)]
        public string Smoker { get; set; } = "";

        [LoadColumn(5)]
        public string Region { get; set; } = "";

        [LoadColumn(6)]
        public float Charges { get; set; }
    }
}
