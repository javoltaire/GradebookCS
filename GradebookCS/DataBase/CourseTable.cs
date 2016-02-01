using GradebookCS.Model;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.DataBase
{
    public class CourseTable : TableBase<Course, string>
    {
        #region Properties
        protected override string DeleteItemStatement { get { return "DELETE FROM Course WHERE Id = ? "; } }

        protected override string InsertItemStatement { get { return "INSERT INTO Course (Id, Name, UsePercent, ARangeLow, ARangeHigh, BRangeLow, BRangeHigh, CRangeLow, CRangeHigh, NRRangeLow, NRRangeHigh) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"; } }

        protected override string UpdateItemStatement { get { return "UPDATE Course SET Id = ?, Name = ?, UsePercent = ?, ARangeLow = ?, ARangeHigh = ?, BRangeLow = ?, BRangeHigh = ?, CRangeLow = ?, CRangeHigh = ?, NRRangeLow = ?, NRRangeHigh = ? WHERE Id = ?"; } }

        protected override string SelectAllStatement { get { return @"SELECT Id, Name, UsePercent, ARangeLow, ARangeHigh, BRangeLow, BRangeHigh, CRangeLow, CRangeHigh, NRRangeLow, NRRangeHigh FROM Course ORDER BY Name"; } }

        protected override string SelectItemStatement { get { return "SELECT Id, Name, Weight, CourseId FROM Course WHERE Id = ?"; } }

        public static CourseTable Instance { get; } = new CourseTable();
        #endregion

        #region Methods
        protected override Course CreateItem(ISQLiteStatement statement)
        {
            return new Course()
            {
                Id = (string)statement[0],
                Name = (string)statement[1],
                UsePercent = (long)statement[2] == 1 ? true : false,
                ARangeLowEnd = (double)statement[3],
                ARangeHighEnd = (double)statement[4],
                BRangeLowEnd = (double)statement[5],
                BRangeHighEnd = (double)statement[6],
                CRangeLowEnd = (double)statement[7],
                CRangeHighEnd = (double)statement[8],
                NRRangeLowEnd = (double)statement[9],
                NRRangeHighEnd = (double)statement[10]
            };
        }

        protected override void FillDeleteItemStatement(ISQLiteStatement statement, string key)
        {
            statement.Bind(1, key);
        }

        protected override void FillInsertItemStatement(ISQLiteStatement statement, Course item)
        {
            statement.Bind(1, item.Id);
            statement.Bind(2, item.Name);
            statement.Bind(3, item.UsePercent ? 1: 0);
            statement.Bind(4, item.ARangeLowEnd);
            statement.Bind(5, item.ARangeHighEnd);
            statement.Bind(6, item.BRangeLowEnd);
            statement.Bind(7, item.BRangeHighEnd);
            statement.Bind(8, item.CRangeLowEnd);
            statement.Bind(9, item.CRangeHighEnd);
            statement.Bind(10, item.NRRangeLowEnd);
            statement.Bind(11, item.NRRangeHighEnd);
        }

        protected override void FillUpdateItemStatement(ISQLiteStatement statement, Course item, string key)
        {
            FillInsertItemStatement(statement, item);
            statement.Bind(12, key);
        }

        protected override void FillSelectItemStatement(ISQLiteStatement statement, string key)
        {
            statement.Bind(1, key);
        }

        protected override void FillSelectAllStatement(ISQLiteStatement statement)
        {
            //throw new NotImplementedException();
            //Nothing really to do.
        }
        #endregion

        #region Constructors
        private CourseTable() { }
        #endregion
    }
}
