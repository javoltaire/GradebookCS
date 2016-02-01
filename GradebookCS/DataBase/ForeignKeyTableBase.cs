using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;
using System.Collections.ObjectModel;

namespace GradebookCS.DataBase
{
    public abstract class ForeignKeyTableBase<TItem,TKey,TForeignKey> : TableBase<TItem,TKey>
    {
        #region Properties
        protected abstract string SelectAllForIdStatement { get; }
        #endregion

        #region Methods
        #region Abstract Methods
        protected abstract void FillSelectAllForIdStatement(ISQLiteStatement statement, TForeignKey foreignKey);
        #endregion
        public ObservableCollection<TItem> GetAllItemsForId(TForeignKey fKey)
        {
            //List to hold the items
            var items = new ObservableCollection<TItem>();
            using (var statement = SQLiteConnection.Prepare(SelectAllForIdStatement))
            {
                FillSelectAllForIdStatement(statement, fKey);   //Fill the statetement
                while (statement.Step() == SQLiteResult.ROW)    //Loop through the table
                {

                    var item = CreateItem(statement);               //Create an item using the statement
                    items.Add(item);                                //Add the item to the list
                }
            }
            return items;
        }
        #endregion





    }
}
