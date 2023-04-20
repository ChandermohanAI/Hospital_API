namespace Hospital_API.Model.DTO.Patient
{
	public class PatientUpdate_DTO
	{
		public int PatientId { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public string Disease { get; set; }
		public int Bill { get; set; }
		public int DoctorId { get; set; }
	}
}
