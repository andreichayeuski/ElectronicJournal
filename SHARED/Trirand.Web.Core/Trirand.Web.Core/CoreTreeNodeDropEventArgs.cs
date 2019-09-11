using System.Collections;
using System.Collections.Generic;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class CoreTreeNodeDropEventArgs
	{
		public List<CoreTreeNode> DraggedNodes
		{
			get;
			set;
		}

		public CoreTreeNode DestinationNode
		{
			get;
			set;
		}

		public string SourceTreeViewID
		{
			get;
			set;
		}

		public string DestinationTreeViewID
		{
			get;
			set;
		}

		public Hashtable PostData
		{
			get;
			set;
		}
	}
}
