using System;

namespace DTO
{
	public class AI_TrainingFeedbackDTO
	{
		public int FeedbackID { get; set; }
		public int PhienKhamID { get; set; }
		public string ImagePath { get; set; }
		public string AIPredictionsJSON { get; set; }
		public string Doctor_FinalDiagnosis { get; set; }
		public string ErrorType { get; set; }
		public decimal? ErrorScore { get; set; }
		public bool IsUsedForTraining { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
