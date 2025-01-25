using System.Collections.Generic;

namespace BZ.Core.Collections
{
    public class BlackBoard : IBlackBoard
    {
        public static IBlackBoard CreateBlackBoard()
        {
            return new BlackBoard();
        }

        private BlackBoard()
        {
        }

        private struct Parameter
        {
            //            internal Type type;
            internal object Value;
            //            internal string name;

            internal Parameter(object value)
            {
                this.Value = value;
            }
        }

        private readonly Dictionary<int, Parameter> blackBoard = new Dictionary<int, Parameter>();

        public void Write<T>(string name, T value)
        {
            lock ( blackBoard )
            {
                var hash = name.GetHashCode();
                var p = new Parameter(value);
                if ( blackBoard.ContainsKey(hash) )
                {
                    blackBoard[hash] = p;
                }
                else
                {
                    this.blackBoard.Add(name.GetHashCode(), p);
                }
            }
        }

        public T Read<T>(string name)
        {
            lock ( blackBoard )
            {
                var hash = name.GetHashCode();
                var p = this.blackBoard[hash];

                return (T) p.Value;
            }
        }
        public bool Delete(string name)
        {
            lock ( blackBoard )
            {
                var hash = name.GetHashCode();
                return this.blackBoard.Remove(hash);
            }
        }

        public bool ContainsParameter(string name)
        {
            lock ( blackBoard )
            {
                var hash = name.GetHashCode();
                return this.blackBoard.ContainsKey(hash);
            }
        }

        public void Clear()
        {
            lock ( blackBoard )
            {
                this.blackBoard.Clear();
            }
        }

    }
}
