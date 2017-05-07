using Algorithm;

namespace English_Dictionary.Algorithm
{
    abstract public class AlgorithmWrapper
    {
        protected DictionaryReader reader;

        abstract public QueryData Search(string searchQuery);

        abstract public void Init(DictionaryReader rd);
    }
}