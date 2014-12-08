using System.Threading.Tasks;

namespace AnimationMaker.Services
{
	public interface IUserDialogService
	{
		OpenDialogResult GetSavePath();
		OpenDialogResult GetLoadPath();

		Task Alert(string text);
	}
}