﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

using ICSharpCode.Reports.Core.BaseClasses.Printing;
using ICSharpCode.Reports.Core.Exporter;

/// <summary>
/// This Class is used for Databased items
/// </summary>
/// <remarks>
/// 	created by - Forstmeier Peter
/// 	created on - 22.08.2005 00:12:59
/// </remarks>
namespace ICSharpCode.Reports.Core
{
	public class BaseDataItem : BaseTextItem, IDataRenderer, IExportColumnBuilder, IDataItem
	{

		private string columnName;

		#region Constructor

		public BaseDataItem() : base()
		{
		}

		public BaseDataItem(string columnName) : base()
		{
			this.columnName = columnName;
		}

		#endregion

		#region privates

		/*
		private string CheckForNullValue()
		{
		
			if (String.IsNullOrEmpty(this.dbValue)) {
				if (String.IsNullOrEmpty(this.NullValue)) {
					return GlobalValues.UnboundName;
				} else
					return this.NullValue;
			}
			
			return this.dbValue;
		}
*/

		#endregion

		#region IExportColumnBuilder  implementation

		public new BaseExportColumn CreateExportColumn()
		{
//			string toPrint = CheckForNullValue();
			TextStyleDecorator st = base.CreateItemStyle();
			ExportText item = new ExportText(st, false);
			item.Text = StandardFormatter.FormatOutput(DBValue, this.FormatString, base.DataType, this.NullValue);

			return item;
		}

		#endregion

		public override void Render(ReportPageEventArgs rpea)
		{
			base.Text = StandardFormatter.FormatOutput(DBValue, this.FormatString, base.DataType, this.NullValue);
			base.Render(rpea);
		}

		public override string ToString()
		{
			return this.GetType().Name;
		}

		#region Properies

		[XmlIgnoreAttribute()]
		[Browsable(false)]
		public virtual string DBValue {get;set;}
			
		public virtual string ColumnName {
			get {
				return columnName;
			}
			set {
				columnName = value;
				this.Text = this.columnName;
			}
		}

		///<summary>
		/// Mappingname to Datasource
		/// </summary>
		/// 

		[XmlIgnoreAttribute()]
		[Browsable(true), Category("Databinding"), Description("Mapping Name to DataTable")]
		public string MappingName {
			get { return BaseTableName + "." + this.ColumnName; }
		}


		public string BaseTableName {get;set;}
		

		[Browsable(true),
		 Category("Databinding"),
		 Description("Display Value for empty Field")]
		public string NullValue {get;set;}
		

		#endregion

	}
}
