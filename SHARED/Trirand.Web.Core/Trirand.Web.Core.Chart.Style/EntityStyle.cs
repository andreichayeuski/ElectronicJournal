namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Style
{
	public class EntityStyle<T> where T : class, new()
	{
		public T Normal
		{
			get;
			set;
		}

		public T Emphasis
		{
			get;
			set;
		}
	}
}
