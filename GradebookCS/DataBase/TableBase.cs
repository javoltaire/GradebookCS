using System;
using System.Collections.Generic;
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
    public abstract class TableBase<ItemType, KeyType>
    {
        private string selectAllStatement;

        public string SelectAllStatement
        {
            get { return selectAllStatement; }
            set { selectAllStatement = value; }
        }

    }
}
