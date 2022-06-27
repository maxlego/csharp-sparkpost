namespace SparkPost
{
    public class EmailValidationResponse
    {
        public string Result { get; set; }
        public bool Valid { get; set; }
        public string Reason { get; set; }
        public bool IsRole { get; set; }
        public bool IsDisposable { get; set; }
        public int DeliveryConfidence { get; set; }
        public bool IsFree { get; set; }

        public static EmailValidationResponse ConvertToResponse(dynamic r) =>
            new EmailValidationResponse
            {
                Result = r.result,
                Valid = r.valid,
                Reason = r.reason ?? string.Empty,
                IsRole = r.is_role,
                IsDisposable = r.is_disposable,
                DeliveryConfidence = r.delivery_confidence,
                IsFree = r.is_free
            };
    }
}
