using System;
using System.Drawing;

using TheBox.Data;
using TheBox.MapViewer;

namespace TheBox.MapViewer.DrawObjects
{
	/// <summary>
	/// Summary description for SpawnDrawObject.
	/// </summary>
	public class SpawnDrawObject : IMapDrawable
	{
		private SpawnEntry m_Spawn;

		/// <summary>
		/// Gets or sets the SpawnEntry represented by this spawn draw object
		/// </summary>
		public SpawnEntry Spawn
		{
			get { return m_Spawn; }
			set { m_Spawn = value; }
		}

		public SpawnDrawObject( SpawnEntry spawn )
		{
			m_Spawn = spawn;
		}

		#region IMapDrawable Members

		public bool IsVisible(System.Drawing.Rectangle bounds, TheBox.MapViewer.Maps map)
		{
			if ( m_Spawn.Map != (int) map )
				return false;

			if ( m_Spawn.X >= bounds.Left && m_Spawn.X <= bounds.Right && m_Spawn.Y >= bounds.Top && m_Spawn.Y <= bounds.Bottom )
				return true;
			else
				return false;
		}

		public void Draw(System.Drawing.Graphics g, TheBox.MapViewer.MapViewInfo ViewInfo)
		{
			System.Drawing.Point c = ViewInfo.MapToControl( new System.Drawing.Point( m_Spawn.X, m_Spawn.Y ) );

			int x1 = c.X - 3;
			int x2 = c.X + 3;
			int y1 = c.Y - 3;
			int y2 = c.Y + 3;

			Color color = Pandora.Profile.Travel.SpawnColor;

			Pen pen = new Pen( color );

			g.DrawLine( pen, x1, y1, x2, y2 );
			g.DrawLine( pen, x1, y2, x2, y1 );
			g.DrawLine( pen, c.X, y1, c.X, y2 );
			g.DrawLine( pen, x1, c.Y, x2, c.Y );

			pen.Dispose();
		}

		#endregion
	}
}
