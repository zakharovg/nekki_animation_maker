namespace AnimationMaker.Services
{
	public struct OpenDialogResult
	{
		public OpenDialogResult(bool isSuccessful, string filename) : this()
		{
			IsSuccessful = isSuccessful;
			Filename = filename;
		}

		public bool IsSuccessful { get; private set; }
		public string Filename { get; private set; }
	}
}