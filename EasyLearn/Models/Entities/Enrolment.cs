namespace EasyLearn.Models.Entities
{
    public class Enrolment : BaseEntity
    {
        public string StudentId { get; set; }
        public Student Student { get; set; }

    }
}
