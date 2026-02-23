namespace SocioWeb.Entities
{
    public class Treatment
    {
        public string TreatmentName { get; set; }

        public DateTime? StartDate { get; set; }

        public string Instructions { get; set; }

        public DateTime? EndDate { get; set; }

        public List<TreatmentMedication> Medications { get; set; }

        public bool FollowUpRequired { get; set; }
    }
}