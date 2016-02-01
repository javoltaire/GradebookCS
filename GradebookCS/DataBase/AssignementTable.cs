using GradebookCS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;

namespace GradebookCS.DataBase
{
    public class AssignementTable : ForeignKeyTableBase<Assignment, string, string>
    {
        #region Properties
        protected override string DeleteItemStatement { get { return "DELETE FROM Assignment WHERE Id = ? "; } }

        protected override string InsertItemStatement { get { return "INSERT INTO Assignment (Id, Name, Score, MaxScore, ComponentId) VALUES (?, ?, ?, ?, ?)"; } }

        protected override string UpdateItemStatement { get { return "UPDATE Assignment SET Id = ?, Name = ?, Score = ?, MaxScore = ?, ComponentId = ? WHERE Id = ?"; } }

        protected override string SelectAllStatement { get { return @"SELECT Id, Name, Score, MaxScore, ComponentId FROM Assignment ORDER BY Name"; } }

        protected override string SelectAllForIdStatement { get { return @"SELECT Id, Name, Score, MaxScore, ComponentId FROM Assignment WHERE ComponentId = ? ORDER BY Name"; } }

        protected override string SelectItemStatement { get { return "SELECT Id, Name, Score, MaxScore, ComponentId FROM Assignment WHERE Id = ?"; } }

        public static AssignementTable Instance { get; } = new AssignementTable();
        #endregion

        #region Methods
        protected override Assignment CreateItem(ISQLiteStatement statement)
        {
            string id = (string)statement[0];
            string name = (string)statement[1];
            double score = (double)statement[2];
            double maxScore = (double)statement[3];
            string componentId = (string)statement[4];

            return new Assignment(id, componentId, name, score, maxScore);
        }

        protected override void FillDeleteItemStatement(ISQLiteStatement statement, string key)
        {
            statement.Bind(1, key);
        }

        protected override void FillInsertItemStatement(ISQLiteStatement statement, Assignment item)
        {
            statement.Bind(1, item.Id);
            statement.Bind(2, item.Name);
            statement.Bind(3, item.Grade.Score);
            statement.Bind(4, item.Grade.MaximumScore);
            statement.Bind(5, item.ComponentId);
        }

        protected override void FillUpdateItemStatement(ISQLiteStatement statement, Assignment item, string key)
        {
            FillInsertItemStatement(statement, item);
            statement.Bind(6, key);
        }

        protected override void FillSelectAllForIdStatement(ISQLiteStatement statement, string fKey)
        {
            statement.Bind(1, fKey);
        }

        protected override void FillSelectItemStatement(ISQLiteStatement statement, string key)
        {
            statement.Bind(1, key);
        }

        protected override void FillSelectAllStatement(ISQLiteStatement statement)
        {
            //throw new NotImplementedException();
            //Nothing to fill
        }
        #endregion
        #region Constructors
        private AssignementTable() { }
        #endregion




















    }
}
