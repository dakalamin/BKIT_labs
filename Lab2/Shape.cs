using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public abstract class Shape
    {
        public string Type
        {
            get
            {
                return this._Type;
            }
            protected set
            {
                this._Type = value;
            }
        }
        string _Type;

        /// <summary> Area calculation </summary>
        public abstract double Area();

        /// <summary> Setting to string </summary>
        public override string ToString()
        {
            return this.Type + " covers an area of " + this.Area().ToString();
        }
    }
}
