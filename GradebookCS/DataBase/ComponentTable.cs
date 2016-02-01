using GradebookCS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;
using System.Collections.ObjectModel;

namespace GradebookCS.DataBase
{
    public class ComponentTable : ForeignKeyTableBase<Component, string, string>
    {
        #region Properties
        protected override string DeleteItemStatement { get { return "DELETE FROM Component WHERE Id = ? "; } }

        protected override string InsertItemStatement { get { return "INSERT INTO Component (Id, Name, Weight, CourseId) VALUES (?, ?, ?, ?)"; } }

        protected override string UpdateItemStatement { get { return "UPDATE Component SET Id = ?, Name = ?, Weight = ?, CourseId = ? WHERE Id = ?"; } }

        protected override string SelectAllStatement { get { return @"SELECT Id, Name, Weight, CourseId FROM Component ORDER BY Name"; } }

        protected override string SelectItemStatement { get { return "SELECT Id, Name, Weight, CourseId FROM Component WHERE Id = ?"; } }

        protected override string SelectAllForIdStatement { get { return @"SELECT Id, Name, Weight, CourseId FROM Component WHERE CourseId = ? ORDER BY Name"; } }

        public static ComponentTable Instance { get; } = new ComponentTable();
        #endregion

        #region Methods
        protected override Component CreateItem(ISQLiteStatement statement)
        {
            return new Component()
            {
                Id = (string)statement[0],
                Name = (string)statement[1],
                Weight = (double)statement[2],
                CourseId = (string)statement[3]
            };
        }

        protected override void FillDeleteItemStatement(ISQLiteStatement statement, string key)
        {
            statement.Bind(1, key);
        }

        protected override void FillInsertItemStatement(ISQLiteStatement statement, Component item)
        {
            statement.Bind(1, item.Id);
            statement.Bind(2, item.Name);
            statement.Bind(3, item.Weight);
            statement.Bind(4, item.CourseId);
        }

        protected override void FillUpdateItemStatement(ISQLiteStatement statement, Component item, string key)
        {
            FillInsertItemStatement(statement, item);
            statement.Bind(5, key);
        }

        protected override void FillSelectItemStatement(ISQLiteStatement statement, string key)
        {
            statement.Bind(1, key);
        }

        protected override void FillSelectAllStatement(ISQLiteStatement statement)
        {
            throw new NotImplementedException();
        }

        protected override void FillSelectAllForIdStatement(ISQLiteStatement statement, string fKey)
        {
            statement.Bind(1, fKey);
        }
        #endregion

        #region Constructors
        private ComponentTable() { }
        #endregion

    }
}
