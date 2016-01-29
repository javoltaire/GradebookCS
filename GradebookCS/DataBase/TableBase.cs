using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.DataBase
{
    /// <summary>
    /// Abstract base class that represents a table in sqlite
    /// </summary>
    /// <remarks>Idea came from https://github.com/Windows-XAML/201505-MVA/tree/master/TODOSQLiteSample </remarks>
    /// <typeparam name="ItemType">The type of item that will contained in the table</typeparam>
    /// <typeparam name="KeyType">The type of the primary key</typeparam>
    public abstract class TableBase<TItem, TKey>
    {
        #region Properties
        #region Abstract Statement Properties
        public abstract string SelectAllStatement { get; }
        public abstract string SelectItemStatement { get; }
        public abstract string DeleteItemStatement { get; }
        public abstract string InsertItemStatement { get; }
        public abstract string UpdateItemStatement { get; }
        #endregion
        #region Other Properties
        private ISQLiteConnection SQLiteConnection { get { return DatabaseService._Connection; } }
        #endregion
        #endregion

        #region Methods
        #region Abstract Fill Statement Methods
        protected abstract TItem CreateItem(ISQLiteStatement statement);
        protected abstract void FillSelectAllStatement(ISQLiteStatement statement);
        protected abstract void FillSelectItemStatement(ISQLiteStatement statement, TKey key);
        protected abstract void FillDeleteItemStatement(ISQLiteStatement statement, TKey key);
        protected abstract void FillInsertItemStatement(ISQLiteStatement statement, TItem item);
        protected abstract void FillUpdateItemStatement(ISQLiteStatement statement, TItem item, TKey key);
        #endregion

        #region Other Methods
        /// <summary>
        /// Gets all the items in a database table
        /// </summary>
        /// <returns>A list containing all the items.</returns>
        public ObservableCollection<TItem> GetAllItems()
        {
            //List to hold the items
            var items = new ObservableCollection<TItem>();
            using (var statement = SQLiteConnection.Prepare(SelectAllStatement))
            {
                
                FillSelectAllStatement(statement);              //Fill the statement object with the string constraints
                while (statement.Step() == SQLiteResult.ROW)    //Loop through the table
                {
                    
                    var item = CreateItem(statement);               //Create an item using the statement
                    items.Add(item);                                //Add the item to the list
                }
            }
            return items;                                       //return the item
        }

        /// <summary>
        /// Retreives an item from the table given the key
        /// </summary>
        /// <param name="key">The key of the item to be retrieved</param>
        /// <returns>The retrived item</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the key is not found</exception>
        public TItem GetItem(TKey key)
        {
            using (var statement = SQLiteConnection.Prepare(SelectItemStatement))
            {
                FillSelectItemStatement(statement, key);    //Fill the statement object with the string constraints
                if (statement.Step() == SQLiteResult.ROW)   //Loop through the table
                {
                    var item = CreateItem(statement);           //Create an item using the statement
                    return item;                                //return the item
                }
            }
            //if the key is not found, throw an exception
            throw new ArgumentOutOfRangeException("key not found");
        }

        /// <summary>
        /// Inserts an item in the table
        /// </summary>
        /// <param name="item">The item to be inserted</param>
        public void InsertItem(TItem item)
        {
            using (var statement = SQLiteConnection.Prepare(InsertItemStatement))   //
            {
                FillInsertItemStatement(statement, item);                               //Fill the statement object with the string constraints
                statement.Step();                                                       //Step/Execute the statement
            }
        }

        /// <summary>
        /// Updates an item
        /// </summary>
        /// <param name="key">The key of the item to be updated</param>
        /// <param name="item">The item to be updated</param>
        public void UpdateItem(TKey key, TItem item)
        {
            using (var statement = SQLiteConnection.Prepare(UpdateItemStatement))
            {
                FillUpdateItemStatement(statement, item, key);  //Fill the statement object with the string constraints
                statement.Step();                               //Step/execute the statement
            }
        }

        /// <summary>
        /// Deletes an item
        /// </summary>
        /// <param name="key">The key of the item to be deleted</param>
        public void DeleteItem(TKey key)
        {
            using (var statement = SQLiteConnection.Prepare(DeleteItemStatement))
            {
                FillDeleteItemStatement(statement, key);    //Fill the statement object with the string constraints
                statement.Step();                           //Step/execute the statement
            }
        }
        #endregion
        #endregion









    }
}
