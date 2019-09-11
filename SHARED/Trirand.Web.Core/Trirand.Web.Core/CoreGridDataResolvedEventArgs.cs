using System;
using System.Linq;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class CoreGridDataResolvedEventArgs : EventArgs
	{
		public IQueryable _currentData;

		public IQueryable _allData;

		public CoreGrid _gridModel;

		public CoreGrid GridModel
		{
			get
			{
				return _gridModel;
			}
			set
			{
				_gridModel = value;
			}
		}

		public IQueryable CurrentData
		{
			get
			{
				return _currentData;
			}
			set
			{
				_currentData = value;
			}
		}

		public IQueryable AllData
		{
			get
			{
				return _allData;
			}
			set
			{
				_allData = value;
			}
		}

		public CoreGridDataResolvedEventArgs(CoreGrid gridModel, IQueryable currentData, IQueryable allData)
		{
			_currentData = currentData;
			_allData = allData;
			_gridModel = gridModel;
		}
	}
}
