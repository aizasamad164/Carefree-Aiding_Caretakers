using Microsoft.ML.Data;


namespace Caretaker_System_3
{
    class InsurancePrediction
    {
        [ColumnName("Score")]
        public float PredictedCharges { get; set; }
    }
}
