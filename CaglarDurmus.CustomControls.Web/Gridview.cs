using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CaglarDurmus.CustomControls.Web
{
    public partial class CustomControl
    {
        public class GridviewColumn : CustomControlBase
        {
            public GridviewColumn() : base()
            {

            }
            public GridviewColumn(string caption)
               : this()
            {
                this.Caption = caption;
            }
            public GridviewColumn(string caption, string value)
               : this()
            {
                this.Caption = caption;
                this.Value = value;
            }
            public string Value { get; protected internal set; }

            public override MvcHtmlString RenderControl()
            {
                StringBuilder html = new StringBuilder();
                html.AppendFormat(@"<th>{0}</th>", this.Caption);

                return MvcHtmlString.Create(html.ToString());
            }
        }
        public class GridviewRow : CustomControlBase
        {
            public GridviewRow()
                : base()
            {
            }

            public GridviewRow AddRowCells(params GridviewRowCell[] cells)
            {
                foreach (GridviewRowCell cell in cells)
                    this.Controls.Add(cell);
                return this;
            }

            public override MvcHtmlString RenderControl()
            {
                StringBuilder html = new StringBuilder();

                html.Append("<tr>");

                foreach (GridviewRowCell cell in this.Controls)
                    html.Append(cell.RenderControl().ToString());

                html.Append("</tr>");

                return MvcHtmlString.Create(html.ToString());
            }
        }
        public class GridviewRowCell : CustomControlBase
        {
            public GridviewRowCell()
                : base()
            {
            }
            public GridviewRowCell(string caption)
                : base()
            {
                this.Caption = caption;
            }

            protected internal GridviewRowCell AddControl(CustomControlBase control)
            {
                this.Controls.Add(control);
                return this;
            }

            public GridviewRowCell SetCaption(string value)
            {
                this.Caption = value;
                return this;
            }

            public override MvcHtmlString RenderControl()
            {
                StringBuilder html = new StringBuilder();

                html.Append("<td>");

                if (!string.IsNullOrWhiteSpace(this.Caption))
                {
                    html.Append(this.Caption);
                }

                foreach (CustomControlBase control in this.Controls)
                    html.Append(control.RenderControl().ToString());

                html.Append(String.Format("</td>", this.Caption));

                return MvcHtmlString.Create(html.ToString());
            }
        }
        public class Gridview : CustomControlBase
        {
            public event CellPreparingEventHandler CellPreparing;
            public event PreparingEventHandler RowPreparing;
            public event HeaderRowPreparingEventHandler HeaderRowPreparing;

            public Gridview(string id, string caption) : base(id, caption)
            {
            }
            public Gridview AddColumns(params GridviewColumn[] columns)
            {
                foreach (GridviewColumn column in columns)
                {
                    this.Columns.Add(column);
                }

                return this;
            }
            public Gridview AddRows(params GridviewRow[] rows)
            {
                foreach (GridviewRow row in rows)
                    this.Controls.Add(row);

                return this;
            }

            private List<GridviewColumn> Columns = new List<GridviewColumn>();
            public List<GridviewColumn> GetColumns
            {
                get
                {
                    return this.Columns;
                }
            }

            public Gridview DataBind<T>(List<T> dataList)
            {
                if (this.Columns.Count == 0)
                    return this;

                if (this.Columns.Count == 0 || dataList == null || dataList.Count == 0)
                    return this;

                GridviewRow newRow = null;
                GridviewRowCell newCell = null;
                int rowIndex = -1;

                foreach (object listItem in dataList)
                {
                    newRow = new GridviewRow();
                    rowIndex++;

                    foreach (GridviewColumn column in this.Columns)
                    {
                        newCell = new GridviewRowCell();

                        if (string.IsNullOrWhiteSpace(column.Value))
                            newCell.Caption = string.Empty;
                        else
                        {
                            var caption = GetCaption(listItem, column.Value);

                            newCell.Caption = caption != null ? caption.ToString() : string.Empty;
                        }

                        if (this.CellPreparing != null)
                        {
                            var eventArgs_CellPreparing = new CellPreparingEventArgs(column, newCell, listItem, rowIndex);
                            this.CellPreparing(this, eventArgs_CellPreparing);
                        }

                        newRow.AddRowCells(newCell);
                    }

                    if (this.RowPreparing != null)
                    {
                        var eventArgs_RowPreparing = new RowPreparingEventArgs(newRow, listItem);
                        this.RowPreparing(this, eventArgs_RowPreparing);
                    }

                    this.AddRows(newRow);
                }

                return this;

            }

            public Gridview DataBind(DataTable dataTable)
            {
                if (this.Columns.Count == 0)
                    return this;

                if (this.Columns.Count == 0 || dataTable == null || dataTable.Rows.Count == 0)
                    return this;

                GridviewRow newRow = null;
                GridviewRowCell newCell = null;
                int rowIndex = -1;

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    newRow = new GridviewRow();
                    rowIndex++;

                    foreach (GridviewColumn column in this.Columns)
                    {
                        newCell = new GridviewRowCell();

                        if (string.IsNullOrWhiteSpace(column.Value))
                            newCell.Caption = string.Empty;
                        else
                            newCell.Caption = dataRow[column.Value].ToString();

                        if (this.CellPreparing != null)
                        {
                            var eventArgs_CellPreparing = new CellPreparingEventArgs(column, newCell, dataRow, rowIndex);
                            this.CellPreparing(this, eventArgs_CellPreparing);
                        }

                        newRow.AddRowCells(newCell);
                    }

                    if (this.RowPreparing != null)
                    {
                        var eventArgs_RowPreparing = new RowPreparingEventArgs(newRow, dataRow);
                        this.RowPreparing(this, eventArgs_RowPreparing);
                    }

                    this.AddRows(newRow);
                }

                return this;

            }

            #region oldRender

            /*
            public string RenderControl()
            {
                StringBuilder html = new StringBuilder();
                html.AppendFormat(@"
                    <div class=""row"">
                        <div class=""col-12"">
                            <div class=""card"">
                                <div class=""card-body"">
                                    <h5 class=""card-title"">{0}</h5>
                                    <div class=""table-responsive"">
                                        <table id=""{1}"" class=""table table-striped table-bordered"">
                                            <thead>", this.Caption, this.Id);

                #region Columns

                if (this.Controls.Count > 0)
                {
                    html.Append(@"
                <tr>");

                    foreach (GridviewColumn column in this.Columns)
                    {
                        if (this.HtmlDataHeaderRowPreparing != null)
                        {
                            var eventArgs_HeaderRowPreparing = new HtmlDataHeaderRowPreparingEventArgs(column);
                            this.HtmlDataHeaderRowPreparing(this, eventArgs_HeaderRowPreparing);
                        }
                        html.Append(column.RenderControl().ToString());
                    }

                    html.Append(@"
                </tr>");
                }

                #endregion

                html.Append(@"</thead>");

                #region tbody

                html.Append(@"
            <tbody>");

                if (this.Controls.Count == 0)
                {
                    this.AddRows(new GridviewRow().AddRowCells(new GridviewRowCell("Kayıt Bulunamadı!")));
                }

                foreach (GridviewRow rows in this.Controls)
                    html.Append(rows.RenderControl().ToString());

                html.Append(@"
            </tbody>");

                #endregion

                html.AppendFormat(@"
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <script type=""text/javascript"">
                        $('#{0}').DataTable();
                    </script>", this.Id);

                return html.ToString();
            }
            */

            #endregion

            public override MvcHtmlString RenderControl()
            {
                StringBuilder html = new StringBuilder();
                html.AppendFormat(@"
                    <div class=""row"">
                        <div class=""col-12"">
                            <div class=""card"">
                                <div class=""card-body"">
                                    <h5 class=""card-title"">{0}</h5>
                                    <div class=""table-responsive"">
                                        <table id=""{1}"" class=""table table-striped table-bordered"">
                                            <thead>", this.Caption, this.Id);

                #region Columns

                if (this.Controls.Count > 0)
                {
                    html.Append(@"
                <tr>");

                    foreach (GridviewColumn column in this.Columns)
                    {
                        if (this.HeaderRowPreparing != null)
                        {
                            var eventArgs_HeaderRowPreparing = new HeaderRowPreparingEventArgs(column);
                            this.HeaderRowPreparing(this, eventArgs_HeaderRowPreparing);
                        }
                        html.Append(column.RenderControl().ToString());
                    }

                    html.Append(@"
                </tr>");
                }

                #endregion

                html.Append(@"</thead>");

                #region tbody

                html.Append(@"
            <tbody>");

                if (this.Controls.Count == 0)
                {
                    this.AddRows(new GridviewRow().AddRowCells(new GridviewRowCell("Kayıt Yok!")));
                }

                foreach (GridviewRow rows in this.Controls)
                    html.Append(rows.RenderControl().ToString());

                html.Append(@"
            </tbody>");

                #endregion

                html.AppendFormat(@"
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <script type=""text/javascript"">
                        $('#{0}').DataTable();
                    </script>", this.Id);

                return MvcHtmlString.Create(html.ToString());
            }
        }

        public class CellPreparingEventArgs
        {
            internal CellPreparingEventArgs(
                GridviewColumn column,
                GridviewRowCell cell,
                object dataItem,
                int rowIndex)
            {
                this.Column = column;
                this.Cell = cell;
                this.DataItem = dataItem;
                this.RowIndex = rowIndex;
            }

            public int RowIndex { get; private set; }
            public GridviewColumn Column { get; private set; }
            public GridviewRowCell Cell { get; private set; }
            public object DataItem { get; private set; }
        }

        public class RowPreparingEventArgs
        {
            internal RowPreparingEventArgs(
                GridviewRow row,
                object dataItem)
            {
                this.Row = row;
                this.DataItem = dataItem;
            }

            public object DataItem { get; private set; }
            public GridviewRow Row { get; set; }
        }

        public class HeaderRowPreparingEventArgs
        {
            internal HeaderRowPreparingEventArgs(
                GridviewColumn column)
            {
                this.Column = column;
            }

            public GridviewColumn Column { get; private set; }
        }
        
        public static object GetCaption(object src, string propName)
        {
            var properties = propName.Split('.');

            if (properties.Length > 1)
            {
                foreach (var propertie in properties)
                    src = GetCaption(src, propertie);

                return src;
            }

            if (src == null)
                return null;

            if (src.GetType().GetProperty(propName) == null)
                return null;
            else
                return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public delegate void CellPreparingEventHandler(object sender, CellPreparingEventArgs e);
        public delegate void PreparingEventHandler(object sender, RowPreparingEventArgs e);
        public delegate void HeaderRowPreparingEventHandler(object sender, HeaderRowPreparingEventArgs e);

    }

}
