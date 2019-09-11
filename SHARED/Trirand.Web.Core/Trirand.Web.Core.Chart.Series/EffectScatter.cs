namespace Trirand.Web.Core.Trirand.Web.Core.Chart.Series
{
	public class EffectScatter : Scatter
	{
		public string EffectType
		{
			get;
			set;
		}

		public ShowEffectType ShowEffectOn
		{
			get;
			set;
		}

		public RippleEffect RippleEffect
		{
			get;
			set;
		}

		public EffectScatter()
		{
			base.Type = ChartType.EffectScatter;
		}

		public EffectScatter(string name)
			: this()
		{
			base.Name = name;
		}
	}
}
