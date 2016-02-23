using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GradebookCS.Common
{
    /// <summary>
    /// A base class that implements the <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public abstract class BaseINPC : INotifyPropertyChanged
    {
        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Will notified which property is changed
        /// </summary>
        /// <param name="propertyName">The property changing</param>
        protected void onPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
