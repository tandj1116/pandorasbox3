using System;
using System.Collections;
using System.Windows.Forms;

using TheBox.Common;

namespace TheBox.Data
{
	/// <summary>
	/// Summary description for TreeSearch.
	/// </summary>
	public class TreeSearch
	{
		/// <summary>
		/// Scans a TreeView control searching for data
		/// </summary>
		/// <param name="tree">The TreeView control to search</param>
		/// <param name="text">The text to search for</param>
		/// <returns>A SearchResults object containing the results</returns>
		public static SearchResults Find( TreeView tree, string text )
		{
			SearchResults results = new SearchResults();

			foreach ( TreeNode node in tree.Nodes )
			{
				DoNode( node, results, text );
			}			

			return results;
		}

		private static void DoNode( TreeNode node, SearchResults results, string text )
		{
			foreach ( TreeNode subNode in node.Nodes )
			{
				DoNode( subNode, results, text );
			}

			if ( node.Tag != null && node.Tag is ArrayList )
			{
				ArrayList list = node.Tag as ArrayList;

				for ( int i = 0; i < list.Count; i++ )
				{
					object o = list[ i ];

					if ( o is BoxMobile )
					{
						BoxMobile mob = o as BoxMobile;

						if ( mob.Name.ToLower().IndexOf( text.ToLower() ) > -1 )
						{
							Result res = new Result( node, i );
							results.Add( res );
						}
					}
					else if ( o is BoxItem )
					{
						BoxItem item = o as BoxItem;

						if ( item.Name.ToLower().IndexOf( text.ToLower() ) > -1 )
						{
							Result res = new Result( node, i );
							results.Add( res );
						}
					}
				}
			}
		}
	}
}